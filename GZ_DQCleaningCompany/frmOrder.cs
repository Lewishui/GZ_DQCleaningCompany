using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        // 后台执行控件
        private BackgroundWorker bgWorker;
        // 消息显示窗体
        private frmMessageShow frmMessageShow;
        // 后台操作是否正常完成
        private bool blnBackGroundWorkIsOK = false;
        //后加的后台属性显
        private bool backGroundRunResult;
        DateTime startAt;
        DateTime endAt;
        List<clsOrderinfo> Orderinfolist_Server;
        int rowcount;
        string txfind;
        private SortableBindingList<clsOrderinfo> sortableOrderList;
        List<int> changeindex;
        List<clsOrderinfo> deletedorderList;
        string prc_folderpath;
        int pagenowindex = 0;
        int selectindex = 1;

        private Hashtable dataGridChanges = null;
        private bool is_AdminIS;

        public frmOrder(bool Is_AdminIS)
        {
            InitializeComponent();
            is_AdminIS = Is_AdminIS;

            this.dataGridChanges = new Hashtable();
            changeindex = new List<int>();
            Orderinfolist_Server = new List<clsOrderinfo>();
            deletedorderList = new List<clsOrderinfo>();


            this.BindDataGridView(Orderinfolist_Server);

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
            pagenowindex = 0;

            selectindex = 1;
            #region 显示信息
            var filtercout = Orderinfolist_Server.Where(o => o.Input_Date != null).Skip(pagenowindex).ToList();
            pagenowindex += 50;
            filtercout = filtercout.Where(o => o.Input_Date != null).Take(50).ToList();

            BindDataGridView(filtercout);   //   
            Pagelogic();

            #endregion

            //   this.BindDataGridView(  Orderinfolist_Server);

        }
        private void BindDataGridView(List<clsOrderinfo> Orderinfolist_Server)
        {
            if (Orderinfolist_Server != null)
            {

                sortableOrderList = new SortableBindingList<clsOrderinfo>(Orderinfolist_Server);
                bindingSource1.DataSource = new SortableBindingList<clsOrderinfo>(Orderinfolist_Server);
                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.DataSource = bindingSource1;
                this.toolStripLabel3.Text = "" + sortableOrderList.Count.ToString() + "/" + Orderinfolist_Server.Count + " (当前页面条目/条数总计)";
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            //e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);

            if (dataGridView1.RowCount > 0)
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = row.Index + 1;
                }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (is_AdminIS == false)
            {
                MessageBox.Show("普通用户 无权修改单据，如需修改请联系管理员", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
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
            BindDataGridView(Orderinfolist_Server);
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

        private void newButton_Click(object sender, EventArgs e)
        {
            clsOrderinfo item = new clsOrderinfo();
            item.Input_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
            item.xinzeng = "true";

            this.bindingSource1.Add(item);
            this.dataGridView1.Refresh();
        }

        private void delScheduleButton_Click(object sender, EventArgs e)
        {
            var schedule = GetSelectedSchedule();

            if (schedule != null)
            {
                deletedorderList.Add(schedule);
                bindingSource1.Remove(schedule);
                this.dataGridView1.Refresh();
            }
        }
        private clsOrderinfo GetSelectedSchedule()
        {
            clsOrderinfo schedule = null;
            var row = this.dataGridView1.CurrentRow;
            if (row != null)
            {
                schedule = row.DataBoundItem as clsOrderinfo;
            }
            return schedule;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                int s = this.tabControl1.SelectedIndex;
                if (s == 0)
                {
                    dataGridView1.Enabled = false;
                    if (changeindex.Count < 1)
                    {
                        IEnumerable<int> orderIds = GetChangedOrderIds();
                        foreach (var id in orderIds.Distinct())
                        {
                            changeindex.Add(id);
                        }
                    }
                }

                if (backgroundWorker2.IsBusy != true)
                {
                    backgroundWorker2.RunWorkerAsync(new WorkerArgument { OrderCount = 0, CurrentIndex = 0 });

                }
                dataGridChanges.Clear();

            }
            catch (Exception ex)
            {
                dataGridChanges.Clear();
                return;
                throw;
            }
        }

        private IEnumerable<int> GetChangedOrderIds()
        {

            List<int> rows = new List<int>();
            foreach (DictionaryEntry entry in dataGridChanges)
            {
                var key = entry.Key as string;
                if (key.EndsWith("_changed"))
                {
                    int row = Int32.Parse(key.Split('_')[0]);
                    rows.Add(row);
                }
                //                    Console.WriteLine("Key -- {0}; Value --{1}.", entry.Key, entry.Value);
            }
            return rows.Distinct();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            DataGridViewRow dgrSingle = dataGridView1.Rows[e.RowIndex];
            string cell_key = e.RowIndex.ToString() + "_" + e.ColumnIndex.ToString();

            if (!dataGridChanges.ContainsKey(cell_key))
            {
                dataGridChanges[cell_key] = dgrSingle.Cells[e.ColumnIndex].Value;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string cell_key = e.RowIndex.ToString() + "_" + e.ColumnIndex.ToString() + "_changed";

            if (dataGridChanges.ContainsKey(cell_key))
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.SelectionBackColor = Color.DarkRed;

            }


            if (e.ColumnIndex == 18 && is_AdminIS == false)//
            {
                //string xinzeng = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["xinzeng"].EditedFormattedValue.ToString());
                //if (xinzeng != "true")
                //{
                //    dataGridView1.Rows[e.RowIndex].ReadOnly = true;
                //    dataGridView1.Rows[e.RowIndex].Cells["shengyushu"].ReadOnly = false;
                //}
                //return;

            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string cell_key = e.RowIndex.ToString() + "_" + e.ColumnIndex.ToString();
            var new_cell_value = row.Cells[e.ColumnIndex].Value;
            var original_cell_value = dataGridChanges[cell_key];
            #region    //判断如果 当前修改的 比之前数据 大的话不让修改

            if (new_cell_value != null && original_cell_value != null && e.ColumnIndex == 14 && is_AdminIS == false)
                if (Convert.ToInt32(new_cell_value) > Convert.ToInt32(original_cell_value))
                {
                    MessageBox.Show("信息填写不符合要求！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dataGridView1.Rows[e.RowIndex].Cells["shengyushu"].Value = original_cell_value;

                    return;
                }
            #endregion
            // original_cell_value could null
            //Console.WriteLine(" original = {0} {3}, new ={1} {4}, compare = {2}, {5}", original_cell_value, new_cell_value, original_cell_value == new_cell_value, original_cell_value.GetType(), new_cell_value.GetType(), new_cell_value.Equals(original_cell_value));
            if (new_cell_value == null && original_cell_value == null)
            {
                dataGridChanges.Remove(cell_key + "_changed");
            }
            else if ((new_cell_value == null && original_cell_value != null) || (new_cell_value != null && original_cell_value == null) || !new_cell_value.Equals(original_cell_value))
            {
                dataGridChanges[cell_key + "_changed"] = new_cell_value;
            }
            else
            {
                dataGridChanges.Remove(cell_key + "_changed");
            }
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            bool success = dailysaveList(worker, e);
        }
        private bool dailysaveList(BackgroundWorker worker, DoWorkEventArgs e)
        {
            WorkerArgument arg = e.Argument as WorkerArgument;
            clsAllnew BusinessHelp = new clsAllnew();
            bool success = true;
            try
            {

                int rowCount = changeindex.Count;
                arg.OrderCount = rowCount;
                int j = 1;
                int progress = 0;
                #region MyRegion
                for (int ik = 0; ik < changeindex.Count; ik++)
                {
                    j = ik;

                    arg.CurrentIndex = j + 1;
                    progress = Convert.ToInt16(((j + 1) * 1.0 / rowCount) * 100);

                    int i = changeindex[ik];
                    var row = dataGridView1.Rows[i];

                    var model = row.DataBoundItem as clsOrderinfo;

                    clsOrderinfo item = new clsOrderinfo();

                    item.hetongbianhao = Convert.ToString(dataGridView1.Rows[i].Cells["hetongbianhao"].EditedFormattedValue.ToString());

                    item.kehuxingming = Convert.ToString(dataGridView1.Rows[i].Cells["kehuxingming"].EditedFormattedValue.ToString());

                    item.kehudizhi = Convert.ToString(dataGridView1.Rows[i].Cells["kehudizhi"].EditedFormattedValue.ToString());

                    item.lianxidianhua = Convert.ToString(dataGridView1.Rows[i].Cells["lianxidianhua"].EditedFormattedValue.ToString());

                    item.hetongdaoqishijian = Convert.ToString(dataGridView1.Rows[i].Cells["hetongdaoqishijian"].EditedFormattedValue.ToString());

                    item.hetongshichang = Convert.ToString(dataGridView1.Rows[i].Cells["hetongshichang"].EditedFormattedValue.ToString());

                    item.shishou = Convert.ToString(dataGridView1.Rows[i].Cells["shishou"].EditedFormattedValue.ToString());

                    item.yuejia = Convert.ToString(dataGridView1.Rows[i].Cells["yuejia"].EditedFormattedValue.ToString());

                    item.mianji = Convert.ToString(dataGridView1.Rows[i].Cells["mianji"].EditedFormattedValue.ToString());

                    item.tixingshoufeishijian = Convert.ToString(dataGridView1.Rows[i].Cells["tixingshoufeishijian"].EditedFormattedValue.ToString());
                    item.meiyuanci = Convert.ToString(dataGridView1.Rows[i].Cells["meiyuanci"].EditedFormattedValue.ToString());
                    item.zongcishu = Convert.ToString(dataGridView1.Rows[i].Cells["zongcishu"].EditedFormattedValue.ToString());
                    item.shengyushu = Convert.ToString(dataGridView1.Rows[i].Cells["shengyushu"].EditedFormattedValue.ToString());
                    item.chaboli = Convert.ToString(dataGridView1.Rows[i].Cells["chaboli"].EditedFormattedValue.ToString());
                    item.kehuyaoqiu = Convert.ToString(dataGridView1.Rows[i].Cells["kehuyaoqiu"].EditedFormattedValue.ToString());
                    item.beizhu = Convert.ToString(dataGridView1.Rows[i].Cells["beizhu"].EditedFormattedValue.ToString());
                    item.xinzeng = Convert.ToString(dataGridView1.Rows[i].Cells["xinzeng"].EditedFormattedValue.ToString());
                    item.Message = Convert.ToString(dataGridView1.Rows[i].Cells["Message"].EditedFormattedValue.ToString());

                    item.Input_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                    item.order_id = model.order_id;

                #endregion

                    #region MyRegion
                    var startAt = this.stockOutDateTimePicker.Value.AddDays(0).Date;


                    #region  构造查询条件
                    string conditions = "";
                    conditions = sql_yuju(e, item, conditions);
                    // conditions += " order by Id desc";
                    #endregion
                    #endregion

                    int isrun = BusinessHelp.updatecustomer_Server(conditions);
                    if (item.xinzeng == "true" && isrun == 1)
                        item.xinzeng = "";

                    if (arg.CurrentIndex % 5 == 0)
                    {
                        backgroundWorker2.ReportProgress(progress, arg);
                    }
                }
                backgroundWorker2.ReportProgress(100, arg);
                e.Result = string.Format("{0} 已保存 ！", changeindex.Count);

            }
            catch (Exception ex)
            {
                if (!e.Cancel)
                {

                    e.Result = ex.Message + "";
                }
                success = false;
            }

            return success;
        }

        private string sql_yuju(DoWorkEventArgs e, clsOrderinfo item, string conditions)
        {
            string unsal = conditions;

            if (item.hetongbianhao != null)
            {
                conditions += " hetongbianhao ='" + item.hetongbianhao + "'";
            }
            if (item.kehuxingming != null)
            {
                conditions += " ,kehuxingming ='" + item.kehuxingming + "'";
            }
            if (item.kehudizhi != null)
            {
                conditions += " ,kehudizhi ='" + item.kehudizhi + "'";
            }
            if (item.lianxidianhua != null)
            {
                conditions += " ,lianxidianhua ='" + item.lianxidianhua + "'";
            }
            if (item.hetongdaoqishijian != null)
            {
                conditions += " ,hetongdaoqishijian ='" + item.hetongdaoqishijian + "'";
            }
            if (item.hetongshichang != null)
            {
                conditions += " ,hetongshichang ='" + item.hetongshichang + "'";
            }
            if (item.shishou != null)
            {
                conditions += " ,shishou ='" + item.shishou + "'";
            }
            if (item.yuejia != null)
            {
                conditions += " ,yuejia ='" + item.yuejia + "'";
            }
            if (item.mianji != null)
            {
                conditions += " ,mianji ='" + item.mianji + "'";
            }
            if (item.tixingshoufeishijian != null)
            {
                conditions += " ,tixingshoufeishijian ='" + item.tixingshoufeishijian + "'";
            }
            if (item.meiyuanci != null)
            {
                conditions += " ,meiyuanci ='" + item.meiyuanci + "'";
            }
            if (item.zongcishu != null)
            {
                conditions += " ,zongcishu ='" + item.zongcishu + "'";
            }
            if (item.shengyushu != null)
            {
                conditions += " ,shengyushu ='" + item.shengyushu + "'";
            }
            if (item.chaboli != null)
            {
                conditions += " ,chaboli ='" + item.chaboli + "'";
            }
            if (item.kehuyaoqiu != null)
            {
                conditions += " ,kehuyaoqiu ='" + item.kehuyaoqiu + "'";
            }
            if (item.beizhu != null)
            {
                conditions += " ,beizhu ='" + item.beizhu + "'";
            }
            //if (item.xinzeng != null)
            //{
            //    conditions += " ,xinzeng ='" + item.xinzeng + "'";
            //}
            if (item.Message != null)
            {
                conditions += " ,Message ='" + item.Message + "'";
            }
            if (item.Input_Date != null)
            {
                conditions += " ,Input_Date ='" + item.Input_Date.ToString("yyyy/MM/dd") + "'";
            }
            if (item.xinzeng == "true")
                conditions = "insert into GZCleaning_Order(hetongbianhao,kehuxingming,kehudizhi,lianxidianhua,hetongdaoqishijian,hetongshichang,shishou,yuejia,mianji,tixingshoufeishijian,meiyuanci,zongcishu,shengyushu,chaboli,kehuyaoqiu,beizhu,Input_Date,Message) values ('" + item.hetongbianhao + "','" + item.kehuxingming + "','" + item.kehudizhi + "','" + item.lianxidianhua + "','" + item.hetongdaoqishijian + "','" + item.hetongshichang + "','" + item.shishou + "','" + item.yuejia + "','" + item.mianji + "','" + item.tixingshoufeishijian + "','" + item.meiyuanci + "','" + item.zongcishu + "','" + item.shengyushu + "','" + item.chaboli + "','" + item.kehuyaoqiu + "','" + item.beizhu + "','" + item.Input_Date.ToString("yyyy/MM/dd") + "','" + item.Message + "')";
            else if (is_AdminIS == true)
                conditions = "update GZCleaning_Order set  " + conditions + " where order_id = " + item.order_id + " ";
            else if (is_AdminIS == false)
            {

                if (item.shengyushu != null)
                {
                    unsal += "shengyushu ='" + item.shengyushu + "'";
                }
                unsal = "update GZCleaning_Order set  " + unsal + " where order_id = " + item.order_id + " ";

                return unsal;

                //e.Result = "[" + item.kehuxingming + "]普通用户无权修改单据，如需修改请联系管理员";
                //throw new Exception("[" + item.kehuxingming + "]普通用户 无权修改单据，如需修改请联系管理员");
            }
            return conditions;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show(string.Format("It is cancelled!"));
            }
            else
            {
                if (!e.Result.ToString().Contains("Dear"))
                {
                    toolStripLabel1.Text = "" + "(" + e.Result + ")" + "--数据已成功保存 可以继续编辑无需刷新";
                }
                else
                    toolStripLabel1.Text = e.Result.ToString();

                changeindex = new List<int>();

                dataGridView1.Enabled = true;
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WorkerArgument arg = e.UserState as WorkerArgument;
            if (!arg.HasError)
            {
                this.toolStripLabel1.Text = String.Format("{0}/{1}", arg.CurrentIndex, arg.OrderCount);
                this.ProgressValue = e.ProgressPercentage;
            }
            else
            {
                this.toolStripLabel1.Text = arg.ErrorMessage;
            }

        }
        public int ProgressValue
        {
            get { return this.pbStatus.Value; }
            set { pbStatus.Value = value; }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Orderinfolist_Server != null)
                dataGridView1.DataSource = Orderinfolist_Server;

            if (this.dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry , No Data Output !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".csv";
            saveFileDialog.Filter = "csv|*.csv";
            string strFileName = " 贵州诚德清洁服务管理有限公司客户专员数据" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
            MessageBox.Show("Dear User, Down File  Successful ！", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                blnBackGroundWorkIsOK = false;
            }
            else if (e.Cancelled)
            {
                blnBackGroundWorkIsOK = true;
            }
            else
            {
                blnBackGroundWorkIsOK = true;
            }
        }
        private void InitialBackGroundWorker()
        {
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            bgWorker.ProgressChanged +=
                new ProgressChangedEventHandler(bgWorker_ProgressChanged);
        }
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (frmMessageShow != null && frmMessageShow.Visible == true)
            {
                //设置显示的消息
                frmMessageShow.setMessage(e.UserState.ToString());
                //设置显示的按钮文字
                if (e.ProgressPercentage == clsConstant.Thread_Progress_OK)
                {
                    frmMessageShow.setStatus(clsConstant.Dialog_Status_Enable);
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            prc_folderpath = "";

            OpenFileDialog tbox = new OpenFileDialog();
            tbox.Multiselect = false;
            tbox.Filter = "Excel Files(*.xls,*.xlsx,*.xlsm,*.xlsb)|*.xls;*.xlsx;*.xlsm;*.xlsb";
            if (tbox.ShowDialog() == DialogResult.OK)
            {
                prc_folderpath = tbox.FileName;
            }
            if (prc_folderpath == null || prc_folderpath == "")
                return;
            if (MessageBox.Show("是否继续上传 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
            }
            else
                return;


            try
            {

                InitialBackGroundWorker();
                bgWorker.DoWork += new DoWorkEventHandler(CheckCaseList);

                bgWorker.RunWorkerAsync();
                // 启动消息显示画面
                frmMessageShow = new frmMessageShow(clsShowMessage.MSG_001,
                                                    clsShowMessage.MSG_007,
                                                    clsConstant.Dialog_Status_Disable);
                frmMessageShow.ShowDialog();
                // 数据读取成功后在画面显示
                if (blnBackGroundWorkIsOK)
                {
                    this.dataGridView1.DataSource = null;

                    this.dataGridView1.AutoGenerateColumns = false;
                    if (Orderinfolist_Server != null)
                    {
                        sortableOrderList = new SortableBindingList<clsOrderinfo>(Orderinfolist_Server);
                        bindingSource1.DataSource = new SortableBindingList<clsOrderinfo>(Orderinfolist_Server);

                        this.dataGridView1.DataSource = bindingSource1;

                        this.toolStripLabel1.Text = "已上传条目： " + Orderinfolist_Server.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("12320" + ex);
                return;

                throw ex;
            }
        }
        private void CheckCaseList(object sender, DoWorkEventArgs e)
        {
            try
            {
                Orderinfolist_Server = new List<clsOrderinfo>();

                //初始化信息
                clsAllnew BusinessHelp = new clsAllnew();

                DateTime oldDate = DateTime.Now;
                Orderinfolist_Server = BusinessHelp.Readexcel(ref this.bgWorker, prc_folderpath);
                foreach (clsOrderinfo item in Orderinfolist_Server)
                {
                    item.xinzeng = "true";

                    string conditions = "";
                    conditions = sql_yuju(e, item, conditions);
                    int isrun = BusinessHelp.updatecustomer_Server(conditions);
                    item.xinzeng = "false";
                }
                DateTime FinishTime = DateTime.Now;
                TimeSpan s = DateTime.Now - oldDate;
                string timei = s.Minutes.ToString() + ":" + s.Seconds.ToString();
                string Showtime = clsShowMessage.MSG_029 + timei.ToString();
                bgWorker.ReportProgress(clsConstant.Thread_Progress_OK, clsShowMessage.MSG_009 + "\r\n" + Showtime);
            }
            catch (Exception ex)
            {

                MessageBox.Show("12320" + ex);
                return;

                throw;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
            string _kehuxingming = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["kehuxingming"].EditedFormattedValue.ToString());

            if (column == kehuxingming)
            {
                var form = new frmClient_status(is_AdminIS, _kehuxingming);

                if (form.ShowDialog() == DialogResult.OK)
                {

                }

            }
        }

        private void moveDayUpButton_Click(object sender, EventArgs e)
        {
            if (selectindex <= 1)
                return;


            var filtercout = Orderinfolist_Server.Where(o => o.Input_Date != null).Skip(pagenowindex).ToList();
            pagenowindex -= 50;
            filtercout = filtercout.Where(o => o.Input_Date != null).Take(50).ToList();

            BindDataGridView(filtercout);   //   
            if (selectindex > 0)
                selectindex--;

            Pagelogic();
        }

        private void moveDayDownButton_Click(object sender, EventArgs e)
        {
            if (selectindex > Orderinfolist_Server.Count / 50)
                return;

            var filtercout = Orderinfolist_Server.Where(o => o.Input_Date != null).Skip(pagenowindex).ToList();
            pagenowindex += 50;
            filtercout = filtercout.Where(o => o.Input_Date != null).Take(50).ToList();

            BindDataGridView(filtercout);
            if (selectindex <= Orderinfolist_Server.Count / 50)
                selectindex++;
            Pagelogic();
        }

        private void Pagelogic()
        {
            if (Orderinfolist_Server != null)
            {
                if (selectindex > Orderinfolist_Server.Count / 50)
                    selectindex = 1;

                if (Orderinfolist_Server.Count / 50 == 0)
                {

                    this.toolStripLabel1.Text = " 当前页" + selectindex + "/" + "1";

                }
                else
                    this.toolStripLabel1.Text = " 当前页 " + selectindex + "/" + Orderinfolist_Server.Count / 50;
            }
        }

    }
}
