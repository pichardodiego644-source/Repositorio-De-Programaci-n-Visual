using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Word = Microsoft.Office.Interop.Word;

namespace Threads1
{
    public partial class Form1 : Form
    {
        // --- Configuración de snapshots ---
        private int snapshotCount = 10; //max amount 
        private List<List<int>> snapshotsBurbuja = new List<List<int>>();
        private List<List<int>> snapshotsQuick = new List<List<int>>();
        private List<List<int>> snapshotsMerge = new List<List<int>>();
        private List<List<int>> snapshotsSelection = new List<List<int>>();

        private List<int> listaOriginal;
        private List<int> listaBurbuja;
        private List<int> listaQuick;
        private List<int> listaMerge;
        private List<int> listaSelection;

        private Thread hiloBurbuja;
        private volatile bool cancelarBurbuja;
        private int quickPivotesColocados;
        private Stopwatch relojBurbuja = new Stopwatch();
        private Stopwatch relojQuick = new Stopwatch();
        private Stopwatch relojMerge = new Stopwatch();
        private Stopwatch relojSelection = new Stopwatch();

        public Form1()
        {
            InitializeComponent();

            backgroundWorkerQuickSort.WorkerReportsProgress = true;
            backgroundWorkerQuickSort.WorkerSupportsCancellation = true;

            // --- ENLAZAR EVENTOS CON BACKGROUND WORKA ---
            backgroundWorkerQuickSort.DoWork -= backgroundWorkerQuickSort_DoWork;
            backgroundWorkerQuickSort.ProgressChanged -= backgroundWorkerQuickSort_ProgressChanged;
            backgroundWorkerQuickSort.RunWorkerCompleted -= backgroundWorkerQuickSort_RunWorkerCompleted;

            backgroundWorkerQuickSort.DoWork += backgroundWorkerQuickSort_DoWork;
            backgroundWorkerQuickSort.ProgressChanged += backgroundWorkerQuickSort_ProgressChanged;
            backgroundWorkerQuickSort.RunWorkerCompleted += backgroundWorkerQuickSort_RunWorkerCompleted;

            if (chartResultados.Series.Count == 0)
            {
                chartResultados.Series.Add("Tiempos");
                chartResultados.Series["Tiempos"].ChartType = SeriesChartType.Column;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listaOriginal == null || listaOriginal.Count == 0)
            {
                MessageBox.Show("Primero genera los datos.");
                return;
            }
                
            cancelarBurbuja = false;
            cancelarMerge = false;
            cancelarSelection = false;

            // Copias independientes para cada algoritmo, son para las snapshots :DDD
            listaBurbuja = new List<int>(listaOriginal);
            listaQuick = new List<int>(listaOriginal);
            listaMerge = new List<int>(listaOriginal);
            listaSelection = new List<int>(listaOriginal);

            // Parte para hacer clear a algunas snapshots residuales 
            snapshotsBurbuja.Clear();
            snapshotsQuick.Clear();
            snapshotsMerge.Clear();
            snapshotsSelection.Clear();

            // Guardar snapshot inicial en la lista de snapshots
            RecordSnapshot(listaBurbuja, snapshotsBurbuja);
            RecordSnapshot(listaQuick, snapshotsQuick);
            RecordSnapshot(listaMerge, snapshotsMerge);
            RecordSnapshot(listaSelection, snapshotsSelection);

            quickPivotesColocados = 0;
            ResetProgressLabels();

            hiloBurbuja = new Thread(OrdenarBurbuja);
            hiloBurbuja.IsBackground = true;
            hiloBurbuja.Start();

            if (!backgroundWorkerQuickSort.IsBusy)
                backgroundWorkerQuickSort.RunWorkerAsync(listaQuick);

            Task.Run(() => OrdenarSelection());
            Task.Run(() => OrdenarMerge());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = 100000;
            if (numericCantidad != null) n = (int)numericCantidad.Value; // permite elegir cantidad
            Random rnd = new Random();
            listaOriginal = new List<int>(n);
            for (int i = 0; i < n; i++)
                listaOriginal.Add(rnd.Next(0, 1000000));

            MessageBox.Show($"Lista generada correctamente con {n} números.");
        }

