namespace TP1_LSDA.UI
{
    partial class FrmMissions
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
            this.tlpMissions = new System.Windows.Forms.TableLayoutPanel();
            this.grpCreation = new System.Windows.Forms.GroupBox();
            this.lblLieu = new System.Windows.Forms.Label();
            this.cbLieu = new System.Windows.Forms.ComboBox();
            this.lblPersonnage = new System.Windows.Forms.Label();
            this.cbPersonnage = new System.Windows.Forms.ComboBox();
            this.lblDebut = new System.Windows.Forms.Label();
            this.dtpDebut = new System.Windows.Forms.DateTimePicker();
            this.lblFin = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.btnVerifierConflit = new System.Windows.Forms.Button();
            this.btnCreerMission = new System.Windows.Forms.Button();
            this.lblResultatConflit = new System.Windows.Forms.Label();
            this.grpRecherche = new System.Windows.Forms.GroupBox();
            this.lblTypeRecherche = new System.Windows.Forms.Label();
            this.cbTypeRecherche = new System.Windows.Forms.ComboBox();
            this.lblCritere = new System.Windows.Forms.Label();
            this.cbCritere = new System.Windows.Forms.ComboBox();
            this.lblRechDebut = new System.Windows.Forms.Label();
            this.dtpRechDebut = new System.Windows.Forms.DateTimePicker();
            this.lblRechFin = new System.Windows.Forms.Label();
            this.dtpRechFin = new System.Windows.Forms.DateTimePicker();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.lblNbResultats = new System.Windows.Forms.Label();
            this.dgvResultats = new System.Windows.Forms.DataGridView();
            this.grpListe = new System.Windows.Forms.GroupBox();
            this.dgvMissions = new System.Windows.Forms.DataGridView();
            this.btnActualiserMissions = new System.Windows.Forms.Button();
            this.btnSupprimerMission = new System.Windows.Forms.Button();
            this.lblNbMissions = new System.Windows.Forms.Label();
            this.tlpMissions.SuspendLayout();
            this.grpCreation.SuspendLayout();
            this.grpRecherche.SuspendLayout();
            this.grpListe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).BeginInit();
            this.SuspendLayout();
            //
            // grpCreation
            //
            this.grpCreation.Controls.Add(this.lblLieu);
            this.grpCreation.Controls.Add(this.cbLieu);
            this.grpCreation.Controls.Add(this.lblPersonnage);
            this.grpCreation.Controls.Add(this.cbPersonnage);
            this.grpCreation.Controls.Add(this.lblDebut);
            this.grpCreation.Controls.Add(this.dtpDebut);
            this.grpCreation.Controls.Add(this.lblFin);
            this.grpCreation.Controls.Add(this.dtpFin);
            this.grpCreation.Controls.Add(this.btnVerifierConflit);
            this.grpCreation.Controls.Add(this.btnCreerMission);
            this.grpCreation.Controls.Add(this.lblResultatConflit);
            this.grpCreation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCreation.Size = new System.Drawing.Size(420, 330);
            this.grpCreation.TabStop = false;
            this.grpCreation.Text = "Création d'une mission";
            this.grpCreation.Name = "grpCreation";
            this.grpCreation.TabIndex = 0;
            //
            // lblLieu
            //
            this.lblLieu.AutoSize = true;
            this.lblLieu.Location = new System.Drawing.Point(15, 35);
            this.lblLieu.Text = "Lieu :";
            this.lblLieu.Name = "lblLieu";
            this.lblLieu.TabIndex = 0;
            //
            // cbLieu
            //
            this.cbLieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLieu.FormattingEnabled = true;
            this.cbLieu.Location = new System.Drawing.Point(140, 32);
            this.cbLieu.Size = new System.Drawing.Size(260, 25);
            this.cbLieu.Name = "cbLieu";
            this.cbLieu.TabIndex = 1;
            this.cbLieu.SelectedIndexChanged += new System.EventHandler(this.SaisieMission_Changed);
            //
            // lblPersonnage
            //
            this.lblPersonnage.AutoSize = true;
            this.lblPersonnage.Location = new System.Drawing.Point(15, 75);
            this.lblPersonnage.Text = "Personnage :";
            this.lblPersonnage.Name = "lblPersonnage";
            this.lblPersonnage.TabIndex = 2;
            //
            // cbPersonnage
            //
            this.cbPersonnage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPersonnage.FormattingEnabled = true;
            this.cbPersonnage.Location = new System.Drawing.Point(140, 72);
            this.cbPersonnage.Size = new System.Drawing.Size(260, 25);
            this.cbPersonnage.Name = "cbPersonnage";
            this.cbPersonnage.TabIndex = 3;
            this.cbPersonnage.SelectedIndexChanged += new System.EventHandler(this.SaisieMission_Changed);
            //
            // lblDebut
            //
            this.lblDebut.AutoSize = true;
            this.lblDebut.Location = new System.Drawing.Point(15, 115);
            this.lblDebut.Text = "Date de début :";
            this.lblDebut.Name = "lblDebut";
            this.lblDebut.TabIndex = 4;
            //
            // dtpDebut
            //
            this.dtpDebut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDebut.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpDebut.Location = new System.Drawing.Point(140, 112);
            this.dtpDebut.Size = new System.Drawing.Size(260, 25);
            this.dtpDebut.Name = "dtpDebut";
            this.dtpDebut.TabIndex = 5;
            this.dtpDebut.ValueChanged += new System.EventHandler(this.SaisieMission_Changed);
            //
            // lblFin
            //
            this.lblFin.AutoSize = true;
            this.lblFin.Location = new System.Drawing.Point(15, 155);
            this.lblFin.Text = "Date de fin :";
            this.lblFin.Name = "lblFin";
            this.lblFin.TabIndex = 6;
            //
            // dtpFin
            //
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFin.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpFin.Location = new System.Drawing.Point(140, 152);
            this.dtpFin.Size = new System.Drawing.Size(260, 25);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.TabIndex = 7;
            this.dtpFin.ValueChanged += new System.EventHandler(this.SaisieMission_Changed);
            //
            // btnVerifierConflit
            //
            this.btnVerifierConflit.Location = new System.Drawing.Point(15, 200);
            this.btnVerifierConflit.Size = new System.Drawing.Size(185, 40);
            this.btnVerifierConflit.Text = "Vérifier le conflit";
            this.btnVerifierConflit.UseVisualStyleBackColor = true;
            this.btnVerifierConflit.Name = "btnVerifierConflit";
            this.btnVerifierConflit.TabIndex = 8;
            this.btnVerifierConflit.Click += new System.EventHandler(this.btnVerifierConflit_Click);
            //
            // btnCreerMission
            //
            this.btnCreerMission.Location = new System.Drawing.Point(215, 200);
            this.btnCreerMission.Size = new System.Drawing.Size(185, 40);
            this.btnCreerMission.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreerMission.Text = "Créer la mission";
            this.btnCreerMission.UseVisualStyleBackColor = true;
            this.btnCreerMission.Name = "btnCreerMission";
            this.btnCreerMission.TabIndex = 9;
            this.btnCreerMission.Click += new System.EventHandler(this.btnCreerMission_Click);
            //
            // lblResultatConflit
            //
            this.lblResultatConflit.Location = new System.Drawing.Point(15, 255);
            this.lblResultatConflit.Size = new System.Drawing.Size(385, 60);
            this.lblResultatConflit.Text = "";
            this.lblResultatConflit.Name = "lblResultatConflit";
            this.lblResultatConflit.TabIndex = 10;
            //
            // grpRecherche
            //
            this.grpRecherche.Controls.Add(this.lblTypeRecherche);
            this.grpRecherche.Controls.Add(this.cbTypeRecherche);
            this.grpRecherche.Controls.Add(this.lblCritere);
            this.grpRecherche.Controls.Add(this.cbCritere);
            this.grpRecherche.Controls.Add(this.lblRechDebut);
            this.grpRecherche.Controls.Add(this.dtpRechDebut);
            this.grpRecherche.Controls.Add(this.lblRechFin);
            this.grpRecherche.Controls.Add(this.dtpRechFin);
            this.grpRecherche.Controls.Add(this.btnRechercher);
            this.grpRecherche.Controls.Add(this.lblNbResultats);
            this.grpRecherche.Controls.Add(this.dgvResultats);
            this.grpRecherche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRecherche.Size = new System.Drawing.Size(732, 330);
            this.grpRecherche.TabStop = false;
            this.grpRecherche.Text = "Recherche filtrée (mode connecté)";
            this.grpRecherche.Name = "grpRecherche";
            this.grpRecherche.TabIndex = 1;
            //
            // lblTypeRecherche
            //
            this.lblTypeRecherche.AutoSize = true;
            this.lblTypeRecherche.Location = new System.Drawing.Point(15, 35);
            this.lblTypeRecherche.Text = "Type de recherche :";
            this.lblTypeRecherche.Name = "lblTypeRecherche";
            this.lblTypeRecherche.TabIndex = 0;
            //
            // cbTypeRecherche
            //
            this.cbTypeRecherche.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeRecherche.FormattingEnabled = true;
            this.cbTypeRecherche.Location = new System.Drawing.Point(150, 32);
            this.cbTypeRecherche.Size = new System.Drawing.Size(240, 25);
            this.cbTypeRecherche.Name = "cbTypeRecherche";
            this.cbTypeRecherche.TabIndex = 1;
            this.cbTypeRecherche.SelectedIndexChanged += new System.EventHandler(this.cbTypeRecherche_SelectedIndexChanged);
            //
            // lblCritere
            //
            this.lblCritere.AutoSize = true;
            this.lblCritere.Location = new System.Drawing.Point(405, 35);
            this.lblCritere.Text = "Royaume :";
            this.lblCritere.Name = "lblCritere";
            this.lblCritere.TabIndex = 2;
            //
            // cbCritere
            //
            this.cbCritere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cbCritere.FormattingEnabled = true;
            this.cbCritere.Location = new System.Drawing.Point(495, 32);
            this.cbCritere.Size = new System.Drawing.Size(220, 25);
            this.cbCritere.MaxLength = 50;
            this.cbCritere.Name = "cbCritere";
            this.cbCritere.TabIndex = 3;
            //
            // lblRechDebut
            //
            this.lblRechDebut.AutoSize = true;
            this.lblRechDebut.Location = new System.Drawing.Point(15, 75);
            this.lblRechDebut.Text = "Période du :";
            this.lblRechDebut.Name = "lblRechDebut";
            this.lblRechDebut.TabIndex = 4;
            //
            // dtpRechDebut
            //
            this.dtpRechDebut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRechDebut.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpRechDebut.Location = new System.Drawing.Point(150, 72);
            this.dtpRechDebut.Size = new System.Drawing.Size(240, 25);
            this.dtpRechDebut.Name = "dtpRechDebut";
            this.dtpRechDebut.TabIndex = 5;
            //
            // lblRechFin
            //
            this.lblRechFin.AutoSize = true;
            this.lblRechFin.Location = new System.Drawing.Point(405, 75);
            this.lblRechFin.Text = "au :";
            this.lblRechFin.Name = "lblRechFin";
            this.lblRechFin.TabIndex = 6;
            //
            // dtpRechFin
            //
            this.dtpRechFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRechFin.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpRechFin.Location = new System.Drawing.Point(495, 72);
            this.dtpRechFin.Size = new System.Drawing.Size(220, 25);
            this.dtpRechFin.Name = "dtpRechFin";
            this.dtpRechFin.TabIndex = 7;
            //
            // btnRechercher
            //
            this.btnRechercher.Location = new System.Drawing.Point(150, 108);
            this.btnRechercher.Size = new System.Drawing.Size(150, 34);
            this.btnRechercher.Text = "Rechercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.TabIndex = 8;
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);
            //
            // lblNbResultats
            //
            this.lblNbResultats.AutoSize = true;
            this.lblNbResultats.Location = new System.Drawing.Point(320, 116);
            this.lblNbResultats.Text = "";
            this.lblNbResultats.Name = "lblNbResultats";
            this.lblNbResultats.TabIndex = 9;
            //
            // dgvResultats
            //
            this.dgvResultats.AllowUserToAddRows = false;
            this.dgvResultats.AllowUserToDeleteRows = false;
            this.dgvResultats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            this.dgvResultats.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvResultats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultats.Location = new System.Drawing.Point(15, 150);
            this.dgvResultats.MultiSelect = false;
            this.dgvResultats.ReadOnly = true;
            this.dgvResultats.RowHeadersWidth = 30;
            this.dgvResultats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultats.Size = new System.Drawing.Size(700, 165);
            this.dgvResultats.Name = "dgvResultats";
            this.dgvResultats.TabIndex = 10;
            //
            // grpListe
            //
            this.grpListe.Controls.Add(this.dgvMissions);
            this.grpListe.Controls.Add(this.btnActualiserMissions);
            this.grpListe.Controls.Add(this.btnSupprimerMission);
            this.grpListe.Controls.Add(this.lblNbMissions);
            this.grpListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpListe.Size = new System.Drawing.Size(1164, 355);
            this.grpListe.TabStop = false;
            this.grpListe.Text = "Liste globale des missions (mode déconnecté : DataSet)";
            this.grpListe.Name = "grpListe";
            this.grpListe.TabIndex = 2;
            //
            // dgvMissions
            //
            this.dgvMissions.AllowUserToAddRows = false;
            this.dgvMissions.AllowUserToDeleteRows = false;
            this.dgvMissions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            this.dgvMissions.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMissions.Location = new System.Drawing.Point(15, 28);
            this.dgvMissions.MultiSelect = false;
            this.dgvMissions.ReadOnly = true;
            this.dgvMissions.RowHeadersWidth = 30;
            this.dgvMissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMissions.Size = new System.Drawing.Size(1134, 275);
            this.dgvMissions.Name = "dgvMissions";
            this.dgvMissions.TabIndex = 0;
            //
            // btnActualiserMissions
            //
            this.btnActualiserMissions.Location = new System.Drawing.Point(15, 310);
            this.btnActualiserMissions.Size = new System.Drawing.Size(170, 36);
            this.btnActualiserMissions.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnActualiserMissions.Text = "Actualiser la liste";
            this.btnActualiserMissions.UseVisualStyleBackColor = true;
            this.btnActualiserMissions.Name = "btnActualiserMissions";
            this.btnActualiserMissions.TabIndex = 1;
            this.btnActualiserMissions.Click += new System.EventHandler(this.btnActualiserMissions_Click);
            //
            // btnSupprimerMission
            //
            this.btnSupprimerMission.Location = new System.Drawing.Point(195, 310);
            this.btnSupprimerMission.Size = new System.Drawing.Size(260, 36);
            this.btnSupprimerMission.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.btnSupprimerMission.Text = "Supprimer la mission sélectionnée";
            this.btnSupprimerMission.UseVisualStyleBackColor = true;
            this.btnSupprimerMission.Name = "btnSupprimerMission";
            this.btnSupprimerMission.TabIndex = 2;
            this.btnSupprimerMission.Click += new System.EventHandler(this.btnSupprimerMission_Click);
            //
            // lblNbMissions
            //
            this.lblNbMissions.AutoSize = true;
            this.lblNbMissions.Location = new System.Drawing.Point(470, 319);
            this.lblNbMissions.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lblNbMissions.Text = "";
            this.lblNbMissions.Name = "lblNbMissions";
            this.lblNbMissions.TabIndex = 3;
            //
            // tlpMissions
            //
            this.tlpMissions.ColumnCount = 2;
            this.tlpMissions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tlpMissions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tlpMissions.Controls.Add(this.grpCreation, 0, 0);
            this.tlpMissions.Controls.Add(this.grpRecherche, 1, 0);
            this.tlpMissions.Controls.Add(this.grpListe, 0, 1);
            this.tlpMissions.SetColumnSpan(this.grpListe, 2);
            this.tlpMissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMissions.Name = "tlpMissions";
            this.tlpMissions.Padding = new System.Windows.Forms.Padding(6);
            this.tlpMissions.RowCount = 2;
            this.tlpMissions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tlpMissions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tlpMissions.TabIndex = 0;
            //
            // FrmMissions
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 714);
            this.Controls.Add(this.tlpMissions);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FrmMissions";
            this.Text = "Missions";
            this.Load += new System.EventHandler(this.FrmMissions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).EndInit();
            this.grpCreation.ResumeLayout(false);
            this.grpCreation.PerformLayout();
            this.grpRecherche.ResumeLayout(false);
            this.grpRecherche.PerformLayout();
            this.grpListe.ResumeLayout(false);
            this.grpListe.PerformLayout();
            this.tlpMissions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMissions;
        private System.Windows.Forms.GroupBox grpCreation;
        private System.Windows.Forms.Label lblLieu;
        private System.Windows.Forms.ComboBox cbLieu;
        private System.Windows.Forms.Label lblPersonnage;
        private System.Windows.Forms.ComboBox cbPersonnage;
        private System.Windows.Forms.Label lblDebut;
        private System.Windows.Forms.DateTimePicker dtpDebut;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Button btnVerifierConflit;
        private System.Windows.Forms.Button btnCreerMission;
        private System.Windows.Forms.Label lblResultatConflit;
        private System.Windows.Forms.GroupBox grpRecherche;
        private System.Windows.Forms.Label lblTypeRecherche;
        private System.Windows.Forms.ComboBox cbTypeRecherche;
        private System.Windows.Forms.Label lblCritere;
        private System.Windows.Forms.ComboBox cbCritere;
        private System.Windows.Forms.Label lblRechDebut;
        private System.Windows.Forms.DateTimePicker dtpRechDebut;
        private System.Windows.Forms.Label lblRechFin;
        private System.Windows.Forms.DateTimePicker dtpRechFin;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.Label lblNbResultats;
        private System.Windows.Forms.DataGridView dgvResultats;
        private System.Windows.Forms.GroupBox grpListe;
        private System.Windows.Forms.DataGridView dgvMissions;
        private System.Windows.Forms.Button btnActualiserMissions;
        private System.Windows.Forms.Button btnSupprimerMission;
        private System.Windows.Forms.Label lblNbMissions;
    }
}
