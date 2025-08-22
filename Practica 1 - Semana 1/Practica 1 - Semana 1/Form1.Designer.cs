namespace Practica_1___Semana_1
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.login = new System.Windows.Forms.TabPage();
            this.btIniciar = new System.Windows.Forms.Button();
            this.ctClave = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ctUsuario = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cntclick = new System.Windows.Forms.TabPage();
            this.btnContador = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.temperature = new System.Windows.Forms.TabPage();
            this.btnConvertir = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.txtEntrada = new System.Windows.Forms.TextBox();
            this.cmbConversion = new System.Windows.Forms.ComboBox();
            this.imc_weight = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.typeweight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.imc_res = new System.Windows.Forms.TextBox();
            this.btncalculate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.heightenter = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.weightenter = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.login.SuspendLayout();
            this.cntclick.SuspendLayout();
            this.temperature.SuspendLayout();
            this.imc_weight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.login);
            this.tabControl1.Controls.Add(this.cntclick);
            this.tabControl1.Controls.Add(this.temperature);
            this.tabControl1.Controls.Add(this.imc_weight);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // login
            // 
            this.login.Controls.Add(this.btIniciar);
            this.login.Controls.Add(this.ctClave);
            this.login.Controls.Add(this.label5);
            this.login.Controls.Add(this.ctUsuario);
            this.login.Controls.Add(this.label6);
            this.login.Location = new System.Drawing.Point(4, 25);
            this.login.Name = "login";
            this.login.Padding = new System.Windows.Forms.Padding(3);
            this.login.Size = new System.Drawing.Size(792, 421);
            this.login.TabIndex = 0;
            this.login.Text = "Inicio Sesion";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // btIniciar
            // 
            this.btIniciar.Location = new System.Drawing.Point(355, 238);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(148, 41);
            this.btIniciar.TabIndex = 9;
            this.btIniciar.Text = "Entrar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // ctClave
            // 
            this.ctClave.Location = new System.Drawing.Point(343, 174);
            this.ctClave.Name = "ctClave";
            this.ctClave.PasswordChar = '*';
            this.ctClave.Size = new System.Drawing.Size(173, 22);
            this.ctClave.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Contraseña:";
            // 
            // ctUsuario
            // 
            this.ctUsuario.Location = new System.Drawing.Point(343, 132);
            this.ctUsuario.Name = "ctUsuario";
            this.ctUsuario.Size = new System.Drawing.Size(173, 22);
            this.ctUsuario.TabIndex = 6;
            this.ctUsuario.TextChanged += new System.EventHandler(this.ctUsuario_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(264, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Usuario:";
            // 
            // cntclick
            // 
            this.cntclick.Controls.Add(this.btnContador);
            this.cntclick.Controls.Add(this.label4);
            this.cntclick.Location = new System.Drawing.Point(4, 25);
            this.cntclick.Name = "cntclick";
            this.cntclick.Padding = new System.Windows.Forms.Padding(3);
            this.cntclick.Size = new System.Drawing.Size(792, 421);
            this.cntclick.TabIndex = 1;
            this.cntclick.Text = "Contador Clicks";
            this.cntclick.UseVisualStyleBackColor = true;
            this.cntclick.Click += new System.EventHandler(this.cntclick_Click);
            // 
            // btnContador
            // 
            this.btnContador.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContador.Location = new System.Drawing.Point(291, 259);
            this.btnContador.Name = "btnContador";
            this.btnContador.Size = new System.Drawing.Size(244, 79);
            this.btnContador.TabIndex = 3;
            this.btnContador.Text = "Da el primer toque !!";
            this.btnContador.UseVisualStyleBackColor = true;
            this.btnContador.Click += new System.EventHandler(this.btnContador_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(178, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(436, 54);
            this.label4.TabIndex = 2;
            this.label4.Text = "Contador de Clicks";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // temperature
            // 
            this.temperature.Controls.Add(this.btnConvertir);
            this.temperature.Controls.Add(this.label3);
            this.temperature.Controls.Add(this.label2);
            this.temperature.Controls.Add(this.label1);
            this.temperature.Controls.Add(this.txtResultado);
            this.temperature.Controls.Add(this.txtEntrada);
            this.temperature.Controls.Add(this.cmbConversion);
            this.temperature.Location = new System.Drawing.Point(4, 25);
            this.temperature.Name = "temperature";
            this.temperature.Size = new System.Drawing.Size(792, 421);
            this.temperature.TabIndex = 2;
            this.temperature.Text = "Temperatura °C";
            this.temperature.UseVisualStyleBackColor = true;
            this.temperature.Click += new System.EventHandler(this.temperature_Click);
            // 
            // btnConvertir
            // 
            this.btnConvertir.Location = new System.Drawing.Point(447, 143);
            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.Size = new System.Drawing.Size(139, 36);
            this.btnConvertir.TabIndex = 14;
            this.btnConvertir.Text = "Convertir";
            this.btnConvertir.UseVisualStyleBackColor = true;
            this.btnConvertir.Click += new System.EventHandler(this.btnConvertir_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(191, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 38);
            this.label3.TabIndex = 13;
            this.label3.Text = "Temperaturas";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Resultado";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Dato";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // txtResultado
            // 
            this.txtResultado.Location = new System.Drawing.Point(447, 204);
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.Size = new System.Drawing.Size(143, 22);
            this.txtResultado.TabIndex = 10;
            this.txtResultado.TextChanged += new System.EventHandler(this.txtResultado_TextChanged);
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(198, 204);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(143, 22);
            this.txtEntrada.TabIndex = 9;
            this.txtEntrada.TextChanged += new System.EventHandler(this.txtEntrada_TextChanged);
            // 
            // cmbConversion
            // 
            this.cmbConversion.FormattingEnabled = true;
            this.cmbConversion.Items.AddRange(new object[] {
            "Fahrenheit a Celsius",
            "Celsius a Fahrenheit"});
            this.cmbConversion.Location = new System.Drawing.Point(198, 143);
            this.cmbConversion.Name = "cmbConversion";
            this.cmbConversion.Size = new System.Drawing.Size(210, 24);
            this.cmbConversion.TabIndex = 8;
            this.cmbConversion.SelectedIndexChanged += new System.EventHandler(this.cmbConversion_SelectedIndexChanged);
            // 
            // imc_weight
            // 
            this.imc_weight.Controls.Add(this.label11);
            this.imc_weight.Controls.Add(this.label10);
            this.imc_weight.Controls.Add(this.typeweight);
            this.imc_weight.Controls.Add(this.label9);
            this.imc_weight.Controls.Add(this.imc_res);
            this.imc_weight.Controls.Add(this.btncalculate);
            this.imc_weight.Controls.Add(this.label8);
            this.imc_weight.Controls.Add(this.heightenter);
            this.imc_weight.Controls.Add(this.label7);
            this.imc_weight.Controls.Add(this.weightenter);
            this.imc_weight.Location = new System.Drawing.Point(4, 25);
            this.imc_weight.Name = "imc_weight";
            this.imc_weight.Size = new System.Drawing.Size(792, 421);
            this.imc_weight.TabIndex = 3;
            this.imc_weight.Text = "IMC";
            this.imc_weight.UseVisualStyleBackColor = true;
            this.imc_weight.Click += new System.EventHandler(this.imc_weight_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(125, 196);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 16);
            this.label11.TabIndex = 9;
            this.label11.Text = "En Metros.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(550, 186);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(155, 29);
            this.label10.TabIndex = 8;
            this.label10.Text = "Clasificacion:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // typeweight
            // 
            this.typeweight.Location = new System.Drawing.Point(513, 218);
            this.typeweight.Name = "typeweight";
            this.typeweight.Size = new System.Drawing.Size(226, 22);
            this.typeweight.TabIndex = 7;
            this.typeweight.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(590, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 29);
            this.label9.TabIndex = 6;
            this.label9.Text = "IMC:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // imc_res
            // 
            this.imc_res.Location = new System.Drawing.Point(513, 139);
            this.imc_res.Name = "imc_res";
            this.imc_res.Size = new System.Drawing.Size(226, 22);
            this.imc_res.TabIndex = 5;
            // 
            // btncalculate
            // 
            this.btncalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncalculate.Location = new System.Drawing.Point(296, 177);
            this.btncalculate.Name = "btncalculate";
            this.btncalculate.Size = new System.Drawing.Size(212, 35);
            this.btncalculate.TabIndex = 4;
            this.btncalculate.Text = "CONVERTIR";
            this.btncalculate.UseVisualStyleBackColor = true;
            this.btncalculate.Click += new System.EventHandler(this.btncalculate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(64, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(205, 29);
            this.label8.TabIndex = 3;
            this.label8.Text = "Introduce tu Altura";
            // 
            // heightenter
            // 
            this.heightenter.Location = new System.Drawing.Point(69, 218);
            this.heightenter.Name = "heightenter";
            this.heightenter.Size = new System.Drawing.Size(226, 22);
            this.heightenter.TabIndex = 2;
            this.heightenter.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(81, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 29);
            this.label7.TabIndex = 1;
            this.label7.Text = "Introduce tu Peso";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // weightenter
            // 
            this.weightenter.Location = new System.Drawing.Point(69, 137);
            this.weightenter.Name = "weightenter";
            this.weightenter.Size = new System.Drawing.Size(226, 22);
            this.weightenter.TabIndex = 0;
            this.weightenter.TextChanged += new System.EventHandler(this.weightenter_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Practica - Semana 1";
            this.tabControl1.ResumeLayout(false);
            this.login.ResumeLayout(false);
            this.login.PerformLayout();
            this.cntclick.ResumeLayout(false);
            this.cntclick.PerformLayout();
            this.temperature.ResumeLayout(false);
            this.temperature.PerformLayout();
            this.imc_weight.ResumeLayout(false);
            this.imc_weight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage login;
        private System.Windows.Forms.TabPage cntclick;
        private System.Windows.Forms.TabPage temperature;
        private System.Windows.Forms.TabPage imc_weight;
        private System.Windows.Forms.Button btnConvertir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.TextBox txtEntrada;
        private System.Windows.Forms.ComboBox cmbConversion;
        private System.Windows.Forms.Button btnContador;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.TextBox ctClave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ctUsuario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox weightenter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox heightenter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox imc_res;
        private System.Windows.Forms.Button btncalculate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox typeweight;
        private System.Windows.Forms.Label label11;
    }
}