        private void ResetProgressLabels()
        {
            progressBurbuja.Value = 0; lblBurbuja.Text = "Burbuja: 0%";
            progressQuickSort.Value = 0; lblQuickSort.Text = "QuickSort: 0%";
            // Si existen:
            if (progressMerge != null) { progressMerge.Value = 0; lblMerge.Text = "Merge: 0%"; }
            if (progressSelection != null) { progressSelection.Value = 0; lblSelection.Text = "Selection: 0%"; }
        }


        private void OrdenarBurbuja()
        {
            relojBurbuja.Restart();
            int n = listaBurbuja.Count;
            for (int i = 0; i < n - 1; i++)
            {
                if (cancelarBurbuja) break;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (cancelarBurbuja) break;
                    if (listaBurbuja[j] > listaBurbuja[j + 1])
                    {
                        int temp = listaBurbuja[j];
                        listaBurbuja[j] = listaBurbuja[j + 1];
                        listaBurbuja[j + 1] = temp;
                    }
                }

                if (i % 100 == 0) // reporte más frecuente para que se vea movimiento
                {
                    int progreso = (int)((i / (float)n) * 100);
                    this.Invoke(new Action(() =>
                    {
                        progressBurbuja.Value = Math.Min(progreso, 100);
                        lblBurbuja.Text = $"Burbuja: {progreso}%";
                    }));

                    // Snapshot cada X iteraciones, más fluido :NERD:
                    int step = Math.Max(1, n / snapshotCount);
                    if (i % step == 0) RecordSnapshot(listaBurbuja, snapshotsBurbuja);
                }

            }

            relojBurbuja.Stop();
            this.Invoke(new Action(() =>
            {
                progressBurbuja.Value = 100;
                lblBurbuja.Text = $"Burbuja: {(cancelarBurbuja ? "Cancelado" : $"Completado en {relojBurbuja.ElapsedMilliseconds} ms")}";
                ActualizarChart("Bubble Sort", cancelarBurbuja ? -1 : (int)relojBurbuja.ElapsedMilliseconds);
            }));
        }

        private void QuickSort(List<int> lista, int izquierda, int derecha, BackgroundWorker worker)
        {
            if (worker.CancellationPending) throw new OperationCanceledException();

            if (izquierda < derecha)
            {
                int pivot = Particionar(lista, izquierda, derecha, worker);
                // Reportar progreso pa Particionar. .
                QuickSort(lista, izquierda, pivot - 1, worker);
                QuickSort(lista, pivot + 1, derecha, worker);
            }
        }

        private int Particionar(List<int> lista, int izquierda, int derecha, BackgroundWorker worker)
        {
            if (worker.CancellationPending) throw new OperationCanceledException();

            int pivote = lista[derecha];
            int i = izquierda - 1;
            for (int j = izquierda; j < derecha; j++)
            {
                if (worker.CancellationPending) throw new OperationCanceledException();

                if (lista[j] <= pivote)
                {
                    i++;
                    int temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;
                }
            }
            int temp2 = lista[i + 1];
            lista[i + 1] = lista[derecha];
            lista[derecha] = temp2;

            // Contador atómico de pivotes definitivos :D
            int colocados = Interlocked.Increment(ref quickPivotesColocados);

            // Reportar progreso de forma más frecuente y escalable:
            // calculamos un paso para notificar ~100 veces durante la ejecución
            int total = lista.Count;
            int reportEvery = Math.Max(1, total / 100);
            if (colocados % reportEvery == 0 || colocados == total)
            {
                int progreso = Math.Min(100, (int)((colocados / (float)total) * 100));
                try
                {
                    if (worker.WorkerReportsProgress) worker.ReportProgress(progreso);
                }
                catch { }

                // Tomar snapshot cuando alcanzamos un % aproximado similar
                int stepQuick = Math.Max(1, total / snapshotCount);
                if (colocados % stepQuick == 0)
                {
                    RecordSnapshot(lista, snapshotsQuick); // usa 'lista' (parámetro) en lugar de listaQuick
                }
            }


            return i + 1;


        }

