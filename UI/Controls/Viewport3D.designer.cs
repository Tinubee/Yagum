namespace DSEV.UI.Controls
{
    partial class Viewport3D
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
            this.VM뷰어 = new VMControls.Winform.Release.VmRenderControl();
            this.SuspendLayout();
            // 
            // VM뷰어
            // 
            this.VM뷰어.BackColor = System.Drawing.Color.Black;
            this.VM뷰어.CoordinateInfoVisible = true;
            this.VM뷰어.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VM뷰어.ImageSource = null;
            this.VM뷰어.IsShowCustomROIMenu = false;
            this.VM뷰어.Location = new System.Drawing.Point(0, 0);
            this.VM뷰어.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VM뷰어.ModuleSource = null;
            this.VM뷰어.Name = "VM뷰어";
            this.VM뷰어.Size = new System.Drawing.Size(783, 575);
            this.VM뷰어.TabIndex = 0;
            // 
            // Viewport3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VM뷰어);
            this.Name = "Viewport3D";
            this.Size = new System.Drawing.Size(783, 575);
            this.ResumeLayout(false);

        }

        #endregion

        private VMControls.Winform.Release.VmRenderControl VM뷰어;
    }
}
