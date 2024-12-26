using DevExpress.XtraPrinting.Native;
using MvUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DSEV.Schemas
{
    partial class 장치통신
    {
        private DateTime 오류알림시간 = DateTime.Today.AddDays(-1);
        private Int32 오류알림간격 = 30; // 초
        public Int32 CurrentIndex = 1;

        public void 통신오류알림(Int32 오류코드)
        {
            if (오류코드 == 0)
            {
                this.정상여부 = true;
                return;
            }
            if ((DateTime.Now - this.오류알림시간).TotalSeconds < this.오류알림간격) return;
            this.오류알림시간 = DateTime.Now;
            this.정상여부 = false;
            Global.오류로그(로그영역, "Communication", $"[{오류코드.ToString("X8")}] Communication error.", false);
        }

        private Boolean 입출자료갱신()
        {
            DateTime 현재 = DateTime.Now;
            // 입출자료 갱신
            Int32[] 자료 = ReadDeviceRandom(입출자료.주소목록, out Int32 오류);
            if (오류 != 0)
            {
                통신오류알림(오류);
                //Debug.WriteLine("오류확인");
                return false;
            }
            this.입출자료.Set(자료);
            return true;
        }

        private Boolean 입출자료분석()
        {
            if (Global.환경설정.동작구분 == 동작구분.LocalTest) return true;
            if (!입출자료갱신()) return false;
            검사위치확인();
            제품검사수행();
            장치상태확인();
            //통신핑퐁수행();
            return true;
        }

        private void 장치상태확인()
        {
            if (this.입출자료.Changed(정보주소.상부촬영트리거) || this.입출자료.Changed(정보주소.상부촬영완료))
                this.동작상태알림?.Invoke();
        }

        // 검사위치 변경 확인
        private void 검사위치확인()
        {
            Dictionary<정보주소, Int32> 변경 = this.입출자료.Changes(정보주소.상부촬영트리거, 정보주소.상부촬영완료);

            if (변경.Count < 1) return;

            if (this.상부촬영트리거신호 && this.상부촬영완료신호)
            {
                this.상부촬영완료신호 = false;
                if (Global.환경설정.검사시작)
                    this.전체완료알림?.Invoke();
                else
                {
                    this.초기완료알림?.Invoke();
                    Global.정보로그("원점복귀", "원점복귀완료", "원점복귀완료", true);
                }
                Debug.WriteLine("완료신호끔");
            }
            
            this.검사위치알림?.Invoke();
        }

        private void 제품검사수행()
        {
            영상촬영수행();
            검사결과전송();
        }

        // 카메라 별 현재 검사 위치의 검사번호를 요청
        public Int32 촬영위치번호(카메라구분 구분)
        {
            return this.인덱스버퍼[정보주소.상부촬영트리거];
        }

        // 트리거 입력 시 현재 인덱스를 버퍼에 입력하고 검사 수행 시 해당 버퍼의 인덱스를 기준으로 검사
        private Int32 검사위치번호(정보주소 구분)
        {
            if (!this.입출자료.Firing(구분, true, out Boolean 현재, out Boolean 변경))
            {
                return -1;
            }

            Int32 index = 0;
            if (구분 == 정보주소.상부촬영트리거) index += this.CurrentIndex;
            

            this.인덱스버퍼[구분] = index;

            if (index == 0) Global.경고로그(로그영역, 구분.ToString(), $"해당 위치에 검사할 제품의 Index가 없습니다.", false);
            else Debug.WriteLine($"{Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss.fff}")}  {구분} => {index}", "Trigger");
            return index;
        }

        public List<Int32> 검사중인항목()
        {
            List<Int32> 대상 = new List<Int32>();
            Int32 시작 = (Int32)정보주소.서보X속도;
            Int32 종료 = (Int32)정보주소.서보Z위치;
            for (Int32 i = 종료; i >= 시작; i--)
            {
                정보주소 구분 = (정보주소)i;
                if (this.입출자료[구분].정보 <= 0) continue;
                대상.Add(this.입출자료[구분].정보);
            }
            return 대상;
        }

        private void 영상촬영수행()
        {
            return;
            //Int32 상부촬영검사번호 = this.검사위치번호(정보주소.상부촬영트리거);

            //if (상부촬영검사번호 > 0)
            //{
            //    //this.상부촬영완료신호 = true;
            //    new Thread(() =>
            //    {
            //        //Global.조명제어.TurnOn(카메라구분.Cam01);
            //        Global.그랩제어.SoftwareTrigger(카메라구분.Cam01);
            //        this.상부촬영완료신호 = true;
            //    })
            //    { Priority = ThreadPriority.Highest }.Start();

            //    Debug.WriteLine("aa");
            //}

            
        }

        // 최종 검사 결과 보고
        private void 검사결과전송()
        {
            Int32 검사번호 = 0;
            if (검사번호 <= 0) return;

            Global.모델자료.선택모델.검사종료(검사번호);
            검사결과 검사 = Global.검사자료.검사결과계산(검사번호);

            // 강제배출
            Debug.WriteLine("검사결과 강제배출 확인중");
            if (Global.환경설정.강제배출)
            {
                결과전송(Global.환경설정.양품불량, 검사);
                Global.검사자료.검사완료알림함수(검사);
                return;
            }

            Debug.WriteLine("강제배출 아님. 검사 비어있는지 확인 중");
            if (검사 == null)
            {
                결과전송(false, 검사);
                Global.검사자료.검사완료알림함수(검사);
                return;
            }

            Debug.WriteLine("안비어있음. 결과전송 진행 예정");
            // 배출 수행
            결과전송(검사.측정결과 == 결과구분.OK, 검사);
            Debug.WriteLine($"{검사.측정결과}");

            Global.검사자료.검사완료알림함수(검사);
        }

        // 신호 Writing 순서 중요
        private void 결과전송(Boolean 양품여부, 검사결과 검사)
        {
            //

            return;
        }

        // 핑퐁
        //private void 통신핑퐁수행()
        //{
        //    if (!this.입출자료[정보주소.통신핑퐁].Passed()) return;
        //    if (this.시작일시.Day != DateTime.Today.Day)
        //    {
        //        this.시작일시 = DateTime.Now;
        //        this.검사번호리셋 = true;
        //        Global.모델자료.선택모델.날짜변경();
        //    }

        //    this.통신확인핑퐁 = !this.통신확인핑퐁;
        //    this.통신상태알림?.Invoke();
        //}

    }
}
