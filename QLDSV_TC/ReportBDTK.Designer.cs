
namespace QLDSV_TC
{
    partial class ReportBDTK
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportBDTK));
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.lOPTableAdapter = new QLDSV_TC.DS1TableAdapters.LOPTableAdapter();
            this.sqlDataSource2 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.fieldMASV1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldHOTEN1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldTENMH1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDiemHocPhan1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.LbKhoa = new DevExpress.XtraReports.UI.XRLabel();
            this.LbLopKhoaHoc = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.dS11 = new QLDSV_TC.DS1();
            ((System.ComponentModel.ISupportInitialize)(this.dS11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            this.Detail.Name = "Detail";
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.xrPivotGrid1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.Appearance.Lines.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.xrPivotGrid1.DataAdapter = this.lOPTableAdapter;
            this.xrPivotGrid1.DataMember = "SP_BanDiemTongKet";
            this.xrPivotGrid1.DataSource = this.sqlDataSource2;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldMASV1,
            this.fieldHOTEN1,
            this.fieldTENMH1,
            this.fieldDiemHocPhan1});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OLAPConnectionString = "";
            this.xrPivotGrid1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(688F, 68.33334F);
            // 
            // lOPTableAdapter
            // 
            this.lOPTableAdapter.ClearBeforeFill = true;
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionName = "QLDSV_TC.Properties.Settings.QLDSV_TCConnectionString1";
            this.sqlDataSource2.Name = "sqlDataSource2";
            storedProcQuery1.Name = "SP_BanDiemTongKet";
            queryParameter1.Name = "@MaLop";
            queryParameter1.Type = typeof(string);
            queryParameter1.ValueInfo = "D15CQCP01 ";
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "SP_BanDiemTongKet";
            this.sqlDataSource2.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource2.ResultSchemaSerializable = resources.GetString("sqlDataSource2.ResultSchemaSerializable");
            // 
            // fieldMASV1
            // 
            this.fieldMASV1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldMASV1.AreaIndex = 0;
            this.fieldMASV1.Caption = "MASV";
            this.fieldMASV1.FieldName = "MASV";
            this.fieldMASV1.Name = "fieldMASV1";
            // 
            // fieldHOTEN1
            // 
            this.fieldHOTEN1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldHOTEN1.AreaIndex = 1;
            this.fieldHOTEN1.Caption = "HOTEN";
            this.fieldHOTEN1.FieldName = "HOTEN";
            this.fieldHOTEN1.Name = "fieldHOTEN1";
            // 
            // fieldTENMH1
            // 
            this.fieldTENMH1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldTENMH1.AreaIndex = 0;
            this.fieldTENMH1.Caption = "Môn Học";
            this.fieldTENMH1.FieldName = "TENMH";
            this.fieldTENMH1.MinWidth = 100;
            this.fieldTENMH1.Name = "fieldTENMH1";
            this.fieldTENMH1.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.fieldTENMH1.Width = 148;
            // 
            // fieldDiemHocPhan1
            // 
            this.fieldDiemHocPhan1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldDiemHocPhan1.AreaIndex = 0;
            this.fieldDiemHocPhan1.FieldName = "DiemHocPhan";
            this.fieldDiemHocPhan1.Name = "fieldDiemHocPhan1";
            this.fieldDiemHocPhan1.Options.ShowGrandTotal = false;
            this.fieldDiemHocPhan1.Options.ShowInExpressionEditor = false;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "november.QLDSV_TC.3dbo";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery2.Name = "SP_BanDiemTongKet";
            queryParameter2.Name = "@MaLop";
            queryParameter2.Type = typeof(string);
            queryParameter2.ValueInfo = "D15CQCP01";
            storedProcQuery2.Parameters.Add(queryParameter2);
            storedProcQuery2.StoredProcName = "SP_BanDiemTongKet";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.LbKhoa,
            this.LbLopKhoaHoc,
            this.xrLabel1});
            this.ReportHeader.HeightF = 140.6667F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // LbKhoa
            // 
            this.LbKhoa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbKhoa.LocationFloat = new DevExpress.Utils.PointFloat(10.00003F, 76.83334F);
            this.LbKhoa.Multiline = true;
            this.LbKhoa.Name = "LbKhoa";
            this.LbKhoa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LbKhoa.SizeF = new System.Drawing.SizeF(688F, 23F);
            this.LbKhoa.StylePriority.UseFont = false;
            this.LbKhoa.StylePriority.UseTextAlignment = false;
            this.LbKhoa.Text = "KHOA: XXXXXX";
            this.LbKhoa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LbLopKhoaHoc
            // 
            this.LbLopKhoaHoc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbLopKhoaHoc.LocationFloat = new DevExpress.Utils.PointFloat(10.00003F, 53.83334F);
            this.LbLopKhoaHoc.Multiline = true;
            this.LbLopKhoaHoc.Name = "LbLopKhoaHoc";
            this.LbLopKhoaHoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LbLopKhoaHoc.SizeF = new System.Drawing.SizeF(688F, 23F);
            this.LbLopKhoaHoc.StylePriority.UseFont = false;
            this.LbLopKhoaHoc.StylePriority.UseTextAlignment = false;
            this.LbLopKhoaHoc.Text = "LỚP: XXXXXX    KHOÁ HỌC:  XXXXXXXX";
            this.LbLopKhoaHoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.ForeColor = System.Drawing.Color.SkyBlue;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(688F, 30.50001F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Bảng Điểm Tổng Kết Cuối Khoá";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(598F, 46.16664F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 23F);
            // 
            // dS11
            // 
            this.dS11.DataSetName = "DS1";
            this.dS11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportBDTK
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1,
            this.dS11,
            this.sqlDataSource2});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(100, 42, 100, 100);
            this.Version = "19.2";
            ((System.ComponentModel.ISupportInitialize)(this.dS11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMASV1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldHOTEN1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldTENMH1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDiemHocPhan1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DS1TableAdapters.LOPTableAdapter lOPTableAdapter;
        private DS1 dS11;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource2;
        public DevExpress.XtraReports.UI.XRLabel LbLopKhoaHoc;
        public DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        public DevExpress.XtraReports.UI.XRLabel LbKhoa;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
    }
}
