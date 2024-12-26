using Euresys.MultiCam;
using MvCamCtrl.NET;
using MvUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DSEV.Multicam;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp;

namespace DSEV.Schemas
{
    public class 그랩제어 : Dictionary<카메라구분, 그랩장치>
    {
        public static List<카메라구분> 대상카메라 = new List<카메라구분>() { 카메라구분.Cam01 };

        public delegate void 그랩완료대리자(그랩장치 장치);
        public event 그랩완료대리자 그랩완료보고;

        [JsonIgnore]
        public HikeGigE 상부검사카메라 = null;

        [JsonIgnore]
        private const string 로그영역 = "Camera";
        [JsonIgnore]
        private string 저장파일 => Path.Combine(Global.환경설정.기본경로, "Cameras.json");
        [JsonIgnore]
        public Boolean 정상여부 => !this.Values.Any(e => !e.상태);


        public Boolean Init()
        {
            this.상부검사카메라 = new HikeGigE() { 구분 = 카메라구분.Cam01, 코드 = "DA0988355" };

            this.Add(카메라구분.Cam01, this.상부검사카메라);

            // 카메라 설정 저장정보 로드
            그랩장치 정보;
            List<그랩장치> 자료 = Load();
            if (자료 != null)
            {
                foreach (그랩장치 설정 in 자료)
                {
                    정보 = this.GetItem(설정.구분);
                    if (정보 == null) continue;
                    정보.Set(설정);
                }
            }
            if (Global.환경설정.동작구분 != 동작구분.Live) return true;

            // GigE 카메라 초기화
            List<CCameraInfo> 카메라들 = new List<CCameraInfo>();
            Int32 nRet = CSystem.EnumDevices(CSystem.MV_GIGE_DEVICE, ref 카메라들);
            if (!Validate("Enumerate devices fail!", nRet, true)) return false;

            for (int i = 0; i < 카메라들.Count; i++)
            {
                CGigECameraInfo gigeInfo = 카메라들[i] as CGigECameraInfo;
                HikeGigE gige = this.GetItem(gigeInfo.chSerialNumber) as HikeGigE;
                if (gige == null) continue;

                Debug.WriteLine(gigeInfo.chSerialNumber);
                gige.Init(gigeInfo);
            }

            Debug.WriteLine($"카메라 갯수: {this.Count}");
            GC.Collect();
            return true;
        }

        private List<그랩장치> Load()
        {
            if (!File.Exists(this.저장파일)) return null;
            return JsonConvert.DeserializeObject<List<그랩장치>>(File.ReadAllText(this.저장파일), Utils.JsonSetting());
        }

        public void Save()
        {
            if (!Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this.Values, Utils.JsonSetting())))
                Global.오류로그(로그영역, "카메라 설정 저장", "카메라 설정 저장에 실패하였습니다.", true);
        }

        public void Close()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            foreach (그랩장치 장치 in this.Values)
                장치?.Close();
        }
        public void Active(카메라구분 구분) => this.GetItem(구분)?.Active();

        public void SoftwareTrigger(카메라구분 구분) => this.GetItem(구분)?.SoftwareTrigger();

        public 그랩장치 GetItem(카메라구분 구분)
        {
            if (this.ContainsKey(구분)) return this[구분];
            return null;
        }
        private 그랩장치 GetItem(String serial) => this.Values.Where(e => e.코드 == serial).FirstOrDefault();

        public void 그랩완료(그랩장치 장치)
        {
            Debug.WriteLine("그랩완료 진입");

            장치.MatImage().SaveImage(Path.Combine(장치.라이브폴더경로, 장치.이미지번호.ToString("D5") + ".bmp"));
            장치.이미지번호 += 1;
        }

        public void 연속촬영(카메라구분 구분, Boolean 동작)
        {
            if (Global.장치상태.자동수동) return;
            그랩장치 장치 = GetItem(구분);
            if (장치 == null) return;
            if (동작)
            {
                if (장치.연속촬영여부) return;
                장치.연속촬영여부 = true;
                스냅촬영(장치);
            }
            else
            {
                장치.연속촬영여부 = false;
            }
        }

        public void 스냅촬영(그랩장치 장치)
        {
            new Thread(() => {
                장치.SoftwareTrigger();
            }).Start();
        }

        #region 오류메세지
        public static Boolean Validate(String message, Int32 errorNum, Boolean show)
        {
            if (errorNum == CErrorDefine.MV_OK) return true;

            String errorMsg = String.Empty;
            switch (errorNum)
            {
                case CErrorDefine.MV_E_HANDLE: errorMsg = "Error or invalid handle"; break;
                case CErrorDefine.MV_E_SUPPORT: errorMsg = "Not supported function"; break;
                case CErrorDefine.MV_E_BUFOVER: errorMsg = "Cache is full"; break;
                case CErrorDefine.MV_E_CALLORDER: errorMsg = "Function calling order error"; break;
                case CErrorDefine.MV_E_PARAMETER: errorMsg = "Incorrect parameter"; break;
                case CErrorDefine.MV_E_RESOURCE: errorMsg = "Applying resource failed"; break;
                case CErrorDefine.MV_E_NODATA: errorMsg = "No data"; break;
                case CErrorDefine.MV_E_PRECONDITION: errorMsg = "Precondition error, or running environment changed"; break;
                case CErrorDefine.MV_E_VERSION: errorMsg = "Version mismatches"; break;
                case CErrorDefine.MV_E_NOENOUGH_BUF: errorMsg = "Insufficient memory"; break;
                case CErrorDefine.MV_E_UNKNOW: errorMsg = "Unknown error"; break;
                case CErrorDefine.MV_E_GC_GENERIC: errorMsg = "General error"; break;
                case CErrorDefine.MV_E_GC_ACCESS: errorMsg = "Node accessing condition error"; break;
                case CErrorDefine.MV_E_ACCESS_DENIED: errorMsg = "No permission"; break;
                case CErrorDefine.MV_E_BUSY: errorMsg = "Device is busy, or network disconnected"; break;
                case CErrorDefine.MV_E_NETER: errorMsg = "Network error"; break;
                default: errorMsg = "Unknown error"; break;
            }

            Global.오류로그("Camera", "Error", $"[{errorNum}] {message} {errorMsg}", show);
            return false;
        }
        #endregion
    }


    class CameraLiveCapture
    {
        private CancellationTokenSource cts;

        public CameraLiveCapture()
        {
            cts = new CancellationTokenSource();
        }

        public async Task CaptureImagesAsync(string folderPath, int durationSeconds)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            int frameCount = 0;
            int totalFrames = durationSeconds * 10; // 초당 10프레임 기준
            var stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();

            await Task.Run(async () =>
            {
                while (frameCount < totalFrames && !cts.Token.IsCancellationRequested)
                {
                    long elapsedMs = stopwatch.ElapsedMilliseconds;

                    Global.그랩제어[카메라구분.Cam01].SoftwareTrigger();

                    // 다음 캡처 타이밍까지 대기
                    long waitTime = (100 * frameCount) - elapsedMs; // 100ms 간격
                    if (waitTime > 0)
                    {
                        await Task.Delay((int)waitTime);
                    }
                }
            });

            stopwatch.Stop();
        }

        // 캡처 중지
        public void StopCapture()
        {
            cts.Cancel();
            Console.WriteLine("캡처 중지 요청됨.");
        }
    }






}