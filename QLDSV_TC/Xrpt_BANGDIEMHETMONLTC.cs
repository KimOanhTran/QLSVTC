using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLDSV_TC
{
    public partial class Xrpt_BANGDIEMHETMONLTC : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_BANGDIEMHETMONLTC()
        {
        
        }
        public Xrpt_BANGDIEMHETMONLTC(string nienkhoa, int hocky, int nhom, string mamh)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = nienkhoa;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = hocky;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = nhom;
            this.sqlDataSource1.Queries[0].Parameters[3].Value = mamh;
            this.sqlDataSource1.Fill();
        }
    }
}
