using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SimplexMethod
{
    public partial class Form1 : Form
    {
        int countVar = 0;
        int countCondition = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            countVar = Convert.ToInt32(textBox2.Text);
            countCondition = Convert.ToInt32(textBox1.Text);
            fillDataGrid();
        }

        void fillDataGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = countVar + 1;
            dataGridView1.RowHeadersVisible = false;
            for (int i = 0; i < countVar + 1; i++)
            {
                if (i < countVar)
                {
                    dataGridView1.Columns[i].Name = "x" + (i + 1).ToString();
                }
                else if (i == countVar)
                {
                    dataGridView1.Columns[i].Name = "b";
                }
            }

            for (int i = 0; i < countCondition; i++)
            {
                string[] Row = new string[countVar + 1];
                dataGridView1.Rows.Add(Row);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[,] Mas = new double[countCondition + 1, countVar + 1];
            double[,] TrMas;
            fillMas(Mas);
            TrMas = TransportMas(Mas);
            Simplex S = new Simplex(TrMas);
            double[,] Res = S.Calculate();
            string mes = "";
            for (int j = 0; j < countVar + countCondition + 1; j++)
            {
                if (j == 0)
                {
                    mes = "F = " + Math.Round(-1 * Res[countVar, j], 3) + "\n";
                }
                else if (j > countVar)
                {
                    mes += "X" + (j - countVar).ToString() + " = " + Math.Round(-1 * Res[countVar, j], 3) + "\n";
                }
            }
            MessageBox.Show(mes);
        }

        void fillMas(double[,] Mas)
        {
            for (int i = 0; i < countCondition + 1; i++)
            {
                for (int j = 0; j < countVar + 1; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        Mas[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }
            }
        }

        private void CreateDataTable()
        {
            // Получаем значения из textBox1 и textBox2
            int rowCount = Convert.ToInt32(textBox1.Text);
            int colCount = Convert.ToInt32(textBox2.Text);

            // Очищаем dataGridView1, если она уже содержит данные
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Создаем столбцы в dataGridView1
            for (int col = 0; col < colCount; col++)
            {
                dataGridView1.Columns.Add("Column" + col, "Column" + col);
            }

            // Создаем строки в dataGridView1
            for (int row = 0; row < rowCount; row++)
            {
                dataGridView1.Rows.Add();
            }
        }

        double[,] TransportMas(double[,] Mas)
        {
            double[,] TrMas = new double[countVar + 1, countCondition + 1];
            for (int i = 0; i < countVar + 1; i++)
            {
                for (int j = 0; j < countCondition + 1; j++)
                {
                    TrMas[i, j] = -1 * Mas[j, i];
                }
            }
            return TrMas;
        }
    }
}


