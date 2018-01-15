using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DCTS.CustomComponents;
using Order.Buiness;
using Order.Common;
using Order.DB;

namespace GZ_DQCleaningCompany
{
    public partial class frmOrder : Form
    {
        DateTime startAt;
        DateTime endAt;
        List<clsOrderinfo> Orderinfolist_Server;
        int rowcount;
        string txfind;
        private SortableBindingList<clsOrderinfo> sortableOrderList;
        List<int> changeindex;

        private Hashtable dataGridChanges = null;
        public frmOrder(string limit)
        {
            InitializeComponent();
            this.dataGridChanges = new Hashtable();
            changeindex = new List<int>();
            this.WindowState = FormWindowState.Maximized;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var form = new frmAddOrder("");

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            this.dataGridChanges = new Hashtable();

            this.pbStatus.Value = 0;
            this.toolStripLabel1.Text = "";

            startAt = this.stockOutDateTimePicker.Value.AddDays(0).Date;
            endAt = this.stockInDateTimePicker1.Value.AddDays(0).Date;
            txfind = this.textBox8.Text;

            string strSelect = "select * from GZCleaning_Order where Input_Date>='" + startAt.ToString("yyyy/MM/dd") + "'" + "and " + "Input_Date<='" + endAt.ToString("yyyy/MM/dd") + "'";
            // strSelect = "select * from JNOrder_customer where Input_Date BETWEEN #" + startAt + "# AND #" + endAt + "#";//成功

            #region 判断汉字或字母
            int istrue = 0;

            bool ischina = clsCommHelp.HasChineseTest(txfind.ToString());
            if (ischina == false && txfind != "")
            {
                if (Regex.Matches(txfind.ToString(), "[a-zA-Z]").Count > 0 && !txfind.ToString().Contains("/"))
                {
                    istrue = 1;

                }
            }
            #endregion

            if (txfind.Length > 0 && istrue == 1)
            {
                strSelect += " And hetongbianhao like '%" + txfind + "%'";
                if (txfind == "所有")
                    strSelect = "select * from GZCleaning_Order";
            }

            if (txfind.Length > 0 && istrue == 0)
            {
                strSelect += " And kehuxingming like '%" + txfind + "%'";
                if (txfind == "所有")
                    strSelect = "select * from GZCleaning_Order";

            }


            strSelect += " order by hetongbianhao asc";

            clsAllnew BusinessHelp = new clsAllnew();
            Orderinfolist_Server = new List<clsOrderinfo>();
            Orderinfolist_Server = BusinessHelp.findOrder(strSelect);
            //1.预计交货时间-订货时间=2 天标记颜色
            DateTime dt3;
            DateTime dt2;
            foreach (clsOrderinfo item in Orderinfolist_Server)
            {
                //if (item.dinghuoshijian == null || item.dinghuoshijian.ToString() == "" || item.yujijiaohuoshijian == null || item.yujijiaohuoshijian.ToString() == "")
                //    continue;

                //dt3 = Convert.ToDateTime(item.dinghuoshijian);
                //dt2 = Convert.ToDateTime(item.yujijiaohuoshijian);

                //TimeSpan ts = dt2 - dt3;
                //int timeTotal = ts.Days;
                //if (timeTotal <= 2)
                //{
                //    item.Message = "False";

                //}
            }


            this.BindDataGridView();
        }
        private void BindDataGridView()
        {
            sortableOrderList = new SortableBindingList<clsOrderinfo>(Orderinfolist_Server);
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.DataSource = sortableOrderList;
            this.toolStripLabel1.Text = "条数：" + sortableOrderList.Count.ToString();

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" 确认删除这条信息 , 继续 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
            else
                return;

            var oids = GetOrderIdsBySelectedGridCell();
            for (int j = 0; j < oids.Count; j++)
            {
                var filtered = Orderinfolist_Server.FindAll(s => s.order_id == oids[j]);
                clsAllnew BusinessHelp = new clsAllnew();
                //批量删 
                int istu = BusinessHelp.deleteOrder(filtered[0].order_id.ToString());

                for (int i = 0; i < filtered.Count; i++)
                {
                    //单个删除
                    if (filtered[i].order_id != 0)
                    {
                        Orderinfolist_Server.Remove(Orderinfolist_Server.Where(o => o.order_id == filtered[i].order_id).Single());
                    }
                    if (istu != 1)
                    {
                        MessageBox.Show("删除失败，请查看" + filtered[i].hetongbianhao + filtered[i].kehuxingming, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            BindDataGridView();
        }
        private List<long> GetOrderIdsBySelectedGridCell()
        {

            List<long> order_ids = new List<long>();
            var rows = GetSelectedRowsBySelectedCells(dataGridView1);
            foreach (DataGridViewRow row in rows)
            {
                var Diningorder = row.DataBoundItem as clsOrderinfo;
                order_ids.Add((long)Diningorder.order_id);
            }

            return order_ids;
        }
        private IEnumerable<DataGridViewRow> GetSelectedRowsBySelectedCells(DataGridView dgv)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                rows.Add(cell.OwningRow);

            }
            rowcount = dgv.SelectedCells.Count;

            return rows.Distinct();
        }

    }
}
