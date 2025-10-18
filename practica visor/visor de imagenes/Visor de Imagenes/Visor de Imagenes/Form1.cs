using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visor_de_Imagenes
{
    public partial class Form1 : Form
    {


        //para las rutas:
        private List<string> imagePaths = new List<string>();

        // Lista de imágenes originales cargadas desde Resources
        private List<Image> originalImages = new List<Image>();

        // Lista de imágenes de trabajo 
        private List<Image> workingImages = new List<Image>();

        // Nombres de las imágenes (para combobox y status)
        private List<string> imageNames = new List<string>();

        // índice actual en la colección
        private int currentIndex = 0;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            // Context menu para la picture box :D
            var cms = new ContextMenuStrip();
            var copy = new ToolStripMenuItem("Copiar");
            var rotateLeft = new ToolStripMenuItem("Girar imagen 90° a la izquierda");
            var rotateRight = new ToolStripMenuItem("Girar imagen 90° a la derecha");

            copy.Click += Copy_Click;
            rotateLeft.Click += RotateLeft_Click;
            rotateRight.Click += RotateRight_Click;

            // añadir el copy primero
            cms.Items.AddRange(new ToolStripItem[] { copy, rotateLeft, rotateRight });
            pbVisorImage.ContextMenuStrip = cms;

            rotateLeft.Click += RotateLeft_Click;
            rotateRight.Click += RotateRight_Click;
            cms.Items.AddRange(new ToolStripItem[] { rotateLeft, rotateRight });
            pbVisorImage.ContextMenuStrip = cms;
        }



        private void Copy_Click(object sender, EventArgs e)
        {
            if (pbVisorImage.Image == null)
            {
                MessageBox.Show("No hay imagen para copiar.", "Copiar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // pegar la imagen al clipboard
                // (Usar pbVisorImage.Image para tomar la versión mostrada — por ejemplo, cuando la imagen se está en escala de grises).
                Clipboard.SetImage(pbVisorImage.Image);
                // OPCIONAL, se muestra mensaje (innecesario pero ajá :D)
                // MessageBox.Show("Imagen copiada al portapapeles.", "Copiar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al copiar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            originalImages.Clear();
            workingImages.Clear();
            imageNames.Clear();
            imagePaths.Clear();

            // Carga de imágenes desde Resources 
            originalImages.Add(Properties.Resources.rdr1);
            originalImages.Add(Properties.Resources.rdr2);
            originalImages.Add(Properties.Resources.rdr3);
            originalImages.Add(Properties.Resources.rdr4);

            imageNames.Add("rdr1");
            imageNames.Add("rdr2");
            imageNames.Add("rdr3");
            imageNames.Add("rdr4");

            // En este caso, son recursos, así que se añaden así.
            imagePaths.Add("(Recurso añadido) rdr1");
            imagePaths.Add("(Recurso añadido) rdr2");
            imagePaths.Add("(Recurso añadido) rdr3");
            imagePaths.Add("(Recurso añadido) rdr4");

            // Crear clones para workingImages para lo de guardar :D
            foreach (var img in originalImages)
            {
                workingImages.Add((Image)img.Clone());
            }

            // combobox con los nombres
            cbImagenAc.Items.Clear();
            cbImagenAc.Items.AddRange(imageNames.ToArray());

            // Seleccionar la primera imagen
            if (workingImages.Count > 0)
            {
                currentIndex = 0;
                cbImagenAc.SelectedIndex = currentIndex;
                ShowCurrentImage();
            }

            // opciones por default...
            cbNormal.Checked = true;
            rbCentral.Checked = true;
            normalToolStripMenuItem.Checked = true;
            centradaToolStripMenuItem.Checked = true;
            toolStripStatusLabel1.Text = imagePaths[currentIndex]; // aqui se usaria la ruta
        }
        private void ShowCurrentImage()
        {
            if (workingImages == null || workingImages.Count == 0)
            {
                pbVisorImage.Image = null;
                toolStripStatusLabel1.Text = "";
                return;
            }

            // indice normalizado
            if (currentIndex < 0) currentIndex = 0;
            if (currentIndex >= workingImages.Count) currentIndex = workingImages.Count - 1;

            // imagen de trabajo actual
            Image toShow = workingImages[currentIndex];

            // Si la opción "Escala de grises" está activa, se va a mostrar la imagen con su versión
            if (cbEscalaGris.Checked || escalaDeGrisesToolStripMenuItem.Checked || tsGris.Checked)
            {
                // devuelve un nuevo bitmap
                pbVisorImage.Image = ConvertToGrayscale((Bitmap)workingImages[currentIndex]);
            }
            else
            {
                pbVisorImage.Image = workingImages[currentIndex];
            }

            // Aplicar SizeMode 
            if (rbCentral.Checked || centradaToolStripMenuItem.Checked || tsCentral.Checked)
            {
                pbVisorImage.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else if (rbAjust.Checked || ajustarToolStripMenuItem.Checked || tsAjustar.Checked)
            {
                pbVisorImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else // zoom
            {
                pbVisorImage.SizeMode = PictureBoxSizeMode.Zoom;
            }
            // Actualizar status con la ruta completa (como aqui no existe ja ja ja)
            toolStripStatusLabel1.Text = imagePaths.ElementAtOrDefault(currentIndex)
                ?? imageNames.ElementAtOrDefault(currentIndex)
                ?? "";

        }


        // ----------------- HELPER ---------------------
        private void SetGrayscale(bool on)
        {
            cbEscalaGris.Checked = on;
            escalaDeGrisesToolStripMenuItem.Checked = on;
            tsGris.Checked = on;

            cbNormal.Checked = !on;
            normalToolStripMenuItem.Checked = !on;

            ShowCurrentImage();
        }

        private void SetSizeMode(PictureBoxSizeMode mode)
        {
            switch (mode)
            {
                case PictureBoxSizeMode.CenterImage:
                    rbCentral.Checked = true;
                    centradaToolStripMenuItem.Checked = true;
                    tsCentral.Checked = true;
                    rbAjust.Checked = rbZoom.Checked = false;
                    ajustarToolStripMenuItem.Checked = zoomToolStripMenuItem.Checked = false;
                    tsAjustar.Checked = tsZoom.Checked = false;
                    break;
                case PictureBoxSizeMode.StretchImage:
                    rbAjust.Checked = true;
                    ajustarToolStripMenuItem.Checked = true;
                    tsAjustar.Checked = true;
                    rbCentral.Checked = rbZoom.Checked = false;
                    centradaToolStripMenuItem.Checked = zoomToolStripMenuItem.Checked = false;
                    tsCentral.Checked = tsZoom.Checked = false;
                    break;
                default:
                    rbZoom.Checked = true;
                    zoomToolStripMenuItem.Checked = true;
                    tsZoom.Checked = true;
                    rbCentral.Checked = rbAjust.Checked = false;
                    centradaToolStripMenuItem.Checked = ajustarToolStripMenuItem.Checked = false;
                    tsCentral.Checked = tsAjustar.Checked = false;
                    break;
            }

            ShowCurrentImage();
        }
        // ----------------- Navegación -----------------
        private void btFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            ShowCurrentImage();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0) currentIndex--;
            ShowCurrentImage();
        }

        private void btForward_Click(object sender, EventArgs e)
        {
            if (currentIndex < workingImages.Count - 1) currentIndex++;
            ShowCurrentImage();
        }

        private void btLast_Click(object sender, EventArgs e)
        {
            if (workingImages.Count == 0) return;
            currentIndex = Math.Max(0, workingImages.Count - 1);
            ShowCurrentImage();
        }

        private void cbImagenAc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cbImagenAc.SelectedIndex;
            if (idx >= 0 && idx < workingImages.Count)
            {
                currentIndex = idx;
                ShowCurrentImage();
            }
        }

        // ----------------- Guardar -----------------
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pbVisorImage.Image == null)
            {
                MessageBox.Show("No hay imagen para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp|GIF Image|*.gif|TIFF Image|*.tif;*.tiff";
                sfd.Title = "Guardar imagen editada";
                sfd.FileName = imageNames[currentIndex];

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Determinar el formato (png, jpeg, etc)
                    ImageFormat fmt = ImageFormat.Png;
                    string ext = System.IO.Path.GetExtension(sfd.FileName).ToLower();
                    switch (ext)
                    {
                        case ".jpg":
                        case ".jpeg":
                            fmt = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            fmt = ImageFormat.Bmp;
                            break;
                        case ".gif":
                            fmt = ImageFormat.Gif;
                            break;
                        case ".tif":
                        case ".tiff":
                            fmt = ImageFormat.Tiff;
                            break;
                        default:
                            fmt = ImageFormat.Png;
                            break;
                    }

                    // Si se está mostrando la versión en escala de grises, guardamos la imagen actual del PictureBox
                    // porque pbVisorImage.Image puede ser una copia gris. Si no, guardamos el workingImages[currentIndex].
                    Image toSave = pbVisorImage.Image ?? workingImages[currentIndex];

                    try
                    {
                        toSave.Save(sfd.FileName, fmt);
                        MessageBox.Show("Imagen guardada correctamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ----------------- Visión: Normal / Escala de grises -----------------
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Reestablecer la imagen actual a su original
            workingImages[currentIndex].Dispose();
            workingImages[currentIndex] = (Image)originalImages[currentIndex].Clone();

            // fixes para desactivar checkbox/ToolStrip/ToolStripButton de escala de grises
            cbEscalaGris.Checked = false;
            escalaDeGrisesToolStripMenuItem.Checked = false;
            tsGris.Checked = false;
            cbNormal.Checked = true;
            normalToolStripMenuItem.Checked = true;


            workingImages[currentIndex].Dispose();
            workingImages[currentIndex] = (Image)originalImages[currentIndex].Clone();
            SetGrayscale(false);

            ShowCurrentImage();
        }

        private void escalaDeGrisesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGrayscale(true);
            // activar el estado de escala de grises
            cbEscalaGris.Checked = true;
            escalaDeGrisesToolStripMenuItem.Checked = true;
            tsGris.Checked = true;
            cbNormal.Checked = false;
            normalToolStripMenuItem.Checked = false;

            ShowCurrentImage();
        }

        private void cbNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNormal.Checked)
            {
                normalToolStripMenuItem.Checked = true;
                cbEscalaGris.Checked = false;
                escalaDeGrisesToolStripMenuItem.Checked = false;
                ShowCurrentImage();
            }
        }

        private void cbEscalaGris_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEscalaGris.Checked)
            {
                escalaDeGrisesToolStripMenuItem.Checked = true;
                cbNormal.Checked = false;
                normalToolStripMenuItem.Checked = false;
                ShowCurrentImage();
            }
            else
            {
                // si se quita la casilla, vuelve al formato normal
                ShowCurrentImage();
            }
        }

        // ----------------- Tamaño: Centrada / Ajustar / Zoom -----------------
        private void centradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbCentral.Checked = true;
            centradaToolStripMenuItem.Checked = true;
            ajustarToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = false;
            ShowCurrentImage();
        }

        private void ajustarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbAjust.Checked = true;
            ajustarToolStripMenuItem.Checked = true;
            centradaToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = false;
            ShowCurrentImage();
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbZoom.Checked = true;
            zoomToolStripMenuItem.Checked = true;
            centradaToolStripMenuItem.Checked = false;
            ajustarToolStripMenuItem.Checked = false;
            ShowCurrentImage();
        }

        private void rbCentral_CheckedChanged(object sender, EventArgs e)
        {
            if(rbCentral.Checked) SetSizeMode(PictureBoxSizeMode.CenterImage);
        }

        private void rbAjust_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAjust.Checked) SetSizeMode(PictureBoxSizeMode.StretchImage);
        }

        private void rbZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (rbZoom.Checked) SetSizeMode(PictureBoxSizeMode.Zoom);
        }

        // ----------------- Rotaciones por menú contextual -----------------
        private void RotateLeft_Click(object sender, EventArgs e)
        {
            RotateCurrentImage(RotateFlipType.Rotate270FlipNone); // 90° a la izquierda == 270° a la derecha
        }

        private void RotateRight_Click(object sender, EventArgs e)
        {
            RotateCurrentImage(RotateFlipType.Rotate90FlipNone);
        }

        private void RotateCurrentImage(RotateFlipType type)
        {
            // cambio en imagen de working pa guardar
            workingImages[currentIndex].RotateFlip(type);

            ShowCurrentImage();
        }

        // ----------------- Helper: convertir a escala de grises -----------------
        private Bitmap ConvertToGrayscale(Bitmap source)
        {
            // bit map con nueva escala
            Bitmap bmp = new Bitmap(source.Width, source.Height);

            // Matriz de color
            // para convertir !! IMPORTANTE
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[]{0.3f, 0.3f, 0.3f, 0, 0},
                    new float[]{0.59f,0.59f,0.59f,0,0},
                    new float[]{0.11f,0.11f,0.11f,0,0},
                    new float[]{0,0,0,1,0},
                    new float[]{0,0,0,0,1}
                });

            using (Graphics g = Graphics.FromImage(bmp))
            using (ImageAttributes ia = new ImageAttributes())
            {
                ia.SetColorMatrix(colorMatrix);
                g.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, source.Width, source.Height, GraphicsUnit.Pixel, ia);
            }

            return bmp;
        }

        // ----------------- Liberar recursos al cerrar el form -----------------
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Dispose imágenes
            foreach (var img in workingImages) img.Dispose();
            foreach (var img in originalImages) img.Dispose();
        }
        // ------ Handlers del diseñador (por alguna razon no funciona si no los pones) ------
        private void cbEscalaGris_CheckedChanged_1(object sender, EventArgs e)
        {
            // Esta y más es solamente reusar lo que ya está hecho)
            cbEscalaGris_CheckedChanged(sender, e);
        }

        private void btLast_Click_1(object sender, EventArgs e)
        {
            btLast_Click(sender, e);
        }

        private void btForward_Click_1(object sender, EventArgs e)
        {
            btForward_Click(sender, e);
        }

        private void btBack_Click_1(object sender, EventArgs e)
        {
            btBack_Click(sender, e);
        }

        private void btFirst_Click_1(object sender, EventArgs e)
        {
            btFirst_Click(sender, e);
        }

        // ToolStripButtons (iconos) 

        private void tsNormal_Click(object sender, EventArgs e) => SetGrayscale(false);
        private void tsGris_Click(object sender, EventArgs e) => SetGrayscale(true);
        private void tsCentral_Click(object sender, EventArgs e) => SetSizeMode(PictureBoxSizeMode.CenterImage);
        private void tsAjustar_Click(object sender, EventArgs e) => SetSizeMode(PictureBoxSizeMode.StretchImage);
        private void tsZoom_Click(object sender, EventArgs e) => SetSizeMode(PictureBoxSizeMode.Zoom);

        // RadioButtons 
        private void rbCentral_CheckedChanged_1(object sender, EventArgs e)
        {
            rbCentral_CheckedChanged(sender, e);
        }

        private void rbAjust_CheckedChanged_1(object sender, EventArgs e)
        {
            rbAjust_CheckedChanged(sender, e);
        }

        private void rbZoom_CheckedChanged_1(object sender, EventArgs e)
        {
            rbZoom_CheckedChanged(sender, e);
        }

        // Menú 
        private void normalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            normalToolStripMenuItem_Click(sender, e);
        }

        private void escalaDeGrisesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            escalaDeGrisesToolStripMenuItem_Click(sender, e);
        }

        private void centradaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            centradaToolStripMenuItem_Click(sender, e);
        }

        private void ajustarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ajustarToolStripMenuItem_Click(sender, e);
        }

        private void zoomToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            zoomToolStripMenuItem_Click(sender, e);
        }

        // ComboBox seleccionado (sufijo _1) 
        private void cbImagenAc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cbImagenAc_SelectedIndexChanged(sender, e);
        }
        private void cbNormal_CheckedChanged_1(object sender, EventArgs e)
        {
            cbNormal_CheckedChanged(sender, e);
        }

        // Status label (puede quedar vacío o mostrar info)
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            //aqui nada, solo le di por accidente ajajaj
            MessageBox.Show("Hola");
        }

        private void pbVisorImage_Click(object sender, EventArgs e)
        {

        }
    }
}
