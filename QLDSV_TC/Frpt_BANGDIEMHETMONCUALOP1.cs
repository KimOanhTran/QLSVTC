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

namespace QLDSV_TC
{
    public partial class Frpt_BANGDIEMHETMONCUALOP1 : DevExpress.XtraEditors.XtraForm
    {
        string maMH;
        public Frpt_BANGDIEMHETMONCUALOP1()
        {
            InitializeComponent();
        }

        private void Frpt_BANGDIEMHETMONCUALOP1_Load(object sender, EventArgs e)
        {
            dS.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'dS.NIENKHOA' table. You can move, or remove it, as needed.
            this.nIENKHOATableAdapter.Connection.ConnectionString = Program.connstr_publicsher;
            this.nIENKHOATableAdapter.Fill(this.dS.NIENKHOA);
            // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
            //dS.EnforceConstraints = false;

            var dataSource = Program.bds_dspm;

            var filteredDataSource = dataSource.Cast<DataRowView>()
                .Where(row => row["TENKHOA"].ToString() != "Học Phí")
                .ToList();
            cmbKHOA.DataSource = filteredDataSource;
            cmbKHOA.DisplayMember = "TENKHOA";
            cmbKHOA.ValueMember = "TENSERVER";
            cmbKHOA.SelectedIndex = Program.mKhoa;
            cmbTENMH.DataSource = this.dS.MONHOC;
            cmbTENMH.DisplayMember = "TENMH";
           
            if (Program.mGroup == "KHOA")
            {
                cmbKHOA.Enabled = false;

            }
            if (Program.mGroup == "PGV")
            {
                cmbKHOA.Enabled = true;
            }
        }

        private void cmbTENMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                maMH = cmbTENMH.SelectedValue.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbKHOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKHOA.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cmbKHOA.SelectedValue.ToString();
            if (cmbKHOA.SelectedIndex != Program.mKhoa)
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
            }
            else
            {
                try
                {
                    this.hOCKYTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.hOCKYTableAdapter.Fill(this.dS.HOCKY);

                    this.nHOMTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.nHOMTableAdapter.Fill(this.dS.NHOM);

                    this.nIENKHOATableAdapter.Connection.ConnectionString = Program.connstr;
                    this.nIENKHOATableAdapter.Fill(this.dS.NIENKHOA);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể load dữ liệu từ sever đã chọn", "", MessageBoxButtons.OK);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nienkhoa = cmbNIENKHOA.SelectedValue.ToString();
            int hocky = int.Parse(cmbHOCKY.SelectedValue.ToString());
            int nhom = int.Parse(cmbNHOM.SelectedValue.ToString());

            Xrpt_BANGDIEMHETMONLTC rpt = new Xrpt_BANGDIEMHETMONLTC(nienkhoa, hocky, nhom, maMH);

            /*      rpt.lblTieuDe.Text = “DANH SÁCH PHIẾU “ +cmbLoai.Text.ToUpper() + “ NHÂN VIÊN LẬP TRONG NĂM “ +cmbNam.Text;
                  rpt.lblHoTen.Text = cmbHoten.Text;*/

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            string maMH1 = maMH;
            string nienkhoa = cmbNIENKHOA.Text;
            int hocky = int.Parse(cmbHOCKY.Text);
            int nhom = int.Parse(cmbNHOM.Text);

            Xrpt_BANGDIEMHETMONLTC rpt = new Xrpt_BANGDIEMHETMONLTC(nienkhoa, hocky, nhom, maMH1);



            rpt.lblTIEUDE.Text = "BANG DIEM HET MON " + (cmbKHOA.Text).ToUpper();
            rpt.txtNienKhoa.Text = cmbNIENKHOA.Text;
            rpt.txtHOCKY.Text = cmbHOCKY.Text;
            rpt.txtNHOM.Text = cmbNHOM.Text;
            rpt.txtMONHOC.Text = cmbTENMH.Text;
            ReportPrintTool print = new ReportPrintTool(rpt);
           print.ShowPreviewDialog();
        }

        private void cmbTENMH_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                cmbTENMH.DataSource = this.dS.MONHOC;
                cmbTENMH.DisplayMember = "TENMH";
                cmbTENMH.ValueMember = "MAMH";

                maMH = cmbTENMH.SelectedValue.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}