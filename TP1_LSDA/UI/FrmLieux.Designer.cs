namespace TP1_LSDA.UI
{
    partial class FrmLieux
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
            this.dgvLieux = new System.Windows.Forms.DataGridView();
            this.bsLieux = new System.Windows.Forms.BindingSource(this.components);
            this.grpEdition = new System.Windows.Forms.GroupBox();
            this.lblNomLieu = new System.Windows.Forms.Label();
            this.txtNomLieu = new System.Windows.Forms.TextBox();
            this.lblRoyaume = new System.Windows.Forms.Label();
            this.txtRoyaume = new System.Windows.Forms.TextBox();
            this.lblDangerosite = new System.Windows.Forms.Label();
            this.nudDangerosite = new System.Windows.Forms.NumericUpDown();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnSauvegarder = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.lblAide = new System.Windows.Forms.Label();
            this.lblStatut = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLieux)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLieux)).BeginInit();
            this.grpEdition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDangerosite)).BeginInit();
            this.SuspendLayout();
            //
            // dgvLieux
            //
            this.dgvLieux.AllowUserToAddRows = false;
            this.dgvLieux.AllowUserToDeleteRows = false;
            this.dgvLieux.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLieux.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvLieux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLieux.Location = new System.Drawing.Point(12, 12);
            this.dgvLieux.MultiSelect = false;
            this.dgvLieux.Name = "dgvLieux";
            this.dgvLieux.ReadOnly = true;
            this.dgvLieux.RowHeadersWidth = 30;
            this.dgvLieux.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLieux.Size = new System.Drawing.Size(780, 690);
            this.dgvLieux.TabIndex = 0;
            this.dgvLieux.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvLieux_DataError);
            //
            // grpEdition
            //
            this.grpEdition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEdition.Controls.Add(this.lblNomLieu);
            this.grpEdition.Controls.Add(this.txtNomLieu);
            this.grpEdition.Controls.Add(this.lblRoyaume);
            this.grpEdition.Controls.Add(this.txtRoyaume);
            this.grpEdition.Controls.Add(this.lblDangerosite);
            this.grpEdition.Controls.Add(this.nudDangerosite);
            this.grpEdition.Controls.Add(this.btnAjouter);
            this.grpEdition.Controls.Add(this.btnSauvegarder);
            this.grpEdition.Controls.Add(this.btnSupprimer);
            this.grpEdition.Controls.Add(this.btnAnnuler);
            this.grpEdition.Location = new System.Drawing.Point(806, 12);
            this.grpEdition.Name = "grpEdition";
            this.grpEdition.Size = new System.Drawing.Size(370, 300);
            this.grpEdition.TabIndex = 1;
            this.grpEdition.TabStop = false;
            this.grpEdition.Text = "Édition du lieu sélectionné";
            //
            // lblNomLieu
            //
            this.lblNomLieu.AutoSize = true;
            this.lblNomLieu.Location = new System.Drawing.Point(15, 35);
            this.lblNomLieu.Name = "lblNomLieu";
            this.lblNomLieu.Size = new System.Drawing.Size(90, 19);
            this.lblNomLieu.TabIndex = 0;
            this.lblNomLieu.Text = "Nom du lieu :";
            //
            // txtNomLieu
            //
            this.txtNomLieu.Location = new System.Drawing.Point(130, 32);
            this.txtNomLieu.MaxLength = 100;
            this.txtNomLieu.Name = "txtNomLieu";
            this.txtNomLieu.Size = new System.Drawing.Size(220, 25);
            this.txtNomLieu.TabIndex = 1;
            //
            // lblRoyaume
            //
            this.lblRoyaume.AutoSize = true;
            this.lblRoyaume.Location = new System.Drawing.Point(15, 75);
            this.lblRoyaume.Name = "lblRoyaume";
            this.lblRoyaume.Size = new System.Drawing.Size(70, 19);
            this.lblRoyaume.TabIndex = 2;
            this.lblRoyaume.Text = "Royaume :";
            //
            // txtRoyaume
            //
            this.txtRoyaume.Location = new System.Drawing.Point(130, 72);
            this.txtRoyaume.MaxLength = 50;
            this.txtRoyaume.Name = "txtRoyaume";
            this.txtRoyaume.Size = new System.Drawing.Size(220, 25);
            this.txtRoyaume.TabIndex = 3;
            //
            // lblDangerosite
            //
            this.lblDangerosite.AutoSize = true;
            this.lblDangerosite.Location = new System.Drawing.Point(15, 115);
            this.lblDangerosite.Name = "lblDangerosite";
            this.lblDangerosite.Size = new System.Drawing.Size(110, 19);
            this.lblDangerosite.TabIndex = 4;
            this.lblDangerosite.Text = "Dangerosité :";
            //
            // nudDangerosite
            //
            this.nudDangerosite.Location = new System.Drawing.Point(130, 112);
            this.nudDangerosite.Name = "nudDangerosite";
            this.nudDangerosite.Size = new System.Drawing.Size(80, 25);
            this.nudDangerosite.TabIndex = 5;
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
            this.lblAide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAide.Location = new System.Drawing.Point(806, 325);
            this.lblAide.Name = "lblAide";
            this.lblAide.Size = new System.Drawing.Size(370, 130);
            this.lblAide.TabIndex = 2;
            this.lblAide.Text = "Sélectionnez un lieu dans la grille pour le modifier dans les zones d'édition.\r\n\r\n" +
    "« Ajouter » crée une nouvelle ligne; « Sauvegarder » envoie tous les ajouts et modifications " +
    "à la base de données (mode déconnecté : DataSet + SqlDataAdapter.Update).\r\n\r\n" +
    "La dangerosité va de 1 (sûr) à 10 (mortel).";
            //
            // lblStatut
            //
            this.lblStatut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatut.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblStatut.Location = new System.Drawing.Point(806, 465);
            this.lblStatut.Name = "lblStatut";
            this.lblStatut.Size = new System.Drawing.Size(370, 60);
            this.lblStatut.TabIndex = 3;
            //
            // FrmLieux
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 714);
            this.Controls.Add(this.lblStatut);
            this.Controls.Add(this.lblAide);
            this.Controls.Add(this.grpEdition);
            this.Controls.Add(this.dgvLieux);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmLieux";
            this.Text = "Lieux";
            this.Load += new System.EventHandler(this.FrmLieux_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLieux)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLieux)).EndInit();
            this.grpEdition.ResumeLayout(false);
            this.grpEdition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDangerosite)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLieux;
        private System.Windows.Forms.BindingSource bsLieux;
        private System.Windows.Forms.GroupBox grpEdition;
        private System.Windows.Forms.Label lblNomLieu;
        private System.Windows.Forms.TextBox txtNomLieu;
        private System.Windows.Forms.Label lblRoyaume;
        private System.Windows.Forms.TextBox txtRoyaume;
        private System.Windows.Forms.Label lblDangerosite;
        private System.Windows.Forms.NumericUpDown nudDangerosite;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnSauvegarder;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Label lblAide;
        private System.Windows.Forms.Label lblStatut;
    }
}
