using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLDSV_TC
{
    public partial class Xrpt_INDANHSACHDONGHOCPHI : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_INDANHSACHDONGHOCPHI()
        {
           
        }
        public Xrpt_INDANHSACHDONGHOCPHI(string nienkhoa, int hocky, string malop, string connstr)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = nienkhoa;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = hocky;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = malop;

        }

    }
}
