using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ImageQuiz
{
    public partial class Form1 : Form
    {
        Baza_Dziel Base = Baza_Dziel.GetInstance();
        int _ActiveID = 0;
        int _Filtered_flag; // Flaga ustawiana przy włączonym filtrowaniu. 0-wyłączony 1-włączony
        int _Check_flagE; // Flaga powiązana z filtrem, ustawia: 0 - zawieranie / 1 - wyłączanie / 2 - brak filtru w filtrze Epoki
        int _Check_flagA; // Flaga powiązana z filtrem, ustawia: 0 - zawieranie / 1 - wyłączanie / 2 - brak filtru w filtrze Autora
        List <string> _FiltrEpoka = new List<string>(50);
        List <string> _FiltrAutor = new List <string>(100);
        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        private void LoadRandomImage () //Funkcja losująca dowolny losowy obraz lub losowy mieszczący się w filtrze.
        {
            string fileName = "";
            int przefiltrowane = 0;

            if (_Filtered_flag == 1)
            {
                if (_Check_flagE == 0)
                    if (_Check_flagA == 0)
                    {
                        for (int i = 0; i <= Base.SizeOfList()-1; i++)
                            if (_FiltrEpoka.Contains(Base.AtrZIndeks(i, 4)) == true)
                                if (_FiltrAutor.Contains(Base.AtrZIndeks(i, 3)) == true)
                                    przefiltrowane++;
                        do
                        {
                            if (przefiltrowane == 0)
                                break;
                            _ActiveID = rnd.Next(Base.SizeOfList());
                        } while (_FiltrEpoka.Contains(Base.AtrZIndeks(_ActiveID, 4)) == false || _FiltrAutor.Contains(Base.AtrZIndeks(_ActiveID, 3)) == false);
                    }
                    else
                    {
                        for (int i = 0; i <= Base.SizeOfList()-1; i++)
                            if (_FiltrEpoka.Contains(Base.AtrZIndeks(i, 4)) == true)
                                if (_FiltrAutor.Contains(Base.AtrZIndeks(i, 3)) == false)
                                    przefiltrowane++;
                        do
                        {
                            if (przefiltrowane == 0)
                                break;
                            _ActiveID = rnd.Next(Base.SizeOfList());
                        } while (_FiltrEpoka.Contains(Base.AtrZIndeks(_ActiveID, 4)) == false || _FiltrAutor.Contains(Base.AtrZIndeks(_ActiveID, 3)) == true);
                    }
                else
                {
                    if (_Check_flagA == 0)
                    {
                        for (int i = 0; i <= Base.SizeOfList()-1; i++)
                            if (_FiltrEpoka.Contains(Base.AtrZIndeks(i, 4)) == false)
                                if (_FiltrAutor.Contains(Base.AtrZIndeks(i, 3)) == true)
                                    przefiltrowane++;
                        do
                        {
                            if (przefiltrowane == 0)
                                break;
                            _ActiveID = rnd.Next(Base.SizeOfList());
                        } while (_FiltrEpoka.Contains(Base.AtrZIndeks(_ActiveID, 4)) == true || _FiltrAutor.Contains(Base.AtrZIndeks(_ActiveID, 3)) == false);
                    }
                    else
                    {
                        for (int i = 0; i <= Base.SizeOfList()-1; i++)
                            if (_FiltrEpoka.Contains(Base.AtrZIndeks(i, 4)) == false)
                                if (_FiltrAutor.Contains(Base.AtrZIndeks(i, 3)) == false)
                                    przefiltrowane++;
                        do
                        {
                            if (przefiltrowane == 0)
                                break;
                            _ActiveID = rnd.Next(Base.SizeOfList());
                        } while (_FiltrEpoka.Contains(Base.AtrZIndeks(_ActiveID, 4)) == true || _FiltrAutor.Contains(Base.AtrZIndeks(_ActiveID, 3)) == true);
                    }
                }
            }
            else
                _ActiveID = rnd.Next(Base.SizeOfList());

            fileName = Application.StartupPath + @"\Images\" + Base.AtrZIndeks(_ActiveID, 5);
            //pictureBox1.ImageLocation = (fileName);
            pictureBox1.Load(fileName);
            if (pictureBox1.Image.Width > this.Width)
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
            {
                if(pictureBox1.Image.Height > this.Height)
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                else
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MessageBox.Show(Base.OdczytZXML(), "Komunikat");
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void rozpocznijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadRandomImage();
        }

        private void button2_Click(object sender, EventArgs e) //Przycisk Pomiń
        {
            LoadRandomImage();
            textBox1.BackColor = Color.White; textBox1.Text = "";
            textBox2.BackColor = Color.White; textBox2.Text = "";
            textBox3.BackColor = Color.White; textBox3.Text = "";
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ImageQuiz ver. 1.2\nAutor: Michał Krela", "O programie");
        }

        private void pomocToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wybieramy Plik -> Rozpocznij.\nNastępnie wyświetli się pierwszy obraz.\nNależy wprowadzić wymagane dane, po czym kliknąć przycisk Sprawdź!\nMożna również użyć filtra dla losowanych dzieł, wybierając Plik->Filtr losowania", "Instrukcja");
        }

        private void dodajDziełoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dziela_DodajDzielo Nowe_dzielo = new Dziela_DodajDzielo();
            Nowe_dzielo.ShowDialog();
        }

        private void wyświetlListęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dziela_WyswietlListe Wyswietl_Liste = new Dziela_WyswietlListe();
            Wyswietl_Liste.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) // Przycisk Sprawdź
        {
            if (textBox1.Text == Base.AtrZIndeks(_ActiveID, 2)) textBox1.BackColor = Color.Green;
            else textBox1.BackColor = Color.Red;

            if (textBox2.Text == Base.AtrZIndeks(_ActiveID, 3)) textBox2.BackColor = Color.Green;
            else textBox2.BackColor = Color.Red;

            if (textBox3.Text == Base.AtrZIndeks(_ActiveID, 4)) textBox3.BackColor = Color.Green;
            else textBox3.BackColor = Color.Red;

            if (textBox1.Text == Base.AtrZIndeks(_ActiveID, 2) &&
                textBox2.Text == Base.AtrZIndeks(_ActiveID, 3) &&
                textBox3.Text == Base.AtrZIndeks(_ActiveID, 4) )
            {
                MessageBox.Show("Poprawnie!", "Komunikat");
                LoadRandomImage();
                textBox1.BackColor = Color.White; textBox1.Text = "";
                textBox2.BackColor = Color.White; textBox2.Text = "";
                textBox3.BackColor = Color.White; textBox3.Text = "";
            }
        }

        private void zapiszListęDoXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Base.ZapisDoXML(), "Komunikat");
        }

        private void button3_Click(object sender, EventArgs e) // Przycisk szczegóły
        {
            MessageBox.Show("Tytuł: " + Base.AtrZIndeks(_ActiveID, 2) + "\nAutor: " + Base.AtrZIndeks(_ActiveID, 3) + "\nEpoka: " + Base.AtrZIndeks(_ActiveID, 4), "Szczegóły dzieła");
        }

        private void filtrujLosowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_Filtered_flag == 0)
            {
                Filtr_losowania FiltrLosowania = new Filtr_losowania();
                if (FiltrLosowania.ShowDialog() == DialogResult.OK)
                {
                    _FiltrEpoka.Clear(); _FiltrAutor.Clear();
                    foreach (string element in FiltrLosowania._FiltrEpoka)
                        _FiltrEpoka.Add(element);
                    foreach (string element in FiltrLosowania._FiltrAutor)
                        _FiltrAutor.Add(element);
                    _Filtered_flag = FiltrLosowania._Filtered_flag;
                    _Check_flagE = FiltrLosowania._check_filterE;
                    _Check_flagA = FiltrLosowania._check_filterA;
                }
            }
            else
            {
                Filtr_losowania FiltrLosowania = new Filtr_losowania(_FiltrEpoka, _FiltrAutor, _Filtered_flag, _Check_flagE, _Check_flagA);
                if (FiltrLosowania.ShowDialog() == DialogResult.OK)
                {
                    _FiltrEpoka.Clear(); _FiltrAutor.Clear();
                    foreach (string element in FiltrLosowania._FiltrEpoka)
                        _FiltrEpoka.Add(element);
                    foreach (string element in FiltrLosowania._FiltrAutor)
                        _FiltrAutor.Add(element);
                    _Filtered_flag = FiltrLosowania._Filtered_flag;
                    _Check_flagE = FiltrLosowania._check_filterE;
                    _Check_flagA = FiltrLosowania._check_filterA;
                }
            }
        }
    }
}