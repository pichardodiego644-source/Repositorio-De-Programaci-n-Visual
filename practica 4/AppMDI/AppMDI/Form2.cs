using MDIEstudiantes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppMDI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            SetupGrid();
        }


        private void SetupGrid()
        {
            dvgAsignaturas.Columns.Clear();
            dvgAsignaturas.Columns.Add("colCarnet", "Carnet");
            dvgAsignaturas.Columns.Add("colNombre", "Nombre");
            dvgAsignaturas.Columns.Add("colAsignatura", "Asignatura");
            dvgAsignaturas.Columns.Add("colNota", "Nota");

            dvgAsignaturas.AllowUserToAddRows = false;
            dvgAsignaturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgAsignaturas.ReadOnly = true;

            RefreshGridFromDatos();
        }

        // Vuelve a cargar el DataGridView desde DatosCompartidos (todos los estudiantes)
        private void RefreshGridFromDatos()
        {
            dvgAsignaturas.Rows.Clear();
            foreach (var est in DatosCompartidos.Estudiantes)
            {
                if (est.Asignaturas == null || est.Asignaturas.Count == 0)
                {
                    dvgAsignaturas.Rows.Add(est.Carnet, est.Nombre, string.Empty, string.Empty);
                }
                else
                {
                    foreach (var a in est.Asignaturas)
                    {
                        dvgAsignaturas.Rows.Add(est.Carnet, est.Nombre, a.Nombre, a.Nota);
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Holis");
        }


   
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //nada :D
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            // validaciones básicas
            var carnet = txtCarnet.Text?.Trim();
            var nombre = txtNombre.Text?.Trim();
            var asig = txtAsignatura.Text?.Trim();
            var notaTxt = txtNota.Text?.Trim();

            if (string.IsNullOrWhiteSpace(carnet))
            {
                MessageBox.Show("Ingrese un carnet.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCarnet.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre del estudiante.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(asig))
            {
                MessageBox.Show("Ingrese la asignatura.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAsignatura.Focus();
                return;
            }
            if (!double.TryParse(notaTxt, out double nota))
            {
                MessageBox.Show("Ingrese una nota válida (número).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNota.Focus();
                return;
            }

            // Buscar si ya existe el estudiante por carnet
            var exist = DatosCompartidos.Estudiantes.FirstOrDefault(x => x.Carnet == carnet);
            if (exist != null)
            {
                // actualizar nombre por si cambió y añadir asignatura
                exist.Nombre = nombre;
                exist.Asignaturas.Add(new Asignatura { Nombre = asig, Nota = nota });
                DatosCompartidos.EstudianteActual = exist;
            }
            else
            {
                // crear nuevo estudiante y agregar asignatura
                var nuevo = new Estudiante
                {
                    Carnet = carnet,
                    Nombre = nombre
                };
                nuevo.Asignaturas.Add(new Asignatura { Nombre = asig, Nota = nota });
                DatosCompartidos.Estudiantes.Add(nuevo);
                DatosCompartidos.EstudianteActual = nuevo;
            }

            // refrescar vista
            RefreshGridFromDatos();

            MessageBox.Show("Asignatura añadida y estudiante guardado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // limpiar campos de asignatura (mantener Carnet/Nombre para seguir añadiendo al mismo)
            txtAsignatura.Clear();
            txtNota.Clear();
            txtAsignatura.Focus();
        }
    }
}
