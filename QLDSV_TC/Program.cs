using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;


namespace QLDSV_TC
{
    static class Program
    {
        public static SqlConnection conn = new SqlConnection("Data Source=DESKTOP-N44BQ15;Initial Catalog=QLDSV_TC;Persist Security Info=True;User ID=sa;Password=123456");
        public static String connstr;
        public static String connstr_publicsher = "Data Source=DESKTOP-N44BQ15;Initial Catalog=QLDSV_TC;User ID=sa;Password=123456";
        public static SqlDataAdapter da;
        public static SqlDataReader myReader;
        public static String servername = "DESKTOP-N44BQ15";
        public static String servername1 = "DESKTOP-N44BQ15\\SERVER1";
        public static String servername2 = "DESKTOP-N44BQ15\\SERVER2";
        public static String username = "";
        public static String mlogin = "";
        public static String password = "";
        public static String login = "";

        public static String database = "QLDSV_TC";
        public static String remotelogin = "HTKN";
        public static String remotepassword = "123456";
        public static String mLogin = "";
        public static String mPassword = "";
        public static String mGroup = "";
        public static String mHoten = "";
        public static int mKhoa = 0;

        public static BindingSource bds_dspm = new BindingSource();
        public static FormMain formMain;
        public static FormDangNhap formDangNhap;

        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open) Program.conn.Close();
            try
            {
                Console.WriteLine(Program.password);
                Console.WriteLine(Program.servername);
                Program.connstr = "Data Source=" + Program.servername + ";Initial Catalog=" + Program.database + ";User ID=" +
                      Program.login + ";Password=" + Program.password;
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }

        public static SqlDataReader ExecSqlDataReader(string strlenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strlenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static DataTable ExecSqlDataTable(string cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public static DS.LOPTINCHIDataTable ExecSqlDataTable2(string cmd)
        {
            DS.LOPTINCHIDataTable dt = new DS.LOPTINCHIDataTable();
            if (Program.conn.State == ConnectionState.Closed)
            {
                Program.conn.ConnectionString = Program.connstr_publicsher;
                Program.conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public static int ExecSqlNonQuery(string strlenh)
        {
            SqlCommand sqlcmd = new SqlCommand(strlenh, conn);
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandTimeout = 300; //5 phút
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                sqlcmd.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Error converting data type varchar to int"))
                    MessageBox.Show("Bạn format cell lại cột \"Ngày \" qua kiểu Number hoặc mở file Excel.");
                else
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                return ex.State; // trạng thái lỗi gởi từ RaisError trong sql server qua
            }
        }
        public static int CheckDataHelper(String query)
        {
            SqlDataReader dataReader = Program.ExecSqlDataReader(query);

            // nếu null thì thoát luôn  ==> lỗi kết nối
            if (dataReader == null)
            {
                return -1;
            }
            dataReader.Read();
            int result = int.Parse(dataReader.GetValue(0).ToString());
            dataReader.Close();
            return result;
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formMain = new FormMain();
            formDangNhap = new FormDangNhap();
            Application.Run(formDangNhap);
        }
    }
}
