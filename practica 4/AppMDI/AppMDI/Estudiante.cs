using System.Collections.Generic;
using System.Linq;

namespace MDIEstudiantes
{
    public class Estudiante
    {
        public string Carnet { get; set; }
        public string Nombre { get; set; }
        public List<Asignatura> Asignaturas { get; set; } = new List<Asignatura>();
        public double Promedio => Asignaturas != null && Asignaturas.Count > 0
                                  ? Asignaturas.Average(a => a.Nota)
                                  : 0.0;
    }

    public class Asignatura
    {
        public string Nombre { get; set; }
        public double Nota { get; set; }
    }

    public static class DatosCompartidos
    {
        // Lista global de estudiantes (Viene por defecto con la explicación...)
        public static List<Estudiante> Estudiantes { get; } = new List<Estudiante>();

        // Estudiante seleccionado/cargado recientemente (Aca se hizo pero en si, puede ser opcional :D)
        public static Estudiante EstudianteActual { get; set; }

        // Se agrega o actualiza por carnet
        public static void AgregarOModificar(Estudiante e)
        {
            if (e == null) return;
            var exist = Estudiantes.FirstOrDefault(x => x.Carnet == e.Carnet);
            if (exist != null)
            {
                exist.Nombre = e.Nombre;
                exist.Asignaturas = e.Asignaturas;
                EstudianteActual = exist;
            }
            else
            {
                Estudiantes.Add(e);
                EstudianteActual = e;
            }
        }

        // Se obtienen los promedios (Estudiante - Promedio)
        public static IEnumerable<(Estudiante Est, double Prom)> GetPromedios()
        {
            return Estudiantes.Select(x => (x, x.Promedio));
        }
    }
}