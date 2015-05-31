using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GameController2Keys
{
    public partial class MainForm : Form
    {
        private NotifyIcon notifyIcon;

        public MainForm()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Text = "GameController2Keys";
            notifyIcon.Icon = new Icon("32.ico");
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("Exit", exit_MouseClick) });
        }

        private void exit_MouseClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!File.Exists(Program.ConfigPath)) File.Copy("default.config", Program.ConfigPath);
            Process.Start("notepad.exe", Program.ConfigPath);
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(false);
        }
    }
}