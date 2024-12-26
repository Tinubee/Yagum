using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DSEV.Schemas;
using System;
using System.Diagnostics;
using System.Windows.Media.Media3D;

namespace DSEV.UI.Controls
{
    public partial class ResultInspection : XtraUserControl
    {
        public ResultInspection()
        {
            InitializeComponent();
        }

        public enum ViewTypes { Auto, Manual }
        private ViewTypes RunType = ViewTypes.Manual;
        VDA590TPA3D TPA = null;
        public void Init(ViewTypes runType = ViewTypes.Manual)
        {
            RunType = runType;
            TPA = new VDA590TPA3D();
            {
                TPA.CameraPosition = new Point3D(0.6, -2.6, 967);
                TPA.CameraLookDirection = new Vector3D(0, 0, -967);
                TPA.CameraUpDirection = new Vector3D(0, 1, 0);
            }

            this.e결과뷰어.Init(TPA);
            this.e결과목록.Init();

            if (this.RunType == ViewTypes.Auto)
            {
                Global.검사자료.검사완료알림 += 검사완료알림;
                Global.장치통신.전체완료알림 += 장치통신_전체완료알림;
                Global.장치통신.초기완료알림 += 장치통신_초기완료알림;
                검사완료알림(Global.검사자료.현재검사찾기());
            }
        }

        private void 장치통신_초기완료알림()
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { 장치통신_초기완료알림(); })); return; }
       
            검사결과 결과 = Global.검사자료.검사항목찾기2();
            this.e결과목록.SetResults(결과);
            this.e측정결과.Appearance.ForeColor = 환경설정.ResultColor(결과.측정결과);
            this.eCTQ결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.CTQ결과);
            this.e외관결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.외관결과);
            this.Bind검사결과.DataSource = 결과;
            this.Bind검사결과.ResetBindings(false);
            Debug.WriteLine("4");
        }

        Boolean ing = false;

        private void 장치통신_전체완료알림()
        {
            //Debug.WriteLine("!");
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { 장치통신_전체완료알림(); })); return; }

            if (!ing)
            {
                ing = true;
                검사결과 결과 = Global.검사자료.검사항목찾기(Global.환경설정.검사번호);
                
                if (결과 == null)
                {
                    ing = false;
                    return;
                }
                결과.결과계산();
                Global.검사자료.검사완료알림함수(결과);

              
                //Global.모델자료.수량추가(결과.모델구분, 결과.측정결과);

                //this.e결과목록.SetResults(결과);
                //this.e측정결과.Appearance.ForeColor = 환경설정.ResultColor(결과.측정결과);
                //this.eCTQ결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.CTQ결과);
                //this.e외관결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.외관결과);
                //this.Bind검사결과.DataSource = 결과;
                //this.Bind검사결과.ResetBindings(false);

                //Global.검사자료.검사결과계산(Global.환경설정.검사번호);

                ing = false;
            }

        }

        public void Close() { }

        public void 검사완료알림(검사결과 결과)
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { 검사완료알림(결과); })); return; }
            Debug.WriteLine("검사완료알림 들어옴");

            if (Global.장치상태.자동수동) Global.검사자료.Save();
            this.e결과뷰어.SetResults(결과);
            this.e결과목록.SetResults(결과);
            this.e측정결과.Appearance.ForeColor = 환경설정.ResultColor(결과.측정결과);
            this.eCTQ결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.CTQ결과);
            this.e외관결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.외관결과);
            this.Bind검사결과.DataSource = 결과;
            this.Bind검사결과.ResetBindings(false);

            Global.검사자료.검사결과계산(Global.환경설정.검사번호);
        }

        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = sender as GridView;
            검사정보 정보 = view.GetRow(e.RowHandle) as 검사정보;
            if (정보 == null) return;
            정보.SetAppearance(e);
        }
    }
}
