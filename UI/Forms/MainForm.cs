using DevExpress.XtraWaitForm;
using MvUtils;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSEV.Schemas;
using DSEV.UI.Controls;
using System.Diagnostics;
using DSEV.UI.Forms;
using System.Windows.Media.Media3D;
using System.IO;

namespace DSEV
{
    public partial class MainForm : DevExpress.XtraBars.TabForm
    {
        private LocalizationMain 번역 = new LocalizationMain();
        private UI.Controls.WaitForm WaitForm;
        private StateForm StateForm = null;
        public MainForm()
        {
            InitializeComponent();
            this.ShowWaitForm();
            this.e프로젝트.Caption = $"IVM: {환경설정.프로젝트번호}";
            this.SetLocalization();
            this.TabFormControl.SelectedPage = this.p결과뷰어;
            this.p환경설정.Enabled = false;
            this.p검사내역.Enabled = false;
            this.Shown += MainFormShown;
            this.FormClosing += MainFormClosing;
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
           
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
               
            }

            //if (e.KeyCode == Keys.Q)
            //{
            //    Global.환경설정.검사시작 = false;

            //    Int32 서보초기X속도 = 50000;
            //    Int32 서보초기X위치 = 17000000;

            //    Int32 서보초기Y속도 = 50000;
            //    Int32 서보초기Y위치 = 12000000;

            //    Int32 서보초기Z속도 = 100000;
            //    Int32 서보초기Z위치 = 11400000;


            //    Global.장치통신.서보X속도 = 서보초기X속도;
            //    Global.장치통신.서보X위치 = 서보초기X위치;
            //    Global.장치통신.서보Y속도 = 서보초기Y속도;
            //    Global.장치통신.서보Y위치 = 서보초기Y위치;
            //    Global.장치통신.서보Z속도 = 서보초기Z속도;
            //    Global.장치통신.서보Z위치 = 서보초기Z위치;

            //    Debug.WriteLine("서보 동작");
            //    Global.장치통신.상부촬영트리거신호 = true;

            //    //움직임 완료 신호 확인

            //}

            //if (e.KeyCode == Keys.W)
            //{

            //    Global.환경설정.검사시작 = true;
            //    Global.환경설정.검사번호++;
            //    Global.검사자료.검사시작(Global.환경설정.검사번호);
            //    ///
            //    Int32 서보검사X속도 = 50000;
            //    Int32 서보검사X위치 = 17000000;

            //    Int32 서보검사Y속도 = 50000;
            //    Int32 서보검사Y위치 = 12000000;

            //    Int32 서보검사Z속도 = 100000;
            //    Int32 서보검사Z위치 = 10400000;

            //    Int32 대기시간 = 100;
            //    Int32 갯수 = 50;

            //    if (Global.환경설정.고정밀모드)
            //    {
            //       서보검사X속도 = 50000;
            //       서보검사X위치 = 17000000;

            //       서보검사Y속도 = 50000;
            //       서보검사Y위치 = 12000000;

            //       서보검사Z속도 = 25000;
            //       서보검사Z위치 = 10400000;

            //       대기시간 = 70;
            //       갯수 = 200;
            //    }

            //    Global.장치통신.서보X속도 = 서보검사X속도;
            //    Global.장치통신.서보X위치 = 서보검사X위치;
            //    Global.장치통신.서보Y속도 = 서보검사Y속도;
            //    Global.장치통신.서보Y위치 = 서보검사Y위치;
            //    Global.장치통신.서보Z속도 = 서보검사Z속도;
            //    Global.장치통신.서보Z위치 = 서보검사Z위치;

            //    Debug.WriteLine("서보동작 및 라이브촬영 시작");
            //    HikeGigE 라이브촬영용카메라 = Global.그랩제어[카메라구분.Cam01] as HikeGigE;

            //    string 경로 = Path.Combine(Global.환경설정.사진저장, Global.환경설정.선택모델.ToString()) + $"{DateTime.Now.ToString("yyMMdd_HHmmss_fff")}";
            //    라이브촬영용카메라.라이브폴더경로 = 경로;
            //    라이브촬영용카메라.이미지번호 = 1;
            //    Global.장치통신.상부촬영트리거신호 = true;
            //    라이브촬영용카메라.GrabCont(경로, 대기시간, 갯수);
            //}

            //if (e.KeyCode == Keys.L)
            //{
            //    //Debug.WriteLine("softwareTrigger On");
            //    Debug.WriteLine(Global.환경설정.고정밀모드);
            //}

            //if (e.KeyCode == Keys.P)
            //{
            //    Debug.WriteLine("softwareTrigger On");
            //    Debug.WriteLine(Global.그랩제어[카메라구분.Cam01].코드);
            //}

            //if (e.KeyCode == Keys.I)
            //{
            //    Debug.WriteLine("softwareTrigger On");
            //    Global.그랩제어[카메라구분.Cam01].SoftwareTrigger();
            //}

            //if (e.KeyCode == Keys.A)
            //{
            //    Debug.WriteLine("Active On");
            //    Global.그랩제어[카메라구분.Cam01].Active();
            //}

            //if (e.KeyCode == Keys.S)
            //{
            //    Debug.WriteLine("Stop On");
            //    Global.그랩제어[카메라구분.Cam01].Stop();
            //}
        }



