using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using TestInterop;
using System.Text.RegularExpressions;
using System.IO;

namespace CheckWeight
{
    public enum eCtrl { ebtnConnect, eTxtServer, eCmbPort, eTxtDown, eTxtUp, eTxtSN };
    public enum eState { eTesting, ePass, eFail };

    public partial class Form1 : Form
    {
        bool m_bInitOK;

        string m_strSN;

        string m_strPort;

        double m_dDown;

        double m_dUp;

        int i = 0;
        int j = 0;

        string dWeight_math = "";

        SerialPort m_Port;

        string m_strOperatorID;

        string m_strUnitid;

        //DateTime stattime;
        //DateTime endtime;

        public delegate void EnableCtrlDelegate(eCtrl nCtrl, bool bEnable);
        public delegate void ShowResultDelegate(string strOut, eState eI);
        public delegate void RefreshUIDelegate(bool bOk);
        public delegate void ShowIpInfoDelegate(string s);

        public Form1()
        {
            m_bInitOK = false;
            InitializeComponent();
        }

        private void txtSN_KeyUp(object sender, KeyEventArgs e)
        {
            if (!m_bInitOK)
            {
                MessageBox.Show("请先进行初始化");
                return;
            }

            if (txtDown.Text == "" || txtUp.Text == "")
            {
                //MessageBox.Show("门限范围必须处于锁定状态");
                if (Keys.Enter == e.KeyCode)
                {
                    if ((txtSN.Text.Length == 32) || (txtSN.Text.Length == 12) || (txtSN.Text.Length == 24) || (txtSN.Text.Length == 39))
                    {
                        m_strSN = txtSN.Text;
                        if (DoOperation_math(out double dWeight, out double fweight_c) && i < 10)
                        {
                            dWeight_math = dWeight_math + "," + dWeight.ToString("f4");
                            i++;
                            ShowResult("产品重量为" + dWeight.ToString("f4") + " 计算上下限还需要" + (10 - i) + "pcs,请继续扫描下一产品", eState.eTesting);
                        }

                        if (i >= 10)
                        {
                            string[] dWeight_Temp = dWeight_math.Split(',');
                            double dWeight_Min = GetMin(dWeight_Temp, i);
                            double dWeight_Max = GetMax(dWeight_Temp, i);
                            double dWeight_Mean = GetMean(dWeight_Temp, i);
                            double dWeight_DownUp = (dWeight_Max - dWeight_Min) * fweight_c;
                            txtDown.Text = (dWeight_Mean - dWeight_DownUp).ToString("f4");
                            txtUp.Text = (dWeight_Mean + dWeight_DownUp).ToString("f4");
                            txtUp.Enabled = false;
                            txtDown.Enabled = false;
                            MathUpDown.Text = "重新计算上下限";
                            ShowResult("重量上下限已计算完成，开始正常称重测试", eState.ePass);
                        }

                        txtSN.SelectAll();
                        txtSN.Focus();
                    }
                    else
                    {
                        txtSN.SelectAll();
                        txtSN.Focus();
                    }
                }
            }
            else
            {
                Regex r = new Regex(@"(^\-?\d+\.?\d*)$");
                Match m1 = r.Match(txtDown.Text);
                Match m2 = r.Match(txtUp.Text);
                if (m1.Success && m2.Success)
                {
                    m_dDown = Convert.ToDouble(m1.Groups[1].Value);
                    m_dUp = Convert.ToDouble(m2.Groups[1].Value);
                }
                else
                {
                    MessageBox.Show("请先计算重量门限");
                    return;
                }

                if (Keys.Enter == e.KeyCode)
                {
                    if ((txtSN.Text.Length == 32) || (txtSN.Text.Length == 12) || (txtSN.Text.Length == 24) || (txtSN.Text.Length == 39))
                    {
                        m_strSN = txtSN.Text;
                        Thread t = new Thread(DoOperation);
                        t.IsBackground = true;
                        t.Start();
                        txtSN.SelectAll();
                        txtSN.Focus();
                    }
                    else
                    {
                        txtSN.SelectAll();
                        txtSN.Focus();
                    }
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            m_strPort = cmbPort.Text;            

            if (!m_strPort.Contains("COM"))
            {
                MessageBox.Show("未选择端口");
                return;
            }

            Thread t = new Thread(InitFunc);
            t.IsBackground = true;
            t.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtServer.Text = "172.18.201.2";
            string[] ports = SerialPort.GetPortNames();
            cmbPort.Items.Clear();
            foreach (string str in ports)
            {
                cmbPort.Items.Add(str);
            }

            m_bInitOK = false;
            RefreshUI(m_bInitOK);
            txtDown.Enabled = false;
            txtUp.Enabled = false;
        }

        void InitFunc()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Config.ini"))
            {
                MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory + "Config.ini" + "文件不存在");
                return;
            }

            //读取配置文件，加载账号、密码、工位码
            string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Config.ini", System.Text.Encoding.GetEncoding("GB2312"));

            string serverIp = "", database = "", user = "", pwd = "";

            foreach (string line in lines)
            {
                if (line.Contains("OperatorID"))
                {
                    m_strOperatorID = line.Substring(line.IndexOf("=") + 1);
                }
                if (line.Contains("UnitID"))
                {
                    m_strUnitid = line.Substring(line.IndexOf("=") + 1);
                }
                if (line.Contains("ServerIP"))
                {
                    serverIp = line.Substring(line.IndexOf("=") + 1);
                }
                if (line.Contains("Database"))
                {
                    database = line.Substring(line.IndexOf("=") + 1);
                }
                if (line.Contains("user"))
                {
                    user = line.Substring(line.IndexOf("=") + 1);
                }
                if (line.Contains("pwd"))
                {
                    pwd = line.Substring(line.IndexOf("=") + 1);
                }
            }

            try
            {
                if (m_bInitOK)
                {
                    m_Port.Close();
                    m_bInitOK = false;
                    RefreshUI(m_bInitOK);
                    ShowIpInfo(serverIp);
                }
                else
                {
                    m_bInitOK = false;
                    RefreshUI(m_bInitOK);
                    ShowIpInfo(serverIp);

                    do
                    {
                        string strConnectString = string.Format("database={0};Server={1},1433;User ID={2};Password={3}", database, serverIp, user, pwd);

                        if (DatabaseFunc.Connect_DataBase(strConnectString) == null)
                        {
                            m_bInitOK = false;
                            MessageBox.Show("连接数据库失败，请检查网络以及服务器地址");
                            break;
                        }

                        if (!OpenPort())
                        {
                            m_bInitOK = false;
                        }
                        m_bInitOK = true;
                    } while (false);

                    RefreshUI(m_bInitOK);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void DoOperation()
        {
            ShowResult("读取中", eState.eTesting);
            bool bRet = false;
            //double dTotal = 0;
            double dValue = 0;
            //int nNum = 0;
            Thread.Sleep(1000);

            do 
            {
                if (!ReadWeight(out dValue))
                {
                    ShowResult("读取重量失败", eState.eFail);
                    break;
                }

                if ((dValue < m_dDown) || (dValue > m_dUp))
                {
                    ShowResult("重量不在门限范围内", eState.eFail);
                    lineleadercheckweight.Visible = true;
                    break;
                }

                if (!SubmitResult(dValue))
                {
                    break;
                }
                bRet = true;
            } while (false);

            if (bRet)
            {
                ShowResult(dValue.ToString("f4"), eState.ePass);
                if (lineleadercheckweight.Visible == true)
                {
                    lineleadercheckweight.Visible = false;
                }
            }
        }

        //计算上下限用
        bool DoOperation_math(out double dWeight,out double fweight_c)
        {
            ShowResult("读取中", eState.eTesting);
            bool bRet = false;
            //double dTotal = 0;
            double dValue = 0;
            //int nNum = 0;
            dWeight = 0;
            fweight_c = 0;
            Thread.Sleep(1000);

            do
            {
                if (!ReadWeight(out dValue))
                {
                    ShowResult("读取重量失败", eState.eFail);
                    break;
                }

                if (!SubmitResult_math(dValue,out fweight_c))
                {
                    break;
                }

                bRet = true;
            } while (false);

            if (bRet)
            {
                dWeight = dValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        //线长特权上传
        bool DoOperation_lineleader(out double dWeight)
        {
            ShowResult("读取中", eState.eTesting);
            bool bRet = false;
            //double dTotal = 0;
            double dValue = 0;
            //int nNum = 0;
            dWeight = 0;
            Thread.Sleep(1000);

            do
            {
                if (!ReadWeight(out dValue))
                {
                    ShowResult("读取重量失败", eState.eFail);
                    break;
                }

                if (!SubmitResult_lineleader(dValue))
                {
                    break;
                }

                bRet = true;
            } while (false);

            if (bRet)
            {
                dWeight = dValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool SubmitResult(double dValue)
        {
            string strTaskCode, strMac;
            string remark = "Down:" + txtDown.Text + ",Up:" + txtUp.Text;

            try
            {
                if (!DatabaseFunc.GetInfo(m_strSN, out strTaskCode, out strMac))
                {
                    ShowResult("获取工单信息失败", eState.eFail);
                    return false;
                }

                if (!DatabaseFunc.SaveResult_Weight("", strTaskCode, strMac, m_strSN, dValue.ToString("f4"), remark))
                {
                    ShowResult("上传测试记录失败", eState.eFail);
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        bool SubmitResult_math(double dValue,out double fweight_c)
        {
            string strTaskCode, strMac;
            fweight_c = 0;
            string remark = "称重计算产品";
            try
            {
                if (!DatabaseFunc.GetInfo(m_strSN, out strTaskCode, out strMac))
                {
                    ShowResult("获取工单信息失败", eState.eFail);
                    return false;
                }

                if (!DatabaseFunc.SaveResult_Weight("", strTaskCode, strMac, m_strSN, dValue.ToString("f4"), remark))
                {
                    ShowResult("上传测试记录失败", eState.eFail);
                    return false;
                }
                
                if (!DatabaseFunc.GetWeight_coefficient(strTaskCode, out fweight_c))
                {
                    ShowResult("获取称重系数失败", eState.eFail);
                    return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        bool SubmitResult_lineleader(double dValue)
        {
            string strTaskCode, strMac;
            string remark = DatabaseFunc.Context.ContextData["Key1"].ToString() + ",线长上传";
            try
            {
                if (!DatabaseFunc.GetInfo(m_strSN, out strTaskCode, out strMac))
                {
                    ShowResult("获取工单信息失败", eState.eFail);
                    return false;
                }

                if (!DatabaseFunc.SaveResult_Weight("", strTaskCode, strMac, m_strSN, dValue.ToString("f4"), remark))
                {
                    ShowResult("上传测试记录失败", eState.eFail);
                    return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        void RefreshUI(bool bOK)
        {
            if (btnConnect.InvokeRequired)
            {
                RefreshUIDelegate RUD = new RefreshUIDelegate(RefreshUI);
                this.BeginInvoke(RUD, new object[]{ bOK });
            }
            else
            {
                cmbPort.Enabled = !bOK;
                txtUp.Text = "待计算";
                txtDown.Text = "待计算";
                lineleadercheckweight.Visible = false;
                btnConnect.Text = (bOK ? "停止" : "初始化");

                //计算上下限提示
                if (btnConnect.Text == "停止")
                {
                    ShowResult("请点击计算上下限按钮开始", eState.eTesting);
                }
            }
        }

        void ShowResult(string strOut, eState eI)
        {
            if (btnResult.InvokeRequired)
            {
                ShowResultDelegate SRD = new ShowResultDelegate(ShowResult);
                this.BeginInvoke(SRD, new object[] { strOut, eI });
            }
            else
            {
                btnResult.Text = strOut;
                switch (eI)
                {
                    case eState.eTesting:
                        {
                            btnResult.BackColor = Color.PaleGoldenrod;
                            break;
                        }
                    case eState.ePass:
                        {
                            btnResult.BackColor = Color.YellowGreen;
                            break;
                        }
                    case eState.eFail:
                        {
                            btnResult.BackColor = Color.Red;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        void ShowIpInfo(string s)
        {
            if (txtServer.InvokeRequired)
            {
                ShowIpInfoDelegate SID = new ShowIpInfoDelegate(ShowIpInfo);
                this.BeginInvoke(SID, new object[] { s });
            } 
            else
            {
                txtServer.Text = s;
            }
        }

        bool OpenPort()
        {
            try
            {
                if (m_Port == null)
                {
                    m_Port = new SerialPort(m_strPort, 19200);
                }
                else
                {
                    m_Port.Close();
                    m_Port = new SerialPort(m_strPort, 19200);
                }

                m_Port.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "打开端口失败");
                return false;
            }

            return true;
        }

        bool ReadWeight(out double dValue)
        {
            dValue = 0;
            List<double> lTotal = new List<double>();
            double d1 = 0;
            try
            {
                lTotal.Clear();
                for (int i = 0; i < 5; ++i)
                {
                    int nCount = m_Port.BytesToRead;
                    byte[] bytearray = new byte[nCount];
                    int nRead = m_Port.Read(bytearray, 0, nCount);

                    string strOut = System.Text.Encoding.ASCII.GetString(bytearray, 0, nRead);
                    //Regex r = new Regex(@"ST NT ([\+|\-]?\d+\.?\d*)\s*[g|kg|lb]", RegexOptions.RightToLeft);
                    Regex r = new Regex(@"([\+|\-]?\d+\.?\d*)\s*[g|kg|lb]", RegexOptions.RightToLeft);
                    Match m = r.Match(strOut);
                    if (m.Success)
                    {
                        d1 = Convert.ToDouble(m.Groups[1].Value);
                        lTotal.Add(d1);
                        Thread.Sleep(200);
                    }
                }

                if (lTotal.Count > 0)
                {
                    foreach (double dt in lTotal)
                    {
                        dValue += dt;
                    }
                    dValue = dValue / lTotal.Count;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void MathUpDown_Click(object sender, EventArgs e)
        {
            bool bRet = false;
            if (txtDown.Text != "" || txtUp.Text != "")
            {
                FormLogin frmlogin = new FormLogin();
                frmlogin.StartPosition = FormStartPosition.CenterParent;
                if (frmlogin.ShowDialog() == DialogResult.OK)
                {
                    bRet = true;
                    ShowResult("扫描称重10pcs产品开始计算上下限", eState.eTesting);
                    i = 0;
                    j = 0;
                    dWeight_math = "";
                }
            }

            txtUp.Text = (bRet ? "" : txtUp.Text);
            txtDown.Text = (bRet ? "" : txtDown.Text);
            if (txtUp.Text != "" && txtUp.Text != "待计算")
            {
                txtSN.Enabled = true;
            }
            MathUpDown.Text = (bRet ? "计算上下限" : "重新计算上下限");
        }

        private void Lineleadercheckweight_Click(object sender, EventArgs e)
        {
            if (j <= 5)
            {
                FormLogin frmlogin = new FormLogin();
                frmlogin.StartPosition = FormStartPosition.CenterParent;
                if (frmlogin.ShowDialog() == DialogResult.OK)
                {
                    j++;
                    DoOperation_lineleader(out double dWeight);
                    lineleadercheckweight.Visible = false;
                    ShowResult("产品重量为" + dWeight.ToString("f4") + " 不良产品重量已使用线长权限上传", eState.ePass);
                }
            }
            else
            {
                txtSN.Enabled = false;
                txtUp.Text = "待计算";
                txtDown.Text = "待计算";
                if (lineleadercheckweight.Visible == true)
                {
                    lineleadercheckweight.Visible = false;
                }
                j = 0;
                ShowResult("线长权限上传数量已达到5pcs,请重新计算重量上下限", eState.eFail);
            }
        }

        //取最小值
        static double GetMin(string[] num, int count)
        {
            double min = double.Parse(num[1]);
            for (int i = 0; i < count; i++)
            {
                if (min > double.Parse(num[i+1]))
                {
                    min = double.Parse(num[i+1]);
                }
            }
            return min;
        }

        //取最大值
        static double GetMax(string[] num, int count)
        {
            double max = double.Parse(num[1]);
            for (int i = 0; i < count; i++)
            {
                if (max < double.Parse(num[i+1]))
                {
                    max = double.Parse(num[i+1]);
                }
            }
            return max;
        }

        //计算平均值
        static double GetMean(string[] num, int count)
        {
            double Mean_math = 0;
            for (int i = 0; i < count; i++)
            {
                Mean_math += double.Parse(num[i+1]);                
            }
            double Mean = Mean_math / count;
            return Mean;
        }


    }    
}




