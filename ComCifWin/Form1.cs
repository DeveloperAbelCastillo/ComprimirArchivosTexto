using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComCifWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrigen_Click(object sender, EventArgs e)
        {
            string text = string.Empty;
            try
            {
                lblOrigen.Text = string.Empty;
                openFileDialog.Title = "Seleccione el archivo a comprimir";
                //openFileDialog.Filter = "*.*| *.txt| *.sql";
                openFileDialog.Multiselect = false;
                //openFileDialog.InitialDirectory = "";
                if (openFileDialog.ShowDialog().Equals(DialogResult.OK))
                {
                    text = openFileDialog.FileName;
                    this.lblOrigen.Text = text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnDestino_Click(object sender, EventArgs e)
        {            
            string text = string.Empty;
            try
            {
                lblDestino.Text = string.Empty;
                saveFileDialog.Title = "Seleccione donde se guardara el archivo comprimido";
                //saveFileDialog.Filter = "*.*| *.txt| *.sql";
                //saveFileDialog.InitialDirectory = "";
                if (saveFileDialog.ShowDialog().Equals(DialogResult.OK))
                {
                    text = saveFileDialog.FileName;
                    this.lblDestino.Text = text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ComCifFram.iConCifFram com = new ComCifFram.ConCifFram();
            if (chkComprimir.Checked)
            {
                com.ComprimeArchivo(txtContraseña.Text, lblOrigen.Text, lblDestino.Text);
            }
            else
            {
                com.DescomprimeArchivo(txtContraseña.Text, lblOrigen.Text, lblDestino.Text);
            }
        }
    }
}
