using Cognex.VisionPro;
using DevExpress.XtraBars;
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
            //this.e결과목록.Init();

            if (this.RunType == ViewTypes.Auto)
            {
                Global.검사자료.검사완료알림 += 검사완료알림;
                Global.그랩제어.그랩완료보고 += 그랩제어_그랩완료보고;
                Global.장치통신.전체완료알림 += 장치통신_전체완료알림;
                Global.장치통신.초기완료알림 += 장치통신_초기완료알림;
                검사완료알림(Global.검사자료.현재검사찾기());
            }

            this.b스냅.ItemClick += 스냅촬영;
            this.b영상.CheckedChanged += 연속촬영;
            this.b조명.CheckedChanged += 조명제어;
        }

        private void 그랩제어_그랩완료보고(그랩장치 장치)
        {
            if (Global.장치상태.자동수동 || 장치.구분 != 카메라구분.Cam01) return;
            if (!장치.연속촬영여부)
            {
                //Global.비전검사.Run(카메라, 장치.CogImage(), Global.검사자료.현재검사찾기(장치.구분));
                return;
            }

            this.e뷰어.Image = 장치.CogImage();
            this.AddCrosLine();
            if (this.연속촬영시간.ElapsedMilliseconds > 3000)
            {
                GC.Collect();
                this.연속촬영시간.Restart();
            }
        }

        private void 스냅촬영(object sender, ItemClickEventArgs e)
        {
            //if (Global.장치상태.자동수동) return;
            //if (Global.환경설정.동작구분 == 동작구분.Live)
            //    Global.그랩제어.스냅촬영(카메라);
            //else Global.비전검사[카메라].마스터로드();
        }

        private Stopwatch 연속촬영시간 = new Stopwatch();

        private void 연속촬영(object sender, ItemClickEventArgs e)
        {
            if (Global.장치상태.자동수동 || Global.환경설정.동작구분 != 동작구분.Live) return;
            BarCheckItem button = sender as BarCheckItem;
            if (button.Checked)
            {
                this.e뷰어.InteractiveGraphics.Clear();
                this.e뷰어.StaticGraphics.Clear();
                this.연속촬영시간.Restart();
                Global.그랩제어.연속촬영(카메라구분.Cam01, true);
            }
            else
            {
                this.연속촬영시간.Stop();
                Global.그랩제어.연속촬영(카메라구분.Cam01, false);
                GC.Collect();
            }
        }
        private void 장치통신_초기완료알림()
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { 장치통신_초기완료알림(); })); return; }
       
            검사결과 결과 = Global.검사자료.검사항목찾기2();
            //this.e결과목록.SetResults(결과);
            this.e측정결과.Appearance.ForeColor = 환경설정.ResultColor(결과.측정결과);
            this.eCTQ결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.CTQ결과);
            this.e외관결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.외관결과);
            this.Bind검사결과.DataSource = 결과;
            this.Bind검사결과.ResetBindings(false);
            Debug.WriteLine("4");
        }
        private void 조명제어(object sender, ItemClickEventArgs e)
        {
            //if (Global.장치상태.자동수동 || Global.환경설정.동작구분 != 동작구분.Live) return;
            //BarCheckItem button = sender as BarCheckItem;
            //if (button.Checked) Global.조명제어.TurnOn(this.카메라);
            //else Global.조명제어.TurnOff(this.카메라);
            //상태색상변경(button);
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
            //this.e결과목록.SetResults(결과);
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

        private void AddCrosLine()
        {
            if (e뷰어.Image == null) return;
            if (this.e뷰어.StaticGraphics.ZOrderGroups.Count < 1)
            {
                this.e뷰어.InteractiveGraphics.Clear();
                this.e뷰어.StaticGraphics.Clear();


                //if (Global.그랩제어[카메라].회전 == 회전구분.FlipAndRotate90Deg || Global.그랩제어[카메라].회전 == 회전구분.FlipAndRotate270Deg || Global.그랩제어[카메라].회전 == 회전구분.Rotate90Deg || Global.그랩제어[카메라].회전 == 회전구분.Rotate270Deg)
                //{
                //    this.e뷰어.StaticGraphics.Add(new CogLine() { X = e뷰어.Image.Height / 2, Y = e뷰어.Image.Width / 2, Rotation = 0 }, "Lines");
                //    this.e뷰어.StaticGraphics.Add(new CogLine() { X = e뷰어.Image.Height / 2, Y = e뷰어.Image.Width / 2, Rotation = Math.PI / 2 }, "Lines");
                //    return;
                //}
                this.e뷰어.StaticGraphics.Add(new CogLine() { X = e뷰어.Image.Width / 2, Y = e뷰어.Image.Height / 2, Rotation = 0 }, "Lines");
                this.e뷰어.StaticGraphics.Add(new CogLine() { X = e뷰어.Image.Width / 2, Y = e뷰어.Image.Height / 2, Rotation = Math.PI / 2 }, "Lines");
            }
        }
    }
}
