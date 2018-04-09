using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Order.Buiness;
using Order.Common;
using Order.DB;

namespace GZ_DQCleaningCompany
{
    public partial class frmlogin : Form
    {
        public log4net.ILog ProcessLogger;
        public log4net.ILog ExceptionLogger;
        private TextBox txtSAPPassword;
        private CheckBox chkSaveInfo;
        Sunisoft.IrisSkin.SkinEngine se = null;
        frmAboutBox aboutbox;
        private System.Timers.Timer timerAlter1;
        int logis = 0;
        private OrdersControl OrdersControl;
        bool is_AdminIS;

        public frmlogin()
        {
            InitializeComponent();
            is_AdminIS = false;
            aboutbox = new frmAboutBox();

            InitialSystemInfo();
            //se = new Sunisoft.IrisSkin.SkinEngine();
            //se.SkinAllForm = true;
            //se.SkinFile = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "PageColor1.ssk");


            InitialPassword();
            ProcessLogger.Fatal("login" + DateTime.Now.ToString());

        }
        private void InitialSystemInfo()
        {
            #region 初始化配置
            ProcessLogger = log4net.LogManager.GetLogger("ProcessLogger");
            ExceptionLogger = log4net.LogManager.GetLogger("SystemExceptionLogger");
            ProcessLogger.Fatal("System Start " + DateTime.Now.ToString());
            #endregion
        }
        private void InitialPassword()
        {
            try
            {
                txtSAPPassword = new TextBox();
                txtSAPPassword.PasswordChar = '*';
                ToolStripControlHost t = new ToolStripControlHost(txtSAPPassword);
                t.Width = 100;
                t.AutoSize = false;
                t.Alignment = ToolStripItemAlignment.Right;
                this.toolStrip1.Items.Insert(this.toolStrip1.Items.Count - 4, t);

                chkSaveInfo = new CheckBox();
                chkSaveInfo.Text = "";
                chkSaveInfo.Padding = new Padding(5, 2, 0, 0);
                ToolStripControlHost t1 = new ToolStripControlHost(chkSaveInfo);
                t1.AutoSize = true;

                t1.ToolTipText = clsShowMessage.MSG_002;
                t1.Alignment = ToolStripItemAlignment.Right;
                this.toolStrip1.Items.Insert(this.toolStrip1.Items.Count - 5, t1);
                getUserAndPassword();
                chkSaveInfo.Checked = false;

            }
            catch (Exception ex)
            {
                //clsLogPrint.WriteLog("<frmMain> InitialPassword:" + ex.Message);
                throw ex;
            }
        }
        private void getUserAndPassword()
        {
            try
            {
                RegistryKey rkLocalMachine = Registry.LocalMachine;
                RegistryKey rkSoftWare = rkLocalMachine.OpenSubKey(clsConstant.RegEdit_Key_SoftWare);
                RegistryKey rkAmdape2e = rkSoftWare.OpenSubKey(clsConstant.RegEdit_Key_AMDAPE2E);
                if (rkAmdape2e != null)
                {
                    this.txtSAPUserId.Text = clsCommHelp.encryptString(clsCommHelp.NullToString(rkAmdape2e.GetValue(clsConstant.RegEdit_Key_User)));
                    this.txtSAPPassword.Text = clsCommHelp.encryptString(clsCommHelp.NullToString(rkAmdape2e.GetValue(clsConstant.RegEdit_Key_PassWord)));
                    if (clsCommHelp.NullToString(rkAmdape2e.GetValue(clsConstant.RegEdit_Key_Date)) != "")
                    {
                        this.chkSaveInfo.Checked = true;
                    }
                    else
                    {
                        this.chkSaveInfo.Checked = false;
                    }
                    rkAmdape2e.Close();
                }
                rkSoftWare.Close();
                rkLocalMachine.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }
        private void saveUserAndPassword()
        {
            try
            {
                RegistryKey rkLocalMachine = Registry.LocalMachine;
                RegistryKey rkSoftWare = rkLocalMachine.OpenSubKey(clsConstant.RegEdit_Key_SoftWare, true);
                RegistryKey rkAmdape2e = rkSoftWare.CreateSubKey(clsConstant.RegEdit_Key_AMDAPE2E);
                if (rkAmdape2e != null)
                {
                    rkAmdape2e.SetValue(clsConstant.RegEdit_Key_User, clsCommHelp.encryptString(this.txtSAPUserId.Text.Trim()));
                    rkAmdape2e.SetValue(clsConstant.RegEdit_Key_PassWord, clsCommHelp.encryptString(this.txtSAPPassword.Text.Trim()));
                    rkAmdape2e.SetValue(clsConstant.RegEdit_Key_Date, DateTime.Now.ToString("yyyMMdd"));
                }
                rkAmdape2e.Close();
                rkSoftWare.Close();
                rkLocalMachine.Close();

            }
            catch (Exception ex)
            {
                //ClsLogPrint.WriteLog("<frmMain> saveUserAndPassword:" + ex.Message);
                throw ex;
            }
        }

        private void 关于系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutbox.ShowDialog();
        }

        private void pBBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmUserManger(this.txtSAPUserId.Text.Trim(), "Admin");

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void 修改登录信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmUserManger(this.txtSAPUserId.Text.Trim(), "User");

            if (form.ShowDialog() == DialogResult.OK)
            {

            }

        }

