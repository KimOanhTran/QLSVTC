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
    public partial class XFormNhapDiem : DevExpress.XtraEditors.XtraForm
    {
        private BindingSource bdsDSLopTC = new BindingSource();
        private BindingSource bdsDSSVDangKy = new BindingSource();
        private Stack<DataTable> undoStack = new Stack<DataTable>();
        private Stack<DataTable> redoStack = new Stack<DataTable>();
        private string seletedMaLTC;
        public XFormNhapDiem()
        {
            InitializeComponent();
        }

        private void XFormNhapDiem_Load(object sender, EventArgs e)
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

            LayDSNienKhoa();
            CmbNienKhoa.SelectedIndex = 0;
            LayDSHocKy(CmbNienKhoa.SelectedValue.ToString());
            repositoryItemSpinEdit1.MinValue = 0;
            repositoryItemSpinEdit1.MaxValue = 10;
            gridView1.CellValueChanged += gridControlDSSVDK_CellValueChanged;
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

        void LayDSLopTC()
        {
            string cmd = "EXEC [dbo].[SP_LayDSLopTinChi] '" + CmbNienKhoa.Text + "', '" + CmbHocKy.Text + "'";
            DataTable tableLopTC = Program.ExecSqlDataTable(cmd);
            bdsDSLopTC.DataSource = tableLopTC;
            gridControlDSLTC.DataSource = bdsDSLopTC;
        }

        private void gridControlDSSVDK_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var modifiedTable = (DataTable)bdsDSSVDangKy.DataSource;

            var currentTable = modifiedTable.Copy();
            undoStack.Push(currentTable);
            BtnUndo.Enabled = true;


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
            }
            else
            {
                LayDSNienKhoa();
                CmbNienKhoa.SelectedIndex = 0;
            }
        }

        private void CmbNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayDSHocKy(CmbNienKhoa.SelectedValue.ToString());

        }

        private void CmbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnTaiLTC_Click(object sender, EventArgs e)
        {
            LayDSLopTC();
        }

        private void gridControlDSLTC_Click(object sender, EventArgs e)
        {
            if (bdsDSLopTC.Count > 0)
            {
                redoStack.Clear();
                undoStack.Clear();
                BtnRedo.Enabled = false;
                BtnUndo.Enabled = false;

                seletedMaLTC = ((DataRowView)bdsDSLopTC[bdsDSLopTC.Position])["MALTC"].ToString();

                lbLop.Text = seletedMaLTC + " - " + ((DataRowView)bdsDSLopTC[bdsDSLopTC.Position])["TENMH"].ToString();
                panelLop.Visible = true;
                string cmd = "EXEC [dbo].[SP_LayDSSVDangky] " + seletedMaLTC;
                DataTable tableDSSVDangKy = Program.ExecSqlDataTable(cmd);
                bdsDSSVDangKy.DataSource = tableDSSVDangKy;
                gridControlDSSVDK.DataSource = tableDSSVDangKy;
            }
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                // Push the current table to the redo stack
                var currentTable = ((DataTable)bdsDSSVDangKy.DataSource).Copy();
                redoStack.Push(currentTable);

                // Pop the previous table from the undo stack
                var previousTable = undoStack.Pop();

                // Update the binding source and grid control with the previous table
                bdsDSSVDangKy.DataSource = previousTable;
                gridControlDSSVDK.DataSource = previousTable;

                if (undoStack.Count <= 0) BtnUndo.Enabled = false;
                if (redoStack.Count > 0) BtnRedo.Enabled = true;
            }

        }

        private void BtnRedo_Click(object sender, EventArgs e)
        {

            if (redoStack.Count > 0)
            {
                // Push the current table to the undo stack
                var currentTable = ((DataTable)bdsDSSVDangKy.DataSource).Copy();
                undoStack.Push(currentTable);

                // Pop the next table from the redo stack
                var nextTable = redoStack.Pop();

                // Update the binding source and grid control with the next table
                bdsDSSVDangKy.DataSource = nextTable;
                gridControlDSSVDK.DataSource = nextTable;

                if (redoStack.Count <= 0) BtnRedo.Enabled = false;
                if (undoStack.Count > 0) BtnUndo.Enabled = true;
            }

        }

        private void BtnGhi_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("MALTC", typeof(int));
                dt.Columns.Add("MASV", typeof(string));
                dt.Columns.Add("DIEM_CC", typeof(int));
                dt.Columns.Add("DIEM_GK", typeof(float));
                dt.Columns.Add("DIEM_CK", typeof(float));
                int intMaLTC = int.Parse(seletedMaLTC);
                for (int i = 0; i < bdsDSSVDangKy.Count; i++)
                {
                    string maSV = ((DataRowView)bdsDSSVDangKy[i])["MASV"].ToString();
                    int? diemCC;
                    float? diemGK;
                    float? diemCK;

                    // Check if "DIEM_CC" is an empty string, then add null, otherwise parse the value
                    diemCC = (((DataRowView)bdsDSSVDangKy[i])["DIEM_CC"].ToString() == "") ? (int?)null : int.Parse(((DataRowView)bdsDSSVDangKy[i])["DIEM_CC"].ToString());

                    // Check if "DIEM_GK" is an empty string, then add null, otherwise parse the value
                    diemGK = (((DataRowView)bdsDSSVDangKy[i])["DIEM_GK"].ToString() == "") ? (float?)null : float.Parse(((DataRowView)bdsDSSVDangKy[i])["DIEM_GK"].ToString());

                    // Check if "DIEM_CK" is an empty string, then add null, otherwise parse the value
                    diemCK = (((DataRowView)bdsDSSVDangKy[i])["DIEM_CK"].ToString() == "") ? (float?)null : float.Parse(((DataRowView)bdsDSSVDangKy[i])["DIEM_CK"].ToString());

                    dt.Rows.Add(intMaLTC, maSV, diemCC, diemGK, diemCK);

                }
                SqlParameter para = new SqlParameter();
                para.SqlDbType = SqlDbType.Structured;
                para.TypeName = "dbo.Type_DANGKY";
                para.ParameterName = "@DIEMTHI";
                para.Value = dt;

                Program.KetNoi();
                SqlCommand sqlcmd = new SqlCommand("SP_UpdateDiem", Program.conn);
                sqlcmd.Parameters.Clear();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add(para);
                sqlcmd.ExecuteNonQuery();
                MessageBox.Show("Lưu thành công");
            }
            catch
            {
                MessageBox.Show("Lưu thất bại");
            }
        }


    }
}