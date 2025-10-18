using MDIEstudiantes;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AppMDI
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            SetupGrid();
        }
        private void SetupGrid()
        {
            // Asignatura | Nota
            dgvDatos.Columns.Clear();
            dgvDatos.Columns.Add("colNombre", "Nombre");
            dgvDatos.Columns.Add("colAsignatura", "Asignatura");
            dgvDatos.Columns.Add("colNota", "Nota");
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.ReadOnly = true;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            RefrescarListado();
        }

        private void btnVisualizar_Click_1(object sender, EventArgs e)
        {
            VisualizarPorCarnet();
        }


        private void VisualizarPorCarnet()
        {
            var carnet = txtCarnet.Text?.Trim();
            if (string.IsNullOrWhiteSpace(carnet))
            {
                MessageBox.Show("Ingrese un carnet para buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCarnet.Focus();
                return;
            }

            var est = DatosCompartidos.Estudiantes.FirstOrDefault(x => x.Carnet == carnet);
            if (est == null)
            {
                MessageBox.Show("No se encontró ningún estudiante con ese carnet.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDatos.Rows.Clear();
                return;
            }

            // Cargar asignaturas en dgvDatos
            dgvDatos.Rows.Clear();
            foreach (var a in est.Asignaturas)
            {
                dgvDatos.Rows.Add(est.Nombre,a.Nombre, a.Nota);
            }
        }

        // Método público que puede llamar Form2 para refrescar
        public void RefrescarListado()
        {
            var carnetVisible = txtCarnet.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(carnetVisible))
            {
                // volver a ejecutar la búsqueda para actualizar la vista si corresponde
                VisualizarPorCarnet();
            }
            else
            {
                // si no hay carnet escrito, simplemente limpiamos la tabla
                dgvDatos.Rows.Clear();
            }
        }


        // Cuando el Form3 se active (vuelva a primer plano), actualizamos por si añadieron estudiantes desde Form2
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            RefrescarListado();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
