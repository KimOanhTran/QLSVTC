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

namespace QLDSV_TC
{
    public partial class FormDangKy : DevExpress.XtraEditors.XtraForm
    {
        private Color primaryColor = System.Drawing.ColorTranslator.FromHtml("#00c0c0");
        private Color dangerColor = System.Drawing.ColorTranslator.FromHtml("#e41400");

        private int currentFromState = 1; // 0 = huy dk, 1  = dk

        private string maSV = "";
        private string ho = "";
        private string ten = "";
        private string maLop = "";
        private BindingSource bdsDSLopTC = new BindingSource();
        private BindingSource bdsDSLopTCHuy = new BindingSource();
        public FormDangKy()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        void LayThongTinSV()
        {
            string cmd = "EXEC dbo.SP_LayThongTinSVDangKy '" + Program.username + "'";
            Program.myReader = Program.ExecSqlDataReader(cmd);
            if (Program.myReader == null) return;
            Program.myReader.Read(); // Đọc 1 dòng nếu dữ liệu có nhiều dùng thì dùng for lặp nếu null thì break
            if (Program.myReader == null) return;
            this.maSV = Program.myReader.GetString(0);
            this.ho = Program.myReader.GetString(1);
            this.ten = Program.myReader.GetString(2);
            this.maLop = Program.myReader.GetString(3);
            this.lbDataMaSV.Text = this.maSV;
            this.lbDataHo.Text = this.ho;
            this.lbDataTen.Text = this.ten;
            this.lbDataMaLop.Text = this.maLop;
            Program.myReader.Close();

        }

        void LayDSNienKhoa()
        {
            string cmd = "SELECT * FROM [dbo].[LayDSNienKhoa]";
            DataTable dt = Program.ExecSqlDataTable(cmd);
            BindingSource bdsNienKhoa = new BindingSource();
            bdsNienKhoa.DataSource = dt;
            CmbNienKhoa.DataSource = bdsNienKhoa;
            CmbNienKhoa.DisplayMember = "NIENKHOA";
            CmbNienKhoa.ValueMember = "NIENKHOA";
        }

        void LayDSHocKy(string nienkhoa)
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC SP_LayDSHocKy '" + nienkhoa + "'";
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsHocKi = new BindingSource();
            bdsHocKi.DataSource = dt;
            CmbHocKy.DataSource = bdsHocKi;
            CmbHocKy.DisplayMember = "HOCKY";
            CmbHocKy.ValueMember = "HOCKY";
        }
        void LayDSLopTCHuy()
        {
            string cmd = "EXEC [dbo].[SP_LayDSDangKyCoTheHuy] '" + this.maSV + "'";
            DataTable tableLopTC = Program.ExecSqlDataTable(cmd);
            this.bdsDSLopTCHuy.DataSource = tableLopTC;
            this.gridControlDSLTCHuy.DataSource = this.bdsDSLopTCHuy;
        }
        private void FormDangKy_Load(object sender, EventArgs e)
        {
            LayThongTinSV();
            LayDSNienKhoa();
            CmbNienKhoa.SelectedIndex = 0;
            LayDSHocKy(CmbNienKhoa.SelectedValue.ToString());
            LayDSLopTCHuy();
        }

        private void CmbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayDSHocKy(CmbNienKhoa.Text);
            try
            {
                CmbHocKy.SelectedIndex = 0;
                LayDSHocKy(CmbNienKhoa.SelectedValue.ToString());
            }
            catch
            {
            }

        }

        private void BtnTimDSLTC_Click(object sender, EventArgs e)
        {
            string cmd = "EXEC [dbo].[SP_LayDSLopTinChi] '" + CmbNienKhoa.Text + "', '" + CmbHocKy.Text + "'";
            DataTable tableLopTC = Program.ExecSqlDataTable(cmd);
            this.bdsDSLopTC.DataSource = tableLopTC;
            this.gridControlDSLTC.DataSource = this.bdsDSLopTC;
        }

        private void gridControlDSLTC_Click(object sender, EventArgs e)
        {
            if (bdsDSLopTC.Count > 0)
            {
                this.currentFromState = 1;
                this.btnDangky.Text = "Đăng ký";
                this.btnDangky.BackColor = primaryColor;
                this.txbMaLopTC.Text = ((DataRowView)bdsDSLopTC[bdsDSLopTC.Position])["MALTC"].ToString();
                this.txbMaMH.Text = ((DataRowView)bdsDSLopTC[bdsDSLopTC.Position])["MAMH"].ToString();
                this.txbTenMH.Text =((DataRowView)bdsDSLopTC[bdsDSLopTC.Position])["TENMH"].ToString();
                this.txbNhom.Text = ((DataRowView)bdsDSLopTC[bdsDSLopTC.Position])["NHOM"].ToString();
                this.txbGV.Text = ((DataRowView)bdsDSLopTC[bdsDSLopTC.Position])["HOTENGV"].ToString();
                this.btnDangky.Enabled = true;
            }

        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Bạn có chắc chắn muốn đăng kí lớp học này ?";
            string messageBoxTextSuccess = "Đăng kí thành công!";
            string messageBoxTextError = "Đăng kí thất bại";
            if (currentFromState == 0)
            {
                messageBoxText = "Bạn có chắc chắn muốn huỷ đăng kí lớp học này ?";
                messageBoxTextSuccess = "Huỷ thành công!";
                messageBoxTextError = "Huỷ thất bại";
            }
            if (MessageBox.Show(messageBoxText, "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string cmd = "EXEC [dbo].[SP_XuLy_LTC] '" + maSV + "' , '" + txbMaLopTC.Text + "' , '" + CmbNienKhoa.SelectedValue.ToString() + "' , '" + CmbHocKy.SelectedValue.ToString() + "', " + currentFromState;
                MessageBox.Show(cmd);
                if (Program.ExecSqlNonQuery(cmd) == 0)
                {
                    MessageBox.Show(messageBoxTextSuccess);
                    this.txbMaLopTC.Text = "";
                    this.txbMaMH.Text = "";
                    this.txbTenMH.Text = "";
                    this.txbNhom.Text = "";
                    this.txbGV.Text = "";
                    this.btnDangky.Enabled = false;
                    LayDSLopTCHuy();
                }
                else
                {
                    MessageBox.Show(messageBoxTextError);
                }
            }
            else
            {
                return;
            }
        }

        private void gridControlDSLTCHuy_Click(object sender, EventArgs e)
        {
            if (bdsDSLopTCHuy.Count > 0)
            {
                this.currentFromState = 0;
                this.btnDangky.Text = "Huỷ đăng ký";
                this.btnDangky.BackColor = dangerColor;
                this.txbMaLopTC.Text = ((DataRowView)bdsDSLopTCHuy[bdsDSLopTCHuy.Position])["MALTC"].ToString();
                this.txbMaMH.Text = ((DataRowView)bdsDSLopTCHuy[bdsDSLopTCHuy.Position])["MAMH"].ToString();
                this.txbTenMH.Text = ((DataRowView)bdsDSLopTCHuy[bdsDSLopTCHuy.Position])["TENMH"].ToString();
                this.txbNhom.Text = ((DataRowView)bdsDSLopTCHuy[bdsDSLopTCHuy.Position])["NHOM"].ToString();
                this.txbGV.Text = ((DataRowView)bdsDSLopTCHuy[bdsDSLopTCHuy.Position])["HOTENGV"].ToString();
                this.btnDangky.Enabled = true;
            }
        }
    }
}