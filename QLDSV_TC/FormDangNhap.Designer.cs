
namespace QLDSV_TC
{
    partial class FormDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnDangNhap = new System.Windows.Forms.Button();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.CmbKhoa = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.TxbTaiKhoan = new System.Windows.Forms.TextBox();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.LabelTK = new System.Windows.Forms.Label();
            this.CbSinhVien = new System.Windows.Forms.CheckBox();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.TxbMatKhau = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbHienMK = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnDangNhap
            // 
            this.BtnDangNhap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnDangNhap.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDangNhap.Location = new System.Drawing.Point(24, 314);
            this.BtnDangNhap.Name = "BtnDangNhap";
            this.BtnDangNhap.Size = new System.Drawing.Size(352, 40);
            this.BtnDangNhap.TabIndex = 7;
            this.BtnDangNhap.Text = "Đăng Nhập";
            this.BtnDangNhap.UseVisualStyleBackColor = true;
            this.BtnDangNhap.Click += new System.EventHandler(this.BtnDangNhap_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.CmbKhoa);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(24, 16);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(352, 83);
            this.panelControl1.TabIndex = 8;
            // 
            // CmbKhoa
            // 
            this.CmbKhoa.Dock = System.Windows.Forms.DockStyle.Top;
            this.CmbKhoa.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbKhoa.FormattingEnabled = true;
            this.CmbKhoa.Location = new System.Drawing.Point(0, 37);
            this.CmbKhoa.Name = "CmbKhoa";
            this.CmbKhoa.Size = new System.Drawing.Size(352, 32);
            this.CmbKhoa.TabIndex = 6;
            this.CmbKhoa.SelectedIndexChanged += new System.EventHandler(this.CmbKhoa_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.label1.Size = new System.Drawing.Size(99, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "Phòng/Khoa";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.TxbTaiKhoan);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(24, 99);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(352, 79);
            this.panelControl2.TabIndex = 9;
            // 
            // TxbTaiKhoan
            // 
            this.TxbTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxbTaiKhoan.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbTaiKhoan.Location = new System.Drawing.Point(0, 37);
            this.TxbTaiKhoan.Name = "TxbTaiKhoan";
            this.TxbTaiKhoan.Size = new System.Drawing.Size(352, 35);
            this.TxbTaiKhoan.TabIndex = 8;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.LabelTK);
            this.panelControl3.Controls.Add(this.CbSinhVien);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(352, 37);
            this.panelControl3.TabIndex = 7;
            // 
            // LabelTK
            // 
            this.LabelTK.AutoSize = true;
            this.LabelTK.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelTK.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTK.Location = new System.Drawing.Point(0, 0);
            this.LabelTK.Name = "LabelTK";
            this.LabelTK.Size = new System.Drawing.Size(84, 21);
            this.LabelTK.TabIndex = 8;
            this.LabelTK.Text = "Tài Khoản";
            // 
            // CbSinhVien
            // 
            this.CbSinhVien.AutoSize = true;
            this.CbSinhVien.Dock = System.Windows.Forms.DockStyle.Right;
            this.CbSinhVien.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbSinhVien.Location = new System.Drawing.Point(252, 0);
            this.CbSinhVien.Name = "CbSinhVien";
            this.CbSinhVien.Size = new System.Drawing.Size(100, 37);
            this.CbSinhVien.TabIndex = 7;
            this.CbSinhVien.Text = "Sinh Viên";
            this.CbSinhVien.UseVisualStyleBackColor = true;
            this.CbSinhVien.CheckedChanged += new System.EventHandler(this.CbSinhVien_CheckedChanged);
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.TxbMatKhau);
            this.panelControl4.Controls.Add(this.label2);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(24, 178);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(352, 81);
            this.panelControl4.TabIndex = 10;
            // 
            // TxbMatKhau
            // 
            this.TxbMatKhau.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxbMatKhau.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbMatKhau.Location = new System.Drawing.Point(0, 37);
            this.TxbMatKhau.Name = "TxbMatKhau";
            this.TxbMatKhau.Size = new System.Drawing.Size(352, 35);
            this.TxbMatKhau.TabIndex = 8;
            this.TxbMatKhau.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.label2.Size = new System.Drawing.Size(80, 37);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mật Khẩu";
            // 
            // cbHienMK
            // 
            this.cbHienMK.AutoSize = true;
            this.cbHienMK.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHienMK.Location = new System.Drawing.Point(28, 266);
            this.cbHienMK.Name = "cbHienMK";
            this.cbHienMK.Size = new System.Drawing.Size(139, 25);
            this.cbHienMK.TabIndex = 11;
            this.cbHienMK.Text = "Hiện mật khẩu";
            this.cbHienMK.UseVisualStyleBackColor = true;
            this.cbHienMK.CheckedChanged += new System.EventHandler(this.cbHienMK_CheckedChanged);
            // 
            // FormDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 370);
            this.Controls.Add(this.cbHienMK);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.BtnDangNhap);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormDangNhap";
            this.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.FormDangNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnDangNhap;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbKhoa;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.TextBox TxbTaiKhoan;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.Label LabelTK;
        private System.Windows.Forms.CheckBox CbSinhVien;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private System.Windows.Forms.TextBox TxbMatKhau;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbHienMK;
    }
}