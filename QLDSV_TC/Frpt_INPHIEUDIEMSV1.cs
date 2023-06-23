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
    public partial class Frpt_INPHIEUDIEMSV1 : DevExpress.XtraEditors.XtraForm
    {
        public Frpt_INPHIEUDIEMSV1()
        {
            InitializeComponent();
        }

        private void Frpt_INPHIEUDIEMSV1_Load(object sender, EventArgs e)
        {
            if (Program.mGroup.Equals("SV"))
            {
                txtMASV.Enabled = false;
            }
        }

        private void btnIN_Click(object sender, EventArgs e)
        {

            string maSV = txtMASV.Text;
           
            if (maSV == "" && !Program.mGroup.Equals("SV"))
            {
                MessageBox.Show("Mã sinh vien không được thiếu", "", MessageBoxButtons.OK);
                txtMASV.Focus();
                return;
            }
            string query = "DECLARE @return_value int \n" +
               $"EXEC @return_value = [dbo].[SP_CHECKSV] " +
               $"@MASV = N'{maSV}'" +
               "SELECT 'Return Value' = @return_value";
            if (!Program.mGroup.Equals("SV"))
            {
                int resultCheckSV = Program.CheckDataHelper(query);
                if (resultCheckSV == 0)
                {
                    MessageBox.Show("Ma Sinh Vien khong hop le!", "", MessageBoxButtons.OK);
                    txtMASV.Focus();
                    return;
                }
                if (resultCheckSV == 2 && Program.mGroup.Equals("KHOA"))
                {
                    MessageBox.Show("Ma Sinh Vien ton tai o chi nhanh khac!", "", MessageBoxButtons.OK);
                    txtMASV.Focus();
                    return;
                }
            }       

            int type = 2;
            if (Program.mGroup.Equals("SV"))
            {
                txtMASV.Enabled = false;
                maSV = Program.username;
                type = 1;
  
            }

            if (Program.mGroup.Equals("KHOA"))
            {
                type = 1;
            }
            if (Program.mGroup.Equals("PGV"))
            {
                type = 0;
            }
            Xrpt_INPHIEUDIEMSV2 rpt1 = new Xrpt_INPHIEUDIEMSV2(maSV, type);
           rpt1.txtMASV.Text = maSV;
            ReportPrintTool print = new ReportPrintTool(rpt1);
            print.ShowPreviewDialog();
        }

        private void btnTHOAT_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}