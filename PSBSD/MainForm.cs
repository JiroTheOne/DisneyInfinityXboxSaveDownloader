using System.Diagnostics;
using System.Text;

namespace PSBSD
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public void Log(string m)
        {
            _ = LogList.Items.Add(m);
            LogList.TopIndex = Math.Max(LogList.Items.Count - (LogList.ClientSize.Height / LogList.ItemHeight) + 1, 0);
            _ = ValidateChildren();
        }


        private async void DownloadBtn_Click(object sender, EventArgs e)
        {
            if (Config.OutputPath != string.Empty)
            {
                if (Directory.EnumerateFileSystemEntries(Config.OutputPath).Any())
                {
                    if (File.Exists(Path.Combine(Config.OutputPath, Config.MetaFileName)))
                    {
                        Log("partial download detected. Continueing download");
                    }
                    else
                    {
                        MessageBox.Show("Please select a Valid and Empty Output folder first", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Tools.SelectFolder();
                        return;
                    }
                }
                if (!Directory.Exists(Config.OutputPath))
                {
                    MessageBox.Show("Folder selected doesnt exitst.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Tools.Log("Starting...");
                await Tools.Download();
                Enableinput();
                ProgressBarUpdate(0, 100);

            }
            else
            {
                MessageBox.Show("Please select a Output folder first", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Tools.SelectFolder();
                return;
            }
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            Tools.SelectFolder();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new();
            foreach (string s in LogList.Items)
            {
                _ = sb.Append(s);
                _ = sb.Append('\n');
            }
            Clipboard.SetText(sb.ToString());
            Tools.Log("Copied logs to clipboard");
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Tools.Log("Loaded");
            DialogResult result = MessageBox.Show(Config.Disclaimer, "DISCLAIMER", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Tools.Log("Accepted the disclaimer");
            }
            else if (result == DialogResult.No)
            {
                Tools.Log("Rejected the disclaimer. exiting");
                Environment.Exit(0);
            }
            Tools.Log("READY");
        }
        public void ProgressBarUpdate(int value, int max)
        {
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Maximum = max;
            progressBar.Value = value;
            _ = ValidateChildren();
        }
        public void ProgressBarUpdate(int value)
        {
            progressBar.Value = value;
            _ = ValidateChildren();
        }
        public void ProgressBarUpdate()
        {
            progressBar.PerformStep();
            _ = ValidateChildren();
        }
        public void ProgressBarMarquee()
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            _ = ValidateChildren();
        }

        public void Finished()
        {
            DownloadBtn.Text = "FINISH";
            DownloadBtn.Click -= DownloadBtn_Click;
            DownloadBtn.Click += Exit;
            BrowseBtn.Enabled = false;
            ProgressBarUpdate(100, 100);
        }
        public void Downloading()
        {
            Disableinput();
            DownloadBtn.Text = "DOWNLOADING";
        }
        public void Exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        public void Disableinput()
        {
            button1.Enabled = false;
            BrowseBtn.Enabled = false;
            DownloadBtn.Enabled = false;
        }
        public void Enableinput()
        {
            button1.Enabled = true;
            BrowseBtn.Enabled = true;
            DownloadBtn.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://discord.gg/RWSt8xnZGy",
                UseShellExecute = true
            });
        }

        private void EditionImage2_Click(object sender, EventArgs e)
        {
            Config.ServiceId = Config.ServiceId2;
            Config.PackageFamilyName = Config.PackageFamilyName2;
            Tools.Log("Set Version to 2.0");
        }

        private void EditionImage3_Click(object sender, EventArgs e)
        {
            Config.ServiceId = Config.ServiceId3;
            Config.PackageFamilyName = Config.PackageFamilyName3;
            Tools.Log("Set Version to 3.0");

        }
    }
}