        private void backgroundWorkerQuickSort_DoWork(object sender, DoWorkEventArgs e)
        {
            relojQuick.Restart();
            quickPivotesColocados = 0;
            List<int> lista = (List<int>)e.Argument;
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                QuickSort(lista, 0, lista.Count - 1, worker);
            }
            catch (OperationCanceledException)
            {
                // Cancelado por Request
                e.Cancel = true;
            }
            finally
            {
                relojQuick.Stop();
                e.Result = lista; // opcional
            }
        }

        private void backgroundWorkerQuickSort_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressQuickSort.Value = e.ProgressPercentage;
            lblQuickSort.Text = $"QuickSort: {e.ProgressPercentage}%";
        }

        private void backgroundWorkerQuickSort_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblQuickSort.Text = $"QuickSort: Cancelado";
                progressQuickSort.Value = Math.Min(progressQuickSort.Value, 100);
                ActualizarChart("QuickSort", -1);
            }
            else if (e.Error != null)
            {
                lblQuickSort.Text = $"QuickSort: Error ({e.Error.Message})";
                ActualizarChart("QuickSort", -1);
            }
            else
            {
                lblQuickSort.Text = $"QuickSort: Completado en {relojQuick.ElapsedMilliseconds} ms";
                progressQuickSort.Value = 100;
                ActualizarChart("QuickSort", (int)relojQuick.ElapsedMilliseconds);
            }
        }

        private volatile bool cancelarMerge;
        private int mergeProgresoContador;
        private void OrdenarMerge()
        {
            relojMerge.Restart();
            mergeProgresoContador = 0;
            try
            {
                MergeSort(listaMerge, 0, listaMerge.Count - 1);
            }
            catch (OperationCanceledException) { }
            relojMerge.Stop();

            this.Invoke(new Action(() =>
            {
                lblMerge.Text = $"Merge: {(cancelarMerge ? "Cancelado" : $"Completado en {relojMerge.ElapsedMilliseconds} ms")}";
                if (progressMerge != null) progressMerge.Value = 100;
                ActualizarChart("MergeSort", cancelarMerge ? -1 : (int)relojMerge.ElapsedMilliseconds);
            }));
        }

        private void MergeSort(List<int> arr, int l, int r)
        {
            if (cancelarMerge) throw new OperationCanceledException();
            if (l < r)
            {
                int m = (l + r) / 2;
                MergeSort(arr, l, m);
                if (cancelarMerge) throw new OperationCanceledException(); // Verificar después de la 1era recursión
                MergeSort(arr, m + 1, r);
                if (cancelarMerge) throw new OperationCanceledException(); // Verificar después de la 2da recursión 
                Merge(arr, l, m, r);

                if (Interlocked.Increment(ref mergeProgresoContador) % 50 == 0)
                {
                    int prog = Math.Min(100, (int)((mergeProgresoContador / (float)arr.Count) * 100));
                    if (progressMerge != null && !cancelarMerge)
                        this.Invoke(new Action(() => {
                            progressMerge.Value = Math.Min(100, prog);
                            lblMerge.Text = $"Merge: {progressMerge.Value}%";
                        }));
                }
            }
        }

        private void Merge(List<int> arr, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;
            int[] L = new int[n1];
            int[] R = new int[n2];
            for (int i = 0; i < n1; ++i) L[i] = arr[l + i];
            for (int j = 0; j < n2; ++j) R[j] = arr[m + 1 + j];
            int ii = 0, jj = 0; int k = l;
            while (ii < n1 && jj < n2)
            {
                if (L[ii] <= R[jj]) { arr[k++] = L[ii++]; } else { arr[k++] = R[jj++]; }
            }
            while (ii < n1) arr[k++] = L[ii++];
            while (jj < n2) arr[k++] = R[jj++];

            // Snapshot después de haber combinado esta sección
            if (snapshotsMerge.Count < snapshotCount)
            {
                RecordSnapshot(arr, snapshotsMerge);
            }
        }


        // ---------- SelectionSort (Task) ----------
        private volatile bool cancelarSelection;
        private void OrdenarSelection()
        {
            relojSelection.Restart();
            int n = listaSelection.Count;
            for (int i = 0; i < n - 1; i++)
            {
                if (cancelarSelection) break;
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (cancelarSelection) break;
                    if (listaSelection[j] < listaSelection[min_idx]) min_idx = j;
                }
                int temp = listaSelection[min_idx];
                listaSelection[min_idx] = listaSelection[i];
                listaSelection[i] = temp;

                if (i % 100 == 0)
                {
                    int stepSel = Math.Max(1, n / snapshotCount);
                    if (i % stepSel == 0) RecordSnapshot(listaSelection, snapshotsSelection);

                    int progreso = (int)((i / (float)n) * 100);
                    if (progressSelection != null)
                    {
                        this.Invoke(new Action(() =>
                        {
                            progressSelection.Value = Math.Min(progreso, 100);
                            lblSelection.Text = $"Selection: {progreso}%";
                        }));
                    }
                }
            }
            relojSelection.Stop();
            this.Invoke(new Action(() =>
            {
                if (progressSelection != null) progressSelection.Value = 100;
                lblSelection.Text = $"Selection: {(cancelarSelection ? "Cancelado" : $"Completado en {relojSelection.ElapsedMilliseconds} ms")}";
                ActualizarChart("SelectionSort", cancelarSelection ? -1 : (int)relojSelection.ElapsedMilliseconds);
            }));
        }

        // ---------- Detener ----------
        private void btnDetener_Click(object sender, EventArgs e)
        {
            // Cancelar Burbuja
            cancelarBurbuja = true;

            // Cancelar QuickSort
            if (backgroundWorkerQuickSort != null && backgroundWorkerQuickSort.IsBusy)
            {
                try
                {
                    backgroundWorkerQuickSort.CancelAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error cancelando QuickSort: " + ex.Message);
                }
            }

            // Cancelar MergeSort y SelectionSort
            cancelarMerge = true;
            cancelarSelection = true;

            // Esperar un poco a que los hilos respondan (opcional, pero recomendado)
            if (hiloBurbuja != null && hiloBurbuja.IsAlive)
            {
                hiloBurbuja.Join(500); // Espera máximo 500ms
            }
        }

        // ---------- Chart update ----------
        private void ActualizarChart(string nombre, int ms)
        {
            if (chartResultados == null) return;

            if (chartResultados.InvokeRequired)
            {
                chartResultados.Invoke(new Action(() => ActualizarChart(nombre, ms)));
                return;
            }

            // Nombre fijo de la serie que usábamos antes:
            string seriePrincipal = "Tiempos";

            // Buscar la serie por nombre (no usar indexador directo que lanza si no existe)
            Series s = null;
            foreach (Series sr in chartResultados.Series)
            {
                if (sr.Name == seriePrincipal) { s = sr; break; }
            }

            // Si no existe, crearla
            if (s == null)
            {
                s = new Series(seriePrincipal);
                s.ChartType = SeriesChartType.Column;
                chartResultados.Series.Add(s);

                // Asegúrate de que exista un ChartArea (necesario si el diseñador no lo creó)
                if (chartResultados.ChartAreas.Count == 0)
                    chartResultados.ChartAreas.Add(new ChartArea("Default"));
                s.ChartArea = chartResultados.ChartAreas[0].Name;
            }

            // Ahora usamos la serie (s) de forma segura
            // agrego/actualizo punto por etiqueta 'nombre'
            int idx = -1;
            for (int i = 0; i < s.Points.Count; i++)
            {
                if (s.Points[i].AxisLabel == nombre) { idx = i; break; }
            }
            if (idx >= 0) s.Points.RemoveAt(idx);

            if (ms >= 0)
            {
                var p = s.Points.Add(ms);
                p.AxisLabel = nombre;
            }
            else
            {
                var p = s.Points.Add(0);
                p.AxisLabel = nombre + " (cancelado)";
            }
        }


        // ---------- Guardar iteraciones en Word (Fragmento a Ejecutar después) ----------
        private void btnGuardarResultados_Click(object sender, EventArgs e)
        {
            GuardarResultadosWord();
        }

        private int maxElementsPerSnapshot = 200; // ajustar según conveniencia

        private void RecordSnapshot(List<int> source, List<List<int>> target)
        {
            if (source == null || target == null) return;
            lock (target)
            {
                if (target.Count >= snapshotCount) return;

                if (source.Count <= maxElementsPerSnapshot)
                    target.Add(new List<int>(source));
                else
                    target.Add(new List<int>(source.Take(maxElementsPerSnapshot))); // sólo primeros N
            }
        }

        private void GuardarResultadosWord()
        {
            // Validaciones iniciales
            if (listaOriginal == null || listaOriginal.Count == 0)
            {
                MessageBox.Show("Primero genera los datos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NUEVO, si la lista es mayor a 1000 elementos, no hay word :D
            const int maxElementsToAllowDoc = 1000;
            if (listaOriginal.Count > maxElementsToAllowDoc)
            {
                MessageBox.Show(
                    $"La lista tiene {listaOriginal.Count} elementos. No se generará un documento Word porque supera el límite de {maxElementsToAllowDoc} elementos.",
                    "Límite excedido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var algoritmos = new[]
            {
        new { Name = "Bubble Sort", List = listaBurbuja, Snapshots = snapshotsBurbuja },
        new { Name = "QuickSort", List = listaQuick, Snapshots = snapshotsQuick },
        new { Name = "MergeSort", List = listaMerge, Snapshots = snapshotsMerge },
        new { Name = "SelectionSort", List = listaSelection, Snapshots = snapshotsSelection }
    };

            // Carpeta destino: Escritorio\SAVES
            string destFolder = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "SAVES"
            );
            System.IO.Directory.CreateDirectory(destFolder);

            const int maxInlineElementsWord = 5000; // si la lista es mayor, la mostramos truncada
            string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            foreach (var alg in algoritmos)
            {
                Word.Application wordApp = null;
                Word.Document doc = null;
                try
                {
                    wordApp = new Word.Application();
                    wordApp.Visible = false; // cambia a true si quieres ver Word al crear
                    doc = wordApp.Documents.Add();

                    // --- Header ---
                    var titlePara = doc.Paragraphs.Add();
                    titlePara.Range.Text = $"RESULTADOS - {alg.Name}";
                    titlePara.Range.Font.Bold = 1;
                    titlePara.Range.Font.Size = 16;
                    titlePara.Format.SpaceAfter = 12;
                    titlePara.Range.InsertParagraphAfter();

                    // --- Info general ---
                    var infoPara = doc.Paragraphs.Add();
                    infoPara.Range.Text = $"Fecha ejecución: {DateTime.Now}\nElementos totales (original): {listaOriginal?.Count ?? 0}\n";
                    infoPara.Range.Font.Size = 12;
                    infoPara.Range.InsertParagraphAfter();

                    // --- Tabla con tiempos (4 filas: encabezado + 4 algoritmos) ---
                    var tableTitle = doc.Paragraphs.Add();
                    tableTitle.Range.Text = "\nTabla de tiempos (ms):";
                    tableTitle.Range.Font.Bold = 1;
                    tableTitle.Range.InsertParagraphAfter();

                    Word.Table tiemposTable = doc.Tables.Add(doc.Paragraphs.Add().Range, 5, 2);
                    tiemposTable.Borders.Enable = 1;
                    tiemposTable.Rows[1].Cells[1].Range.Text = "Algoritmo";
                    tiemposTable.Rows[1].Cells[2].Range.Text = "Tiempo (ms) / Estado";

                    // Llenar filas con los 4 algoritmos
                    string[] names = { "Bubble Sort", "QuickSort", "MergeSort", "SelectionSort" };
                    for (int r = 0; r < names.Length; r++)
                    {
                        tiemposTable.Rows[r + 2].Cells[1].Range.Text = names[r];
                        tiemposTable.Rows[r + 2].Cells[2].Range.Text = ObtenerTiempoAlgoritmo(names[r]);
                    }
                    tiemposTable.Range.InsertParagraphAfter();

                    // --- 1) Lista desordenada (original) ---
                    Word.Paragraph unsortedHeader = doc.Paragraphs.Add();
                    unsortedHeader.Range.Text = "\nLista desordenada (original):";
                    unsortedHeader.Range.Font.Bold = 1;
                    unsortedHeader.Range.InsertParagraphAfter();

                    var original = listaOriginal ?? new List<int>();
                    if (original.Count <= maxInlineElementsWord)
                    {
                        Word.Paragraph unsortedPara = doc.Paragraphs.Add();
                        unsortedPara.Range.Text = string.Join(", ", original);
                        unsortedPara.Range.InsertParagraphAfter();
                    }
                    else
                    {
                        // Mostrar una versión truncada dentro del Word para lectura
                        int head = Math.Min(500, original.Count);
                        int tail = 500;
                        var preview = original.Take(head).ToList();
                        preview.AddRange(new int[] { -1 }); // marcador visual 
                        preview.AddRange(original.Skip(Math.Max(0, original.Count - tail)));
                        Word.Paragraph unsortedPara = doc.Paragraphs.Add();
                        unsortedPara.Range.Text = $"(Lista demasiado larga: {original.Count} elementos). Se muestra cabeza ({head}) ... cola ({tail}):\n" +
                                                  string.Join(", ", preview.Select(x => x == -1 ? "..." : x.ToString()));
                        unsortedPara.Range.InsertParagraphAfter();
                    }

                    // --- 2) Progresión / snapshots ---
                    Word.Paragraph progHeader = doc.Paragraphs.Add();
                    progHeader.Range.Text = "\nProgresión (snapshots):";
                    progHeader.Range.Font.Bold = 1;
                    progHeader.Range.InsertParagraphAfter();

                    var snaps = alg.Snapshots;
                    if (snaps == null || snaps.Count == 0)
                    {
                        Word.Paragraph noSnapPara = doc.Paragraphs.Add();
                        noSnapPara.Range.Text = "No se tomaron snapshots durante la ordenación (revisa snapshotCount o el punto de captura).";
                        noSnapPara.Range.InsertParagraphAfter();
                    }
                    else
                    {
                        for (int i = 0; i < snaps.Count; i++)
                        {
                            var snap = snaps[i];
                            Word.Paragraph sPara = doc.Paragraphs.Add();
                            int porcentajeAprox = (int)((i / (float)Math.Max(1, snaps.Count - 1)) * 100);
                            sPara.Range.Text = $"Snapshot {i + 1}/{snaps.Count} (~{porcentajeAprox}%):";
                            sPara.Range.Font.Bold = 0;
                            sPara.Range.InsertParagraphAfter();

                            // truncar la visualización si es MUUUUY Grande
                            if (snap.Count <= maxInlineElementsWord)
                            {
                                Word.Paragraph snapPara = doc.Paragraphs.Add();
                                snapPara.Range.Text = string.Join(", ", snap);
                                snapPara.Range.InsertParagraphAfter();
                            }
                            else
                            {
                                // Mostrar los primeros y últimos 200 del snapshot
                                int head = 200;
                                int tail = 200;
                                var preview = snap.Take(head).ToList();
                                preview.Add(-1); // marcador
                                preview.AddRange(snap.Skip(Math.Max(0, snap.Count - tail)));
                                Word.Paragraph snapPara = doc.Paragraphs.Add();
                                snapPara.Range.Text = $"(Snapshot muy grande: {snap.Count} elems). Muestra cabeza {head} ... cola {tail}:\n" +
                                                      string.Join(", ", preview.Select(x => x == -1 ? "..." : x.ToString()));
                                snapPara.Range.InsertParagraphAfter();
                            }
                        }
                    }

                    // --- 3) Lista final ordenada ---
                    Word.Paragraph finalHeader = doc.Paragraphs.Add();
                    finalHeader.Range.Text = "\nLista final ordenada:";
                    finalHeader.Range.Font.Bold = 1;
                    finalHeader.Range.InsertParagraphAfter();

                    var sorted = alg.List ?? new List<int>();
                    if (sorted.Count <= maxInlineElementsWord)
                    {
                        Word.Paragraph finalPara = doc.Paragraphs.Add();
                        finalPara.Range.Text = string.Join(", ", sorted);
                        finalPara.Range.InsertParagraphAfter();
                    }
                    else
                    {
                        int head = Math.Min(500, sorted.Count);
                        int tail = 500;
                        var preview = sorted.Take(head).ToList();
                        preview.Add(-1);
                        preview.AddRange(sorted.Skip(Math.Max(0, sorted.Count - tail)));
                        Word.Paragraph finalPara = doc.Paragraphs.Add();
                        finalPara.Range.Text = $"(Lista final muy larga: {sorted.Count} elementos). Muestra cabeza ({head}) ... cola ({tail}):\n" +
                                               string.Join(", ", preview.Select(x => x == -1 ? "..." : x.ToString()));
                        finalPara.Range.InsertParagraphAfter();
                    }

                    // --- Guardar documento Word (Puede ser inestable si son +1000 elementos) ---
                    string ruta = System.IO.Path.Combine(destFolder, $"{alg.Name}_Resultados_{timeStamp}.docx");
                    doc.SaveAs2(ruta);

                    // Notificación
                    MessageBox.Show($"Documento guardado: {ruta}", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar doc de {alg.Name}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    try { LiberarRecursosWord(wordApp, doc); } catch { }
                }
            } // foreach
        }





        // Mévtodo auxiliar para obtener los tiempos
        private string ObtenerTiempoAlgoritmo(string algoritmo)
        {
            switch (algoritmo)
            {
                case "Bubble Sort":
                    return cancelarBurbuja ? "Cancelado" : $"{relojBurbuja.ElapsedMilliseconds} ms";
                case "QuickSort":
                    return backgroundWorkerQuickSort.CancellationPending ? "Cancelado" : $"{relojQuick.ElapsedMilliseconds} ms";
                case "MergeSort":
                    return cancelarMerge ? "Cancelado" : $"{relojMerge.ElapsedMilliseconds} ms";
                case "SelectionSort":
                    return cancelarSelection ? "Cancelado" : $"{relojSelection.ElapsedMilliseconds} ms";
                default:
                    return "N/A";
            }
        }
        private void LiberarRecursosWord(Word.Application wordApp, Word.Document doc)
        {
            try
            {
                if (doc != null)
                {
                    doc.Close(SaveChanges: false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                }
                if (wordApp != null)
                {
                    wordApp.Quit(SaveChanges: false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error liberando recursos Word: " + ex.Message);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        // ---------- Manejo de cierre de Form para liberar recursos ----------
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cancelarBurbuja = true;
            cancelarMerge = true;
            cancelarSelection = true;
            if (backgroundWorkerQuickSort.IsBusy) backgroundWorkerQuickSort.CancelAsync();
            base.OnFormClosing(e);
        }

        private void lblQuickSort_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LOL");
        }

        private void btnGuardarResultados_Click_1(object sender, EventArgs e)
        {
            GuardarResultadosWord();
        }
    }
}
