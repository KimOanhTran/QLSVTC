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
using System.Collections;
using System.Text.RegularExpressions;

namespace QLDSV_TC
{
    public partial class FormMonHoc : DevExpress.XtraEditors.XtraForm
    {
        private int vitri = 0;
        private string flagOption;
        private string oldMaMonHoc = "";
        private string oldTenMonHoc = "";
        bool dangthemmoi = false;
        Stack undoList = new Stack();

        public FormMonHoc()
        {
            InitializeComponent();
        }

        private void mONHOCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsMH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void FrmSubject_Load(object sender, EventArgs e)
        {
            dS.EnforceConstraints = false;
            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.dS.MONHOC);

            this.lOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTINCHITableAdapter.Fill(this.dS.LOPTINCHI);

            btnGhi.Enabled = groupBox1.Enabled = false;


        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsMH.Position;
            groupBox1.Enabled = true;
            flagOption = "Add";
            dangthemmoi = true;
            txtMaMH.Enabled = true;
            bdsMH.AddNew();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;

            btnGhi.Enabled = groupBox1.Enabled  = btnLamMoi.Enabled = btnPhucHoi.Enabled = true;
            gcMonHoc.Enabled = false;
            
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangthemmoi == true && this.btnThem.Enabled == false)
            {
                dangthemmoi = false;

                this.btnThem.Enabled = this.btnXoa.Enabled = this.btnGhi.Enabled = this.btnLamMoi.Enabled = this.btnThoat.Enabled = btnSua.Enabled = btnHuy.Enabled = true;
                this.btnPhucHoi.Enabled = false;
                bdsMH.CancelEdit();
                bdsMH.RemoveCurrent();
                bdsMH.Position = vitri;

                gcMonHoc.Enabled = true;
                groupBox1.Enabled = false;
                return;


            }
            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnPhucHoi.Enabled = false;
                return;
            }
            bdsMH.CancelEdit();
            String queryUndo = undoList.Pop().ToString();
            int n = Program.ExecSqlNonQuery(queryUndo);
            this.MONHOCTableAdapter.Fill(this.dS.MONHOC);
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
      
            vitri = bdsMH.Position;
            groupBox1.Enabled = true;
            flagOption = "Update";
            dangthemmoi = false;
            txtMaMH.Enabled = false;
            oldMaMonHoc = txtMaMH.Text.Trim();
            oldTenMonHoc = txtTenMH.Text.Trim();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = btnLamMoi.Enabled= groupBox1.Enabled = true;
            gcMonHoc.Enabled = false;
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.MONHOCTableAdapter.Fill(this.dS.MONHOC);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Loi Reload:" + ex.Message, "", MessageBoxButtons.OK);
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            if(bdsLTC.Count > 0)
            {
                MessageBox.Show("Khong the xoa Mon Hoc nay vi da duoc ton tai Lop Tin Chi", "", MessageBoxButtons.OK);
                return;
            }
            //Ch Kiem Tra xem Mon Hoc co dang duoc them LTC o site khac khong
            string maMH = txtMaMH.Text.Trim();
            string queryCheckMonHocChiNhanhKhac = "DECLARE @return_value int; " +
          "EXEC @return_value =[dbo].[CheckExist_MonHoc_ChiNhanhKhac]  " +
          "@MAMH = N'" + maMH + "'; " +
          "SELECT 'Return Value' = @return_value;";

            int resultMa = Program.CheckDataHelper(queryCheckMonHocChiNhanhKhac);
            if (resultMa == 2)
            {
                MessageBox.Show("Khong the xoa Mon Hoc nay vi da duoc ton tai Lop Tin Chi", "", MessageBoxButtons.OK);
                return;
            }
            string queryUndo = 
                                "INSERT INTO DBO.MONHOC( MAMH,TENMH,SOTIET_LT,SOTIET_TH) " +
                                 " VALUES( '" + txtMaMH.Text + "',N'" +
                                                txtTenMH.Text + "','" +
                                                ST_LT.Value + "', " +
                                                ST_TH.Value + " ) ";
            undoList.Push(queryUndo);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa MH này không ?", "Thông báo",
             MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    maMH = ((DataRowView)bdsMH[bdsMH.Position])["MAMH"].ToString();
                    bdsMH.RemoveCurrent();
                    this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.MONHOCTableAdapter.Update(this.dS.MONHOC);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Loi xoa mon hoc. Ban hay xoa lai \n" + ex.Message, "", MessageBoxButtons.OK);
                    this.MONHOCTableAdapter.Fill(this.dS.MONHOC);
                    bdsMH.Position = bdsMH.Find("MAMH", maMH);
                }

            }
            if (bdsMH.Count == 0) btnXoa.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(txtMaMH.Text.Trim() == "")
            {
                MessageBox.Show("Ma Mon hoc khong duoc thieu", "", MessageBoxButtons.OK);
                txtMaMH.Focus();
                return;
            }
            if (Regex.IsMatch(txtMaMH.Text.Trim(), @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("Mã mon hoc chỉ có chữ cái và số", "Thông báo", MessageBoxButtons.OK);
                txtMaMH.Focus();
                return ;
            }
            if (txtTenMH.Text.Trim() == "")
            {
                MessageBox.Show("Ten mon hoc khong duoc trong", "", MessageBoxButtons.OK);
                txtTenMH.Focus();
                return;
            }
            if (Regex.IsMatch(txtTenMH.Text.Trim(), @"^[\p{L}0-9\s]+$") == false)
            {
                MessageBox.Show("Ten mon hoc chỉ có chữ cái và số", "Thông báo", MessageBoxButtons.OK);
                txtTenMH.Focus();
                return;
            }
            if ((ST_LT.Value + ST_TH.Value) % 15 != 0)
            {
                MessageBox.Show("Tổng số tiết lý thuyết và thực hành phải là bội của 15! (Một chỉ 15 tiết)", "", MessageBoxButtons.OK);
                ST_LT.Focus();
                return;
            }

            
            if (flagOption == "Update")
            {

                if (!string.Equals(this.txtTenMH.Text.Trim(), oldTenMonHoc, StringComparison.OrdinalIgnoreCase))
                {
                    string query1 = "DECLARE  @return_value int \n"
                                + "EXEC @return_value = SP_CHECKMONHOC_ID_NAME \n"
                                + "@MAMH = '',@TENMH = N'" + txtTenMH.Text + "' \n"
                                + "SELECT 'Return Value' = @return_value";

                    int resultMa = Program.CheckDataHelper(query1);
                    if (resultMa == 3)
                    {
                        MessageBox.Show("Ten môn học đã tồn tại !", "", MessageBoxButtons.OK);
                        txtMaMH.Focus();
                        return;
                    }
                }
            }

           

            if(flagOption == "Add")
            {
                string query1 = "DECLARE  @return_value int \n"
                            + "EXEC @return_value = SP_CHECKMONHOC_ID_NAME \n"
                            + "@MAMH = N'" + txtMaMH.Text + "',@TENMH = N'" + txtTenMH.Text + "' \n"
                            + "SELECT 'Return Value' = @return_value";

                int resultMa = Program.CheckDataHelper(query1);
                if (resultMa == 1)
                  {
                     MessageBox.Show("Mã môn học đã tồn tại va ten mon hoc da ton tai!", "", MessageBoxButtons.OK);
                     txtMaMH.Focus();
                return;
                    }
            if (resultMa == 2)
            {
                MessageBox.Show("Mã môn học đã tồn tại !", "", MessageBoxButtons.OK);
                txtMaMH.Focus();
                return;
            }
            if (resultMa == 3)
            {
                MessageBox.Show("Ten môn học đã tồn tại !", "", MessageBoxButtons.OK);
                txtMaMH.Focus();
                return;
            }
            }

            //buoc 1: Lay DL
            String maMH = txtMaMH.Text.Trim();
            DataRowView datarv = ((DataRowView)bdsMH[bdsMH.Position]);
            String tenMH = datarv["TENMH"].ToString();
            String maMHOld = datarv["MAMH"].ToString();
            String soTietLT = datarv["SOTIET_LT"].ToString();
            String soTietTH = datarv["SOTIET_TH"].ToString();

            if (MessageBox.Show("Bạn có chắc chắn muốn thực hiện thao tác này không ?", "Thông báo",
                    MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    String queryUndo = "";
                    if (dangthemmoi == true)
                    {
                        queryUndo = "" + "DELETE DBO.MONHOC \n" + "WHERE MAMH ='" + maMH + "'";
                    }
                    else
                    {
                        queryUndo = "UPDATE DBO.MONHOC \n" + "SET " +
                                    "TENMH =N'" + tenMH + "'," +
                                    "SOTIET_LT ='" + soTietLT + "'," +
                                    "SOTIET_TH =" + soTietTH + " " +
                                    "WHERE MAMH = '" + maMH + "'";
                    }
                    undoList.Push(queryUndo);
                    bdsMH.EndEdit();
                    bdsMH.ResetCurrentItem();
                    this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.MONHOCTableAdapter.Update(this.dS.MONHOC);
                    dangthemmoi = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi Ghi mon hoc", "", MessageBoxButtons.OK);
                    return;
                }
            }
             
            gcMonHoc.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = btnThoat.Enabled = btnPhucHoi.Enabled = true;
            btnGhi.Enabled= true;
            groupBox1.Enabled = false;
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupBox1.Enabled = false;
            gcMonHoc.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnGhi.Enabled= btnThoat.Enabled = true;
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
            // Application.Exit();
        }



        private void gcMonHoc_Click(object sender, EventArgs e)
        {

        }

        private void fillToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.sP_CHECKMONHOC_ID_NAMETableAdapter.Fill(this.dS.SP_CHECKMONHOC_ID_NAME, txtMaMH.Text, txtTenMH.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}