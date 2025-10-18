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
using System.Windows.Forms.DataVisualization.Charting;

namespace AppMDI
{
    public partial class Form4 : Form
    {
        private const double UmbralChart = 60.0;
        public Form4()
        {
            InitializeComponent();
            SetupGrid();
            SetupChart();
        }


        private void SetupGrid()
        {
            DgvMejorNota.Columns.Clear();
            DgvMejorNota.Columns.Add("colCarnet", "Carnet");
            DgvMejorNota.Columns.Add("colNombre", "Nombre");
            DgvMejorNota.Columns.Add("colAsignatura", "Asignatura");
            DgvMejorNota.Columns.Add("colNota", "Nota");
            DgvMejorNota.AllowUserToAddRows = false;
            DgvMejorNota.ReadOnly = true;
        }
        private void SetupChart()
        {
            // Limpiar y crear area/serie base
            chNotas.Series.Clear();
            chNotas.ChartAreas.Clear();
            chNotas.Titles.Clear();

            var area = new ChartArea("AreaPromedios");
            chNotas.ChartAreas.Add(area);

            chNotas.Titles.Add("Promedio de Estudiantes ");
            chNotas.Legends.Clear();

            var series = new Series("Promedios")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                IsValueShownAsLabel = true, // muestra valor encima de la barra
            };
            chNotas.Series.Add(series);

            // Ajustes visuales
            chNotas.ChartAreas[0].AxisX.LabelStyle.Angle = -35;
            chNotas.ChartAreas[0].AxisX.Interval = 1;
            chNotas.ChartAreas[0].AxisX.Title = "Estudiante (Carnet / Nombre)";
            chNotas.ChartAreas[0].AxisY.Title = "Promedio";
            chNotas.ChartAreas[0].RecalculateAxesScale();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            RefreshBestGrades();
        }

        public void RefreshBestGrades()
        {
            DgvMejorNota.Rows.Clear();

            foreach (var est in DatosCompartidos.Estudiantes)
            {
                if (est.Asignaturas == null) continue;
                foreach (var a in est.Asignaturas)
                {
                    DgvMejorNota.Rows.Add(est.Carnet, est.Nombre, a.Nombre, a.Nota);
                    //if (a.Nota >= 80.0)
                    //{
                    //    DgvMejorNota.Rows.Add(est.Carnet, est.Nombre, a.Nombre, a.Nota);
                    //}
                }
            }
            RefreshChart();
            
        }

        private void RefreshChart()
        {
            var series = chNotas.Series["Promedios"];
            series.Points.Clear();

            // Obtener estudiantes con promedio >= UmbralChart
            var proms = DatosCompartidos.Estudiantes
                         .Where(e => e.Asignaturas != null && e.Asignaturas.Count > 0)
                         .Select(e => new { e.Carnet, e.Nombre, Prom = Math.Round(e.Promedio, 2) })
                         .Where(x => x.Prom >= UmbralChart)
                         .OrderByDescending(x => x.Prom)
                         .ToList();

            foreach (var item in proms)
            {
                var label = $"{item.Carnet}\n{item.Nombre}";
                var p = series.Points.AddXY(label, item.Prom);
                // Opcional: tooltip y etiqueta con valor
                series.Points[p].ToolTip = $"{item.Nombre} ({item.Carnet}): {item.Prom}";
                series.Points[p].Label = item.Prom.ToString("0.##");
            }

            // Ajustes finales
            if (chNotas.ChartAreas.Count > 0)
            {
                chNotas.ChartAreas[0].RecalculateAxesScale();
            }
        }

        //Nuevos elementos: Un chart que muestre EL PROMEDIO de cada estudiante, no aparecerá un estudiante que tenga un promedio menor a 85.
        //Nombre del Chart: chNotas.


        private void chNotas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nuh uh");
        }
    }
}
