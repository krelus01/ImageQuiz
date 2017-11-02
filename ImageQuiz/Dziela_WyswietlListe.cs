using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageQuiz
{
    public partial class Dziela_WyswietlListe : Form
    {
        Baza_Dziel Base = Baza_Dziel.GetInstance();
        DataTable _tabela = new DataTable();

        public Dziela_WyswietlListe()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FillTable ()
        {
            for (int i = 0; i <= Base.SizeOfList() - 1; i++)
            {
                int ID = Int32.Parse(Base.AtrZIndeks(i, 1));
                _tabela.Rows.Add(ID, Base.AtrZIndeks(i, 2), Base.AtrZIndeks(i, 3), Base.AtrZIndeks(i, 4));
            }
        }

        private void Dziela_WyswietlListe_Load(object sender, EventArgs e)
        {
            _tabela.Columns.Add("ID", typeof(int));
            _tabela.Columns.Add("Tytuł", typeof(string));
            _tabela.Columns.Add("Autor", typeof(string));
            _tabela.Columns.Add("Epoka", typeof(string));
            FillTable();
            dataGridView1.DataSource = _tabela;

            DataGridViewButtonColumn _ShowImg = new DataGridViewButtonColumn();
            _ShowImg.HeaderText = "Pokaż";
            _ShowImg.Name = "_ShowImg";
            dataGridView1.Columns.Add(_ShowImg);
            DataGridViewButtonColumn _Remove = new DataGridViewButtonColumn();
            _Remove.HeaderText = "Usuń";
            _Remove.Name = "_Remove";
            dataGridView1.Columns.Add(_Remove);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //Ustawia szerokość kolumn względem zawartości
            int dgv_width = dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.Visible); //Ustawia wielkość okna do wielkości DataGridView
            this.Width = dgv_width + 36;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && senderGrid.Columns[e.ColumnIndex].Name == "_Remove" && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (MessageBox.Show("Jesteś pewny?", "Potwierdzenie Usunięcia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Base.Usun((int)dataGridView1.Rows[senderGrid.Rows[e.RowIndex].Index].Cells[0].Value - 1);
                    _tabela.Clear();
                    FillTable();
                    int dgv_width = dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.Visible); //Ustawia wielkość okna do wielkości DataGridView
                    this.Width = dgv_width + 36;
                }
            }
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && senderGrid.Columns[e.ColumnIndex].Name == "_ShowImg" && e.RowIndex >= 0)
            {
                Dziela_WyswietlListe_Pokaz _NewForm = new Dziela_WyswietlListe_Pokaz(Base.AtrZIndeks((int)dataGridView1.Rows[senderGrid.Rows[e.RowIndex].Index].Cells[0].Value - 1, 5));
                _NewForm.ShowDialog();
            }
        }

        private void Dziela_WyswietlListe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}