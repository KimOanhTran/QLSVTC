using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLDSV_TC
{
    public partial class Xrpt_DANHSACHLOPTINCHI : DevExpress.XtraReports.UI.XtraReport
    {
      
        public Xrpt_DANHSACHLOPTINCHI(string nienKhoa,int hocKy)
        {
            Console.WriteLine("BIEN",nienKhoa,hocKy);
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = nienKhoa;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = hocKy;
         ///   this.sqlDataSource1.Queries[0].Parameters[2].Value = maKhoa;
            this.sqlDataSource1.Fill();
        }
     
    }
}
