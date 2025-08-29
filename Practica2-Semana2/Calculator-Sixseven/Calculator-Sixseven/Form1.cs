using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_Sixseven
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //variables
        string operador = "";
        double num1 = 0;
        double num2 = 0;
        double memoria = 0;
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtScreen.Text = "0";
            num1 = 0;
            num2 = 0;
            operador = "";

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 1) txtScreen.Text = "0";
            else txtScreen.Text = txtScreen.Text.Substring(0, txtScreen.Text.Length - 1);

        }

        private void btnUno_Click(object sender, EventArgs e)
        {
            if(txtScreen.TextLength == 0) txtScreen.Text="";
            txtScreen.Text = txtScreen.Text + 1;
        }

        private void btnDos_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 0) txtScreen.Text = "";
            txtScreen.Text = txtScreen.Text + 2;
        }

        private void btnTres_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 0) txtScreen.Text = "";
            txtScreen.Text = txtScreen.Text + 3;
        }

        private void btnCuatro_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 0) txtScreen.Text = "";
            txtScreen.Text = txtScreen.Text + 4;
        }

        private void btnCinco_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 0) txtScreen.Text = "";
            txtScreen.Text = txtScreen.Text + 5;
        }

        private void btnSeis_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 0) txtScreen.Text = "";
            txtScreen.Text = txtScreen.Text + 6;
        }

        private void btnSiete_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 0) txtScreen.Text = "";
            txtScreen.Text = txtScreen.Text + 7;
        }

        private void btnOcho_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 0) txtScreen.Text = "";
            txtScreen.Text = txtScreen.Text + 8;
        }

        private void btnNueve_Click(object sender, EventArgs e)
        {
            if (txtScreen.TextLength == 0) txtScreen.Text = "";
            txtScreen.Text = txtScreen.Text + 9;
        }

        private void btnCero_Click(object sender, EventArgs e)
        {
            txtScreen.Text = txtScreen.Text + "0";
        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            txtScreen.Text = txtScreen.Text + ".";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            operador =  "+";
            num1 = Convert.ToDouble(txtScreen.Text);
            txtScreen.Text = "0";
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            operador = "-";
            num1 = Convert.ToDouble(txtScreen.Text);
            txtScreen.Text = "0";
        }

        private void btnMultiplicacion_Click(object sender, EventArgs e)
        {
            operador = "*";
            num1 = Convert.ToDouble(txtScreen.Text);
            txtScreen.Text = "0";
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            operador = "/";
            num1 = Convert.ToDouble(txtScreen.Text);
            txtScreen.Text = "0";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            num2 = Convert.ToDouble(txtScreen.Text);
            double resultOperation = 0;

            switch (operador)
            {
                case "+":
                    resultOperation = num1 + num2;
                    break;

                case "-":
                    resultOperation = num1 - num2;
                    break;

                case "*":
                    resultOperation = num1 * num2;
                    break;

                case "/":
                    if (num2 != 0) // Evitar división por cero
                    {
                        resultOperation = num1 / num2;
                    }
                    else
                    {
                        MessageBox.Show("No se puede dividir por cero.");
                        return;
                    }
                    break;

                case "%":
                    if (num2 != 0)
                    {
                        resultOperation = num1 % num2;
                    }
                    else
                    {
                        MessageBox.Show("No se puede calcular el residuo con divisor cero.");
                        return;
                    }
                    break;
                case "^":

                    resultOperation = Math.Pow(num1, num2);
                    break;

                default:
                    MessageBox.Show("Operación no válida.");
                    return;
            }

            txtScreen.Text = resultOperation.ToString();
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnMplus_Click(object sender, EventArgs e)
        {
            double current = Convert.ToDouble(txtScreen.Text); //toma el valor en la pantalla.
            memoria += current; //suma el valor del current a la memoria.
        }

        private void btnMminus_Click(object sender, EventArgs e)
        {
            //aqui es lo contrario, en vez de sumar
            double current = Convert.ToDouble(txtScreen.Text); //toma el valor en la pantalla.
            memoria += current; //se resta.
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            txtScreen.Text = memoria.ToString(); //aqui se recupera el valor y lo muestra :D
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            memoria = 0; //limpiar la memoria, osea memory clear, (no sabia que significaba eso)
            txtScreen.Text = "0"; //opcional, mostrar por pantalla 0
        }

        private void btnPot_Click(object sender, EventArgs e)
        {
            operador = "^"; //se guarda el operador potencia
            num1 = Convert.ToDouble(txtScreen.Text); //aqui se guarda el primer operando
            txtScreen.Text = "0"; //se limpia la pantalla para que se lea la potencia.
        }

        private void btnPi_Click(object sender, EventArgs e)
        {
            txtScreen.Text = Math.PI.ToString(); //Escribirá el valor de PI en la pantalla.
        }

        private void btnPar1_Click(object sender, EventArgs e)
        {
            txtScreen.Text = txtScreen.Text + "(";
        }

        private void btnPar2_Click(object sender, EventArgs e)
        {
            txtScreen.Text = txtScreen.Text + ")";
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            double val = Convert.ToDouble(txtScreen.Text); //Obtiene el valor actual
            if (val > 0) //comprueba el dominio de log base 10
            {
                txtScreen.Text = Math.Log10(val).ToString(); //calcula el log base 10
            }
            else //si no cumple, error.
            {
                MessageBox.Show("Logaritmo solo definido para valores > 0.");
            }
        }

        private void btnRad_Click(object sender, EventArgs e)
        {
            double val = Convert.ToDouble(txtScreen.Text);
            if (val >= 0)
            {
                txtScreen.Text= Math.Sqrt(val).ToString(); //aqui se calcula la raiz CUADRADA de val si cumple la condicion. :D
            }
            else
            {
                MessageBox.Show("No se puede mostrar un radical de un número negativo");
            }
        }

        private void btnOver_Click(object sender, EventArgs e)
        {
            double val = Convert.ToDouble(txtScreen.Text);
            if (val != 0)
            {
                txtScreen.Text= (1.0 /val ).ToString(); //basicamente entra aca si el valor es distinto de 0
            }
            else
            {
                MessageBox.Show("No se puede hacer una division entre 0");
            }
        }

        private void btnFactorial_Click(object sender, EventArgs e)
        {
            double val = Convert.ToDouble(txtScreen.Text);
            if (val < 0 || Math.Floor(val) != val) //verificamos el entero a ver si no es negativo
            {
                MessageBox.Show("El Factorial esta definido solo para enteros, no para negativos");
                return;
            }
            if (val > 170)
            { 
                MessageBox.Show("El factorial es MUUUY grande para hacer un calculo con precision"); //evitar calculos erroneos pq eso es muucho
                return;
            }

            double result = 1; //si todo va bien, inicia un acumulador
            for (int i = 0; i <= (int)val; i++)  // calculo de factorial 
            {
                result *= i;
            
            }
            txtScreen.Text = result.ToString(); //resultado :DDDD
        }
    }
}