        private void tsbLogin_Click(object sender, EventArgs e)
        {
            #region Noway
            DateTime oldDate = DateTime.Now;
            DateTime dt3;
            string endday = DateTime.Now.ToString("yyyy/MM/dd");
            dt3 = Convert.ToDateTime(endday);
            DateTime dt2;
            dt2 = Convert.ToDateTime("2018/08/11");

            TimeSpan ts = dt2 - dt3;
            int timeTotal = ts.Days;
            if (timeTotal < 0)
            {
                MessageBox.Show("六个月运行期已到，请将剩余费用付清 !");
                return;
            }


            #endregion
            try
            {
                ProcessLogger.Fatal("07932:System Login Start " + DateTime.Now.ToString());
                NewMethoduserFind(txtSAPUserId.Text.Trim(), txtSAPPassword.Text.Trim());

                if (logis != 0)
                {
                    ProcessLogger.Fatal("07933:System Login Start " + DateTime.Now.ToString());
                    this.WindowState = FormWindowState.Maximized;
                    if (chkSaveInfo.Checked == true)
                        saveUserAndPassword();
                    ProcessLogger.Fatal("07934:System Login Start " + DateTime.Now.ToString());
                    #region 更新登录时间
                    List<clsuserinfo> userlist_Server = new List<clsuserinfo>();
                    clsuserinfo item = new clsuserinfo();
                    item.name = txtSAPUserId.Text.Trim();

                    item.denglushijian = DateTime.Now.ToString("yyyyMMdd-HH:mm:ss");


                    userlist_Server.Add(item);
                    clsAllnew BusinessHelp = new clsAllnew();

                    BusinessHelp.updateLoginTime_Server(userlist_Server);
                    ProcessLogger.Fatal("07935:System Login Start " + DateTime.Now.ToString());
                    #endregion
                    this.WindowState = FormWindowState.Maximized;
                    tsbLogin.Text = "登录成功";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败，请查看根目录下的System文件夹中IP.txt中服务器IP 地址是否正确！" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

                throw ex;
            }
        }
        private bool NewMethoduserFind(string user, string pass)
        {

            try
            {
                clsAllnew BusinessHelp = new clsAllnew();

                List<clsuserinfo> userlist_Server = new List<clsuserinfo>();
                userlist_Server = BusinessHelp.findUser(txtSAPUserId.Text.Trim());



                if (userlist_Server.Count > 0 && userlist_Server[0].Btype == "lock")
                {
                    MessageBox.Show("登录失败,账户已被锁定，请重试或联系系统管理员，谢谢", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (userlist_Server.Count > 0 && userlist_Server[0].password.ToString().Trim() == pass.Trim() && userlist_Server[0].name.ToString().Trim() == user.Trim())
                    if (userlist_Server[0].AdminIS == "true")
                    {
                        toolStripDropDownButton1.Enabled = true;
                        toolStripDropDownButton3.Enabled = true;
                        toolStripDropDownButton2.Enabled = true;
                        //一键配置ToolStripMenuItem.Enabled = true;
                        pBBToolStripMenuItem.Enabled = true;
                        修改登录信息ToolStripMenuItem.Enabled = true;
                        logis++;
                        //是否是管理员
                        is_AdminIS = true;

                    }
                    else
                    {
                        toolStripDropDownButton1.Enabled = true;
                        toolStripDropDownButton3.Enabled = true;
                        toolStripDropDownButton2.Enabled = true;

                        修改登录信息ToolStripMenuItem.Enabled = true;
                        logis++;
                        // return false;
                    }

                if (logis == 0)
                {
                    MessageBox.Show("登录失败，请确认用户名和密码或联系系统管理员，谢谢", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                else
                    this.WindowState = FormWindowState.Maximized;

                return false;


            }
            catch (Exception ex)
            {
                ProcessLogger.Fatal("0793212:System Login Start " + DateTime.Now.ToString());
                MessageBox.Show("登录失败，验证用户信息异常！" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; ;

                throw;
            }

        }

        private void 导入彩票数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = false;


            if (OrdersControl == null)
            {
                OrdersControl = new OrdersControl(this.txtSAPUserId.Text, this.txtSAPPassword.Text.Trim(), is_AdminIS);
                OrdersControl.FormClosed += new FormClosedEventHandler(FrmOMS_FormClosed);
            }
            if (OrdersControl == null)
            {
                OrdersControl = new OrdersControl(this.txtSAPUserId.Text, this.txtSAPPassword.Text.Trim(), is_AdminIS);
            }
            OrdersControl.Show(this.dockPanel2);
        }
        void FrmOMS_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is OrdersControl)
            {
                OrdersControl = null;
            }
        }

        private void 查询信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmLogCenter("");

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
        }

        private void dockPanel2_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void 重置密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" 确认重置管理者账号 , 继续 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
            else
                return;
            List<clsuserinfo> userlist_Server = new List<clsuserinfo>();
            clsuserinfo item = new clsuserinfo();

            item.name = "admin";
            item.password = "123";
            item.Btype = "Normal";
            item.AdminIS = "true";
            item.jigoudaima = "管理者";
            item.Createdate = DateTime.Now.ToString("yyyy/MM/dd/HH");
            userlist_Server.Add(item);
            clsAllnew BusinessHelp = new clsAllnew();
            BusinessHelp.createUser_Server(userlist_Server);

            MessageBox.Show("初始化用户成功！" + "用户名 ：admin  密码 ：123", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    }
}
