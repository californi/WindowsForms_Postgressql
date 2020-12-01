using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcessaDadosPostgreSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BandaDAO dadosBanda = new BandaDAO();

        private void button1_Click(object sender, EventArgs e)
        {
            Banda novaBanda = new Banda();
            novaBanda.Descricao = textBox1.Text;

            
            bool booRetorno = dadosBanda.Create(novaBanda);
            if (booRetorno)
            {
                MessageBox.Show("Sua banda favorita " + novaBanda.Descricao + " Foi inserida");
                textBox1.Text = "";
            }
            else {
                MessageBox.Show("Banda não inserida");
            }

            dataGridView1.DataSource = dadosBanda.List();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dadosBanda.List();
        }
    }
}
