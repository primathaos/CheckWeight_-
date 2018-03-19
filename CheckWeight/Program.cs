using OAUS.Core;
using System;
using System.Windows.Forms;
using System.IO;


namespace CheckWeight
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Config.ini"))
                {
                    MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory + "Config.ini" + "文件不存在");
                    return;
                }

                //读取配置文件，选择服务器
                string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Config.ini", System.Text.Encoding.GetEncoding("GB2312"));

                string serverUPIP = "";
                int serverPort = 4540;

                foreach (string line in lines)
                {
                    if (line.Contains("ServerUPIP"))
                    {
                        serverUPIP = line.Substring(line.IndexOf("=") + 1);
                    }
                }

                if (VersionHelper.HasNewVersion(serverUPIP, serverPort))
                {
                    //if (DialogResult.Yes == MessageBox.Show("检测到新版本，是否启动升级", "自动升级", MessageBoxButtons.YesNo))
                    //{
                    string updateExePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdater\\AutoUpdater.exe";
                    System.Diagnostics.Process myProcess = System.Diagnostics.Process.Start(updateExePath);
                    return;
                    //}
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("自动升级检测失败");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
