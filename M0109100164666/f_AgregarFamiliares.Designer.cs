namespace M0109100164666
{
    partial class f_AgregarFamiliares
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f_AgregarFamiliares));
            this.gboDatosPersonalesFamiliares = new System.Windows.Forms.GroupBox();
            this.lblDNIFamiliares = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.txtApellidoPaterno = new System.Windows.Forms.TextBox();
            this.lblApellidoPaterno = new System.Windows.Forms.Label();
            this.txtApellidoMaterno = new System.Windows.Forms.TextBox();
            this.lblApellidoMaterno = new System.Windows.Forms.Label();
            this.lblRelacion = new System.Windows.Forms.Label();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.lblNombres = new System.Windows.Forms.Label();
            this.cboRelacion = new System.Windows.Forms.ComboBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dstAtencionesFISE = new M0109100164666.dstAtencionesFISE();
            this.bsognRelacionFamiliarVerificacionFISE = new System.Windows.Forms.BindingSource(this.components);
            this.gboDatosPersonalesFamiliares.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dstAtencionesFISE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsognRelacionFamiliarVerificacionFISE)).BeginInit();
            this.SuspendLayout();
            // 
            // gboDatosPersonalesFamiliares
            // 
            this.gboDatosPersonalesFamiliares.Controls.Add(this.btnBuscar);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.cboRelacion);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.lblRelacion);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.txtNombres);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.lblNombres);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.txtApellidoMaterno);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.lblApellidoMaterno);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.txtApellidoPaterno);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.lblApellidoPaterno);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.txtDni);
            this.gboDatosPersonalesFamiliares.Controls.Add(this.lblDNIFamiliares);
            this.gboDatosPersonalesFamiliares.Location = new System.Drawing.Point(15, 21);
            this.gboDatosPersonalesFamiliares.Name = "gboDatosPersonalesFamiliares";
            this.gboDatosPersonalesFamiliares.Size = new System.Drawing.Size(644, 126);
            this.gboDatosPersonalesFamiliares.TabIndex = 0;
            this.gboDatosPersonalesFamiliares.TabStop = false;
            this.gboDatosPersonalesFamiliares.Text = "Datos Personales";
            // 
            // lblDNIFamiliares
            // 
            this.lblDNIFamiliares.AutoSize = true;
            this.lblDNIFamiliares.Location = new System.Drawing.Point(28, 33);
            this.lblDNIFamiliares.Name = "lblDNIFamiliares";
            this.lblDNIFamiliares.Size = new System.Drawing.Size(29, 13);
            this.lblDNIFamiliares.TabIndex = 0;
            this.lblDNIFamiliares.Text = "DNI:";
            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(130, 30);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(132, 20);
            this.txtDni.TabIndex = 1;
            // 
            // txtApellidoPaterno
            // 
            this.txtApellidoPaterno.Location = new System.Drawing.Point(130, 56);
            this.txtApellidoPaterno.Name = "txtApellidoPaterno";
            this.txtApellidoPaterno.Size = new System.Drawing.Size(190, 20);
            this.txtApellidoPaterno.TabIndex = 3;
            // 
            // lblApellidoPaterno
            // 
            this.lblApellidoPaterno.AutoSize = true;
            this.lblApellidoPaterno.Location = new System.Drawing.Point(28, 59);
            this.lblApellidoPaterno.Name = "lblApellidoPaterno";
            this.lblApellidoPaterno.Size = new System.Drawing.Size(90, 13);
            this.lblApellidoPaterno.TabIndex = 2;
            this.lblApellidoPaterno.Text = "Apellido Paterno :";
            // 
            // txtApellidoMaterno
            // 
            this.txtApellidoMaterno.Location = new System.Drawing.Point(428, 56);
            this.txtApellidoMaterno.Name = "txtApellidoMaterno";
            this.txtApellidoMaterno.Size = new System.Drawing.Size(171, 20);
            this.txtApellidoMaterno.TabIndex = 5;
            // 
            // lblApellidoMaterno
            // 
            this.lblApellidoMaterno.AutoSize = true;
            this.lblApellidoMaterno.Location = new System.Drawing.Point(326, 59);
            this.lblApellidoMaterno.Name = "lblApellidoMaterno";
            this.lblApellidoMaterno.Size = new System.Drawing.Size(92, 13);
            this.lblApellidoMaterno.TabIndex = 4;
            this.lblApellidoMaterno.Text = "Apellido Materno :";
            // 
            // lblRelacion
            // 
            this.lblRelacion.AutoSize = true;
            this.lblRelacion.Location = new System.Drawing.Point(326, 85);
            this.lblRelacion.Name = "lblRelacion";
            this.lblRelacion.Size = new System.Drawing.Size(55, 13);
            this.lblRelacion.TabIndex = 8;
            this.lblRelacion.Text = "Relación :";
            // 
            // txtNombres
            // 
            this.txtNombres.Location = new System.Drawing.Point(130, 82);
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(190, 20);
            this.txtNombres.TabIndex = 7;
            // 
            // lblNombres
            // 
            this.lblNombres.AutoSize = true;
            this.lblNombres.Location = new System.Drawing.Point(28, 85);
            this.lblNombres.Name = "lblNombres";
            this.lblNombres.Size = new System.Drawing.Size(58, 13);
            this.lblNombres.TabIndex = 6;
            this.lblNombres.Text = "Nombres : ";
            // 
            // cboRelacion
            // 
            this.cboRelacion.DataSource = this.bsognRelacionFamiliarVerificacionFISE;
            this.cboRelacion.DisplayMember = "NombreRelacionFamiliarVerificacionFISE";
            this.cboRelacion.FormattingEnabled = true;
            this.cboRelacion.Location = new System.Drawing.Point(428, 85);
            this.cboRelacion.Name = "cboRelacion";
            this.cboRelacion.Size = new System.Drawing.Size(171, 21);
            this.cboRelacion.TabIndex = 9;
            this.cboRelacion.ValueMember = "CodigoRelacionFamiliarVerificacionFISE";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(443, 153);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(539, 153);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(75, 23);
            this.btnQuitar.TabIndex = 2;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 194);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(644, 141);
            this.dataGridView1.TabIndex = 3;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(282, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // dstAtencionesFISE
            // 
            this.dstAtencionesFISE.DataSetName = "dstAtencionesFISE";
            this.dstAtencionesFISE.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bsognRelacionFamiliarVerificacionFISE
            // 
            this.bsognRelacionFamiliarVerificacionFISE.DataMember = "gnRelacionFamiliarVerificacionFISE";
            this.bsognRelacionFamiliarVerificacionFISE.DataSource = this.dstAtencionesFISE;
            // 
            // f_AgregarFamiliares
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 363);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.gboDatosPersonalesFamiliares);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "f_AgregarFamiliares";
            this.Text = "Agregar Familiares";
            this.gboDatosPersonalesFamiliares.ResumeLayout(false);
            this.gboDatosPersonalesFamiliares.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dstAtencionesFISE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsognRelacionFamiliarVerificacionFISE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gboDatosPersonalesFamiliares;
        private System.Windows.Forms.TextBox txtApellidoPaterno;
        private System.Windows.Forms.Label lblApellidoPaterno;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblDNIFamiliares;
        private System.Windows.Forms.ComboBox cboRelacion;
        private System.Windows.Forms.Label lblRelacion;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.Label lblNombres;
        private System.Windows.Forms.TextBox txtApellidoMaterno;
        private System.Windows.Forms.Label lblApellidoMaterno;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.BindingSource bsognRelacionFamiliarVerificacionFISE;
        public dstAtencionesFISE dstAtencionesFISE;
    }
}