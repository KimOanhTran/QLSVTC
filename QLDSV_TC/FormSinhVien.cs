using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC
{
  
    public partial class FormSinhVien : Form
    {
        int vitri = 0;
        string maLop = "";
        string maSinhVienSua = "";
        Boolean themSV = true;
        public FormSinhVien()
        {
            InitializeComponent();
        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.BdsLH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS2);

        }

        private void FormSinhVien_Load(object sender, EventArgs e)
        {
         
            DS2.EnforceConstraints = false;
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
            // TODO: This line of code loads data into the 'dS2.LOP' table. You can move, or remove it, as needed.
            this.lOPTableAdapter.Fill(this.DS2.LOP);
            this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            // TODO: This line of code loads data into the 'dS2.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.DS2.SINHVIEN);

            // TODO: This line of code loads data into the 'DS2.DANGKY' table. You can move, or remove it, as needed.
            this.dANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.dANGKYTableAdapter.Fill(this.DS2.DANGKY);

            var dataSource = Program.bds_dspm;
            var filteredDataSource = dataSource.Cast<DataRowView>()
                .Where(row => row["TENKHOA"].ToString() != "Học Phí")
                .ToList();
            CmbKhoa.DataSource = filteredDataSource;
            CmbKhoa.DisplayMember = "TENKHOA";
            CmbKhoa.ValueMember = "TENSERVER";
            CmbKhoa.SelectedIndex = Program.mKhoa;
            panelControl2.Enabled = false;
            if (Program.mGroup == "PGV")
            {
                CmbKhoa.Enabled = true;
            }
            else
            {
                CmbKhoa.Enabled = false;
            }

            BtnGhi.Enabled = false;
            nGAYSINHDateEdit.Properties.MaxValue = DateTime.Today;
        }

        private void lOPBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.BdsLH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS2);

        }

        private bool validatorSinhVien()
        {
            if (TxtMaSV.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                TxtMaSV.Focus();
                return false;
            }
            if (TxtHo.Text.Trim() == "")
            {
                MessageBox.Show("Họ không được thiếu!", "", MessageBoxButtons.OK);
                TxtHo.Focus();
                return false;
            }
            if (TxtTenSV.Text.Trim() == "")
            {
                MessageBox.Show("Tên không được thiếu!", "", MessageBoxButtons.OK);
                TxtTenSV.Focus();
                return false;
            }

            if (TxtMaSV.Text.Trim().Length  >  10)
            {
                MessageBox.Show("Mã sinh viên tối đa 10 kí tự!", "", MessageBoxButtons.OK);
                TxtMaSV.Focus();
                return false;
            }

            if (TxtHo.Text.Trim().Length > 50)
            {
                MessageBox.Show("Họ sinh viên tối đa 50 kí tự!", "", MessageBoxButtons.OK);
                TxtHo.Focus();
                return false;
            }

            if (TxtTenSV.Text.Trim().Length > 10)
            {
                MessageBox.Show("Tên sinh viên tối đa 50 kí tự!", "", MessageBoxButtons.OK);
                TxtTenSV.Focus();
                return false;
            }

            bool match = Regex.IsMatch(TxtMaSV.Text.Trim().ToUpper(), "[NB][0-9][0-9][A-Z][A-Z][A-Z][A-Z][0-9][0-9][0-9]");
            if (!match || TxtMaSV.Text.Trim().Length != 10)
            {
                MessageBox.Show("Mã sinh viên bạn nhập không đúng định dạng \n Ví dụ: N19DCCN001", "", MessageBoxButtons.OK);
                TxtMaSV.Focus();
                return false;
            }

            bool matchHo = Regex.IsMatch(TxtHo.Text.Trim(), "^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹếẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ ]+$");
            if (!matchHo)
            {
                MessageBox.Show("Họ không được có số và kí tự đặc biệt", "", MessageBoxButtons.OK);
                TxtHo.Focus();
                return false;
            }

            bool matchTen = Regex.IsMatch(TxtTenSV.Text.Trim(), "^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹếẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]+$");
            if (!matchTen || TxtTenSV.Text.Trim().Contains(" "))
            {
                MessageBox.Show("Tên không được có số, kí tự đặc biệt và khoảng trắng", "", MessageBoxButtons.OK);
                TxtTenSV.Focus();
                return false;
            }

            if (((TxtMaSV.Text.Trim() != maSinhVienSua.Trim()) && (themSV == false)) || themSV == true)
            {
                string query1 = " DECLARE @result INT " +

                           " EXEC @result= KT_MASV " +

                           TxtMaSV.Text.Trim() +

                           " SELECT  @result AS Result";

                int result = Program.CheckDataHelper(query1);
                if (result == -1)
                {
                    MessageBox.Show("Lỗi kết nối với database. Mời bạn xem lại", "", MessageBoxButtons.OK);
                    this.Close();
                }
                if (result == 1)
                {
                    MessageBox.Show("Mã Sinh Viên đã tồn tại. Mời bạn nhập mã khác !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (result == 2)
                {
                    MessageBox.Show("Mã Sinh Viên đã tồn tại ở Khoa khác. Mời bạn nhập lại !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        private void BtnThemLH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = BdsSv.Position;
            themSV = true;
            panelControl2.Enabled = true;
            BdsSv.AddNew();
            TxtMaSV.Focus();
            TxtMaLop.Text = ((DataRowView)this.BdsLH[this.gridViewLH.FocusedRowHandle])["MALOP"].ToString();
            CbPhai.Checked = false;
            CbNghiHoc.Checked = false;
            CmbKhoa.Enabled = BtnThem.Enabled = BtnXoa.Enabled = BtnLamMoi.Enabled = BtnThoat.Enabled = BtnSua.Enabled = false;
            BtnGhi.Enabled = BtnPhucHoi.Enabled = true;
            GcLopHoc.Enabled = GcSinhVien.Enabled = false;

        }

        private void BtnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn hủy bỏ tất cả thao tác?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                BdsSv.CancelEdit();
                if (BtnThem.Enabled == false) BdsSv.Position = vitri;

                GcLopHoc.Enabled = GcSinhVien.Enabled= true;
                panelControl2.Enabled = false;
                CmbKhoa.Enabled = BtnThem.Enabled = BtnXoa.Enabled = BtnLamMoi.Enabled = BtnThoat.Enabled = BtnSua.Enabled = true;
                BtnGhi.Enabled = BtnPhucHoi.Enabled = false;
            }

        }

        private void BtnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                
                this.lOPTableAdapter.Fill(this.DS2.LOP);
                this.sINHVIENTableAdapter.Fill(this.DS2.SINHVIEN);
                MessageBox.Show("Làm mới thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload:" + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
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
                this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
                // TODO: This line of code loads data into the 'dS2.LOP' table. You can move, or remove it, as needed.
                this.lOPTableAdapter.Fill(this.DS2.LOP);
                this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                // TODO: This line of code loads data into the 'dS2.SINHVIEN' table. You can move, or remove it, as needed.
                this.sINHVIENTableAdapter.Fill(this.DS2.SINHVIEN);

                // TODO: This line of code loads data into the 'DS2.DANGKY' table. You can move, or remove it, as needed.
                this.dANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.dANGKYTableAdapter.Fill(this.DS2.DANGKY);

       
            }

        }

        private void BtnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void BtnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (BdsSv.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để sửa", "", MessageBoxButtons.OK);
                
                return;
            }

            vitri = BdsLH.Position;
            themSV = false;
            maSinhVienSua = TxtMaSV.Text;
            CmbKhoa.Enabled = BtnSua.Enabled = BtnThem.Enabled = BtnLamMoi.Enabled = BtnThoat.Enabled = BtnXoa.Enabled = false;
            panelControl2.Enabled = BtnPhucHoi.Enabled = BtnGhi.Enabled = true;
            GcLopHoc.Enabled = GcSinhVien.Enabled = false;
           

           
        }

        private void BtnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string masv = "";
            if (BdsSv.Count == 0) {
                MessageBox.Show("Lớp không có sinh viên", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (BdsDK.Count > 0)
            {
                MessageBox.Show("Không thể xóa sinh viên này vì sinh viên đã đăng kí lớp tín chỉ", "", MessageBoxButtons.OK);
                return;
            }


            if (MessageBox.Show("Bạn có thật sự muốn xóa sinh viên khỏi lớp học này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    masv = ((DataRowView)BdsSv[BdsSv.Position])["MASV"].ToString();
                    BdsSv.RemoveCurrent();
                    this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.sINHVIENTableAdapter.Update(this.DS2.SINHVIEN);
                    MessageBox.Show("Xoá thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa sinh viên: " + ex.Message, "", MessageBoxButtons.OK);
                    this.sINHVIENTableAdapter.Fill(this.DS2.SINHVIEN);
                    BdsSv.Position = BdsLH.Find("MASV", masv);
                    return;
                }
            }

            if (BdsSv.Count == 0) BtnXoa.Enabled = false;

        }

        private void BtnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //bắt lỗi
            if (validatorSinhVien() == true)
            {
                try
                {
                    BdsSv.EndEdit(); //kt hiệu chỉnh ghi vào bds
                    BdsSv.ResetCurrentItem(); //đưa thông tin lên lưới
                    this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.sINHVIENTableAdapter.Update(this.DS2.SINHVIEN);
                    MessageBox.Show("Đã thành công!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi ghi sinh viên.\n" + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
            }
            else  return;
         
            GcLopHoc.Enabled = GcSinhVien.Enabled = true;
            panelControl2.Enabled = false;
            CmbKhoa.Enabled = BtnThem.Enabled = BtnThoat.Enabled = BtnXoa.Enabled = BtnLamMoi.Enabled = BtnSua.Enabled = true;
            BtnGhi.Enabled = BtnPhucHoi.Enabled = false;
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtMaSV_TextChanged(object sender, EventArgs e)
        {
            TxtMaSV.Text = TxtMaSV.Text.ToUpper();
            TxtMaSV.SelectionStart = TxtMaSV.Text.Length;
        }
    }
}
