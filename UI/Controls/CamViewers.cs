using DevExpress.XtraEditors;
using DSEV.Schemas;

using System;



namespace DSEV.UI.Controls
{
    public partial class CamViewers : DevExpress.XtraEditors.XtraUserControl
    {
        public CamViewers() => InitializeComponent();

        public void Init()
        {

            this.e하부커넥터캠.Init(false);
            //this.e상부커넥터캠.Init(false);

            Global.비전검사.SetDisplay(카메라구분.Cam01, this.e하부커넥터캠);
            //Global.비전검사.SetDisplay(카메라구분.Cam07, this.e상부커넥터캠);
        }
        public void Close() { }
    }
}
