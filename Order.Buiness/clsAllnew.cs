using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Order.Common;
using Order.DB;

namespace Order.Buiness
{
    public class clsAllnew
    {
        #region print
        private List<Stream> m_streams;
        private int m_currentPageIndex;
        List<clsOrderinfo> FilterTIPResults;
        string orderprint;
        #endregion
        private string dataSource = "CDCompanyData.sqlite";
        string newsth;

        public clsAllnew()
        {
            newsth = AppDomain.CurrentDomain.BaseDirectory + "" + dataSource;



        }
        public void createUser_Server(List<clsuserinfo> AddMAPResult)
        {
            //string sql = "insert into GZCleaning_User(name,password,Createdate,Btype,denglushijian,jigoudaima,AdminIS) values ('" + AddMAPResult[0].name + "','" + AddMAPResult[0].password + "','" + AddMAPResult[0].Createdate + "','" + AddMAPResult[0].Btype + "','" + AddMAPResult[0].denglushijian + "','" + AddMAPResult[0].jigoudaima + "','" + AddMAPResult[0].AdminIS + "')";
            string sql = "INSERT INTO GZCleaning_User ( name, password,Createdate,Btype,denglushijian,jigoudaima,AdminIS ) " +

                      "VALUES (\"" + AddMAPResult[0].name + "\"" +

                             ",\"" + AddMAPResult[0].password + "\"" +
                                  ",\"" + AddMAPResult[0].Createdate + "\"" +
                                       ",\"" + AddMAPResult[0].Btype + "\"" +
                                            ",\"" + AddMAPResult[0].denglushijian + "\"" +
                                                 ",\"" + AddMAPResult[0].jigoudaima + "\"" +
                             ",\"" + AddMAPResult[0].AdminIS + "\")";

            int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql, CommandType.Text, null);

            return;



        }
        public void lock_Userpassword_Server(List<clsuserinfo> AddMAPResult)
        {
            string sql = "update GZCleaning_User set Btype ='" + AddMAPResult[0].Btype.Trim() + "' where name ='" + AddMAPResult[0].name + "'";

            int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql, CommandType.Text, null);

