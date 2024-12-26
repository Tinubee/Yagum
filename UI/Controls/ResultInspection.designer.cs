namespace DSEV.UI.Controls
{
    partial class ResultInspection
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultInspection));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.e결과뷰어 = new DSEV.UI.Controls.Viewport3D();
            this.e외관결과 = new DevExpress.XtraEditors.TextEdit();
            this.Bind검사결과 = new System.Windows.Forms.BindingSource(this.components);
            this.eCTQ결과 = new DevExpress.XtraEditors.TextEdit();
            this.e검사순번 = new DevExpress.XtraEditors.TextEdit();
            this.e측정결과 = new DevExpress.XtraEditors.LabelControl();
            this.e검사시간 = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.e뷰어 = new Cogutils.RecordDisplay();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.b스냅 = new DevExpress.XtraBars.BarButtonItem();
            this.b영상 = new DevExpress.XtraBars.BarCheckItem();
            this.b조명 = new DevExpress.XtraBars.BarCheckItem();
            this.b비전 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.b카메라명 = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e외관결과.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사결과)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eCTQ결과.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e검사순번.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e검사시간.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e뷰어)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AutoScroll = false;
            this.layoutControl1.Controls.Add(this.e결과뷰어);
            this.layoutControl1.Controls.Add(this.e외관결과);
            this.layoutControl1.Controls.Add(this.eCTQ결과);
            this.layoutControl1.Controls.Add(this.e검사순번);
            this.layoutControl1.Controls.Add(this.e측정결과);
            this.layoutControl1.Controls.Add(this.e검사시간);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(924, 36);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.AlwaysScrollActiveControlIntoView = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(996, 864);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // e결과뷰어
            // 
            this.e결과뷰어.Location = new System.Drawing.Point(6, 96);
            this.e결과뷰어.Name = "e결과뷰어";
            this.e결과뷰어.Size = new System.Drawing.Size(984, 762);
            this.e결과뷰어.TabIndex = 1;
            // 
            // e외관결과
            // 
            this.e외관결과.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.Bind검사결과, "외관문구", true));
            this.e외관결과.EditValue = "-";
            this.e외관결과.EnterMoveNextControl = true;
            this.e외관결과.Location = new System.Drawing.Point(368, 54);
            this.e외관결과.Name = "e외관결과";
            this.e외관결과.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e외관결과.Properties.Appearance.Options.UseFont = true;
            this.e외관결과.Properties.Appearance.Options.UseTextOptions = true;
            this.e외관결과.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e외관결과.Properties.ReadOnly = true;
            this.e외관결과.Size = new System.Drawing.Size(114, 32);
            this.e외관결과.StyleController = this.layoutControl1;
            this.e외관결과.TabIndex = 9;
            // 
            // Bind검사결과
            // 
            this.Bind검사결과.DataSource = typeof(DSEV.Schemas.검사결과);
            // 
            // eCTQ결과
            // 
            this.eCTQ결과.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.Bind검사결과, "품질문구", true));
            this.eCTQ결과.EditValue = "-";
            this.eCTQ결과.EnterMoveNextControl = true;
            this.eCTQ결과.Location = new System.Drawing.Point(368, 10);
            this.eCTQ결과.Name = "eCTQ결과";
            this.eCTQ결과.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.eCTQ결과.Properties.Appearance.Options.UseFont = true;
            this.eCTQ결과.Properties.Appearance.Options.UseTextOptions = true;
            this.eCTQ결과.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.eCTQ결과.Properties.ReadOnly = true;
            this.eCTQ결과.Size = new System.Drawing.Size(114, 32);
            this.eCTQ결과.StyleController = this.layoutControl1;
            this.eCTQ결과.TabIndex = 8;
            // 
            // e검사순번
            // 
            this.e검사순번.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.Bind검사결과, "검사코드", true));
            this.e검사순번.EditValue = 0;
            this.e검사순번.EnterMoveNextControl = true;
            this.e검사순번.Location = new System.Drawing.Point(572, 54);
            this.e검사순번.Name = "e검사순번";
            this.e검사순번.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e검사순번.Properties.Appearance.Options.UseFont = true;
            this.e검사순번.Properties.Appearance.Options.UseTextOptions = true;
            this.e검사순번.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e검사순번.Properties.DisplayFormat.FormatString = "d4";
            this.e검사순번.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.e검사순번.Properties.EditFormat.FormatString = "d4";
            this.e검사순번.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.e검사순번.Properties.ReadOnly = true;
            this.e검사순번.Size = new System.Drawing.Size(414, 32);
            this.e검사순번.StyleController = this.layoutControl1;
            this.e검사순번.TabIndex = 1;
            // 
            // e측정결과
            // 
            this.e측정결과.Appearance.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e측정결과.Appearance.Options.UseFont = true;
            this.e측정결과.Appearance.Options.UseTextOptions = true;
            this.e측정결과.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e측정결과.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.e측정결과.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Bind검사결과, "결과문구", true));
            this.e측정결과.Location = new System.Drawing.Point(6, 6);
            this.e측정결과.MinimumSize = new System.Drawing.Size(0, 50);
            this.e측정결과.Name = "e측정결과";
            this.e측정결과.Size = new System.Drawing.Size(276, 86);
            this.e측정결과.StyleController = this.layoutControl1;
            this.e측정결과.TabIndex = 2;
            this.e측정결과.Text = "Waiting";
            // 
            // e검사시간
            // 
            this.e검사시간.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.Bind검사결과, "검사일시", true));
            this.e검사시간.EnterMoveNextControl = true;
            this.e검사시간.Location = new System.Drawing.Point(572, 10);
            this.e검사시간.Name = "e검사시간";
            this.e검사시간.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e검사시간.Properties.Appearance.Options.UseFont = true;
            this.e검사시간.Properties.Appearance.Options.UseTextOptions = true;
            this.e검사시간.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e검사시간.Properties.DisplayFormat.FormatString = "{0:yyyy-MM-dd HH:mm:ss}";
            this.e검사시간.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.e검사시간.Properties.EditFormat.FormatString = "{0:yyyy-MM-dd HH:mm:ss}";
            this.e검사시간.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.e검사시간.Properties.ReadOnly = true;
            this.e검사시간.Size = new System.Drawing.Size(414, 32);
            this.e검사시간.StyleController = this.layoutControl1;
            this.e검사시간.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.Root.Size = new System.Drawing.Size(996, 864);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.e측정결과;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(280, 90);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(280, 90);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(280, 90);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.e검사순번;
            this.layoutControlItem4.Location = new System.Drawing.Point(484, 44);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(504, 46);
            this.layoutControlItem4.Text = "Index";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(66, 25);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.eCTQ결과;
            this.layoutControlItem6.Location = new System.Drawing.Point(280, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(204, 44);
            this.layoutControlItem6.Text = "CTQ";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(66, 25);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.e외관결과;
            this.layoutControlItem7.Location = new System.Drawing.Point(280, 44);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem7.Size = new System.Drawing.Size(204, 46);
            this.layoutControlItem7.Text = "Surface";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(66, 25);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.e결과뷰어;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(988, 766);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.e검사시간;
            this.layoutControlItem2.Location = new System.Drawing.Point(484, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(504, 44);
            this.layoutControlItem2.Text = "Time";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(66, 25);
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.FloatSize = new System.Drawing.Size(793, 857);
            this.dockPanel1.ID = new System.Guid("81df941b-8b06-4b13-9a7e-2c01f5ee4a1b");
            this.dockPanel1.Location = new System.Drawing.Point(0, 36);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(924, 200);
            this.dockPanel1.Size = new System.Drawing.Size(924, 864);
            this.dockPanel1.Text = "Cam Live";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.e뷰어);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 30);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(917, 831);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // e뷰어
            // 
            this.e뷰어.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.e뷰어.ColorMapLowerRoiLimit = 0D;
            this.e뷰어.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.e뷰어.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.e뷰어.ColorMapUpperRoiLimit = 1D;
            this.e뷰어.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e뷰어.DoubleTapZoomCycleLength = 2;
            this.e뷰어.DoubleTapZoomSensitivity = 2.5D;
            this.e뷰어.Location = new System.Drawing.Point(0, 0);
            this.e뷰어.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.e뷰어.MouseWheelSensitivity = 1D;
            this.e뷰어.Name = "e뷰어";
            this.e뷰어.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("e뷰어.OcxState")));
            this.e뷰어.Size = new System.Drawing.Size(917, 831);
            this.e뷰어.TabIndex = 10;
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.b스냅,
            this.b영상,
            this.b조명,
            this.b비전});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = ((DevExpress.XtraBars.BarCanDockStyle)((DevExpress.XtraBars.BarCanDockStyle.Top | DevExpress.XtraBars.BarCanDockStyle.Bottom)));
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.b스냅),
            new DevExpress.XtraBars.LinkPersistInfo(this.b영상),
            new DevExpress.XtraBars.LinkPersistInfo(this.b조명)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // b스냅
            // 
            this.b스냅.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.b스냅.Caption = "Snapshot";
            this.b스냅.Hint = "Snapshot";
            this.b스냅.Id = 1;
            this.b스냅.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b스냅.ImageOptions.SvgImage")));
            this.b스냅.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.b스냅.Name = "b스냅";
            // 
            // b영상
            // 
            this.b영상.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.b영상.Caption = "Video";
            this.b영상.Hint = "Video";
            this.b영상.Id = 3;
            this.b영상.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b영상.ImageOptions.SvgImage")));
            this.b영상.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.b영상.Name = "b영상";
            // 
            // b조명
            // 
            this.b조명.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.b조명.Caption = "Lights";
            this.b조명.Hint = "Lights";
            this.b조명.Id = 2;
            this.b조명.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b조명.ImageOptions.SvgImage")));
            this.b조명.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.b조명.Name = "b조명";
            // 
            // b비전
            // 
            this.b비전.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.b비전.Caption = "Settings";
            this.b비전.Hint = "Inspection Settings";
            this.b비전.Id = 4;
            this.b비전.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b비전.ImageOptions.SvgImage")));
            this.b비전.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.b비전.Name = "b비전";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1920, 36);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 900);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1920, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 36);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 864);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl1.Location = new System.Drawing.Point(1920, 36);
            this.barDockControl1.Manager = this.barManager1;
            this.barDockControl1.Size = new System.Drawing.Size(0, 864);
            // 
            // b카메라명
            // 
            this.b카메라명.Caption = "Camera";
            this.b카메라명.Id = 0;
            this.b카메라명.Name = "b카메라명";
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1920, 36);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 864);
            // 
            // ResultInspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ResultInspection";
            this.Size = new System.Drawing.Size(1920, 900);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e외관결과.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사결과)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eCTQ결과.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e검사순번.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e검사시간.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e뷰어)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource Bind검사결과;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit e검사시간;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl e측정결과;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit e검사순번;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.TextEdit e외관결과;
        private DevExpress.XtraEditors.TextEdit eCTQ결과;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private Viewport3D e결과뷰어;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private Cogutils.RecordDisplay e뷰어;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem b스냅;
        private DevExpress.XtraBars.BarCheckItem b영상;
        private DevExpress.XtraBars.BarCheckItem b조명;
        private DevExpress.XtraBars.BarButtonItem b비전;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarStaticItem b카메라명;
    }
}
