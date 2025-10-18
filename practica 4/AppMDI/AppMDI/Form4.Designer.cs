namespace AppMDI
{
    partial class Form4
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.DgvMejorNota = new System.Windows.Forms.DataGridView();
            this.chNotas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.DgvMejorNota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chNotas)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvMejorNota
            // 
            this.DgvMejorNota.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvMejorNota.Location = new System.Drawing.Point(43, 63);
            this.DgvMejorNota.Name = "DgvMejorNota";
            this.DgvMejorNota.RowHeadersWidth = 51;
            this.DgvMejorNota.RowTemplate.Height = 24;
            this.DgvMejorNota.Size = new System.Drawing.Size(505, 379);
            this.DgvMejorNota.TabIndex = 0;
            // 
            // chNotas
            // 
            this.chNotas.BackColor = System.Drawing.SystemColors.ControlLightLight;
            chartArea1.Name = "ChartArea1";
            this.chNotas.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chNotas.Legends.Add(legend1);
            this.chNotas.Location = new System.Drawing.Point(606, 63);
            this.chNotas.Name = "chNotas";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chNotas.Series.Add(series1);
            this.chNotas.Size = new System.Drawing.Size(422, 379);
            this.chNotas.TabIndex = 1;
            this.chNotas.Text = "chart1";
            this.chNotas.Click += new System.EventHandler(this.chNotas_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1084, 555);
            this.Controls.Add(this.chNotas);
            this.Controls.Add(this.DgvMejorNota);
            this.Name = "Form4";
            this.Text = "d";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvMejorNota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chNotas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvMejorNota;
        private System.Windows.Forms.DataVisualization.Charting.Chart chNotas;
    }
}