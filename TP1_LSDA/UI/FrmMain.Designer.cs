namespace TP1_LSDA.UI
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();
            this.tabLieux = new System.Windows.Forms.TabPage();
            this.tabPersonnages = new System.Windows.Forms.TabPage();
            this.tabMissions = new System.Windows.Forms.TabPage();
            this.tabControlPrincipal.SuspendLayout();
            this.SuspendLayout();
            //
            // tabControlPrincipal
            //
            this.tabControlPrincipal.Controls.Add(this.tabLieux);
            this.tabControlPrincipal.Controls.Add(this.tabPersonnages);
            this.tabControlPrincipal.Controls.Add(this.tabMissions);
            this.tabControlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPrincipal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tabControlPrincipal.Name = "tabControlPrincipal";
            this.tabControlPrincipal.SelectedIndex = 0;
            this.tabControlPrincipal.Size = new System.Drawing.Size(1200, 780);
            this.tabControlPrincipal.TabIndex = 0;
            this.tabControlPrincipal.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlPrincipal_Selected);
            this.tabControlPrincipal.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlPrincipal_Deselecting);
            //
            // tabLieux
            //
            this.tabLieux.Location = new System.Drawing.Point(4, 26);
            this.tabLieux.Name = "tabLieux";
            this.tabLieux.Padding = new System.Windows.Forms.Padding(3);
            this.tabLieux.Size = new System.Drawing.Size(1192, 750);
            this.tabLieux.TabIndex = 0;
            this.tabLieux.Text = "Lieux";
            this.tabLieux.UseVisualStyleBackColor = true;
            //
            // tabPersonnages
            //
            this.tabPersonnages.Location = new System.Drawing.Point(4, 26);
            this.tabPersonnages.Name = "tabPersonnages";
            this.tabPersonnages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPersonnages.Size = new System.Drawing.Size(1192, 750);
            this.tabPersonnages.TabIndex = 1;
            this.tabPersonnages.Text = "Personnages";
            this.tabPersonnages.UseVisualStyleBackColor = true;
            //
            // tabMissions
            //
            this.tabMissions.Location = new System.Drawing.Point(4, 26);
            this.tabMissions.Name = "tabMissions";
            this.tabMissions.Padding = new System.Windows.Forms.Padding(3);
            this.tabMissions.Size = new System.Drawing.Size(1192, 750);
            this.tabMissions.TabIndex = 2;
            this.tabMissions.Text = "Missions";
            this.tabMissions.UseVisualStyleBackColor = true;
            //
            // FrmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 780);
            this.Controls.Add(this.tabControlPrincipal);
            this.MinimumSize = new System.Drawing.Size(1100, 760);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Haut Conseil — Réservations de lieux de la Terre du Milieu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.tabControlPrincipal.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPrincipal;
        private System.Windows.Forms.TabPage tabLieux;
        private System.Windows.Forms.TabPage tabPersonnages;
        private System.Windows.Forms.TabPage tabMissions;
    }
}
