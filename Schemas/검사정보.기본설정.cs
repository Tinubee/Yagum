using Cognex.VisionPro;
using DSEV.Schemas;
using MvUtils;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Cont;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DSEV.Schemas
{
    public enum 카메라구분
    {
        [ListBindable(false)]
        None = 0,

        [Description("Cam1(Top Camera)")]
        Cam01 = 1,
    }

    // 카메라구분 과 번호 맞춤
    public enum 장치구분
    {
        [Description("None"), Camera(false)]
        None = 0,
        [Description("Cam01"), Camera(true)]
        Cam01 = 카메라구분.Cam01, 
    }

    public enum 결과분류
    {
        None,
        Summary,
        Detail,
    }

    public enum 검사그룹
    {
        [Description("None"), Translation("None", "없음")]
        None,
        [Description("CTQ"), Translation("CTQ")]
        CTQ,
        [Description("Surface"), Translation("Surface", "외관검사")]
        Surface,
        [Description("Etc"), Translation("Etc", "기타")]
        Etc,
    }


    public enum 검사항목 : Int32
    {
        [Result(), ListBindable(false)]
        None,
        [Result(피씨구분.Server, 검사그룹.CTQ, 결과분류.Summary,장치구분.Cam01, None, "Point01")]
        Point01 = 101,
        [Result(피씨구분.Server,검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, None, "Point02")]
        Point02 = 102,
        [Result(피씨구분.Server,검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, None, "Point03")]
        Point03 = 103,
        [Result(피씨구분.Server,검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, None, "Point04")]
        Point04 = 104,
        [Result(피씨구분.Server, 검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, None, "Point05")]
        Point05 = 105,
        [Result(피씨구분.Server, 검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, None, "Point06")]
        Point06 = 106,
    }

    public enum 단위구분
    {
        [Description("mm")]
        mm = 0,
        [Description("OK/NG")]
        ON = 1,
        [Description("EA")]
        EA = 2,
        [Description("Grade")]
        GA = 3,
    }

    public enum 결과구분
    {
        [Description("Waiting"), Translation("Waiting", "대기중")]
        WA = 0,
        //[Description("Measuring"), Translation("Measuring", "검사중")]
        //ME = 1,
        [Description("PS"), Translation("Pass", "통과")]
        PS = 2,
        [Description("ER"), Translation("Error", "오류")]
        ER = 3,
        [Description("NG"), Translation("NG", "불량")]
        NG = 5,
        [Description("OK"), Translation("OK", "양품")]
        OK = 7,
    }

    [Table("inspd")]
    public class 검사정보
    {
        [Column("idwdt", Order = 0), Required, Key, JsonProperty("idwdt"), Translation("Time", "검사일시")]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [NotMapped, JsonProperty("idnam"), Translation("Name", "명칭")]
        public String 검사명칭 { get; set; } = String.Empty;
        [Column("iditm", Order = 1), Required, Key, JsonProperty("iditm"), Translation("Item", "검사항목")]
        public 검사항목 검사항목 { get; set; } = 검사항목.None;
        [Column("idgrp"), JsonProperty("idgrp"), Translation("Group", "검사그룹")]
        public 검사그룹 검사그룹 { get; set; } = 검사그룹.None;
        [Column("iddev"), JsonProperty("iddev"), Translation("Device", "검사장치")]
        public 장치구분 검사장치 { get; set; } = 장치구분.None;
        [Column("idcat"), JsonProperty("idcat"), Translation("Category", "결과유형")]
        public 결과분류 결과분류 { get; set; } = 결과분류.None;
        [Column("iduni"), JsonProperty("iduni"), Translation("Unit", "단위"), BatchEdit(true)]
        public 단위구분 측정단위 { get; set; } = 단위구분.mm;
        [Column("idstd"), JsonProperty("idstd"), Translation("Norminal", "기준값"), BatchEdit(true)]
        public Decimal 기준값 { get; set; } = 0m;
        [Column("idmin"), JsonProperty("idmin"), Translation("Min", "최소값"), BatchEdit(true)]
        public Decimal 최소값 { get; set; } = 0m;
        [Column("idmax"), JsonProperty("idmax"), Translation("Max", "최대값"), BatchEdit(true)]
        public Decimal 최대값 { get; set; } = 0m;
        [Column("idoff"), JsonProperty("idoff"), Translation("Offset", "보정값"), BatchEdit(true)]
        public Decimal 보정값 { get; set; } = 0m;
        [Column("idcal"), JsonProperty("idcal"), Translation("Calib(µm)", "교정(µm)"), BatchEdit(true)]
        public Decimal 교정값 { get; set; } = 0m;
        [Column("idmes"), JsonProperty("idmes"), Translation("Measure", "측정값")]
        public Decimal 측정값 { get; set; } = 0m;
        [Column("idval"), JsonProperty("idval"), Translation("Value", "결과값")]
        public Decimal 결과값 { get; set; } = 0m;
        [NotMapped, JsonProperty("idrel"), Translation("Real", "실측값")]
        public Decimal 실측값 { get; set; } = 0m;
        [Column("idres"), JsonProperty("idres"), Translation("Result", "판정")]
        public 결과구분 측정결과 { get; set; } = 결과구분.WA;
        [NotMapped, JsonProperty("idmag"), Translation("Margin"), BatchEdit(true)]
        public Decimal 마진값 { get; set; } = 0m;
        [NotMapped, JsonProperty("iduse"), Translation("Used", "검사"), BatchEdit(true)]
        public Boolean 검사여부 { get; set; } = true;

        [NotMapped, JsonIgnore]
        public Double 검사시간 = 0;
        [NotMapped, JsonIgnore]
        public String 변수명칭 = String.Empty;
        [NotMapped, JsonIgnore]
        public Boolean 카메라여부 = false;
        [NotMapped, JsonIgnore]
        public 검사항목 결과항목 = 검사항목.None;
        [NotMapped, JsonIgnore]
        public Int32 결과부호 = 1;

        public 검사정보() { }
        public 검사정보(검사정보 정보) { this.Set(정보); }

        public void Init()
        {

            this.카메라여부 = CameraAttribute.IsCamera(this.검사장치);
            ResultAttribute a = Utils.GetAttribute<ResultAttribute>(this.검사항목);

            //if (a == null) return;

            this.변수명칭 = a.변수명칭;
            this.결과항목 = a.결과항목;
            this.결과부호 = a.결과부호;
        }

        public void Reset(DateTime? 일시 = null)
        {
            if (일시 != null) this.검사일시 = (DateTime)일시;
            this.측정값 = 0;
            this.결과값 = 0;
            this.측정결과 = 결과구분.WA;
        }
        public void Set(검사정보 정보)
        {
            if (정보 == null) return;
            foreach (PropertyInfo p in typeof(검사정보).GetProperties())
            {
                if (!p.CanWrite) continue;
                Object v = p.GetValue(정보);
                if (v == null) continue;
                p.SetValue(this, v);
            }
            this.Reset(null);
            this.Init();
        }

        public Boolean 교정계산()
        {
            if (this.측정값 <= 0) return false;
            this.교정값 = Convert.ToDecimal(Math.Round((this.실측값 - this.보정값) / this.측정값 * 1000, 9));
            return true;
        }

        public Boolean 교정계산2()
        {
            if(this.실측값 <= 0) return false; 
            if (this.측정값 <= 0) return false;
            if(this.검사여부) this.교정값 = Convert.ToDecimal(Math.Round((this.실측값 / this.측정값 * 1000), 9));
            return true;
        }

        public 결과구분 결과계산()
        {
            Boolean ok = this.결과값 >= this.최소값 && this.결과값 <= this.최대값;
            this.측정결과 = ok ? 결과구분.OK : 결과구분.NG;
            return this.측정결과;
        }

        public String DisplayText(Decimal value)
        {
            if (this.측정단위 == 단위구분.EA) return Utils.FormatNumeric(value);
            if (this.측정단위 == 단위구분.ON) return value == 1 ? "OK" : "NG";
            return String.Empty;
        }

        private String[] AppearanceFields = new String[] { nameof(측정결과), nameof(최소값), nameof(최대값), nameof(기준값), nameof(결과값) };
        public void SetAppearance(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e == null || !AppearanceFields.Contains(e.Column.FieldName)) return;
            PropertyInfo p = typeof(검사정보).GetProperty(e.Column.FieldName);
            if (p.Name == nameof(결과값) || p.Name == nameof(측정결과))
                e.Appearance.ForeColor = 환경설정.ResultColor(this.측정결과);
            if (p.PropertyType != typeof(Decimal)) return;
            Object v = p.GetValue(this);
            if (v == null) return;
            String display = DisplayText((Decimal)v);
            if (!String.IsNullOrEmpty(display)) e.DisplayText = display;
        }
    }

    [Table("insuf")]
    public class 불량영역
    {
        [Column("iswdt"), Required, Key, JsonProperty("iswdt"), Translation("Time", "일시")]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [Column("isdev"), JsonProperty("isdev"), Translation("Model", "모델")]
        public 장치구분 장치구분 { get; set; } = 장치구분.None;
        [Column("isitm"), JsonProperty("isitm"), Translation("Item", "검사항목")]
        public 검사항목 검사항목 { get; set; } = 검사항목.None;
        [Column("islef"), JsonProperty("islef"), Translation("X", "X")]
        public Double 가로중심 { get; set; } = 0;
        [Column("istop"), JsonProperty("istop"), Translation("Y", "Y")]
        public Double 세로중심 { get; set; } = 0;
        [Column("iswid"), JsonProperty("iswid"), Translation("Width", "Width")]
        public Double 가로길이 { get; set; } = 0;
        [Column("ishei"), JsonProperty("ishei"), Translation("Height", "Height")]
        public Double 세로길이 { get; set; } = 0;
        [Column("isrot"), JsonProperty("isrot"), Translation("Rotation", "Rotation")]
        public Double 회전각도 { get; set; } = 0;
        [Column("isske"), JsonProperty("isske"), Translation("Skew", "Skew")]
        public Double 기울임 { get; set; } = 0;

        public 불량영역() { }
        public 불량영역(카메라구분 카메라, 검사항목 항목, CogRectangleAffine 영역)
        {
            장치구분 = (장치구분)카메라;
            검사항목 = 항목;
            가로중심 = 영역.CenterX;
            세로중심 = 영역.CenterY;
            가로길이 = 영역.SideXLength;
            세로길이 = 영역.SideYLength;
            회전각도 = 영역.Rotation;
            기울임 = 영역.Skew;
        }
        public 불량영역(카메라구분 카메라, 검사항목 항목, List<Double> r)
        {
            장치구분 = (장치구분)카메라;
            검사항목 = 항목;
            가로중심 = r[0];
            세로중심 = r[1];
            가로길이 = r[2];
            세로길이 = r[3];
            회전각도 = r[4];
            기울임 = r[5];
        }
        public CogRectangleAffine GetRectangleAffine() => new CogRectangleAffine() { CenterX = 가로중심, CenterY = 세로중심, SideXLength = 가로길이, SideYLength = 세로길이, Rotation = 회전각도 };
        public CogRectangleAffine GetRectangleAffine(CogColorConstants color) { var r = GetRectangleAffine(); r.Color = color; return r; }
        public CogRectangleAffine GetRectangleAffine(CogColorConstants color, Int32 lineWidth) { var r = GetRectangleAffine(color); r.LineWidthInScreenPixels = lineWidth; return r; }
    }

    #region Attributes
    public class CameraAttribute : Attribute
    {
        public Boolean Whether = true;
        public CameraAttribute(Boolean cam) { Whether = cam; }

        public static Boolean IsCamera(장치구분 구분)
        {
            CameraAttribute a = Utils.GetAttribute<CameraAttribute>(구분);
            if (a == null) return false;
            return a.Whether;
        }
    }

    public class ResultAttribute : Attribute
    {
        public 피씨구분 피씨구분 = 피씨구분.Server;
        public 검사그룹 검사그룹 = 검사그룹.None;
        public 결과분류 결과분류 = 결과분류.None;
        public 장치구분 장치구분 = 장치구분.None;
        public 검사항목 결과항목 = 검사항목.None;
        public String 변수명칭 = String.Empty;
        public Int32 결과부호 = 1;
        public ResultAttribute() { }
        public ResultAttribute(피씨구분 피씨, 검사그룹 그룹, 결과분류 결과) { 피씨구분 = 피씨; 검사그룹 = 그룹; 결과분류 = 결과; }
        public ResultAttribute(피씨구분 피씨, 검사그룹 그룹, 결과분류 결과, 장치구분 장치) { 피씨구분 = 피씨; 검사그룹 = 그룹; 결과분류 = 결과; 장치구분 = 장치; }
        public ResultAttribute(피씨구분 피씨, 검사그룹 그룹, 결과분류 결과, 장치구분 장치, 검사항목 항목) { 피씨구분 = 피씨; 검사그룹 = 그룹; 결과분류 = 결과; 장치구분 = 장치; 결과항목 = 항목; }
        public ResultAttribute(피씨구분 피씨, 검사그룹 그룹, 결과분류 결과, 장치구분 장치, 검사항목 항목, String 변수) { 피씨구분 = 피씨; 검사그룹 = 그룹; 결과분류 = 결과; 장치구분 = 장치; 결과항목 = 항목; 변수명칭 = 변수; }
        public ResultAttribute(피씨구분 피씨, 검사그룹 그룹, 결과분류 결과, 장치구분 장치, 검사항목 항목, String 변수, Int32 부호) { 피씨구분 = 피씨; 검사그룹 = 그룹; 결과분류 = 결과; 장치구분 = 장치; 결과항목 = 항목; 변수명칭 = 변수; 결과부호 = 부호; }

        public static String VarName(검사항목 항목)
        {
            ResultAttribute a = Utils.GetAttribute<ResultAttribute>(항목);
            if (a == null) return String.Empty;
            return a.변수명칭;
        }
        public static Int32 ValueFactor(검사항목 항목)
        {
            ResultAttribute a = Utils.GetAttribute<ResultAttribute>(항목);
            if (a == null) return 1;
            return a.결과부호;
        }
    }
    #endregion
}