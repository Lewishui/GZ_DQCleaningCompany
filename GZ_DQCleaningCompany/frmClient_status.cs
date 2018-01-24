using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCTS.CustomComponents;
using Order.Buiness;
using Order.Common;
using Order.DB;

namespace GZ_DQCleaningCompany
{
    public partial class frmClient_status : Form
    {
        List<cls_kehucishufashengshijian_info>  list_Server;
        int rowcount;
        string txfind;
        private SortableBindingList<cls_kehucishufashengshijian_info> sortableOrderList;
        List<int> changeindex;
        List<cls_kehucishufashengshijian_info> deletedorderList;
        string prc_folderpath;
        private Hashtable dataGridChanges = null;
        private bool is_AdminIS;
        string b_kehuxingming;

        public frmClient_status(bool Is_AdminIS, string _kehuxingming)
        {
            InitializeComponent();
            is_AdminIS = Is_AdminIS;
            b_kehuxingming = _kehuxingming;
            this.dataGridChanges = new Hashtable();
            changeindex = new List<int>();
            list_Server = new List<cls_kehucishufashengshijian_info>();
            deletedorderList = new List<cls_kehucishufashengshijian_info>();

            string strSelect = "select * from GZCleaning_Status where ";
            strSelect += "kehuxingming like '%" + _kehuxingming + "%'";
            clsAllnew BusinessHelp = new clsAllnew();

            list_Server = BusinessHelp.findStatus(strSelect);


            this.BindDataGridView();


            this.WindowState = FormWindowState.Maximized;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private cls_kehucishufashengshijian_info GetSelectedSchedule()
        {
            cls_kehucishufashengshijian_info schedule = null;
            var row = this.dataGridView1.CurrentRow;
            if (row != null)
            {
                schedule = row.DataBoundItem as cls_kehucishufashengshijian_info;
            }
            return schedule;
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
        private List<long> GetOrderIdsBySelectedGridCell()
        {

            List<long> order_ids = new List<long>();
            var rows = GetSelectedRowsBySelectedCells(dataGridView1);
            foreach (DataGridViewRow row in rows)
            {
                var Diningorder = row.DataBoundItem as cls_kehucishufashengshijian_info;
                order_ids.Add((long)Diningorder.status_id);
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

        private void BindDataGridView()
        {
            if (list_Server != null)
            {

                sortableOrderList = new SortableBindingList<cls_kehucishufashengshijian_info>( list_Server);
                bindingSource1.DataSource = new SortableBindingList<cls_kehucishufashengshijian_info>( list_Server);
                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.DataSource = bindingSource1;
                this.toolStripLabel1.Text = "条数：" + sortableOrderList.Count.ToString();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string cell_key = e.RowIndex.ToString() + "_" + e.ColumnIndex.ToString();
            var new_cell_value = row.Cells[e.ColumnIndex].Value;
            var original_cell_value = dataGridChanges[cell_key];
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string cell_key = e.RowIndex.ToString() + "_" + e.ColumnIndex.ToString() + "_changed";

            if (dataGridChanges.ContainsKey(cell_key))
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.SelectionBackColor = Color.DarkRed;

            }
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

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
          
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

                    var model = row.DataBoundItem as cls_kehucishufashengshijian_info;

                    cls_kehucishufashengshijian_info item = new cls_kehucishufashengshijian_info();

                    item.kehuxingming = Convert.ToString(dataGridView1.Rows[i].Cells["kehuxingming"].EditedFormattedValue.ToString());

                    item.shangcichugongshijian = Convert.ToString(dataGridView1.Rows[i].Cells["shangcichugongshijian"].EditedFormattedValue.ToString());

                    item.shangcichugongbaojie = Convert.ToString(dataGridView1.Rows[i].Cells["shangcichugongbaojie"].EditedFormattedValue.ToString());

                    item.kehupingjia = Convert.ToString(dataGridView1.Rows[i].Cells["kehupingjia"].EditedFormattedValue.ToString());

                    item.beizhu = Convert.ToString(dataGridView1.Rows[i].Cells["beizhu"].EditedFormattedValue.ToString());
                 
                    item.xinzeng = Convert.ToString(dataGridView1.Rows[i].Cells["xinzeng"].EditedFormattedValue.ToString());
                    item.Input_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                    item.status_id = model.status_id;

                #endregion

                    #region MyRegion
               

                    #region  构造查询条件
                    string conditions = "";
                    conditions = sql_yuju(e, item, conditions);
                  
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

        private string sql_yuju(DoWorkEventArgs e, cls_kehucishufashengshijian_info item, string conditions)
        {
            if (item.kehuxingming != null)
            {
                conditions += " kehuxingming ='" + item.kehuxingming + "'";
            }
            if (item.shangcichugongshijian != null)
            {
                conditions += " ,shangcichugongshijian ='" + item.shangcichugongshijian + "'";
            }
            if (item.shangcichugongbaojie != null)
            {
                conditions += " ,shangcichugongbaojie ='" + item.shangcichugongbaojie + "'";
            }
            if (item.kehupingjia != null)
            {
                conditions += " ,kehupingjia ='" + item.kehupingjia + "'";
            }
            if (item.beizhu != null)
            {
                conditions += " ,beizhu ='" + item.beizhu + "'";
            }
           
            if (item.Input_Date != null)
            {
                conditions += " ,Input_Date ='" + item.Input_Date.ToString("yyyy/MM/dd") + "'";
            }
            if (item.xinzeng == "true")
                conditions = "insert into GZCleaning_Status(kehuxingming,shangcichugongshijian,shangcichugongbaojie,kehupingjia,beizhu,Input_Date) values ('" + item.kehuxingming + "','" + item.shangcichugongshijian + "','" + item.shangcichugongbaojie + "','" + item.kehupingjia + "','" + item.beizhu  + "','" + item.Input_Date.ToString("yyyy/MM/dd") + "')";
            else if (is_AdminIS == true)
                conditions = "update GZCleaning_Status set  " + conditions + " where status_id = " + item.status_id + " ";
            else if (is_AdminIS == false)
            {
                e.Result = "[" + item.kehuxingming + "]普通用户无权修改单据，如需修改请联系管理员";
                throw new Exception("[" + item.kehuxingming + "]普通用户 无权修改单据，如需修改请联系管理员");
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
                    toolStripLabel1.Text = "" + "(" + e.Result + ")" + "--数据已成功保存";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            cls_kehucishufashengshijian_info item = new cls_kehucishufashengshijian_info();
            item.Input_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
            item.xinzeng = "true";
            item.kehuxingming = b_kehuxingming;
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

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (is_AdminIS == false)
            //{
            //    MessageBox.Show("普通用户 无权修改单据，如需修改请联系管理员", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}
            if (MessageBox.Show(" 确认删除这条信息 , 继续 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
            else
                return;

            var oids = GetOrderIdsBySelectedGridCell();
            for (int j = 0; j < oids.Count; j++)
            {
                var filtered = list_Server.FindAll(s => s.status_id == oids[j]);
                clsAllnew BusinessHelp = new clsAllnew();
                //批量删 
                int istu = BusinessHelp.deleteStatus(filtered[0].status_id.ToString());

                for (int i = 0; i < filtered.Count; i++)
                {
                    //单个删除
                    if (filtered[i].status_id != 0)
                    {
                        list_Server.Remove(list_Server.Where(o => o.status_id == filtered[i].status_id).Single());
                    }
                    if (istu != 1)
                    {
                        MessageBox.Show("删除失败，请查看" + filtered[i].kehuxingming , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            BindDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridChanges == null)
                return;

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


    }
}
