using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Order.Buiness;
using Order.Common;
using Order.DB;

namespace GZ_DQCleaningCompany
{
    public partial class frmAddOrder : Form
    {
        List<clsOrderinfo> userlist_Server;

        public frmAddOrder(string limit)
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                read();

                clsAllnew BusinessHelp = new clsAllnew();

                int ISURN = BusinessHelp.create_order_Server(userlist_Server);
                if (ISURN == 1)
                {

                    if (MessageBox.Show(" 客户创建成功 , 是否继续添加 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        clear();
                    }
                    else
                        this.Close();
                }
                if (ISURN == 0)
                {
                    MessageBox.Show("客户创建失败,请检查是否录入有误！");

                }
            }
            catch (Exception ex)
            {
                return;

                throw;
            }
        }
        private void read()
        {
            userlist_Server = new List<clsOrderinfo>();

            clsOrderinfo item = new clsOrderinfo();


            item.hetongbianhao = this.txname.Text;
            if (item.hetongbianhao == null || item.hetongbianhao == "")
            {
                errorProvider1.SetError(txname, "不能为空");
                return;
            }
            else
                errorProvider1.SetError(txname, String.Empty);

            item.kehuxingming = this.txadress.Text;
            item.kehudizhi = this.tshuihao.Text;
            item.lianxidianhua = this.txbank.Text;
            item.hetongdaoqishijian = txaccount.Text;
            item.hetongshichang = txphone.Text;
            item.shishou = txcontact.Text;
            item.yuejia = txyuejia.Text;
            item.mianji = txmianji.Text;
            item.tixingshoufeishijian = txtixingshoufeishijian.Text;
            item.meiyuanci = txmeiyueci.Text;
            item.zongcishu = txzongcishu.Text;
            item.shengyushu = txshengyushuBOX.Text;
            item.chaboli = txchaboli.Text;
            item.kehuyaoqiu = txkehuyaoqiu.Text;
            item.beizhu = txbeizhu.Text;
            item.Input_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
            userlist_Server.Add(item);
        }
        private void clear()
        {
            this.txadress.Text = "";
            this.tshuihao.Text = "";
            this.txbank.Text = "";
            txaccount.Text = "";
            txphone.Text = "";
            txcontact.Text = "";
            txyuejia.Text = "";
            txmianji.Text = "";
            txtixingshoufeishijian.Text = "";
            txmeiyueci.Text = "";
            txzongcishu.Text = "";
            txshengyushuBOX.Text = "";
            txchaboli.Text = "";
            txkehuyaoqiu.Text = "";
            txbeizhu.Text = ""; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

    }
}
