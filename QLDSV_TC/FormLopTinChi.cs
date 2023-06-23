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
using System.Text.RegularExpressions;
using System.Collections;

namespace QLDSV_TC
{
    public partial class FormLopTinChi : DevExpress.XtraEditors.XtraForm
    {
        int vitri = 0;
        private string flagOption; 
        string macn = "";
        bool dangthemmoi = false;
        private String oldNIENKHOA = "";
        private int oldHOCKY;
        private String oldMAMH = "";
        private int oldNHOM;

        Stack undoList = new Stack();
        public FormLopTinChi()
        {
            InitializeComponent();
        }

        private void lOPTINCHIBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsLTC.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void FormLopTinChi_Load(object sender, EventArgs e)

        {
            dS.EnforceConstraints = false;//trong table ngoai khoa ngoai cua table con cac khoa ngoai khac vi vay dua ra quy uoc khong kiem tra khoa ngoai
            // TODO: This line of code loads data into the 'dS.LOPTINCHI' table. You can move, or remove it, as needed.
            this.lOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.lOPTINCHITableAdapter.Fill(this.dS.LOPTINCHI);
            // TODO: This line of code loads data into the 'dS.GIANGVIEN' table. You can move, or remove it, as needed.
            this.gIANGVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.gIANGVIENTableAdapter.Fill(this.dS.GIANGVIEN);
            // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.dS.MONHOC);
            // TODO: This line of code loads data into the 'dS.DANGKY' table. You can move, or remove it, as needed.
            this.dANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dANGKYTableAdapter.Fill(this.dS.DANGKY);



            var dataSource = Program.bds_dspm;
            var filteredDataSource = dataSource.Cast<DataRowView>()
                .Where(row => row["TENKHOA"].ToString() != "Học Phí")
                .ToList();
            cmbKHOA.DataSource = filteredDataSource;
            cmbKHOA.DisplayMember = "TENKHOA";
            cmbKHOA.ValueMember = "TENSERVER";
            cmbKHOA.SelectedIndex = Program.mKhoa;
            if (bdsLTC.Count > 0)
            {
                macn = ((DataRowView)bdsLTC[0])["MAKHOA"].ToString();//VAN TON TAI LOI CAN KHAC PHUC NEU K TIM DUOC LOI THI KHI DEMO THAY SE CHEN TH VAO, day la macn cua LTC dau tien '((DataRowView)bdsLTC[0])'=>du doan loi la khong co phan tu nao o chi nhanh hien tai
            }
            else
            {
                if (cmbKHOA.SelectedIndex == 0)
                {
                    macn = "CNTT";
                }
                if (cmbKHOA.SelectedIndex == 1)
                {
                    macn = "VT";
                }
            }
            cmbTENMH.DataSource = bdsMH;
            cmbTENMH.DisplayMember = "TENMH";
            cmbTENMH.ValueMember = "MAMH";

            loadcomboboxGV();

            txtMAMH.Text = cmbTENMH.ValueMember.ToString();
            txtMAGV.Text = cmbTENGV.ValueMember.ToString();
            if (Program.mGroup == "KHOA")
            {
                cmbKHOA.Enabled = false;

            }
            if (Program.mGroup == "PGV")
            {
                cmbKHOA.Enabled = true;
            }
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = btnGhi.Enabled = true;


        }

