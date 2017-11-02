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
    public partial class Dziela_DodajDzielo : Form
    {
        Baza_Dziel Base = Baza_Dziel.GetInstance();

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };
            func(Controls);
        }

        public Dziela_DodajDzielo()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            comboBox1.Items.Add(".jpg");
            comboBox1.Items.Add(".gif");
            comboBox1.Items.Add(".png");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dzielo _dzielo = null;
            _dzielo = new Dzielo(textBox1.Text + comboBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            Base.Dodaj(_dzielo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void Dziela_DodajDzielo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}