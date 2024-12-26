using DevExpress.XtraEditors;
using DSEV.Schemas;
using System;
using System.Diagnostics;

namespace DSEV.UI.Controls
{
    public partial class Viewport3D : XtraUserControl
    {
        public Viewport3D()
        {
            InitializeComponent();
        }

        private VDA590TPA3D Model3D = null;
        public void Init(VDA590TPA3D model)
        {


            this.VM뷰어.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow1).graphicsSetModuleTool;
            Global.장치통신.전체완료알림 += 장치통신_전체완료알림;
            Global.장치통신.초기완료알림 += 장치통신_초기완료알림;
            //this.Model3D = model;
            //if (!Model3D.Init(out String err2)) { Debug.WriteLine(err2, "Model3D Error"); }
            //this.Controls.Add(Model3D.CreateHost());
            //this.SetResults(new 검사결과().Reset());
        }

        private void 장치통신_초기완료알림()
        {
            Global.VM제어.GetItem(Flow구분.Flow1).ImageClear();
        }

        private void 장치통신_전체완료알림()
        {
            검사결과 결과 = Global.검사자료.검사항목찾기(Global.환경설정.검사번호);
            Global.VM제어.GetItem(Flow구분.Flow1).TestRun(결과);
            
        }

        public void SetResults(검사결과 결과)
        {
            //if (결과 == null) return;
            //if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { SetResults(결과); })); return; }
            //this.Model3D.SetResults(결과);
            //this.Invalidate();
        }

        public void RefreshViewport()
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(RefreshViewport)); }
            else this.Invalidate();
        }
    }
}
