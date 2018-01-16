using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCTS.CustomComponents;
using Order.Buiness;
using Order.Common;
using Order.DB;

namespace GZ_DQCleaningCompany
{
    public partial class frmLogCenter : Form
    {
        DateTime startAt;
        DateTime endAt;
        string txfind;
        List<clsOrderinfo> Loglist_Server;
        private SortableBindingList<clsOrderinfo> sortableLogList;
        List<int> changeindex;
        int RowRemark = 0;
        int cloumn = 0;
        private Hashtable dataGridChanges = null;
        DateTimePicker dtp = new DateTimePicker();  //这里实例化一个DateTimePicker控件  
        Rectangle _Rectangle;
        int rowcount;
        List<clsOrderinfo> deletedorderList;
        public frmLogCenter(string typename)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            changeindex = new List<int>();
            this.dataGridChanges = new Hashtable();
            Loglist_Server = new List<clsOrderinfo>();
            changeindex = new List<int>();
            deletedorderList = new List<clsOrderinfo>();


            this.BindDataGridView();
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            //this.pbStatus.Value = 0;
            this.toolStripLabel1.Text = "";

            txfind = this.textBox8.Text;
            if (txfind.Length <= 0 && checkBox1.Checked == false)
            {
                MessageBox.Show("请输入要查找的相关信息 ！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }
            if (checkBox1.Checked == true)
            {
                Read_order();
            }
            else
            {
                string conditions_new = "";


                {
                    if (txfind != "所有")
                        conditions_new += "'" + txfind + "'";
                    if (txfind == "所有")
                        conditions_new += "," + "'" + txfind + "'";
                }

                string strSelect = "select * from GZCleaning_Order where kehuxingming in ( " + conditions_new + " )";
                if (txfind == "所有")
                    strSelect = "select * from GZCleaning_Order ";

                strSelect += " order by Input_Date desc";

                clsAllnew BusinessHelp = new clsAllnew();
                Loglist_Server = new List<clsOrderinfo>();

                Loglist_Server = BusinessHelp.findOrder(strSelect);

            }
            //检查逻辑
            foreach (clsOrderinfo item in Loglist_Server)
            {
                clsCoverTime CoverTimeBusinessHelp = new clsCoverTime();

                string[] fileText = System.Text.RegularExpressions.Regex.Split(item.hetongdaoqishijian, "-");
                if (fileText.Length == 2)
                {
                    string result = "";
                    string result2 = "";
                    string time1 = "";
                    clsR2accrualsapinfo temp1 = new clsR2accrualsapinfo();
                    clsR2accrualsapinfo item1 = new clsR2accrualsapinfo();
                    item1.wenben = item.hetongdaoqishijian;
                    temp1.wenben = item.hetongdaoqishijian;

                    string[] temp2 = clsCoverTime.tiqushijian(item1, temp1, ref result, ref result2);

                    time1 = clsCoverTime.Catchwenbenshijian(item1, result, time1);
                    string[] temp3 = System.Text.RegularExpressions.Regex.Split(time1, "_");
                    DateTime ta1 = DateTime.Now;
                    for (int ilen = 0; ilen < temp3.Length; ilen++)
                    {
                        if (temp3[ilen] != null && temp3[ilen] != "")
                        {
                            ta1 = Convert.ToDateTime(temp3[ilen]);
                        }
                    }
                    DateTime dt3;
                    string endday = DateTime.Now.ToString("yyyy/MM/dd");
                    dt3 = Convert.ToDateTime(endday);

                    TimeSpan ts = ta1 - dt3;
                    int timeTotal = ts.Days;

                    if (timeTotal < 5 && timeTotal > 0)
                    {

                        item.Message = "False 合同将到期";
                    }

                    if (timeTotal <= 0)
                    {

                        item.Message = "False 合同超期";
                    }
                }



            }
            this.BindDataGridView();
        }
        private void BindDataGridView()
        {

            if (Loglist_Server != null)
            {

                sortableLogList = new SortableBindingList<clsOrderinfo>(Loglist_Server);
                bindingSource1.DataSource = new SortableBindingList<clsOrderinfo>(Loglist_Server);
                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.DataSource = bindingSource1;
                this.toolStripLabel1.Text = "条数：" + sortableLogList.Count.ToString();
            }



            if (dataGridView1 != null)
                this.toolStripLabel1.Text = "条目 " + this.dataGridView1.RowCount.ToString();

        }
        private void Read_order()
        {
            startAt = this.stockOutDateTimePicker.Value.AddDays(0).Date;
            endAt = this.stockInDateTimePicker1.Value.AddDays(0).Date;

            string strSelect = "select * from GZCleaning_Order where Input_Date>='" + startAt.ToString("yyyy/MM/dd") + "'" + "and " + "Input_Date<='" + endAt.ToString("yyyy/MM/dd") + "'";

            if (txfind.Length > 0)
            {
                strSelect += " And kehuxingming like '%" + txfind + "%'";
                if (txfind == "所有")
                    strSelect = "select * from GZCleaning_Order";

            }
            else
            {
                // strSelect = "select * from GZCleaning_Order";

            }
            strSelect += " order by Input_Date desc";

            clsAllnew BusinessHelp = new clsAllnew();
            Loglist_Server = new List<clsOrderinfo>();

            Loglist_Server = BusinessHelp.findOrder(strSelect);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry , No Data Output !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".csv";
            saveFileDialog.Filter = "csv|*.csv";
            string strFileName = "工作日志" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.FileName = strFileName;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                strFileName = saveFileDialog.FileName.ToString();
            }
            else
            {
                return;
            }
            FileStream fa = new FileStream(strFileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string strHeader = "";
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                strHeader += this.dataGridView1.Columns[i].HeaderText + delimiter;
            }
            sw.WriteLine(strHeader);

            //output rows data
            for (int j = 0; j < this.dataGridView1.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 0; k < this.dataGridView1.Columns.Count; k++)
                {
                    if (this.dataGridView1.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += this.dataGridView1.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                        if (this.dataGridView1.Rows[j].Cells[k].Value.ToString() == "LIP201507-35")
                        {

                        }

                    }
                    else
                    {
                        strRowValue += this.dataGridView1.Rows[j].Cells[k].Value + delimiter;
                    }
                }
                sw.WriteLine(strRowValue);
            }
            sw.Close();
            fa.Close();
            MessageBox.Show("下载完成 ！", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.groupBox2.Visible = false;
            this.toolStrip2.Visible = false;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void newButton_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)//18
            {
                //if (e.RowIndex > 0)
                {
                    if (e.Value != null && e.Value.ToString().Contains("到期"))
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                    else if (e.Value != null && e.Value.ToString().Contains("超期"))
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
            }

        }
    }
}
