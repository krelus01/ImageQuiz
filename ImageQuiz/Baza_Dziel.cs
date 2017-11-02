using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace ImageQuiz
{
    [XmlRoot("Lista_dziel")]
    public class Baza_Dziel
    {
        [XmlElement("Dzielo")]
        List<Dzielo> ListaDziel; //Deklaracja listy zawierającej obiekty typu Dzielo
        Dzielo _Dzielo = new Dzielo();
        
        private static Baza_Dziel obiekt = null;

        private Baza_Dziel () { }

        
        public Baza_Dziel(int rozmiar) //Konstruktor listy
        {
            ListaDziel = new List<Dzielo>();
        }

        public static Baza_Dziel GetInstance()
        {

            if (obiekt == null) obiekt = new Baza_Dziel(350);
            return obiekt;
        }

        public void Dodaj (Dzielo NoweDzielo)
        {
            ListaDziel.Add(NoweDzielo);
        }

        public void Usun(int index)
        {
            ListaDziel.RemoveAt(index);
            int _size = SizeOfList();
            for (int i=index; i < _size; i++)
            {
                ListaDziel[i]._ndzieloref -= 1;
            }
            _Dzielo.OdczytUstawID(ListaDziel.Count);
        }

        /// <summary>
        /// Funkcja wyciągająca z podanego indeksu atrybut podany we fladze: 1-ID, 2-tytuł, 3-autor, 4-epokę, 5-nazwę pliku
        /// </summary>
        /// <param name="_indeks"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string AtrZIndeks (int _indeks, int flag)
        {
            if (flag < 0 && flag > 5)
                flag = 1;
            switch (flag)
            {
                case 1:
                    return ListaDziel[_indeks]._ndzieloref.ToString();
                case 2:
                    return ListaDziel[_indeks]._stytul;
                case 3:
                    return ListaDziel[_indeks]._sautor;
                case 4:
                    return ListaDziel[_indeks]._sepoka;
                case 5:
                    return ListaDziel[_indeks]._sfileName;
                default:
                    return ListaDziel[_indeks]._ndzieloref.ToString();
            }
        }

        public string ZapisDoXML()
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(typeof(List<Dzielo>));
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, ListaDziel);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save("Baza_Dziel.xml");
                    stream.Close();
                }
                return "Zapis do pliku XML przeprowadzony poprawnie.";
            }
            catch (Exception ex)
            {
                return ex.Message + ", " + ex.InnerException;
            }
        }

        public string OdczytZXML()
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("Baza_Dziel.xml");
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(List<Dzielo>);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        ListaDziel = (List<Dzielo>)serializer.Deserialize(reader);
                        reader.Close();
                    }
                    read.Close();
                }
                _Dzielo.OdczytUstawID(ListaDziel.Count);
                return "Odczyt z pliku XML przeprowadzony poprawnie.";
            }
            catch (Exception ex)
            {
                return ex.Message + ", " + ex.InnerException;
            }
        }

        public int SizeOfList ()
        {
            return ListaDziel.Count();
        }
    }
}