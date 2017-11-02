using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImageQuiz
{
    public class Dzielo
    {
        private static int ID = 0;

        [XmlElement("_ndzieloref")]
        public int _ndzieloref { get; set; }
        [XmlElement("_sfileName")]
        public string _sfileName { get; set; }
        [XmlElement("_stytul")]
        public string _stytul { get; set; }
        [XmlElement("_sautor")]
        public string _sautor{ get; set; }
        [XmlElement("_sepoka")]
        public string _sepoka { get; set; }

        public Dzielo() {}

        public Dzielo (string fileName, string tytul, string autor, string epoka)
        {
            this._ndzieloref = System.Threading.Interlocked.Increment(ref ID);
            _sfileName = fileName;
            _stytul = tytul;
            _sautor = autor;
            _sepoka = epoka;
        }

        public void OdczytUstawID (int _lastID)
        {
            ID = _lastID;
        }
    }
}