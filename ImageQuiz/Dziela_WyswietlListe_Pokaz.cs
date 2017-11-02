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
    public partial class Dziela_WyswietlListe_Pokaz : Form
    {
        public Dziela_WyswietlListe_Pokaz(string FileName)
        {
            InitializeComponent();
            string fileName = Application.StartupPath + @"\Images\" + FileName;

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.ImageLocation = (fileName);
        }

        private void Dziela_WyswietlListe_Pokaz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
