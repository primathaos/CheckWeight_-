using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestInterop;

namespace CheckWeight
{
    public partial class FormLogin : Form
    {
        List<string> m_UserList = new List<string>();
        List<string> m_PwdList = new List<string>();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            DataTable dt = DatabaseFunc.GetAccountInfo();
            m_UserList.Clear();
            m_PwdList.Clear();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        m_UserList.Add(dt.Rows[i]["Fusername"].ToString());
                        m_PwdList.Add(dt.Rows[i]["Fpassword"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("获取账号信息失败");
                    return;
                }
            }
            else
            {
                MessageBox.Show("获取账号信息失败");
                return;
            }

            DataView dv = new DataView(dt);
            cmbUser.DataSource = dv;
            cmbUser.DisplayMember = "fusername";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strUser = cmbUser.Text;
            string strPwd = txtPwd.Text;

            if ((string.IsNullOrEmpty(strUser)) || (string.IsNullOrEmpty(strPwd)))
            {
                MessageBox.Show("用户，密码不能为空");
                return;
            }

            bool bRet = false;
            for (int i = 0; i < m_UserList.Count; ++i)
            {
                if (strUser == m_UserList[i])
                {
                    if (strPwd == m_PwdList[i])
                    {
                        bRet = true;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("用户密码不一致");
                        txtPwd.SelectAll();
                        txtPwd.Focus();
                        break;
                    }
                }
            }

            if (bRet)
            {
                this.DialogResult = DialogResult.OK;
                DatabaseFunc.Context.ContextData.Clear();
                DatabaseFunc.Context.ContextData.Add("Key1", cmbUser.Text);
            } 
        }
    }
}
