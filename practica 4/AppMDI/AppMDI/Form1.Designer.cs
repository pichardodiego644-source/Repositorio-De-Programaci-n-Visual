namespace AppMDI
{
    partial class MDIEstudiantes
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ingresarEstudianteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verEstudianteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mejoresPromediosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ingresarEstudianteToolStripMenuItem,
            this.verEstudianteToolStripMenuItem,
            this.mejoresPromediosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ingresarEstudianteToolStripMenuItem
            // 
            this.ingresarEstudianteToolStripMenuItem.Name = "ingresarEstudianteToolStripMenuItem";
            this.ingresarEstudianteToolStripMenuItem.Size = new System.Drawing.Size(149, 24);
            this.ingresarEstudianteToolStripMenuItem.Text = "Ingresar Estudiante";
            this.ingresarEstudianteToolStripMenuItem.Click += new System.EventHandler(this.ingresarEstudianteToolStripMenuItem_Click);
            // 
            // verEstudianteToolStripMenuItem
            // 
            this.verEstudianteToolStripMenuItem.Name = "verEstudianteToolStripMenuItem";
            this.verEstudianteToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.verEstudianteToolStripMenuItem.Text = "Ver estudiante";
            this.verEstudianteToolStripMenuItem.Click += new System.EventHandler(this.verEstudianteToolStripMenuItem_Click);
            // 
            // mejoresPromediosToolStripMenuItem
            // 
            this.mejoresPromediosToolStripMenuItem.Name = "mejoresPromediosToolStripMenuItem";
            this.mejoresPromediosToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.mejoresPromediosToolStripMenuItem.Text = "Mejores Promedios";
            this.mejoresPromediosToolStripMenuItem.Click += new System.EventHandler(this.mejoresPromediosToolStripMenuItem_Click);
            // 
            // MDIEstudiantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MDIEstudiantes";
            this.Text = "MDIEstudiantes";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ingresarEstudianteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verEstudianteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mejoresPromediosToolStripMenuItem;
    }
}