        private void ShowWaitForm()
        {
            WaitForm = new UI.Controls.WaitForm() { ShowOnTopMode = ShowFormOnTopMode.AboveAll };
            WaitForm.Show(this);
        }
        private void HideWaitForm() => WaitForm.Close();

        private void MainFormShown(object sender, EventArgs e)
        {
            Global.Initialized += GlobalInitialized;
            Task.Run(() => { Global.Init(); });
        }

        private void GlobalInitialized(object sender, Boolean e) =>
            this.BeginInvoke(new Action(() => GlobalInitialized(e)));
        private void GlobalInitialized(Boolean e)
        {
            Global.Initialized -= GlobalInitialized;
            if (!e) { this.Close(); return; }
            this.HideWaitForm();
            Common.SetForegroundWindow(this.Handle.ToInt32());

            // 로그인
            //Login login = new Login();
            //if (Utils.ShowDialog(login, this) == DialogResult.OK)
            //{
            //    Global.DxLocalization();
            //    this.Init();
            //    Global.Start();
            //}
            //else this.Close();

            ////자동로그인
            Global.환경설정.시스템관리자로그인();
            Localization.SetCulture();
            Global.DxLocalization();
            this.Init();
            Global.Start();
            //}
        }

        private void Init()
        {
            this.SetLocalization();
            this.e결과뷰어.Init(ResultInspection.ViewTypes.Auto);
            this.e검사도구.Init();
            this.e검사설정.Init();
            this.e장치설정.Init();
            this.e검사내역.Init();
            this.e검사피봇.Init();
            this.e결과검색.Init();
            this.e상태뷰어.Init();
            this.e로그내역.Init();
            this.p환경설정.Enabled = Global.환경설정.권한여부(유저권한구분.시스템);
            this.p검사내역.Enabled = Global.환경설정.권한여부(유저권한구분.관리자);
            this.TabFormControl.AllowMoveTabs = false;
            this.TabFormControl.AllowMoveTabsToOuterForm = false;

            if (Global.환경설정.동작구분 == 동작구분.Live)
                this.WindowState = FormWindowState.Maximized;
            //this.ShowHideControl();

            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            foreach (Screen s in Screen.AllScreens)
            {
                Debug.WriteLine(s.Bounds, s.DeviceName);
                if (s.Primary) continue;
                ShowStateForm(s);
            }
            // 창이 생성되지 않았으면 메인 모니터에 띄움
            ShowStateForm(Screen.PrimaryScreen);
        }

        private void ShowStateForm(Screen s)
        {
            if (this.StateForm != null) return;
            this.StateForm = new StateForm() { StartPosition = FormStartPosition.Manual, WindowState = FormWindowState.Maximized };
            this.StateForm.SetBounds(s.WorkingArea.X, s.WorkingArea.Y, s.WorkingArea.Width, s.WorkingArea.Height);
            this.StateForm.Show(this);
        }

        private void CloseForm()
        {
            this.e장치설정.Close();
            this.e로그내역.Close();
            this.e상태뷰어.Close();
            Global.Close();
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (Global.환경설정.사용권한 == 유저권한구분.없음) this.CloseForm();
            else
            {
                e.Cancel = !Utils.Confirm(this, 번역.종료확인, Localization.확인.GetString());
                if (!e.Cancel) this.CloseForm();
            }
        }

        private void SetLocalization()
        {
            this.Text = this.번역.타이틀;
            this.타이틀.Caption = this.번역.타이틀;
            this.p결과뷰어.Text = this.번역.검사하기;
            this.p검사도구.Text = this.번역.카메라;
            this.p검사내역.Text = this.번역.검사내역;
            this.p환경설정.Text = this.번역.환경설정;
            this.t검사설정.Text = this.번역.검사설정;
            this.t장치설정.Text = this.번역.장치설정;
            this.t로그내역.Text = this.번역.로그내역;
        }

        private class LocalizationMain
        {
            private enum Items
            {
                [Translation("Inspection", "검사하기", "Inšpekcia")]
                검사하기,
                [Translation("History", "검사내역", "História")]
                검사내역,
                [Translation("Preferences", "환경설정", "Predvoľby")]
                환경설정,
                [Translation("Settings", "검사설정", "Nastavenie")]
                검사설정,
                [Translation("Devices", "장치설정", "Zariadenia")]
                장치설정,
                [Translation("Cameras", "카메라", "Kamery")]
                카메라,
                [Translation("QR Validate", "큐알검증", "QR Validate")]
                큐알검증,
                [Translation("Logs", "로그내역", "Denníky")]
                로그내역,
                [Translation("Are you want to exit the program?", "프로그램을 종료하시겠습나까?", "Naozaj chcete ukončiť program?")]
                종료확인,
            }
            private String GetString(Items item) { return Localization.GetString(item); }

            public String 타이틀   { get => Localization.제목.GetString(); }
            public String 검사하기 { get => GetString(Items.검사하기); }
            public String 검사내역 { get => GetString(Items.검사내역); }
            public String 환경설정 { get => GetString(Items.환경설정); }
            public String 검사설정 { get => GetString(Items.검사설정); }
            public String 장치설정 { get => GetString(Items.장치설정); }
            public String 카메라   { get => GetString(Items.카메라); }
            public String 큐알검증 { get => GetString(Items.큐알검증); }
            public String 로그내역 { get => GetString(Items.로그내역); }
            public String 종료확인 { get => GetString(Items.종료확인); }
        }
    }
}