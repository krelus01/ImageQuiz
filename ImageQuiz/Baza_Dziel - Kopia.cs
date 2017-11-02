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
                return "Zapis przeprowadzony poprawnie";
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
                xmlDocument.Load("Bazal_Dziel.xml");
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(Baza_Dziel);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        ListaDziel = (List<Dzielo>)serializer.Deserialize(reader);
                        reader.Close();
                    }
                    read.Close();
                }
                return "Odczyt przeprowadzony poprawnie";
            }
            catch (Exception ex)
            {
                return ex.Message + ", " + ex.InnerException;
            }
        }
    }
}