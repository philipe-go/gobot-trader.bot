namespace Pombot_UI
{
    partial class PomBotApp
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PomBotApp));
            this.winNameLB = new System.Windows.Forms.Label();
            this.formNamePN = new System.Windows.Forms.Panel();
            this.closeBT = new System.Windows.Forms.Button();
            this.formNamePN.SuspendLayout();
            this.SuspendLayout();
            // 
            // winNameLB
            // 
            resources.ApplyResources(this.winNameLB, "winNameLB");
            this.winNameLB.Name = "winNameLB";
            // 
            // formNamePN
            // 
            this.formNamePN.BackColor = System.Drawing.Color.Gray;
            this.formNamePN.Controls.Add(this.winNameLB);
            this.formNamePN.Controls.Add(this.closeBT);
            resources.ApplyResources(this.formNamePN, "formNamePN");
            this.formNamePN.Name = "formNamePN";
            // 
            // closeBT
            // 
            this.closeBT.BackColor = System.Drawing.Color.Transparent;
            this.closeBT.BackgroundImage = global::Pombot_UI.Properties.Resources.close;
            resources.ApplyResources(this.closeBT, "closeBT");
            this.closeBT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closeBT.FlatAppearance.BorderSize = 0;
            this.closeBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeBT.ForeColor = System.Drawing.Color.White;
            this.closeBT.Name = "closeBT";
            this.closeBT.TabStop = false;
            this.closeBT.UseVisualStyleBackColor = false;
            this.closeBT.Click += new System.EventHandler(this.closeBT_Click);
            // 
            // PomBotApp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.formNamePN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PomBotApp";
            this.formNamePN.ResumeLayout(false);
            this.formNamePN.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label winNameLB;
        private System.Windows.Forms.Button closeBT;
        private System.Windows.Forms.Panel formNamePN;
    }
}