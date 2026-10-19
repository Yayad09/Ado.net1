namespace TP1_LSDA.UI
{
    partial class FrmPersonnages
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
            this.components = new System.ComponentModel.Container();
            this.dgvPersonnages = new System.Windows.Forms.DataGridView();
            this.pnlDroite = new System.Windows.Forms.Panel();
            this.bsPersonnages = new System.Windows.Forms.BindingSource(this.components);
            this.grpEdition = new System.Windows.Forms.GroupBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.lblRace = new System.Windows.Forms.Label();
            this.txtRace = new System.Windows.Forms.TextBox();
            this.lblFaction = new System.Windows.Forms.Label();
            this.txtFaction = new System.Windows.Forms.TextBox();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnSauvegarder = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.lblAide = new System.Windows.Forms.Label();
            this.lblStatut = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersonnages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPersonnages)).BeginInit();
            this.pnlDroite.SuspendLayout();
            this.grpEdition.SuspendLayout();
            this.SuspendLayout();
            //
            // dgvPersonnages
            //
            this.dgvPersonnages.AllowUserToAddRows = false;
            this.dgvPersonnages.AllowUserToDeleteRows = false;
            this.dgvPersonnages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPersonnages.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPersonnages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPersonnages.MultiSelect = false;
            this.dgvPersonnages.Name = "dgvPersonnages";
            this.dgvPersonnages.ReadOnly = true;
            this.dgvPersonnages.RowHeadersWidth = 30;
            this.dgvPersonnages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPersonnages.TabIndex = 0;
            this.dgvPersonnages.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvPersonnages_DataError);
            //
            // grpEdition
            //
            this.grpEdition.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpEdition.Controls.Add(this.lblNom);
            this.grpEdition.Controls.Add(this.txtNom);
            this.grpEdition.Controls.Add(this.lblRace);
            this.grpEdition.Controls.Add(this.txtRace);
            this.grpEdition.Controls.Add(this.lblFaction);
            this.grpEdition.Controls.Add(this.txtFaction);
            this.grpEdition.Controls.Add(this.btnAjouter);
            this.grpEdition.Controls.Add(this.btnSauvegarder);
            this.grpEdition.Controls.Add(this.btnSupprimer);
            this.grpEdition.Controls.Add(this.btnAnnuler);
            this.grpEdition.Name = "grpEdition";
            this.grpEdition.Size = new System.Drawing.Size(370, 300);
            this.grpEdition.TabIndex = 1;
            this.grpEdition.TabStop = false;
            this.grpEdition.Text = "Édition du personnage sélectionné";
            //
            // lblNom
            //
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(15, 35);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(90, 19);
            this.lblNom.TabIndex = 0;
            this.lblNom.Text = "Nom :";
            //
            // txtNom
            //
            this.txtNom.Location = new System.Drawing.Point(130, 32);
            this.txtNom.MaxLength = 100;
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(220, 25);
            this.txtNom.TabIndex = 1;
            //
            // lblRace
            //
            this.lblRace.AutoSize = true;
            this.lblRace.Location = new System.Drawing.Point(15, 75);
            this.lblRace.Name = "lblRace";
            this.lblRace.Size = new System.Drawing.Size(70, 19);
            this.lblRace.TabIndex = 2;
            this.lblRace.Text = "Race :";
            //
            // txtRace
            //
            this.txtRace.Location = new System.Drawing.Point(130, 72);
            this.txtRace.MaxLength = 50;
            this.txtRace.Name = "txtRace";
            this.txtRace.Size = new System.Drawing.Size(220, 25);
            this.txtRace.TabIndex = 3;
            //
            // lblFaction
            //
            this.lblFaction.AutoSize = true;
            this.lblFaction.Location = new System.Drawing.Point(15, 115);
            this.lblFaction.Name = "lblFaction";
            this.lblFaction.Size = new System.Drawing.Size(110, 19);
            this.lblFaction.TabIndex = 4;
            this.lblFaction.Text = "Faction :";
            //
            // txtFaction
            //
            this.txtFaction.Location = new System.Drawing.Point(130, 112);
            this.txtFaction.MaxLength = 50;
            this.txtFaction.Name = "txtFaction";
            this.txtFaction.Size = new System.Drawing.Size(220, 25);
            this.txtFaction.TabIndex = 5;
            //
            // btnAjouter
            //
            this.btnAjouter.Location = new System.Drawing.Point(15, 165);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(165, 40);
            this.btnAjouter.TabIndex = 6;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            //
            // btnSauvegarder
            //
            this.btnSauvegarder.Location = new System.Drawing.Point(185, 165);
            this.btnSauvegarder.Name = "btnSauvegarder";
            this.btnSauvegarder.Size = new System.Drawing.Size(165, 40);
            this.btnSauvegarder.TabIndex = 7;
            this.btnSauvegarder.Text = "Sauvegarder";
            this.btnSauvegarder.UseVisualStyleBackColor = true;
            this.btnSauvegarder.Click += new System.EventHandler(this.btnSauvegarder_Click);
            //
            // btnSupprimer
            //
            this.btnSupprimer.Location = new System.Drawing.Point(15, 215);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(165, 40);
            this.btnSupprimer.TabIndex = 8;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            //
            // btnAnnuler
            //
            this.btnAnnuler.Location = new System.Drawing.Point(185, 215);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(165, 40);
            this.btnAnnuler.TabIndex = 9;
            this.btnAnnuler.Text = "Annuler / Recharger";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            //
            // lblAide
            //
            this.lblAide.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAide.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.lblAide.Name = "lblAide";
            this.lblAide.Size = new System.Drawing.Size(370, 150);
            this.lblAide.TabIndex = 2;
            this.lblAide.Text = "Sélectionnez un personnage dans la grille pour le modifier dans les zones d'édition.\r\n\r\n" +
    "« Ajouter » crée une nouvelle ligne; « Sauvegarder » envoie tous les ajouts et modifications " +
    "à la base de données (mode déconnecté : DataSet + SqlDataAdapter.Update).\r\n\r\n" +
    "Exemples de races : Homme, Elfe, Nain, Hobbit, Maia.";
            //
            // lblStatut
            //
            this.lblStatut.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatut.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.lblStatut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatut.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblStatut.Name = "lblStatut";
            this.lblStatut.Size = new System.Drawing.Size(370, 60);
            this.lblStatut.TabIndex = 3;
            //
            // pnlDroite
            //
            this.pnlDroite.Controls.Add(this.lblStatut);
            this.pnlDroite.Controls.Add(this.lblAide);
            this.pnlDroite.Controls.Add(this.grpEdition);
            this.pnlDroite.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDroite.Name = "pnlDroite";
            this.pnlDroite.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pnlDroite.Size = new System.Drawing.Size(390, 690);
            this.pnlDroite.TabIndex = 1;
            //
            // FrmPersonnages
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 714);
            this.Controls.Add(this.dgvPersonnages);
            this.Controls.Add(this.pnlDroite);
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmPersonnages";
            this.Text = "Personnages";
            this.Load += new System.EventHandler(this.FrmPersonnages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersonnages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPersonnages)).EndInit();
            this.pnlDroite.ResumeLayout(false);
            this.grpEdition.ResumeLayout(false);
            this.grpEdition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPersonnages;
        private System.Windows.Forms.BindingSource bsPersonnages;
        private System.Windows.Forms.Panel pnlDroite;
        private System.Windows.Forms.GroupBox grpEdition;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label lblRace;
        private System.Windows.Forms.TextBox txtRace;
        private System.Windows.Forms.Label lblFaction;
        private System.Windows.Forms.TextBox txtFaction;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnSauvegarder;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Label lblAide;
        private System.Windows.Forms.Label lblStatut;
    }
}
