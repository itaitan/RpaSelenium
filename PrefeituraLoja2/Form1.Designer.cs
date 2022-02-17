namespace PrefeituraLoja2
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textDtInicio = new System.Windows.Forms.MaskedTextBox();
            this.textDtFim = new System.Windows.Forms.MaskedTextBox();
            this.textDtMes = new System.Windows.Forms.MaskedTextBox();
            this.btnFim = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Inicio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Data Fim";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(63, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Data Mês";
            // 
            // textDtInicio
            // 
            this.textDtInicio.Location = new System.Drawing.Point(142, 42);
            this.textDtInicio.Mask = "00/00/0000";
            this.textDtInicio.Name = "textDtInicio";
            this.textDtInicio.Size = new System.Drawing.Size(114, 20);
            this.textDtInicio.TabIndex = 3;
            this.textDtInicio.ValidatingType = typeof(System.DateTime);
            this.textDtInicio.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.textDtInicio_MaskInputRejected);
            // 
            // textDtFim
            // 
            this.textDtFim.Location = new System.Drawing.Point(142, 80);
            this.textDtFim.Mask = "00/00/0000";
            this.textDtFim.Name = "textDtFim";
            this.textDtFim.Size = new System.Drawing.Size(114, 20);
            this.textDtFim.TabIndex = 4;
            this.textDtFim.ValidatingType = typeof(System.DateTime);
            // 
            // textDtMes
            // 
            this.textDtMes.Location = new System.Drawing.Point(142, 120);
            this.textDtMes.Mask = "00/0000";
            this.textDtMes.Name = "textDtMes";
            this.textDtMes.Size = new System.Drawing.Size(114, 20);
            this.textDtMes.TabIndex = 5;
            // 
            // btnFim
            // 
            this.btnFim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFim.Location = new System.Drawing.Point(75, 177);
            this.btnFim.Name = "btnFim";
            this.btnFim.Size = new System.Drawing.Size(75, 23);
            this.btnFim.TabIndex = 6;
            this.btnFim.Text = "Finalizar";
            this.btnFim.UseVisualStyleBackColor = true;
            this.btnFim.Click += new System.EventHandler(this.btnFim_Click);
            // 
            // btnSair
            // 
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Location = new System.Drawing.Point(208, 177);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "Cancelar";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 232);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnFim);
            this.Controls.Add(this.textDtMes);
            this.Controls.Add(this.textDtFim);
            this.Controls.Add(this.textDtInicio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Prefeitura Aruja";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFim;
        private System.Windows.Forms.Button btnSair;
        public System.Windows.Forms.MaskedTextBox textDtInicio;
        public System.Windows.Forms.MaskedTextBox textDtFim;
        public System.Windows.Forms.MaskedTextBox textDtMes;
    }
}