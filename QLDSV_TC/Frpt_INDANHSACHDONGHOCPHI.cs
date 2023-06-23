using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;

namespace QLDSV_TC
{
    public partial class Frpt_INDANHSACHDONGHOCPHI : DevExpress.XtraEditors.XtraForm
    {
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static String database = "QLDSV_TC";
        public Frpt_INDANHSACHDONGHOCPHI()
        {
            InitializeComponent();
        }
        public static int KetNoiSql(string severname, string mlogin, string password)
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
            try
            {
                connstr = "Data Source=" + severname + ";Initial Catalog=" +
                    database + ";User ID=" +
                    mlogin + ";password=" + password;
                conn.ConnectionString = connstr;
                conn.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu. \nBạn xem lại username và password.");
                return 0;
            }

        }

        public static SqlDataReader ExecSqlDataReader(string strlenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strlenh, conn);
            sqlcmd.CommandType = CommandType.Text;
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static string NumberToText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            return "(" + result + (suffix ? " đồng chẵn)" : ")");
        }
        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.lOPBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void Frpt_INDANHSACHDONGHOCPHI_Load(object sender, EventArgs e)
        {
            int type;
            // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.
            dS.EnforceConstraints = false;
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTableAdapter.Fill(this.dS.LOP);

            cmbMALOP.DataSource = this.dS.LOP;

            if (Program.mGroup.Equals("PGV") || Program.mGroup.Equals("KHOA"))
            {
                if (KetNoiSql("DESKTOP-N44BQ15\\SERVER3", Program.remotelogin, Program.remotepassword) == 0)
                {
                    MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
                }
            }
            if (Program.mGroup.Equals("KHOA")) type = 1;
            else type = 0;

            if (Program.mGroup.Equals("PKT"))
            {
                KetNoiSql("DESKTOP-N44BQ15\\SERVER3", "HP", "123456");
                    
            }
            if (type == 1)
            {
                // Lấy ra những lớp có MAKHOA = 'CNNT'
                cmbMALOP.DisplayMember = "MALOP";
                cmbMALOP.ValueMember = "MALOP";
                if(Program.mKhoa == 0)
                {
                    this.dS.LOP.DefaultView.RowFilter = "MAKHOA = 'CNNT'";

                }
                if(Program.mKhoa == 1)
                {
                    this.dS.LOP.DefaultView.RowFilter = "MAKHOA = 'VT'";
                }
            }
            else if (type == 0)
            {
                // Hiển thị tất cả các MALOP
                cmbMALOP.DisplayMember = "MALOP";
                cmbMALOP.ValueMember = "MALOP";
                this.dS.LOP.DefaultView.RowFilter = string.Empty;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string malop = cmbMALOP.Text;
            string nienkhoa = cmbNIENKHOA.Text;
            int hocky = int.Parse(cmbHOCKY.Text);
            string tongtien = "";
            string username = Program.username;
            string khoa = "SELECT MAKHOA FROM LOP WHERE MALOP ='" + malop + "'";
            SqlDataReader reader1 = ExecSqlDataReader(khoa);
            reader1.Read();
            string makhoa = reader1.GetString(0);
            reader1.Close();

           
            string TONGHP = "EXEC [dbo].[TONGTIENHOCPHI] " +
                            $"@NIENKHOA = N'{nienkhoa}', " + $"@HOCKY = {hocky}, " +
                            $"@MALOP = N'{malop}' ";
            SqlDataReader reader = ExecSqlDataReader(TONGHP);
            reader.Read();
            tongtien = reader.GetInt32(0).ToString();
            reader.Close();

            if (tongtien != "0")
            {
                tongtien = NumberToText(double.Parse(tongtien));
            }


            Xrpt_INDANHSACHDONGHOCPHI rpt = new Xrpt_INDANHSACHDONGHOCPHI(nienkhoa, hocky, malop, connstr);

            rpt.txtSUMHOCPHI.Text = tongtien;
            string tenkhoa;
            if (makhoa.Trim() == "CNTT")
            {
                rpt.txtKHOA.Text = "KHOA CONG NGHE THONG TIN";
            }
            if (makhoa.Trim() == "VT")
            {
                rpt.txtKHOA.Text = "KHOA VIEN THONG";
            }
            rpt.txtMALOP.Text = malop;
            ReportPrintTool print = new ReportPrintTool(rpt);

            print.ShowPreviewDialog();
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}