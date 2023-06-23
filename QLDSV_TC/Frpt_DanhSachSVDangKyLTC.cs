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

namespace QLDSV_TC
{
    public partial class Frpt_DanhSachSVDangKyLTC : Form
    {
        private string maKhoa = "";
        public Frpt_DanhSachSVDangKyLTC()
        {
            InitializeComponent();
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
        void LayDSNienKhoa()
        {
            string cmd = "SELECT * FROM [dbo].[LayDSNienKhoa]";
            DataTable dt = Program.ExecSqlDataTable(cmd);
            BindingSource bdsNienKhoa = new BindingSource();
            bdsNienKhoa.DataSource = dt;
            CmbNienKhoa.DataSource = bdsNienKhoa;
            CmbNienKhoa.DisplayMember = "NIENKHOA";
            CmbNienKhoa.ValueMember = "NIENKHOA";
            //CmbNienKhoa.SelectedIndex = 0;
        }

        void LayDSMonHoc(string nienkhoa, int hocky)
        {
            string cmd = "EXEC [dbo].[SP_LayDSMonHoc] '" + nienkhoa + "'," + hocky;

            DataTable dt = Program.ExecSqlDataTable(cmd);
            BindingSource bdsMonHoc = new BindingSource();
            bdsMonHoc.DataSource = dt;
            CmbMonHoc.DataSource = bdsMonHoc;
            CmbMonHoc.DisplayMember = "TENMH";
            CmbMonHoc.ValueMember = "MAMH";
        }
        void LayDSNhom(string nienkhoa, int hocky, string mamh)
        {
            string cmd = "EXEC [dbo].[SP_LayDSNhom] '" + nienkhoa + "'," + hocky + ",'" + mamh + "'";
            DataTable dt = Program.ExecSqlDataTable(cmd);
            BindingSource bdsNhom = new BindingSource();
            bdsNhom.DataSource = dt;
            CmbNhom.DataSource = bdsNhom;
            CmbNhom.DisplayMember = "NHOM";
            CmbNhom.ValueMember = "NHOM";
        }
        private void Frpt_DanhSachSVDangKyLTC_Load(object sender, EventArgs e)
        {

            var dataSource = Program.bds_dspm;
            var filteredDataSource = dataSource.Cast<DataRowView>()
                .Where(row => row["TENKHOA"].ToString() != "Học Phí")
                .ToList();
            CmbKhoa.DataSource = filteredDataSource;
            CmbKhoa.DisplayMember = "TENKHOA";
            CmbKhoa.ValueMember = "TENSERVER";
            CmbKhoa.SelectedIndex = Program.mKhoa;

            DataTable dt = new DataTable();
            dt = Program.ExecSqlDataTable("SELECT MAKHOA FROM KHOA");
            BindingSource bdsCN = new BindingSource();
            bdsCN.DataSource = dt;
            maKhoa = ((DataRowView)bdsCN[0])["MAKHOA"].ToString();

            LayDSNienKhoa();
            CmbNienKhoa.SelectedIndex = 0;
            LayDSHocKy(CmbNienKhoa.SelectedValue.ToString());
            LayDSMonHoc(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()));
            CmbMonHoc.SelectedIndex = 0;
            LayDSNhom(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()), CmbMonHoc.SelectedValue.ToString());


            if (Program.mGroup == "PGV")
                CmbKhoa.Enabled = true;
            else
                CmbKhoa.Enabled = false;

           


        }
        private void CmbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (CmbKhoa.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            Program.servername = CmbKhoa.SelectedValue.ToString();
            if (CmbKhoa.SelectedIndex != Program.mKhoa)
            {
                Program.login = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.login = Program.mLogin;
                Program.password = Program.mPassword;
            }
            if (Program.KetNoi() == 0)
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            else
            {
               
              


                DataTable dt = new DataTable();
                dt = Program.ExecSqlDataTable("SELECT MAKHOA FROM KHOA");
                BindingSource bdsCN = new BindingSource();
                bdsCN.DataSource = dt;
                maKhoa = ((DataRowView)bdsCN[0])["MAKHOA"].ToString();
                LayDSNienKhoa();
                CmbNienKhoa.SelectedIndex = 0;
                LayDSHocKy(CmbNienKhoa.SelectedValue.ToString());
                LayDSMonHoc(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()));
                CmbMonHoc.SelectedIndex = 0;
                LayDSNhom(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()), CmbMonHoc.SelectedValue.ToString());
            }
        }
        private void BtnIn_Click(object sender, EventArgs e)
        {

            Xrpt_DanhSachSVDangKyLTC rpt = new Xrpt_DanhSachSVDangKyLTC(CmbNienKhoa.Text, int.Parse(CmbHocKy.Text), CmbMonHoc.Text, int.Parse(CmbNhom.Text));

            rpt.LbKhoa.Text = CmbKhoa.Text;
            rpt.LbNienKhoa.Text = CmbNienKhoa.Text;
            rpt.LbHocKy.Text = CmbHocKy.Text;

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();

        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 

        private void CmbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LayDSNhom(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()), CmbMonHoc.SelectedValue.ToString());
            }
            catch
            {
            }

        }

        private void BtnIn_Click_1(object sender, EventArgs e)
        {

            Xrpt_DanhSachSVDangKyLTC rpt = new Xrpt_DanhSachSVDangKyLTC(CmbNienKhoa.Text, int.Parse(CmbHocKy.Text), CmbMonHoc.SelectedValue.ToString(), int.Parse(CmbNhom.Text));

            rpt.LbKhoa.Text = CmbKhoa.Text;
            rpt.LbNienKhoa.Text = CmbNienKhoa.Text;
            rpt.LbHocKy.Text = CmbHocKy.Text;
            rpt.LbNhom.Text = CmbNhom.Text;
            rpt.LbMonHoc.Text = CmbMonHoc.Text;

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }

        private void BtnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LayDSHocKy(CmbNienKhoa.Text);
            try
            {
                CmbHocKy.SelectedIndex = 0;
                LayDSHocKy(CmbNienKhoa.SelectedValue.ToString());
                LayDSMonHoc(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()));
                CmbMonHoc.SelectedIndex = 0;
                LayDSNhom(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()), CmbMonHoc.SelectedValue.ToString());
            }
            catch
            {
            }
        }

        private void CmbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LayDSMonHoc(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()));
                CmbMonHoc.SelectedIndex = 0;
                LayDSNhom(CmbNienKhoa.SelectedValue.ToString(), Int32.Parse(CmbHocKy.SelectedValue.ToString()), CmbMonHoc.SelectedValue.ToString());
            }
            catch
            {
            }
        }
    }
}
