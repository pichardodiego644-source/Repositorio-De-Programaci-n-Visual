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
    public partial class MDIEstudiantes : Form
    {
        public MDIEstudiantes()
        {
            InitializeComponent();
        }

        private void ingresarEstudianteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unique(new Form2());
        }

        private void verEstudianteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unique(new Form3());
        }

        private void btnForm2_Click(object sender, EventArgs e)
        {
            // Para que sea no modal, si ya hay un form2 abierto, traerlo al frente 
            // sino crear uno 
            var abierto = this.MdiChildren.OfType<Form2>().FirstOrDefault();
            if (abierto != null)
            {
                abierto.BringToFront();
                return;
            }

            var f2 = new Form2 { MdiParent = this };
            f2.Show();
        }

        private void btnForm3_Click(object sender, EventArgs e)
        {
            var abierto = this.MdiChildren.OfType<Form3>().FirstOrDefault();
            if (abierto != null)
            {
                abierto.BringToFront();
                return;
            }

            var f3 = new Form3 { MdiParent = this };
            f3.Show();

        }

        private void Unique (Form frmNuevo)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            frmNuevo.MdiParent = this;
            frmNuevo.WindowState = FormWindowState.Maximized;
            frmNuevo.Show();
        }



        private void mejoresPromediosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unique(new Form4());
        }
        private void btnForm4_Click(object sender, EventArgs e)
        {
            var abierto = this.MdiChildren.OfType<Form4>().FirstOrDefault();
            if (abierto != null)
            {
                abierto.BringToFront();
                return;
            }

            var f4 = new Form4 { MdiParent = this };
            f4.Show();
        }
    }
}
