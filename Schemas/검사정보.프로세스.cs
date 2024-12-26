using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraRichEdit.Export;
using DSEV.UI.Forms;
using MvUtils;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace DSEV.Schemas
{
    [Table("inspl")]
    public class 검사결과
    {
        [Column("ilwdt"), Required, Key, JsonProperty("ilwdt"), Translation("Time", "일시")]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [Column("ilmcd"), JsonProperty("ilmcd"), Translation("Model", "모델")]
        public 모델구분 모델구분 { get; set; } = 모델구분.None;
        [Column("ilnum"), JsonProperty("ilnum"), Translation("Index", "번호")]
        public Int32 검사코드 { get; set; } = 0;
        [Column("ilres"), JsonProperty("ilres"), Translation("Result", "판정")]
        public 결과구분 측정결과 { get; set; } = 결과구분.WA;
        [Column("ilctq"), JsonProperty("ilctq"), Translation("CTQ", "CTQ결과")] //Critical to Quality
        public 결과구분 CTQ결과 { get; set; } = 결과구분.WA;
        [Column("ilsuf"), JsonProperty("ilsuf"), Translation("Suface", "외관결과")]
        public 결과구분 외관결과 { get; set; } = 결과구분.WA;
        //[Column("ilqrg"), JsonProperty("ilqrg"), Translation("QR Legibility", "QR등급")]
        //public 큐알등급 큐알등급 { get; set; } = 큐알등급.X;
        [Column("ilqrs"), JsonProperty("ilqrs"), Translation("QR Code", "QR코드")]
        public String 큐알내용 { get; set; } = String.Empty;
        //[Column("ilngs"), JsonProperty("ilngs"), Translation("NG Items", "불량정보")]
        //public String 불량정보 { get; set; } = String.Empty;
        [NotMapped, JsonIgnore, Translation("NG Items", "불량정보")]
        public String 불량정보 { get; set; } = String.Empty;

        [NotMapped, JsonIgnore]
        public String 결과문구 => Localization.GetString(측정결과);
        [NotMapped, JsonIgnore]
        public String 품질문구 => Localization.GetString(CTQ결과);
        [NotMapped, JsonIgnore]
        public String 외관문구 => Localization.GetString(외관결과);

        [NotMapped, JsonProperty("inspd")]
        public List<검사정보> 검사내역 { get; set; } = new List<검사정보>();
        [NotMapped, JsonProperty("ilreg"), Browsable(false)]
        public List<불량영역> 표면불량 { get; set; } = new List<불량영역>();
        [NotMapped, JsonIgnore, Browsable(false)]
        public List<String> 불량내역 = new List<String>();


        //마킹 전 확인용
        [NotMapped, JsonIgnore]
        public 결과구분 마킹전결과 { get; set; } = 결과구분.WA;


        public 검사결과()
        {
            this.검사일시 = DateTime.Now;
            this.모델구분 = Global.환경설정.선택모델;
        }

        public 검사결과 Reset()
        {
            this.검사일시 = DateTime.Now;
            this.모델구분 = Global.환경설정.선택모델;
            this.측정결과 = 결과구분.WA;
            this.CTQ결과 = 결과구분.WA;
            this.외관결과 = 결과구분.WA;
            //this.큐알등급 = 큐알등급.X;
            this.큐알내용 = String.Empty;
            this.불량정보 = String.Empty;
            this.검사내역.Clear();
            this.표면불량.Clear();
            this.불량내역.Clear();

            검사설정 자료 = Global.모델자료.GetItem(this.모델구분)?.검사설정;
            foreach (검사정보 정보 in 자료)
            {
                if (!정보.검사여부) continue;
                this.검사내역.Add(new 검사정보(정보) { 검사일시 = this.검사일시 });
            }
            return this;
        }
        public 검사결과 Reset(카메라구분 카메라)
        {
            검사설정 자료 = Global.모델자료.GetItem(this.모델구분)?.검사설정;
            foreach (검사정보 정보 in 자료)
            {
                if ((Int32)정보.검사장치 != (Int32)카메라) continue;
                검사정보 수동 = this.검사내역.Where(e => e.검사항목 == 정보.검사항목).FirstOrDefault();
                if (정보 == null) continue;
                if (수동 == null)
                {
                    Global.정보로그("DataReset", "수동", $"검사내역에 {정보.검사명칭}이 없습니다.", true);
                    continue;
                }
                수동.검사명칭 = 정보.검사명칭;
                수동.최소값 = 정보.최소값;
                수동.기준값 = 정보.기준값;
                수동.최대값 = 정보.최대값;
                수동.보정값 = 정보.보정값;
                수동.교정값 = 정보.교정값;
            }
            this.표면불량.RemoveAll(e => (Int32)e.장치구분 == (Int32)카메라);
            return this;
        }

        public void AddRange(List<검사정보> 자료)
        {
            this.검사내역.AddRange(자료);
            this.검사내역.ForEach(e => { e.Init(); e.검사명칭 = Global.모델자료.GetItemName(this.모델구분, e.검사항목); });
            List<String> 불량내역 = this.검사내역.Where(e => e.측정결과 != 결과구분.OK && e.측정결과 != 결과구분.PS).Select(e => e.검사명칭).ToList();
            if (불량내역.Count > 0) this.불량정보 = String.Join(",", 불량내역);
        }

        private String[] AppearanceFields = new String[] { nameof(측정결과), nameof(CTQ결과), nameof(외관결과) };
        public void SetAppearance(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e == null || !AppearanceFields.Contains(e.Column.FieldName)) return;
            PropertyInfo p = typeof(검사결과).GetProperty(e.Column.FieldName);
            if (p == null || p.PropertyType != typeof(결과구분)) return;
            Object v = p.GetValue(this);
            if (v == null) return;
            e.Appearance.ForeColor = 환경설정.ResultColor((결과구분)v);
        }

        public 검사정보 GetItem(장치구분 장치, String name) => 검사내역.Where(e => e.검사장치 == 장치 && e.변수명칭 == name).FirstOrDefault();
        public 검사정보 GetItem(검사항목 항목) => 검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault();

        private Decimal PixelToMeter(검사정보 검사, Double value)
        {
            Double result = 0;
            if (value == 0 || 검사.교정값 <= 0) result = value;
            else if (검사.카메라여부) result = value * (Decimal.ToDouble(검사.교정값) / 1000);
            else result = value;
            return (Decimal)Math.Round(result, Global.환경설정.결과자릿수);
        }
        private Double MeterToPixel(검사정보 검사, Decimal value)
        {
            if (검사.교정값 <= 0 || !검사.카메라여부) return Decimal.ToDouble(value);
            return Decimal.ToDouble(value) / Decimal.ToDouble(검사.교정값);
        }

        public Boolean SetResultValue_Client(검사정보 검사, Double value, out Decimal 결과값, out Decimal 측정값, Boolean 마진포함 = false)
        {
            Decimal result = (Decimal)value;
            Boolean r = result >= 검사.최소값 && result <= 검사.최대값;
            결과값 = result;
            측정값 = (Decimal)Math.Round(value, Global.환경설정.결과자릿수);
            if (r) return true;
            //if (검사.마진값 <= 0 || 마진포함) return false;

            //if (검사.최소값 > result)
            //{
            //    if (검사.최소값 > result + 검사.마진값 * 검사.결과부호) return false;
            //}
            //else if (검사.최대값 < result)
            //{
            //    if (검사.최대값 < result - 검사.마진값 * 검사.결과부호) return false;
            //}
            return false;
        }
        public Boolean SetResultValue(검사정보 검사, Double value, out Decimal 결과값, out Decimal 측정값, Boolean 마진포함 = false)
        {
            Decimal result = PixelToMeter(검사, value);
            result += 검사.보정값;
            result *= 검사.결과부호;
            Boolean r = result >= 검사.최소값 && result <= 검사.최대값;
            결과값 = result;
            측정값 = (Decimal)Math.Round(value, Global.환경설정.결과자릿수);
            if (r) return true;
            if (검사.마진값 <= 0 || 마진포함) return false;

            Int32 factor = 0;
            if (검사.최소값 > result)
            {
                if (검사.최소값 > result + 검사.마진값 * 검사.결과부호) return false;
                factor = 1;
            }
            else if (검사.최대값 < result)
            {
                if (검사.최대값 < result - 검사.마진값 * 검사.결과부호) return false;
                factor = -1;
            }
            Double value2 = value + MeterToPixel(검사, 검사.마진값) * factor;
            if (value2 == value) return false;

            Boolean r2 = SetResultValue(검사, value2, out Decimal 결과값2, out Decimal 측정값2, true);
            if (r2)
            {
                결과값 = 결과값2;
                측정값 = 측정값2;
                return true;
            }
            return false;
        }

        public 검사정보 SetResult(검사정보 검사, Double value)
        {
            if (검사 == null) return null;
            if (Double.IsNaN(value)) { 검사.측정결과 = 결과구분.ER; return 검사; }
            Boolean ok = SetResultValue(검사, value, out Decimal 결과값, out Decimal 측정값);
            검사.측정값 = 측정값;
            검사.결과값 = 결과값;
            검사.측정결과 = 결과구분.OK;//ok ? 결과구분.OK : 결과구분.NG;
            return 검사;
        }
        public 검사정보 SetResult(String name, Double value) => SetResult(검사내역.Where(e => e.검사항목.ToString() == name).FirstOrDefault(), value);
        public 검사정보 SetResult(검사항목 항목, Double value) => SetResult(검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault(), value);
        public void SetResults(카메라구분 카메라, Dictionary<String, Double> results)
        {
            foreach (KeyValuePair<String, Double> result in results)
            {
                검사정보 정보 = GetItem((장치구분)카메라, result.Key);
                if (정보 == null) continue;

                SetResult(정보, result.Value);
            }
        }

        //public void SetResults(카메라구분 카메라, Dictionary<String, Object> results)
        //{
        //    //불량영역제거(카메라);
        //    //String scratch = ResultAttribute.VarName(검사항목.BottomScratch);
        //    //String dent = ResultAttribute.VarName(검사항목.BottomDent);
        //    foreach (var result in results)
        //    {
        //        //if (result.Key.Equals(scratch) || result.Key.Equals(dent))
        //        //{
        //        //    this.표면불량.AddRange(result.Value as List<불량영역>);
        //        //    continue;
        //        //}
        //        검사정보 정보 = GetItem((장치구분)카메라, result.Key);
        //        if (정보 == null) continue;
        //        Double value = result.Value == null ? Double.NaN : (Double)result.Value;
        //        SetResult(정보, value);
        //    }
        //}
        public void SetResults(Dictionary<Int32, Decimal> 내역)
        {
            if (내역 == null) return;
            foreach (Int32 index in 내역.Keys)
            {
                검사항목 항목 = (검사항목)index;
                SetResult(항목, Convert.ToDouble(내역[index]));
            }
        }

        public 검사정보 SetValue(검사항목 항목, Double value) => SetValue(검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault(), value);
        //결과만 추가하도록 새롭게 추가
        public 검사정보 SetValue(검사정보 검사, Double value)
        {
            if (검사 == null) return null;
            if (Double.IsNaN(value)) { 검사.측정결과 = 결과구분.ER; return 검사; }
            Boolean ok = SetResultValue_Client(검사, value, out Decimal 결과값, out Decimal 측정값);
            검사.측정값 = 측정값;
            검사.결과값 = 결과값;
            검사.측정결과 = ok ? 결과구분.OK : 결과구분.NG;

            return 검사;
        }

        public void SetValues(Dictionary<Int32, Decimal> 내역)
        {
            if (내역 == null) return;
            foreach (Int32 index in 내역.Keys)
            {

                검사항목 항목 = (검사항목)index;
                SetValue(항목, Convert.ToDouble(내역[index]));
                Debug.WriteLine($"{항목}, {내역[index]}");
            }
        }

        public List<불량영역> 불량영역(카메라구분 카메라) => this.표면불량.Where(e => e.장치구분 == (장치구분)카메라).ToList();
        public void 불량영역제거(카메라구분 카메라)
        {
            List<불량영역> 불량 = 불량영역(카메라);
            불량.ForEach(e => this.표면불량.Remove(e));
        }

        private 결과구분 최종결과(List<결과구분> 결과목록)
        {
            if (결과목록.Contains(결과구분.ER)) return 결과구분.ER;
            if (결과목록.Contains(결과구분.NG)) return 결과구분.NG;
            return 결과구분.OK;
        }

        public 결과구분 결과계산()
        {
            Debug.WriteLine("결과계산들어옴");
            List<결과구분> 전체결과 = new List<결과구분>();
            List<결과구분> 품질결과 = new List<결과구분>();
            List<결과구분> 외관결과 = new List<결과구분>();
            foreach (검사정보 정보 in this.검사내역)
            {
                // 임시로 검사중인 항목 완료 처리
                if (정보.측정결과 < 결과구분.PS)
                {

                    this.SetResult(정보, Convert.ToDouble(정보.측정값));
                }

                if (정보.측정결과 == 결과구분.PS) continue;

                if (!전체결과.Contains(정보.측정결과))
                {
                    전체결과.Add(정보.측정결과);

                    //if (정보.검사장치 != 장치구분.QrReader) 마킹전결과목록.Add(정보.측정결과);

                }

                if (정보.검사그룹 == 검사그룹.CTQ) { if (!품질결과.Contains(정보.측정결과)) 품질결과.Add(정보.측정결과); }
                if (정보.검사그룹 == 검사그룹.Surface) {
                    if (!외관결과.Contains(정보.측정결과)) 외관결과.Add(정보.측정결과); 
                }
            }


            this.측정결과 = 최종결과(전체결과);
         
            if (this.측정결과 == 결과구분.OK)
            {
                this.CTQ결과 = 결과구분.OK;
                this.외관결과 = 결과구분.OK;
            }
            else
            {
                this.CTQ결과 = 최종결과(품질결과);
                this.외관결과 = 최종결과(외관결과);

                List<검사정보> 불량내역 = this.검사내역.Where(e => e.결과분류 == 결과분류.Summary && (e.측정결과 == 결과구분.NG || e.측정결과 == 결과구분.ER)).ToList();
                
                if (불량내역.Count > 0)
                {
                    foreach (검사정보 정보 in 불량내역)
                        this.불량내역.Add(정보.검사항목.ToString());
                }
                
                this.불량정보 = String.Join(",", this.불량내역.ToArray());
                this.불량내역.Clear();
            }

            Debug.WriteLine($"{this.검사코드} = {this.측정결과}", "검사완료");
            Debug.WriteLine($"{this.검사코드} = {this.마킹전결과}", "마킹전검사완료");
            
            return this.측정결과;
        }

        

        

        static double DistanceFromPointToPlane(double x0, double y0, double z0, double a, double b, double c, double d)
        {
            // 직선 거리(방향성 포함) 공식 적용
            double numerator = a * x0 + b * y0 + c * z0 + d;
            double denominator = Math.Sqrt(a * a + b * b + c * c);
            double distance = numerator / denominator;
            return distance;
        }

        public static double[] FitPlaneLeastSquares(double[,] points)
        {
            int numPoints = points.GetLength(0);
            double[,] A = new double[numPoints, 3];
            double[] b = new double[numPoints];

            for (int i = 0; i < numPoints; i++)
            {
                A[i, 0] = points[i, 0];
                A[i, 1] = points[i, 1];
                A[i, 2] = 1;
                b[i] = points[i, 2];
            }

            var matrixA = Matrix.Build.DenseOfArray(A);
            var vectorB = DenseVector.OfArray(b);
            var solution = matrixA.PseudoInverse().Multiply(vectorB);

            double a = solution[0];
            double b_coef = solution[1];
            double d = solution[2];
            double c = -1.0;  // We solve for ax + by - z + d = 0

            return new double[] { a, b_coef, c, d };
        }

        public Boolean 카메라검사보기(검사정보 정보)
        {
            try
            {
                if (this.검사코드 >= 9999 || this.검사코드 < 1 || 정보 == null || !CameraAttribute.IsCamera(정보.검사장치)) return false;
                카메라구분 카메라 = (카메라구분)정보.검사장치;
                String file = Global.사진자료.CopyImageFile(this.검사일시, this.검사코드, 카메라, false);
                if (String.IsNullOrEmpty(file) || !File.Exists(file))
                    return Utils.WarningMsg("The image file does not exist.");
                CogToolEdit cogToolEdit = new CogToolEdit() { 사진파일 = file };
                cogToolEdit.Init(Global.비전검사[카메라]);
                cogToolEdit.Show(Global.MainForm);
                return true;
            }
            catch (Exception ex) { Utils.ErrorMsg(ex.Message); }
            return false;
        }
    }
}
