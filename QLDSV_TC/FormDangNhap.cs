using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC
{
    public partial class FormDangNhap : Form
    {
        private SqlConnection conn_publisher = new SqlConnection();
        private bool isSinhVien = false;
        private bool isShowPass = false;
        public FormDangNhap()
        {
            InitializeComponent();
        }
        private void LayDSPM(String cmd)
        {
            DataTable dt = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            Program.bds_dspm.DataSource = dt;
            CmbKhoa.DataSource = Program.bds_dspm;
            CmbKhoa.DisplayMember = "TENKHOA";
            CmbKhoa.ValueMember = "TENSERVER";
        }
        private int KetNoi_CSDLGOC()
        {
            if (conn_publisher != null && conn_publisher.State == ConnectionState.Open)
                conn_publisher.Close();
            try
            {
                conn_publisher.ConnectionString = Program.connstr_publicsher;
                conn_publisher.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu. \nBạn xem lại tên Sever của Publisher, và tên CSDL trong chuỗi kết nối.\n" + e.Message);
                return 0;
            }
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            if (KetNoi_CSDLGOC() == 0) return;
            LayDSPM("SELECT * FROM Get_Subscribes");
            CmbKhoa.SelectedIndex = 1;
            CmbKhoa.SelectedIndex = 0;
            this.CenterToScreen();
        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
    
            if (TxbTaiKhoan.Text.Trim() == "" || TxbMatKhau.Text.Trim() == "")
            {
                String message;
                if(isSinhVien)
                {
                    message = "Mã sinh viên";
                } else
                {
                    message = "Tên đăng nhập";
                }
                MessageBox.Show(message + " và mật khẩu không được trống", "", MessageBoxButtons.OK);
                return;
            }
            if (isSinhVien == false)
            {
                Program.login = TxbTaiKhoan.Text; Program.password = TxbMatKhau.Text;
                if (Program.KetNoi() == 0) return;
            }
            else
            {
                Program.login = "SVKN";
                Program.password = "123456";
                if (Program.KetNoi() == 0) return;
            }


            Program.mKhoa = CmbKhoa.SelectedIndex;
            Program.mLogin = Program.login;
            Program.mPassword = Program.password;



            if (isSinhVien == false)
            {
                string strLenh = "EXEC dbo.SP_Lay_Thong_Tin_GV_Tu_Login '" + Program.login + "'";
                Program.myReader = Program.ExecSqlDataReader(strLenh);
                if (Program.myReader == null) return;
                Program.myReader.Read(); // Đọc 1 dòng nếu dữ liệu có nhiều dùng thì dùng for lặp nếu null thì break
                Program.mGroup = Program.myReader.GetString(2);
                Program.mHoten = Program.myReader.GetString(1);
                Program.username = Program.myReader.GetString(0);
                Program.myReader.Close();
            }
            else
            {
                string strlenh1 = "EXEC [dbo].[SP_LayThongTinSVTuLogin] '" + TxbTaiKhoan.Text + "', '" + TxbMatKhau.Text + "'";

                SqlDataReader reader = Program.ExecSqlDataReader(strlenh1);

                try
                {
                    if (reader == null) return;
                    reader.Read();
                    Program.mHoten = reader.GetString(0);
                    Program.username = TxbTaiKhoan.Text;
                    Program.mGroup = "SV";
                }

                catch (Exception ex)
                {
                    if (ex.Message.Contains("Error converting data type varchar to int"))
                        MessageBox.Show("Bạn format cell lại cột \"Ngày \" qua kiểu Number hoặc mở file Excel.");
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            Program.conn.Close();
            Program.formMain = new FormMain();
            Program.formMain.Show();
            Program.formDangNhap.Visible = false;
        }

        private void CmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CmbKhoa.SelectedValue.ToString() == "System.Data.DataRowView")
                    return;
                Program.servername = CmbKhoa.SelectedValue.ToString();
            }
            catch (Exception) { }
        }

        private void CbSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            isSinhVien = !isSinhVien;
            if (isSinhVien)
            {
                LabelTK.Text = "Mã Sinh Viên";
                return;
            }
            LabelTK.Text = "Tài Khoản";
        }

        private void cbHienMK_CheckedChanged(object sender, EventArgs e)
        {
            isShowPass = !isShowPass;
            if (isShowPass)
            {
                cbHienMK.Text = "Ẩn mật khẩu";
                TxbMatKhau.UseSystemPasswordChar = false;
                return;
            }
            cbHienMK.Text = "Hiện mật khẩu";
            TxbMatKhau.UseSystemPasswordChar = true;
        }
    }
}