        void loadcomboboxGV()
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.SP_LayDSGV";
            dt = Program.ExecSqlDataTable(cmd);
            BindingSource bdsgv = new BindingSource();
            bdsgv.DataSource = dt;
            cmbTENGV.DataSource = bdsgv;
            cmbTENGV.DisplayMember = "HOTEN";
            cmbTENGV.ValueMember = "MAGV";

        }
        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsLTC.Position;
            groupBox2.Enabled = true;
            flagOption = "Add";
            dangthemmoi = true;
            bdsLTC.AddNew();
            txtMAKHOA.Text = macn;
            txtMAKHOA.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            gcLTC.Enabled = false;
        }

        private void txtMAMH_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                cmbTENMH.SelectedValue = txtMAMH.Text;
            }
            catch (Exception ex) { }
        }

        private void txtMAGV_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                cmbTENGV.SelectedValue = txtMAGV.Text;
            }
            catch (Exception ex) { }
        }

        private void cmbTENMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbTENMH.SelectedValue != null)
            {
                txtMAMH.Text = cmbTENMH.SelectedValue.ToString();
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

                    this.lOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                    this.lOPTINCHITableAdapter.Fill(this.dS.LOPTINCHI);
                    if (bdsLTC.Count > 0)
                    {
                        macn = ((DataRowView)bdsLTC[0])["MAKHOA"].ToString();//VAN TON TAI LOI CAN KHAC PHUC NEU K TIM DUOC LOI THI KHI DEMO THAY SE CHEN TH VAO, day la macn cua LTC dau tien '((DataRowView)bdsLTC[0])'=>du doan loi la khong co phan tu nao o chi nhanh hien tai
                    }
                    else
                    {
                        if (cmbKHOA.SelectedIndex == 0)
                        {
                            macn = "CNTT";
                        }
                        if (cmbKHOA.SelectedIndex == 1)
                        {
                            macn = "VT";
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể load dữ liệu từ sever đã chọn", "", MessageBoxButtons.OK);
                }

            }
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();

        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
               
                this.lOPTINCHITableAdapter.Fill(this.dS.LOPTINCHI);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Loi Reload:" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void cmbTENGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbTENGV.SelectedValue != null)
            {
                txtMAGV.Text = cmbTENGV.SelectedValue.ToString();
            }
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangthemmoi == true && this.btnThem.Enabled == false)
            {
                dangthemmoi = false;

                this.btnThem.Enabled = this.btnXoa.Enabled = this.btnGhi.Enabled = this.btnLamMoi.Enabled = this.btnThoat.Enabled = true;
                this.btnPhucHoi.Enabled = false;
                bdsLTC.CancelEdit();
                bdsLTC.RemoveCurrent();
                bdsLTC.Position = vitri;

                gcLTC.Enabled = true;
                groupBox2.Enabled = false;
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
            Console.WriteLine(queryUndo);
            int n = Program.ExecSqlNonQuery(queryUndo);
            this.lOPTINCHITableAdapter.Fill(this.dS.LOPTINCHI);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string maLTC = "";
            if(bdsDK.Count > 0)
            {
                MessageBox.Show("Khong the xoa Lop Tin Chi nay vi da duoc dang ky", "", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa LTC này không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                   
                    bdsLTC.RemoveCurrent();
                    this.lOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                    this.lOPTINCHITableAdapter.Update(this.dS.LOPTINCHI);
                    MessageBox.Show("Xoa thành công", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi xoa mon hoc. Ban hay xoa lai \n" + ex.Message, "", MessageBoxButtons.OK);
                    this.lOPTINCHITableAdapter.Fill(this.dS.LOPTINCHI);
                    bdsLTC.Position = bdsLTC.Find("maLTC", maLTC);
                }

            }
            if (bdsLTC.Count == 0) btnXoa.Enabled = false;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsLTC.Position;
            groupBox2.Enabled = true;
            flagOption = "Update";
            oldNIENKHOA = txtNIENKHOA.Text.Trim();
            oldHOCKY = (int)HOCKY.Value;
            oldMAMH = txtMAMH.Text.Trim();
            oldNHOM = (int)NHOM.Value;
            txtMAKHOA.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = btnHuy.Enabled= true;
            gcLTC.Enabled = false;
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupBox2.Enabled = false;
            gcLTC.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnGhi.Enabled= btnThoat.Enabled = true;
        }

        public static bool CheckSpecialCharacters(string inputString)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9\s]");
            return regex.IsMatch(inputString);
        }
        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnPhucHoi.Enabled = true;
            if(HOCKY.Value < 1)
            {
                MessageBox.Show("Học kì không được thiếu va Phai Lon Hon va bang 1!", "", MessageBoxButtons.OK);
                HOCKY.Focus();
                return;
            }
            if (HOCKY.Value > 4 )
            {
                MessageBox.Show("Học kì phai nho hon hoac bang 4", "", MessageBoxButtons.OK);
                HOCKY.Focus();
                return;
            }
            if (SL_SVMIN.Value <= 0)
            {
                MessageBox.Show("Số sinh viên tối thiểu không được thiếu Va Phai Lon Hon !", "", MessageBoxButtons.OK);
                SL_SVMIN.Focus();
                return;
            }
            if(NHOM.Value < 1)
            {
                MessageBox.Show("Nhóm không được thiếu va Phai lon hon hoac bang 1!", "", MessageBoxButtons.OK);
                NHOM.Focus();
                return;
            }
            if(txtMAGV.Text.Trim()== "")
            {
                MessageBox.Show("Mã giảng viên không được thiếu", "", MessageBoxButtons.OK);
                txtMAGV.Focus();
                return;
            }
            if (Regex.IsMatch(txtMAGV.Text.Trim(), @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("Ma giang vien chỉ có chữ cái và số", "Thông báo", MessageBoxButtons.OK);
                txtMAGV.Focus();
                return;
            }
            if (txtMAKHOA.Text.Trim() == "")
            {
                MessageBox.Show("Niên khóa không được thiếu!", "", MessageBoxButtons.OK);
                txtMAKHOA.Focus();
                return;
            }
            if (txtMAMH.Text.Trim() == "")
            {
                MessageBox.Show("Mã môn học không được thiếu!", "", MessageBoxButtons.OK);
                txtMAMH.Focus();
                return;
            }
            if (Regex.IsMatch(txtMAMH.Text.Trim(), @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("Ma Mon Hoc Chi co chu Cai va so", "Thông báo", MessageBoxButtons.OK);
                txtMAMH.Focus();
                return;
            }
            if (txtNIENKHOA.Text.Trim() == "")
            {
                MessageBox.Show("Niên khóa không được thiếu!", "", MessageBoxButtons.OK);
                txtNIENKHOA.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNIENKHOA.Text))
            {
                MessageBox.Show("Niên khóa không được thiếu!", "", MessageBoxButtons.OK);
                txtNIENKHOA.Focus();
                return;
            }
           
            /*Bat Rang Buoc Nien Khoa*/
            string[] nienKhoaParts = txtNIENKHOA.Text.Split('-');
            if (nienKhoaParts.Length == 2)
            {
                nienKhoaParts[0] = nienKhoaParts[0].Trim();
                nienKhoaParts[1] = nienKhoaParts[1].TrimEnd();
            }
            if (nienKhoaParts.Length != 2 || nienKhoaParts[0].Length != 4 || nienKhoaParts[1].Length != 4)
            {
            
                MessageBox.Show("Niên khóa phải có định dạng 'yyyy-yyyy'!", "", MessageBoxButtons.OK);
                txtNIENKHOA.Focus();
                return;
            }

            int startYear, endYear;
            if (!int.TryParse(nienKhoaParts[0], out startYear) || !int.TryParse(nienKhoaParts[1], out endYear))
            {
                MessageBox.Show("Niên khóa không hợp lệ!", "", MessageBoxButtons.OK);
                txtNIENKHOA.Focus();
                return;
            }

            if (endYear - startYear != 1)
            {
                MessageBox.Show("Niên khóa không hợp lệ!", "", MessageBoxButtons.OK);
                txtNIENKHOA.Focus();
                return;
            }

            string queryCheckMaMH_MaGV = "Declare @return_value int \n" + $"EXEC @return_value = [dbo].[checkExistsGV_MH] " +
                 $"@MAGV = N'{txtMAGV.Text}', " + $"@TENGV = N'{cmbTENGV.Text}', " +
                 $"@MAMH = N'{txtMAMH.Text}' ," + $"@TENMH = N'{cmbTENMH.Text}' " +
                   "SELECT 'Return Value' = @return_value";
            int resultcheckMAMH_MAGV = Program.CheckDataHelper(queryCheckMaMH_MaGV);
            if(resultcheckMAMH_MAGV == 0)
            {
                MessageBox.Show("Mon hoc va Giang Vien Khong Hop Le!", "", MessageBoxButtons.OK);
                return;
            }
            if(resultcheckMAMH_MAGV == 2)
            {
                MessageBox.Show("Mon Hoc Khong Hop Le!", "", MessageBoxButtons.OK);             
                return;
            }
            if (resultcheckMAMH_MAGV == 3)
            {
                MessageBox.Show("Giang Vien Khong Hop Le!", "", MessageBoxButtons.OK);
                return;
            }
            if (flagOption == "Add")
              {
                  if(HUYLOP.Checked == true)
                  {
                      MessageBox.Show("Không thể thêm lớp khi thuộc tính hủy lớp được chọn!", "", MessageBoxButtons.OK);
                      HUYLOP.Focus();
                      return;
                  }
                string queryThem = "Declare @return_value int \n" + $"EXEC @return_value = [dbo].[CheckExistLTC] " +
                  $"@NIENKHOA = N'{txtNIENKHOA.Text}', " +
                  $"@HOCKY = '{HOCKY.Value}', " +
                  $"@MAMH = N'{txtMAMH.Text}', " +
                  $"@NHOM = '{NHOM.Value}'" + "SELECT 'Return Value' = @return_value";
                int resultMa = Program.CheckDataHelper(queryThem);
                if (resultMa == 1)
                {
                    MessageBox.Show("Nien Khoa, Hoc Ky, MaMH, Nhom La khoa duy nhat!", "", MessageBoxButtons.OK);
                    return;
                }

            }
              if(flagOption == "Update")
            {
                if (!string.Equals(this.txtNIENKHOA.Text.Trim(), oldNIENKHOA, StringComparison.OrdinalIgnoreCase) ||!int.Equals(Convert.ToInt32(HOCKY.Value), Convert.ToInt32(oldHOCKY)) || !string.Equals(this.txtMAMH.Text.Trim(), oldMAMH, StringComparison.OrdinalIgnoreCase) || !int.Equals(Convert.ToInt32(NHOM.Value), Convert.ToInt32(oldNHOM)))

                {
                    string queryThem = "Declare @return_value int \n" + $"EXEC @return_value = [dbo].[CheckExistLTC] " +
                 $"@NIENKHOA = N'{txtNIENKHOA.Text}', " +
                 $"@HOCKY = '{HOCKY.Value}', " +
                 $"@MAMH = N'{txtMAMH.Text}', " +
                 $"@NHOM = '{NHOM.Value}'" + "SELECT 'Return Value' = @return_value";
                    int resultMa = Program.CheckDataHelper(queryThem);
                    if (resultMa == 1)
                    {
                        MessageBox.Show("Nien Khoa, Hoc Ky, MaMH, Nhom La khoa duy nhat!", "", MessageBoxButtons.OK);
                        return;
                    }

                }
            }

            //Buoc 1
            //Lay Du Lieu Tu Form De phuc vu cho qua trinh hoan toan
            DataRowView drv = ((DataRowView)bdsLTC[bdsLTC.Position]);
            //string maLTC = drv["MALTC"].ToString();
            string nienKhoa = drv["NIENKHOA"].ToString();
            int hocKy = (int)drv["HOCKY"];
            string maMH = drv["MAMH"].ToString();
            int nhom = (int)drv["NHOM"];
            string maGV = drv["MAGV"].ToString();
            string maKhoa = drv["MAKHOA"].ToString();
            int soSVMIN = (int)drv["SOSVTOITHIEU"];
            string trangThai = drv["HUYLOP"].ToString();


            if (MessageBox.Show("Bạn có chắc chắn muốn thực hiện thao tác này không ?", "Thông báo",
                    MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                   
                    bdsLTC.EndEdit();
                    bdsLTC.ResetCurrentItem();
                    this.lOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                    this.lOPTINCHITableAdapter.Update(this.dS.LOPTINCHI);
                    string queryUndo = "";
                   string maLTC = ((DataRowView)bdsLTC[bdsLTC.Position])["MALTC"].ToString();
                    if (dangthemmoi == true)
                    {
                        queryUndo = "" +
                                "DELETE DBO.LOPTINCHI " +
                                "WHERE MALTC = " + maLTC;
                    }
                    else
                    {
                        queryUndo = "UPDATE DBO.LOPTINCHI \n" + "SET " +
                                    "NIENKHOA =N'" + nienKhoa + "'," +
                                    "HOCKY =" + hocKy + ", " +
                                    "MAMH =N'" + maMH + "'," +
                                    "NHOM ='" + nhom + "'," +
                                    "MAGV =N'" + maGV + "'," +
                                    "MAKHOA =N'" + maKhoa + "'," +
                                    "SOSVTOITHIEU ='" + soSVMIN + "'," +
                                    "HUYLOP ='" + trangThai + "'" +
                                    "WHERE MALTC = '" + maLTC + "'";
                    }

                    undoList.Push(queryUndo);
                    dangthemmoi = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi ghi lớp tín chỉ: " + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
                MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
            }

      
            gcLTC.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = btnThoat.Enabled = btnPhucHoi.Enabled = true;
            btnGhi.Enabled  = false;
            groupBox2.Enabled = false;
        }

        private void gcLTC_Click(object sender, EventArgs e)
        {

        }

        private void txtMAKHOA_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mAMHLabel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void HUYLOP_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
 }
