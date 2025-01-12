using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FallbackMechanism
{
    public partial class GUI : Form
    {
        int criticalValue = 50;
        ContextMenu trayMenu;
        bool warningShown = false;

        public GUI()
        {
            InitializeComponent();

            // Initialize system tray icon and context menu
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Open", ShowFormAgain);
            trayMenu.MenuItems.Add("Exit", ExitApplication);

            //this.Icon = null;
            this.Icon = FallbackMechanism.Properties.Resources.logo_gmp ;
            //this.Icon = Icon.FromHandle(FallbackMechanism.Properties.Resources.logo_gmp.GetHicon());
            //this.Icon = new Icon(@"D:\source\csharp\FallbackMechanism\FallbackMechanism\images\logo-gmp.ico", 40, 40);

            notifyIcon1.Icon = null;
            notifyIcon1.Icon = this.Icon;
            //notifyIcon1.Icon = FallbackMechanism.Properties.Resources.logo_maroon_gmp;
            //notifyIcon1.Icon = Icon.FromHandle(FallbackMechanism.Properties.Resources.logo.GetHicon());
            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenu = trayMenu;
            notifyIcon1.Text = this.Text;
        }

        private void ShowFormAgain(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void ExitApplication(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            this.timer1.Interval = 1000 * 60 * 5; // 5 minutes
            this.timer1.Enabled = true;
            CheckBatteryStatus();
        }

        private void CheckBatteryStatus()
        {
            // Retrieve battery information
            PowerStatus powerStatus = SystemInformation.PowerStatus;
            // Check if the battery is discharging and below 50%
            if (powerStatus.PowerLineStatus == PowerLineStatus.Offline && powerStatus.BatteryChargeStatus != BatteryChargeStatus.Charging) // If it's discharging
            {
                double per = this.criticalValue / 100.0;
                if (powerStatus.BatteryLifePercent <= per)
                {
                    if (!warningShown) // Show warning only once
                    {
                        var ss = string.Format("Warning: Battery is discharging and is below {0}%.", criticalValue);
                        notifyIcon1.Icon = null;
                        notifyIcon1.Icon = FallbackMechanism.Properties.Resources.logo_maroon_gmp;
                        notifyIcon1.ShowBalloonTip(3000, this.Text, ss, ToolTipIcon.Info);
                        warningShown = true;
                    }
                }
                else {
                    warningShown = false;
                }
            }
        }


        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowFormAgain(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckBatteryStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.criticalValue =  Convert.ToInt32(this.textBox1.Text);
            this.Hide();
            this.notifyIcon1.ShowBalloonTip(2000, this.Text, "Value updated.", ToolTipIcon.Info);
        }
    }
}
