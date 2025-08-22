using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Practica_1___Semana_1
{
    public partial class Form1 : Form
    {
        int contador = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {

        }

        private void cntclick_Click(object sender, EventArgs e)
        {

        }

        private void temperature_Click(object sender, EventArgs e)
        {

        }

        private void imc_weight_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbConversion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtEntrada_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            double valor;
            bool confirmacion = double.TryParse(txtEntrada.Text, out valor);
            if (!confirmacion)
            {
                MessageBox.Show("SOLO SE ADMITEN NÚMEROS, NO CARACTERES");
                return;
            }
            string opcion = cmbConversion.SelectedItem?.ToString();

            if (opcion == "Fahrenheit a Celsius")
            {
                double resultado = (valor - 32) * 5 / 9;
                txtResultado.Text = resultado.ToString("F2");
            }
            else if (opcion == "Celsius a Fahrenheit")
            {
                double resultado = (valor * 9 / 5) + 32;
                txtResultado.Text = resultado.ToString("F2");
            }
            else
            {
                MessageBox.Show("Selecciona una opción de conversión.");
            }
        }

        private void txtResultado_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnContador_Click(object sender, EventArgs e)
        {
            contador++;
            btnContador.Text = ("TOTAL DE CLICKS: " + contador);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        int intentos = 3;
        private void btIniciar_Click(object sender, EventArgs e)
        {
            string usuario, clave;
    
            usuario = ctUsuario.Text;
            clave = ctClave.Text;
            // para el ejemplo : user - admin, clave - 1234
            do
            {
                --intentos;
                if (usuario == "admin" && clave == "1234")
                {
                    MessageBox.Show("Ha ingresado " + usuario);
                    return;

                }
                else
                {
                    if (intentos > 0)
                    {
                        MessageBox.Show("Usuario o clave incorrectos, le quedan " + intentos + " intentos");
                        return;
                    }
                    else 
                    { 
                         MessageBox.Show("Ha agotado sus intentos, vuelva a intentarlo mas tarde");
                        Close();
                    }

                }
            } while (intentos > 0);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btncalculate_Click(object sender, EventArgs e)
        {
            double peso;

            bool confirmweight = double.TryParse(weightenter.Text, out peso);

            double altura;

            bool confirmheight = double.TryParse(heightenter.Text, out altura);

            double resultado; 

            resultado = peso / (Math.Pow(altura, 2));

            imc_res.Text = resultado.ToString("F4");

            if (resultado < 18.5)
            {

                typeweight.Text = "Bajo";


            }
            else if (resultado < 24.9)
            {
                typeweight.Text = "Normal";

            }
            else if (resultado < 29.9)
            {
                typeweight.Text = "Sobrepeso";

            }
            else if (resultado > 30)
            {
                typeweight.Text = "Obeso";

            }







        }

        private void weightenter_TextChanged(object sender, EventArgs e)
        {

        }

        private void ctUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
