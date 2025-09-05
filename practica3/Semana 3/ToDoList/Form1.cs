using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{

    public partial class Form1 : Form
    {
        public class Tarea
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime Fecha { get; set; }
            public string Lugar { get; set; }
            public string Estado { get; set; }
        }
        List<Tarea> listaTareas = new List<Tarea>();
        public Form1()
        {

            InitializeComponent();

            cmbBuscar.Items.Clear();
            cmbBuscar.Items.AddRange(new object[] { "Por codigo", "Por Estado", "Por Rango de Fecha" });
            cmbBuscar.SelectedIndex = 0;



            // Se ocultan los cositos dtp
            if (this.Controls.Contains(dtpDesde)) dtpDesde.Visible = false;
            if (this.Controls.Contains(dtpHasta)) dtpHasta.Visible = false;
            if (this.Controls.Contains(cbpros)) dtpHasta.Visible = false;




        }
        // para el refresh
        private void ActualizarGrid()
        {
            dgvTareas.DataSource = null;
            dgvTareas.DataSource = listaTareas;
        }
        // para el agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Tarea nueva = new Tarea()
            {
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Fecha = dtpFecha.Value,
                Lugar = txtLugar.Text,
                Estado = cmbEstado.SelectedItem.ToString()
            };

            listaTareas.Add(nueva);
            ActualizarGrid();
            MessageBox.Show("Tarea agregada correctamente.");

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.SelectedRows.Count > 0)
            {
                int index = dgvTareas.SelectedRows[0].Index;
                listaTareas[index].Codigo = txtCodigo.Text;
                listaTareas[index].Nombre = txtNombre.Text;
                listaTareas[index].Descripcion = txtDescripcion.Text;
                listaTareas[index].Fecha = dtpFecha.Value;
                listaTareas[index].Lugar = txtLugar.Text;
                listaTareas[index].Estado = cmbEstado.SelectedItem.ToString();

                ActualizarGrid();
                MessageBox.Show("Tarea editada correctamente.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTareas.SelectedRows.Count > 0)
            {
                int index = dgvTareas.SelectedRows[0].Index;
                listaTareas.RemoveAt(index);
                ActualizarGrid();
                MessageBox.Show("Tarea eliminada correctamente.");
            }
        }

        private void dgvTareas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCodigo.Text = dgvTareas.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNombre.Text = dgvTareas.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDescripcion.Text = dgvTareas.Rows[e.RowIndex].Cells[2].Value.ToString();
                dtpFecha.Value = (DateTime)dgvTareas.Rows[e.RowIndex].Cells[3].Value;
                txtLugar.Text = dgvTareas.Rows[e.RowIndex].Cells[4].Value.ToString();
                cmbEstado.SelectedItem = dgvTareas.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void cmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cmbBuscar.SelectedItem?.ToString();

            bool esRango = opcion == "Por Rango de Fecha";

            // ++ controles para los txt y el date del rango
            if (this.Controls.Contains(txtBuscar)) txtBuscar.Visible = !esRango;
            if (this.Controls.Contains(dtpDesde)) dtpDesde.Visible = esRango;
            if (this.Controls.Contains(dtpHasta)) dtpHasta.Visible = esRango;
            if (this.Controls.Contains(cbpros)) cbpros.Visible = opcion == "Por Estado";
            if (opcion== "Por Estado")
            {
             txtBuscar.Visible = false;
            }
        }
        //vacio pq lo modifiqué sin querer (perdón)
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {



        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbBuscar.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un tipo de búsqueda.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string opcion = cmbBuscar.SelectedItem.ToString();

            // Por si acaso, si la lista esta vacia...
            if (listaTareas == null || listaTareas.Count == 0)
            {
                MessageBox.Show("No hay tareas para buscar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<Tarea> resultados = new List<Tarea>();

            switch (opcion)
            {
                case "Por codigo":
                    string textoCodigo = (txtBuscar.Text ?? "").Trim();
                    if (string.IsNullOrEmpty(textoCodigo))
                    {
                        MessageBox.Show("Ingrese el código a buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Búsqueda que contenga el texto, case-insensitive, funcion principal
                    resultados = listaTareas
                        .Where(t => !string.IsNullOrEmpty(t.Codigo) &&
                                    t.Codigo.IndexOf(textoCodigo, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();
                    break;
                case "Por Estado":
                    txtBuscar.Visible = false;

                    string estadoSeleccionado = (cbpros.SelectedItem?.ToString() ?? "").Trim();

                    if (string.IsNullOrEmpty(estadoSeleccionado))
                    {
                        MessageBox.Show("Seleccione un estado en la lista desplegable.",
                                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    resultados = listaTareas
                        .Where(t => !string.IsNullOrEmpty(t.Estado) &&
                                    t.Estado.Equals(estadoSeleccionado, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;


                case "Por Rango de Fecha":
                    // leer las fechas de los datetimepickers
                    DateTime desde = dtpDesde.Value.Date;
                    DateTime hasta = dtpHasta.Value.Date;

                    if (desde > hasta)
                    {
                        MessageBox.Show("La fecha 'Desde' no puede ser mayor que 'Hasta'.", "Error en fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    resultados = listaTareas
                        .Where(t => t.Fecha.Date >= desde && t.Fecha.Date <= hasta)
                        .ToList();
                    break;

                default:
                    resultados = listaTareas.ToList(); // fallback 
                    break;
            }

            // Mostrar resultados en el grid
            dgvTareas.DataSource = null;
            dgvTareas.DataSource = resultados; 
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ActualizarGrid();
            if (this.Controls.Contains(txtBuscar)) txtBuscar.Text = "";
            cmbBuscar.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbpros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
