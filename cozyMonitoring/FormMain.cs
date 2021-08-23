using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Linq;
using Newtonsoft.Json;

namespace cozyMonitoring
{
    public partial class FormMain : Form
    {
        #region Classes
        public class Host : IDisposable
        {
            #region Classes
            public class PingTest
            {
                [JsonProperty]
                public readonly DateTime Started;
                [JsonProperty]
                public readonly string ReplyFrom; // IP Address
                [JsonProperty]
                public readonly int? Bytes;
                [JsonProperty]
                public readonly long? Time;
                [JsonProperty]
                public readonly int? TTL;
                public bool Success => this.Time.HasValue;
                public PingTest(DateTime started, PingReply reply)
                {
                    this.Started = started;
                    if (reply != null && reply.Status == IPStatus.Success)
                    {
                        this.ReplyFrom = reply.Address.ToString();
                        this.Bytes = reply.Buffer.Length;
                        this.Time = reply.RoundtripTime;
                        this.TTL = reply.Options.Ttl;
                    }
                }
                public Color? BG => (this.Time.HasValue) ? null : (Color?)Color.Tomato;
                public static PingTest Create(string hostname)
                {
                    var send = DateTime.Now;
                    try
                    {
                        var ping = new Ping();
                        var reply = ping.Send(hostname);
                        return new PingTest(send, reply);
                    }
                    catch { }
                    return new PingTest(send, null);
                }
            }
            public class PingTestGroup
            {
                public readonly DateTime Started; // Group
                public readonly List<PingTest> List;
                public readonly long Sent; // Packets
                public readonly long Lost; // Packets
                public readonly long? Min; // Time
                public readonly long? Max; // Time
                public readonly long? Avg; // Time
                public readonly Color? BG;
                private readonly long TotalTime;
                private long Received => this.Sent - this.Lost;
                public PingTestGroup(DateTime started, List<PingTest> list)
                {
                    this.Started = started;
                    this.List = list;
                    foreach (var item in list)
                    {
                        this.Sent += 1;
                        if (item.Success)
                        {
                            if (!this.Min.HasValue || this.Min > item.Time)
                            {
                                this.Min = item.Time;
                            }
                            if (!this.Max.HasValue || this.Max < item.Time)
                            {
                                this.Max = item.Time;
                            }
                            this.TotalTime += item.Time.Value;
                        }
                        else
                        {
                            this.Lost += 1;
                        }
                    }
                    // Avg
                    if (this.TotalTime > 0)
                    {
                        this.Avg = this.TotalTime / this.Received;
                    }
                    // Color
                    if (this.Lost > 0 && this.Sent > 0)
                    {
                        if ((decimal)Lost / (decimal)Sent > 0.1m)
                        {
                            this.BG = Color.Tomato;
                        }
                        else
                        {
                            this.BG = Color.Khaki;
                        }
                    }
                }
                public static IEnumerable<PingTestGroup> GroupByMinute(IEnumerable<PingTest> list) => list.GroupBy(x => new DateTime(x.Started.Year, x.Started.Month, x.Started.Day, x.Started.Hour, x.Started.Minute, 0)).Select(x => new PingTestGroup(x.Key, x.ToList()));
                public static IEnumerable<PingTestGroup> GroupByHour(IEnumerable<PingTest> list) => list.GroupBy(x => new DateTime(x.Started.Year, x.Started.Month, x.Started.Day, x.Started.Hour, 0, 0)).Select(x => new PingTestGroup(x.Key, x.ToList()));
                public static IEnumerable<PingTestGroup> GroupByDay(IEnumerable<PingTest> list) => list.GroupBy(x => new DateTime(x.Started.Year, x.Started.Month, x.Started.Day, 0, 0, 0)).Select(x => new PingTestGroup(x.Key, x.ToList()));
                public static Dictionary<DateTime, PingTestGroup> GroupByDayDict(IEnumerable<PingTest> list) => list.GroupBy(x => new DateTime(x.Started.Year, x.Started.Month, x.Started.Day, 0, 0, 0)).ToDictionary(x => x.Key, x => new PingTestGroup(x.Key, x.ToList()));
            }
            #endregion
            #region Init
            public readonly string Device;
            public readonly string Hostname;
            private readonly BackgroundWorker Worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            public List<PingTest> PingTests = new List<PingTest>();
            public Host(string device, string hostname)
            {
                this.Device = device;
                this.Hostname = hostname;
                // Restore Data
                this.Restore(DateTime.Now);
                // Worker
                this.Worker.DoWork += (sender, e) =>
                {
                    while (!this.Worker.CancellationPending)
                    {
                        var pingTest = PingTest.Create(hostname);
                        Worker.ReportProgress(0, pingTest);
                        System.Threading.Thread.Sleep(2000);
                    }
                };
                this.Worker.ProgressChanged += (sender, e) =>
                {
                    var pingTest = (PingTest)e.UserState;
                    this.PingTests.Insert(0, pingTest);
                };
                this.Worker.RunWorkerAsync();
            }
            public IEnumerable<PingTest> LastMinute => PingTests.Where(x => x.Started > DateTime.Now.AddMinutes(-1));
            public IEnumerable<PingTest> LastHour => PingTests.Where(x => x.Started > DateTime.Now.AddHours(-1));
            public IEnumerable<PingTest> LastDay => PingTests.Where(x => x.Started > DateTime.Now.AddDays(-1));
            public IEnumerable<PingTestGroup> LastHourGroups => PingTestGroup.GroupByMinute(this.LastHour);
            public IEnumerable<PingTestGroup> LastDayGroups => PingTestGroup.GroupByHour(this.LastDay);
            public IEnumerable<PingTestGroup> AllDayGroups => PingTestGroup.GroupByDay(PingTests);
            #endregion
            #region Methods
            public (string path, string file) FilePath(DateTime t)
            {
                var path = Settings.DataPathCombine("Archive", this.Device, $"{t.Year:0000}", $"{t.Month:00}");
                var file = Path.Combine(path, $"{t.Day:00}.json.gz");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return (path, file);
            }
            public void SaveAndCleanUp()
            {
                foreach (var day in AllDayGroups)
                {
                    var filePath = this.FilePath(day.Started);
                    var jsonString = JsonConvert.SerializeObject(day.List);
                    FileGZ.Write(filePath.file, jsonString);
                }
                // Remove old days
                PingTests.RemoveAll(x => x.Started.Date < DateTime.Now.Date);
            }
            public void Restore(DateTime day)
            {
                var filePath = this.FilePath(day);
                if (File.Exists(filePath.file))
                {
                    var jsonString = FileGZ.ReadText(filePath.file);
                    this.PingTests = JsonConvert.DeserializeObject<List<PingTest>>(jsonString);
                }
            }
            public void Dispose()
            {
                this.Worker.CancelAsync();
            }
            #endregion
        }
        public class ListViewResults : ListViewPlus
        {
            public enum DataMode { Minute, Hour, Day }
            public readonly DataMode Mode;
            public readonly Host ParentHost;
            public ListViewResults(DataMode mode)
            {
                this.Mode = mode;
                switch (this.Mode)
                {
                    case DataMode.Minute:
                        this.AddColumns("Started:160", "Reply from:120:C", "Bytes:60:C", "Time:60:C", "TTL:60:C");
                        break;
                    case DataMode.Hour:
                        this.AddColumns("Minute:160", "Sent:60:C", "Lost:60:C", "Min:60:C", "Max:60:C", "Avg:60:C");
                        break;
                    case DataMode.Day:
                        this.AddColumns("Hour:160", "Sent:60:C", "Lost:60:C", "Min:60:C", "Max:60:C", "Avg:60:C");
                        break;
                }
                this.MouseDoubleClick += (sender, e) =>
                {
                    var item = this.SelectedItem;
                    if (item != null)
                    {
                        switch (this.Mode)
                        {
                            case DataMode.Hour:
                                var minute = (Host.PingTestGroup)item.Tag;
                                FormMinute($"{item.Text}", minute.List).Show(this);
                                break;
                            case DataMode.Day:
                                var hour = (Host.PingTestGroup)item.Tag;
                                FormHour($"{item.Text}", hour.List).Show(this);
                                break;
                        }
                    }
                };
            }
            public void UpdateResults(IEnumerable<Host.PingTest> tests)
            {
                this.UpdateResultsMinute(tests);
            }
            public void UpdateResults(IEnumerable<Host.PingTestGroup> groups)
            {
                switch (this.Mode)
                {
                    case DataMode.Hour:
                        this.UpdateResultsHour(groups);
                        break;
                    case DataMode.Day:
                        this.UpdateResultsDay(groups);
                        break;
                }
            }
            private void UpdateResultsMinute(IEnumerable<Host.PingTest> tests)
            {
                this.BeginUpdate();
                this.Items.Clear();
                foreach (var test in tests)
                {
                    this.AddRow(test, test.BG, $"{test.Started:yyyy-MM-dd HH:mm:ss}", $"{test.ReplyFrom}", $"{test.Bytes}", $"{test.Time}", $"{test.TTL}");
                }
                this.EndUpdate();
            }
            private void UpdateResultsHour(IEnumerable<Host.PingTestGroup> groups)
            {
                this.BeginUpdate();
                this.Items.Clear();
                foreach (var group in groups)
                {
                    this.AddRow(group, group.BG, $"{group.Started:yyyy-MM-dd HH:mm}", $"{group.Sent}", $"{group.Lost}", $"{group.Min}", $"{group.Max}", $"{group.Avg}");
                }
                this.EndUpdate();
            }
            private void UpdateResultsDay(IEnumerable<Host.PingTestGroup> groups)
            {
                this.BeginUpdate();
                this.Items.Clear();
                foreach (var group in groups)
                {
                    this.AddRow(group, group.BG, $"{group.Started:yyyy-MM-dd HH}", $"{group.Sent}", $"{group.Lost}", $"{group.Min}", $"{group.Max}", $"{group.Avg}");
                }
                this.EndUpdate();
            }
            public static Form FormMinute(string title, List<Host.PingTest> list)
            {
                var form = new Form() { Text = title, Width = 520, Height = 600, Font = new Font("Verdana", 9) };
                var listView = new ListViewResults(DataMode.Minute) { Dock = DockStyle.Fill };
                form.Icon = Properties.Resources.Icon;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Controls.Add(listView);
                listView.UpdateResultsMinute(list);
                return form;
            }
            public static Form FormHour(string title, List<Host.PingTest> list)
            {
                var form = new Form() { Text = title, Width = 520, Height = 600, Font = new Font("Verdana", 9) };
                var listView = new ListViewResults(DataMode.Hour) { Dock = DockStyle.Fill };
                var groups = Host.PingTestGroup.GroupByMinute(list);
                form.Icon = Properties.Resources.Icon;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Controls.Add(listView);
                listView.UpdateResultsHour(groups);
                return form;
            }
        }
        #endregion
        #region Init
        public static readonly string FilepathHosts = Settings.DataPathCombine("Hosts.json");
        public ListViewPlus listViewHosts = new ListViewPlus("Device:120", "Hostname:120") { Dock = DockStyle.Fill };
        public ListViewResults listViewMinute = new ListViewResults(ListViewResults.DataMode.Minute) { Dock = DockStyle.Fill };
        public ListViewResults listViewHour = new ListViewResults(ListViewResults.DataMode.Hour) { Dock = DockStyle.Fill };
        public ListViewResults listViewDay = new ListViewResults(ListViewResults.DataMode.Day) { Dock = DockStyle.Fill };
        public ListViewResults listViewArchive = new ListViewResults(ListViewResults.DataMode.Day) { Dock = DockStyle.Fill };
        private Host SelectedHost;
        private Timer timerArchive = new Timer() { Interval = 10 * 60 * 1000, Enabled = true }; // Every 10 minutes
        public FormMain()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Icon;
            notifyIcon.Icon = this.Icon;
            // 
            listViewHosts.ContextMenuStrip = CreteMenuHosts();
            listViewMinute.ContextMenuStrip = CreteMenuMinute();
            listViewHour.ContextMenuStrip = CreteMenuHour();
            listViewDay.ContextMenuStrip = CreteMenuDay();
            listViewArchive.ContextMenuStrip = CreteMenuArchive();
            // 
            splitContainer.Panel1.Controls.Add(listViewHosts);
            tabLastMinute.Controls.Add(listViewMinute);
            tabLastHour.Controls.Add(listViewHour);
            tabLastDay.Controls.Add(listViewDay);
            panelArchive.Controls.Add(listViewArchive);
            // 
            listViewHosts.ItemSelectionChanged += (sender, e) =>
            {
                if (listViewHosts.SelectedItem != null)
                {
                    this.SelectedHost = (Host)listViewHosts.SelectedItem.Tag;
                }
                else
                {
                    this.SelectedHost = null;
                }
                this.UpdateResults();
            };
            this.RestoreHosts();
            worker.RunWorkerAsync();
            // 
            timerArchive.Tick += (sender, e) =>
            {
                this.SaveArchive();
            };
        }
        private void FormMain_Load(object sender, EventArgs e)
        {

        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.HideOrShow();
        }
        #endregion
        #region Context Menu
        public ContextMenuStrip CreteMenuHosts()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Add", null, (sender, e) =>
            {
                if (FormHost.TryGetHost(out string device, out string hostname))
                {
                    var host = new Host(device, hostname);
                    listViewHosts.AddRow(host, host.Device, host.Hostname.ToString());
                    this.SaveHosts();
                }
            });
            menu.Items.Add("Move Up", null, (sender, e) =>
            {
                if (listViewHosts.SelectedItem != null)
                {
                    var item = listViewHosts.SelectedItem;
                    if (item.Index >= 1)
                    {
                        var index = item.Index - 1;
                        listViewHosts.Items.Remove(item);
                        listViewHosts.Items.Insert(index, item);
                        this.SaveHosts();
                    }
                }
            });
            menu.Items.Add("Move Down", null, (sender, e) =>
            {
                if (listViewHosts.SelectedItem != null)
                {
                    var item = listViewHosts.SelectedItem;
                    if (item.Index <= listViewHosts.Items.Count - 2)
                    {
                        var index = item.Index + 1;
                        listViewHosts.Items.Remove(item);
                        listViewHosts.Items.Insert(index, item);
                        this.SaveHosts();
                    }
                }
            });
            menu.Items.Add("Remove", null, (sender, e) =>
            {
                if (listViewHosts.SelectedItem != null)
                {
                    var item = listViewHosts.SelectedItem;
                    var host = (Host)item.Tag;
                    host.Dispose();
                    listViewHosts.Items.Remove(item);
                    this.SaveHosts();
                }
            });
            return menu;
        }
        public ContextMenuStrip CreteMenuMinute()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Refresh", null, (sender, e) =>
            {
                listViewMinute.UpdateResults(this.SelectedHost.LastMinute);
            });
            return menu;
        }
        public ContextMenuStrip CreteMenuHour()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Refresh", null, (sender, e) =>
            {
                listViewHour.UpdateResults(this.SelectedHost.LastHourGroups);
            });
            return menu;
        }
        public ContextMenuStrip CreteMenuDay()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Refresh", null, (sender, e) =>
            {
                listViewDay.UpdateResults(this.SelectedHost.LastDayGroups);
            });
            return menu;
        }
        public ContextMenuStrip CreteMenuArchive()
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("Refresh", null, (sender, e) =>
            {

            });
            return menu;
        }
        #endregion
        #region Main Menu
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveArchive();
        }
        private void DataPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.OpenInFileExplorer();
        }
        private void MikroTikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FormMikroTik()).Show();
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveArchive();
            this.Dispose();
            Application.Exit();
        }
        private void AutoRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoRefreshToolStripMenuItem.Checked = !autoRefreshToolStripMenuItem.Checked;
        }
        private void HideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.HideOrShow();
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Skounakis Yiannis\nskouny@gmail.com");
        }
        #endregion
        #region Events
        private void TabControlResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateResults();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!this.worker.CancellationPending)
            {
                if (this.AutoUpdate)
                {
                    this.UpdateResults();
                }
                System.Threading.Thread.Sleep(2000);
            }
        }
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.HideOrShow();
        }
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.HideOrShow();
        }
        private void dtArchive_ValueChanged(object sender, EventArgs e)
        {
            this.RefreshArchive();
        }
        private void buttonArchiveRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshArchive();
        }
        private bool AutoUpdate
        {
            get
            {
                if (this.Created && !this.Disposing && !this.IsDisposed)
                {
                    return (bool)this.Invoke(new Func<bool>(() =>
                    {
                        return autoRefreshToolStripMenuItem.Checked;
                    }));
                }
                return false;
            }
        }
        #endregion
        #region Methods
        private void HideOrShow()
        {
            if (this.Visible) this.Hide(); else this.Show();
            hideToolStripMenuItem.Checked = !this.Visible;
        }
        private void SaveHosts()
        {
            listViewHosts.Save(FilepathHosts);
        }
        private void RestoreHosts()
        {
            listViewHosts.Restore(FilepathHosts);
            foreach (ListViewItem item in listViewHosts.Items)
            {
                var device = item.Text;
                var hostname = item.SubItems[1].Text;
                item.Tag = new Host(device, hostname);
            }
        }
        private void UpdateResults()
        {
            if (!this.IsDisposed && !this.Disposing && this.SelectedHost != null)
            {
                this.Invoke(new Action(() =>
                {
                    if (tabControlResults.SelectedTab == tabLastMinute)
                    {
                        listViewMinute.UpdateResults(this.SelectedHost.LastMinute);
                    }
                    else if (tabControlResults.SelectedTab == tabLastHour)
                    {
                        listViewHour.UpdateResults(this.SelectedHost.LastHourGroups);
                    }
                    else if (tabControlResults.SelectedTab == tabLastDay)
                    {
                        listViewDay.UpdateResults(this.SelectedHost.LastDayGroups);
                    }
                    else if (tabControlResults.SelectedTab == tabArchive)
                    {
                        this.RefreshArchive();
                    }
                }));
            }
        }
        private void SaveArchive()
        {
            foreach (ListViewItem item in listViewHosts.Items)
            {
                var host = (Host)item.Tag;
                host.SaveAndCleanUp();
            }
        }
        public void RefreshArchive()
        {
            listViewArchive.Items.Clear();
            var selectedItem = listViewHosts.SelectedItem;
            if (selectedItem != null)
            {
                var selectedDate = dtArchive.Value.Date;
                var selectedHost = (Host)selectedItem.Tag;
                (var path, var file) = selectedHost.FilePath(selectedDate);
                var dirArchive = (new DirectoryInfo(path)).Parent.Parent;
                // Refresh dtArchive with date range
                var minDate = DateTime.Now.Date;
                var maxDate = DateTime.Now.Date;
                foreach (var dirYear in dirArchive.GetDirectories())
                {
                    if (int.TryParse(dirYear.Name, out int year))
                    {
                        foreach (var dirMonth in dirYear.GetDirectories())
                        {
                            if (int.TryParse(dirMonth.Name, out int month))
                            {
                                foreach (var fileDay in dirMonth.GetFiles())
                                {
                                    if (int.TryParse(fileDay.Name.Split('.')[0], out int day))
                                    {
                                        var curDate = new DateTime(year, month, day);
                                        if (curDate < minDate) minDate = curDate;
                                        if (curDate > maxDate) maxDate = curDate;
                                    }
                                }
                            }
                        }
                    }
                }
                dtArchive.MinDate = minDate;
                dtArchive.MaxDate = maxDate;
                dtArchive.Enabled = true;
                // Refresh ListView
                if (File.Exists(file))
                {
                    var jsonString = FileGZ.ReadText(file);
                    var tests = JsonConvert.DeserializeObject<List<Host.PingTest>>(jsonString);
                    var groups = Host.PingTestGroup.GroupByHour(tests);
                    listViewArchive.UpdateResults(groups);
                }
            }
            else
            {
                dtArchive.Enabled = false;
            }
        }
        #endregion
    }
}