            return;

        }
        public List<clsuserinfo> ReadUserlistfromServer()
        {
            string conditions = "select * from GZCleaning_User";//成功

            SQLiteConnection dbConn = new SQLiteConnection("Data Source=" + dataSource);

            dbConn.Open();
            SQLiteCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandText = conditions;
            DbDataReader reader = SQLiteHelper.ExecuteReader("Data Source=" + newsth, dbCmd);
            List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();
            while (reader.Read())
            {
                clsuserinfo item = new clsuserinfo();
                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item.Order_id = reader.GetString(0);
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.name = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.password = reader.GetString(2);
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.Createdate = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")
                    item.Btype = reader.GetString(4);
                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")
                    item.denglushijian = reader.GetString(5);
                if (reader.GetValue(6) != null && Convert.ToString(reader.GetValue(6)) != "")
                    item.jigoudaima = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")
                    item.AdminIS = reader.GetString(7);

                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;

        }

        public List<clsuserinfo> findUser(string findtext)
        {
            string strSelect = "select * from GZCleaning_User where name='" + findtext + "'";

            SQLiteConnection dbConn = new SQLiteConnection("Data Source=" + dataSource);

            dbConn.Open();
            SQLiteCommand dbCmd = dbConn.CreateCommand();
            //dbCmd.CommandText = "SELECT * FROM tuijanhaoma";
            dbCmd.CommandText = strSelect;

            DbDataReader reader = SQLiteHelper.ExecuteReader("Data Source=" + newsth, dbCmd);
            List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

            while (reader.Read())
            {
                clsuserinfo item = new clsuserinfo();

                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item.Order_id = reader.GetString(0);
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.name = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.password = reader.GetString(2);
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.Createdate = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")
                    item.Btype = reader.GetString(4);
                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")
                    item.denglushijian = reader.GetString(5);
                if (reader.GetValue(6) != null && Convert.ToString(reader.GetValue(6)) != "")
                    item.jigoudaima = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")
                    item.AdminIS = reader.GetString(7);

                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;

        }
        public void deleteUSER(string name)
        {
            string sql2 = "delete from GZCleaning_User where  name='" + name + "'";

            int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql2, CommandType.Text, null);

            return;

        }
        public void changeUserpassword_Server(List<clsuserinfo> AddMAPResult)
        {
            string sql = "update GZCleaning_User set password ='" + AddMAPResult[0].password.Trim() + "' where name ='" + AddMAPResult[0].name + "'";

            int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql, CommandType.Text, null);
            return;

        }
        public void updateLoginTime_Server(List<clsuserinfo> AddMAPResult)
        {
            string sql = "update GZCleaning_User set denglushijian ='" + AddMAPResult[0].denglushijian.Trim() + "' where name ='" + AddMAPResult[0].name + "'";

            int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql, CommandType.Text, null);

            return;

        }
        public int create_order_Server(List<clsOrderinfo> AddMAPResult)
        {
            string sql = "insert into GZCleaning_Order(hetongbianhao,kehuxingming,kehudizhi,lianxidianhua,hetongdaoqishijian,hetongshichang,shishou,yuejia,mianji,tixingshoufeishijian,meiyuanci,zongcishu,shengyushu,chaboli,kehuyaoqiu,beizhu,Input_Date,xinzeng,Message) values ('" + AddMAPResult[0].hetongbianhao + "','" + AddMAPResult[0].kehuxingming + "','" + AddMAPResult[0].kehudizhi + "','" + AddMAPResult[0].lianxidianhua + "','" + AddMAPResult[0].hetongdaoqishijian + "','" + AddMAPResult[0].hetongshichang + "','" + AddMAPResult[0].shishou + "','" + AddMAPResult[0].yuejia + "','" + AddMAPResult[0].mianji + "','" + AddMAPResult[0].tixingshoufeishijian + "','" + AddMAPResult[0].meiyuanci + "','" + AddMAPResult[0].zongcishu + "','" + AddMAPResult[0].shengyushu + "','" + AddMAPResult[0].chaboli + "','" + AddMAPResult[0].kehuyaoqiu + "','" + AddMAPResult[0].beizhu + "','" + AddMAPResult[0].Input_Date.ToString("yyyy/MM/dd") + "','" + AddMAPResult[0].xinzeng + "','" + AddMAPResult[0].Message + "')";

            int isrun = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql, CommandType.Text, null);

            return isrun;
        }
        public int deletecustomer(string name)
        {
            string sql2 = "delete from JNOrder_customer where  customer_id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }

        public int updatecustomer_Server(string findtext)
        {
        
            int isrun = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, findtext, CommandType.Text, null);

            return isrun;
        }


        public List<clsOrderinfo> findOrder(string findtext)
        {

            SQLiteConnection dbConn = new SQLiteConnection("Data Source=" + dataSource);

            dbConn.Open();
            SQLiteCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandText = findtext;

            DbDataReader reader = SQLiteHelper.ExecuteReader("Data Source=" + newsth, dbCmd);
            List<clsOrderinfo> ClaimReport_Server = new List<clsOrderinfo>();

            while (reader.Read())
            {
                clsOrderinfo item = new clsOrderinfo();

                item.order_id = reader.GetInt32(0);
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.hetongbianhao = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.kehuxingming = Convert.ToString(reader.GetString(2));
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.kehudizhi = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")

                    item.lianxidianhua = reader.GetString(4);
                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")

                    item.hetongdaoqishijian = reader.GetString(5);
                if (reader.GetValue(6) != null && Convert.ToString(reader.GetValue(6)) != "")

                    item.hetongshichang = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")

                    item.shishou = reader.GetString(7);
                if (reader.GetValue(8) != null && Convert.ToString(reader.GetValue(8)) != "")

                    item.yuejia = reader.GetString(8);
                if (reader.GetValue(9) != null && Convert.ToString(reader.GetValue(9)) != "")
                    item.mianji = Convert.ToString(reader.GetString(9));

                if (reader.GetValue(10) != null && Convert.ToString(reader.GetValue(10)) != "")

                    item.tixingshoufeishijian = Convert.ToString(reader.GetString(10));
                if (reader.GetValue(11) != null && Convert.ToString(reader.GetValue(11)) != "")

                    item.meiyuanci = reader.GetString(11);
                if (reader.GetValue(12) != null && Convert.ToString(reader.GetValue(12)) != "")

                    item.zongcishu = reader.GetString(12);
                if (reader.GetValue(13) != null && Convert.ToString(reader.GetValue(13)) != "")

                    item.shengyushu = reader.GetString(13);


                if (reader.GetString(14) != null && reader.GetString(14) != "")
                    item.chaboli = Convert.ToString(reader.GetString(14));
                if (reader.GetValue(15) != null && Convert.ToString(reader.GetValue(15)) != "")

                    item.kehuyaoqiu = reader.GetString(15);
                if (reader.GetValue(16) != null && Convert.ToString(reader.GetValue(16)) != "")
                    item.beizhu = Convert.ToString(reader.GetString(16));

                if (reader.GetValue(17) != null && Convert.ToString(reader.GetValue(17)) != "")
                    item.Input_Date = Convert.ToDateTime(reader.GetString(17));

                if (reader.GetValue(18) != null && Convert.ToString(reader.GetValue(18)) != "")
                    item.xinzeng = Convert.ToString(reader.GetString(18));

                if (reader.GetValue(19) != null && Convert.ToString(reader.GetValue(19)) != "")
                    item.Message = Convert.ToString(reader.GetString(19));

                ClaimReport_Server.Add(item);


            }
            return ClaimReport_Server;
        }
        public int deleteOrder(string name)
        {
            string sql2 = "delete from GZCleaning_Order where  order_id='" + name + "'";

            int isrun = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql2, CommandType.Text, null);

            return isrun;

        }
        public void Run(List<clsOrderinfo> FilterOrderResults)
        {

            LocalReport report = new LocalReport();
            report.ReportPath = Application.StartupPath + "\\Report1.rdlc";

            report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", FilterOrderResults));

            Export(report);
            m_currentPageIndex = 0;

            Print(orderprint, 0, 0);
        }
        public void Export(LocalReport report)
        {

            string deviceInfo =
"<DeviceInfo>" +
"  <OutputFormat>EMF</OutputFormat>" +
"  <PageWidth>8.27in</PageWidth>" +
"  <PageHeight>11.69in</PageHeight>" +
"  <MarginTop>0.0cm</MarginTop>" +
"  <MarginLeft>0.0cm</MarginLeft>" +
"  <MarginRight>0.0cm</MarginRight>" +
"  <MarginBottom>0.0cm</MarginBottom>" +
"</DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        private Stream CreateStream(string name, string fileNameExtension,

     Encoding encoding, string mimeType, bool willSeek)
        {

            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。

            Stream stream = new MemoryStream();

            m_streams.Add(stream);

            return stream;

        }
        public void Print(string defaultPrinterName, int lenpage, int withpage)
        {

            m_currentPageIndex = 0;
            if (m_streams == null || m_streams.Count == 0)
                return;
            //声明PrintDocument对象用于数据的打印

            PrintDocument printDoc = new PrintDocument();

            //指定需要使用的打印机的名称，使用空字符串""来指定默认打印机

            if (defaultPrinterName == "" || defaultPrinterName == null)
                defaultPrinterName = printDoc.PrinterSettings.PrinterName;

            printDoc.PrinterSettings.PrinterName = defaultPrinterName;

            //判断指定的打印机是否可用

            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show("Can't find printer");
                return;
            }
            //声明PrintDocument对象的PrintPage事件，具体的打印操作需要在这个事件中处理。

            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            //执行打印操作，Print方法将触发PrintPage事件。
            printDoc.DefaultPageSettings.Landscape = true;
            //大小
            if (lenpage != 0)
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom", lenpage, withpage);


            printDoc.Print();

        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            StringFormat SF = new StringFormat();
            SF.LineAlignment = StringAlignment.Center;
            SF.Alignment = StringAlignment.Center;
            float left = ev.PageSettings.Margins.Left;//打印区域的左边界
            float top = ev.PageSettings.Margins.Top;//打印区域的上边界
            float width = ev.PageSettings.PaperSize.Width - left - ev.PageSettings.Margins.Right;//计算出有效打印区域的宽度
            float height = ev.PageSettings.PaperSize.Height - top - ev.PageSettings.Margins.Bottom;//计算出有效打印区域的高度

            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public List<clsLog_info> findLog(string findtext)
        {
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);
            List<clsLog_info> ClaimReport_Server = new List<clsLog_info>();

            while (reader.Read())
            {
                clsLog_info item = new clsLog_info();

                item.Log_id = reader.GetInt32(0);
                item.product_no = reader.GetString(1);

                item.indent = reader.GetString(2);
                item.indent_date = reader.GetString(3);

                item.end_user = reader.GetString(4);


                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")

                    item.Input_Date = Convert.ToDateTime(reader.GetString(5));
                item.vendor = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")
                    item.daohuoshijian = reader.GetString(7);



                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
        }
        public int updateLog_Server(string findtext)
        {
            int isrun = MySqlHelper.ExecuteSql(findtext);

            return isrun;
        }
        public int deletelog(string name)
        {
            string sql2 = "delete from JNOrder_log where  Log_id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }

    }
}
