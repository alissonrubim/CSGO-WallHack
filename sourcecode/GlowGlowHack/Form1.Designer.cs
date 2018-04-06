namespace GlowGlowHack
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonWallHack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonWallHack
            // 
            this.buttonWallHack.Location = new System.Drawing.Point(34, 30);
            this.buttonWallHack.Name = "buttonWallHack";
            this.buttonWallHack.Size = new System.Drawing.Size(175, 23);
            this.buttonWallHack.TabIndex = 0;
            this.buttonWallHack.Text = "Habilitar";
            this.buttonWallHack.UseVisualStyleBackColor = true;
            this.buttonWallHack.Click += new System.EventHandler(this.buttonWallHack_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(249, 91);
            this.Controls.Add(this.buttonWallHack);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "Form1";
            this.Text = "CSGlow Wall Hack";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonWallHack;
    }
}

