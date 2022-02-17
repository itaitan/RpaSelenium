using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrefeituraLoja2
{
    public partial class Form1 : Form
    {
        public static String dtInicio, dtFim, dtMes;

        private void btnSair_Click(object sender, EventArgs e)
        {
            Environment.Exit(99);
        }

        private void textDtInicio_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnFim_Click(object sender, EventArgs e)
        {
            dtInicio = textDtInicio.Text.ToString();
            dtFim = textDtFim.Text.ToString();
            dtMes = textDtMes.Text.ToString();
            Application.Exit();

        }
    }
}
