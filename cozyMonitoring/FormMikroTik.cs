using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cozyMonitoring
{
    public partial class FormMikroTik : Form
    {
        public FormMikroTik()
        {
            InitializeComponent();
        }
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            var mikrotik = new MikroTik(textIP.Text);
            if (!mikrotik.Login(textUsername.Text, textPassword.Text))
            {
                MessageBox.Show("Failed.");
                return;
            }
            MessageBox.Show("Successfull!");
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
