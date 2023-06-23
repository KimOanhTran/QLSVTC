
namespace QLDSV_TC
{
    partial class Frpt_INPHIEUDIEMSV1
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
            this.btnTHOAT = new System.Windows.Forms.Button();
            this.btnIN = new System.Windows.Forms.Button();
            this.txtMASV = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtMASV.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTHOAT
            // 
            this.btnTHOAT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnTHOAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTHOAT.Location = new System.Drawing.Point(349, 109);
            this.btnTHOAT.Name = "btnTHOAT";
            this.btnTHOAT.Size = new System.Drawing.Size(75, 34);
            this.btnTHOAT.TabIndex = 7;
            this.btnTHOAT.Text = "THOAT";
            this.btnTHOAT.UseVisualStyleBackColor = false;
            this.btnTHOAT.Click += new System.EventHandler(this.btnTHOAT_Click);
            // 
            // btnIN
            // 
            this.btnIN.BackColor = System.Drawing.Color.SpringGreen;
            this.btnIN.Location = new System.Drawing.Point(235, 109);
            this.btnIN.Name = "btnIN";
            this.btnIN.Size = new System.Drawing.Size(75, 34);
            this.btnIN.TabIndex = 6;
            this.btnIN.Text = "IN";
            this.btnIN.UseVisualStyleBackColor = false;
            this.btnIN.Click += new System.EventHandler(this.btnIN_Click);
            // 
            // txtMASV
            // 
            this.txtMASV.Location = new System.Drawing.Point(235, 59);
            this.txtMASV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMASV.Name = "txtMASV";
            this.txtMASV.Size = new System.Drawing.Size(291, 22);
            this.txtMASV.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(126, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "MA SINH VIEN";
            // 
            // Frpt_INPHIEUDIEMSV1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 442);
            this.Controls.Add(this.btnTHOAT);
            this.Controls.Add(this.btnIN);
            this.Controls.Add(this.txtMASV);
            this.Controls.Add(this.label1);
            this.Name = "Frpt_INPHIEUDIEMSV1";
            this.Text = "Frpt_INPHIEUDIEMSV1";
            this.Load += new System.EventHandler(this.Frpt_INPHIEUDIEMSV1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMASV.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTHOAT;
        private System.Windows.Forms.Button btnIN;
        private DevExpress.XtraEditors.TextEdit txtMASV;
        private System.Windows.Forms.Label label1;
    }
}