using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLDSV_TC
{
    public partial class FormReportBDTK : DevExpress.XtraEditors.XtraForm
    {
        private BindingSource bdsLop = new BindingSource();
        public FormReportBDTK()
        {
            InitializeComponent();
        }

        private void FormReportBDTK_Load(object sender, EventArgs e)
        {
            var dataSource = Program.bds_dspm;
            var filteredDataSource = dataSource.Cast<DataRowView>()
               .Where(row => row["TENKHOA"].ToString() != "Học Phí")
               .ToList();
            CmbKhoa.DataSource = filteredDataSource;
            CmbKhoa.DisplayMember = "TENKHOA";
            CmbKhoa.ValueMember = "TENSERVER";
            CmbKhoa.SelectedIndex = Program.mKhoa;
            if (Program.mGroup == "KHOA")
            {
                CmbKhoa.Enabled = false;
            }
            LayDSMaLop();
        }

        void LayDSMaLop()
        {
            string cmd = "SELECT * FROM [dbo].[LayDSMaLopHoc]";
            DataTable dt = Program.ExecSqlDataTable(cmd);
            bdsLop.DataSource = dt;
            CmbMalop.DataSource = bdsLop;
            CmbMalop.DisplayMember = "MALOP";
            CmbMalop.ValueMember = "MALOP";
            CmbMalop.SelectedIndex = 0;
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
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            } else
            {
                LayDSMaLop();
            }
            
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            string maLop = ((DataRowView)bdsLop[bdsLop.Position])["MALOP"].ToString();
            string khoaHoc = ((DataRowView)bdsLop[bdsLop.Position])["KHOAHOC"].ToString();
            string khoa = ((DataRowView)Program.bds_dspm[CmbKhoa.SelectedIndex])["TENKHOA"].ToString();
            ReportBDTK r = new ReportBDTK(maLop);
            r.LbLopKhoaHoc.Text = "LỚP: " + maLop.ToUpper() + "   " + "KHOÁ HỌC: " + khoaHoc.ToUpper();
            r.LbKhoa.Text = "KHOA: " + khoa.ToUpper();

            ReportPrintTool pt = new ReportPrintTool(r);
            pt.ShowPreviewDialog();
        }

        private void CmbMalop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}