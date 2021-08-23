using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cozyMonitoring
{
    public partial class FormHost : Form
    {
        public string Device;
        public string Hostname;
        public FormHost()
        {
            this.InitializeComponent();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textDevice.Text) && !string.IsNullOrWhiteSpace(textHostname.Text))
            {
                this.Device = textDevice.Text;
                this.Hostname = textHostname.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Both fields are required!");
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static bool TryGetHost(out string device, out string hostname)
        {
            using (var form = new FormHost())
            {
                form.ShowDialog();
                device = form.Device;
                hostname = form.Hostname;
            }
            return (!string.IsNullOrWhiteSpace(device) && !string.IsNullOrWhiteSpace(hostname));
        }
    }
}