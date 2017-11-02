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
    public partial class Filtr_losowania : Form
    {
        Baza_Dziel Base = Baza_Dziel.GetInstance();
        public List<string> _FiltrEpoka = new List<string>(50);
        public List<string> _FiltrAutor = new List<string>(100);
        public int _Filtered_flag = 0; // Flaga ustawiana przy włączonym filtrowaniu. 0-wyłączony 1-włączony
        public int _check_filterA, _check_filterE;

        private void ComboFill ()
        {
            bool _wynikA = false;
            bool _wynikE = false;
            for (int i=0; i<=Base.SizeOfList()-1; i++) {
                if (comboBox1.Items.Count == 0)
                    _wynikE = true;
                else
                    if (comboBox1.Items.Contains(Base.AtrZIndeks(i, 4)) == false)
                        _wynikE = true;

                if (comboBox2.Items.Count == 0)
                    _wynikA = true;
                else
                    if (comboBox2.Items.Contains(Base.AtrZIndeks(i, 3)) == false)
                        _wynikA = true;

                if (_wynikE == true) { comboBox1.Items.Add(Base.AtrZIndeks(i, 4)); _wynikE = false; }
                if (_wynikA == true) { comboBox2.Items.Add(Base.AtrZIndeks(i, 3)); _wynikA = false; }
            }
        }

        public Filtr_losowania()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            ComboFill();
        }

        public Filtr_losowania(List<string> FiltrE, List<string> FiltrA, int Filtered_flag, int Check_flagE, int Check_flagA)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            foreach (string element in FiltrE)
                _FiltrEpoka.Add(element);
            foreach (string element in FiltrA)
                _FiltrAutor.Add(element);
            ComboFill();
            if (Check_flagE != 2)
                if (Check_flagE == 0) {
                    checkBox1.Checked = true; checkBox2.Enabled = false;
                }
                else {
                    checkBox1.Enabled = false; checkBox2.Checked = true;
                }
            if (Check_flagA != 2)
                if (Check_flagA == 0) {
                    checkBox4.Checked = true; checkBox3.Enabled = false;
                }
                else {
                    checkBox4.Enabled = false; checkBox3.Checked = true;
                }
            foreach (string element in _FiltrEpoka)
                richTextBox1.Text = richTextBox1.Text + element + ", ";
            foreach (string element in _FiltrAutor)
                richTextBox2.Text = richTextBox2.Text + element + ", ";

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                checkBox2.Enabled = false;
            else
                checkBox2.Enabled = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
                checkBox3.Enabled = false;
            else
                checkBox3.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                checkBox1.Enabled = false;
            else
                checkBox1.Enabled = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                checkBox4.Enabled = false;
            else
                checkBox4.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e) //Przycisk Epoka +
        {
            if (_FiltrEpoka.Contains(comboBox1.Text) == false)
            {
                _FiltrEpoka.Add(comboBox1.Text);
                 richTextBox1.Text = richTextBox1.Text + comboBox1.Text + ", ";
            }
        }

        private void button2_Click(object sender, EventArgs e) //Przycisk Epoka -
        {
            if (_FiltrEpoka.Contains(comboBox1.Text) == true)
            {
                _FiltrEpoka.Remove(comboBox1.Text);
                richTextBox1.Text = "";
                foreach (string element in _FiltrEpoka)
                    richTextBox1.Text = richTextBox1.Text + element + ", ";
            }
        }

        private void button4_Click(object sender, EventArgs e) //Przycisk Autor +
        {
            if (_FiltrAutor.Contains(comboBox2.Text) == false)
            {
                _FiltrAutor.Add(comboBox2.Text);
                richTextBox2.Text = richTextBox2.Text + comboBox2.Text + ", ";
            }
        }

        private void button3_Click(object sender, EventArgs e) //Przycisk Autor -
        {
            if (_FiltrAutor.Contains(comboBox2.Text) == true)
            {
                _FiltrAutor.Remove(comboBox2.Text);
                richTextBox2.Text = "";
                foreach (string element in _FiltrAutor)
                    richTextBox2.Text = richTextBox2.Text + element + ", ";
            }
        }

        private void button5_Click(object sender, EventArgs e) //Przycisk Wyczyść
        {
            _FiltrEpoka.Clear();
            richTextBox1.Text = "";
            _FiltrAutor.Clear();
            richTextBox2.Text = "";
            checkBox1.Checked = false; checkBox2.Checked = false; checkBox3.Checked = false; checkBox4.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e) //Przycisk Zastosuj
        {
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
            {
                _Filtered_flag = 0;
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
            else
                if (richTextBox1.Text == "" && richTextBox2.Text == "")
                    MessageBox.Show("Nie wybrano filtra!", "Błąd");
                else
                {
                    _Filtered_flag = 1;
                    if (checkBox1.Checked == false && checkBox2.Checked == false)
                        _check_filterE = 2;
                    else
                    {
                        if (checkBox1.Checked == false)
                            _check_filterE = 1;
                        else
                            _check_filterE = 0;
                    }

                    if (checkBox4.Checked == false && checkBox3.Checked == false)
                        _check_filterA = 2;
                    else
                    {
                        if (checkBox4.Checked == false)
                            _check_filterA = 1;
                        else
                            _check_filterA = 0;
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
        }

    }
}