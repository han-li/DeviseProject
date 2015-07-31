using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using Devise.Business.Interfaces;

namespace Devise.Business.Implementations
{
    public class XmlFileCommandReceiver : IDeviseCommandReceiver
    {
        #region Fields

        private WeakReference _cache;

        #endregion

        #region Properties

        public string FileName { get; }

        #endregion

        #region Constructor

        public XmlFileCommandReceiver(string fileName)
        {
            FileName = fileName;
        }

        #endregion

        #region Members

        private Currencies ReadCurrencyFromXmlFile()
        {
            var currencies = new Currencies();
            using (var xmlReader = XmlReader.Create(new StringReader(File.ReadAllText(FileName))))
            {
                while (xmlReader.ReadToFollowing("Currency"))
                {
                    xmlReader.MoveToFirstAttribute();
                    var name = xmlReader.Value;
                    xmlReader.MoveToContent();
                    var val = xmlReader.ReadElementContentAsDouble();
                    currencies.CurrencyDictionary[name] = val;
                }
            }

            return currencies;
        }

        public void TreatCommand(IDeviseCommand command)
        {
            double val;

            if (double.TryParse(command.Source, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
            {
                if (_cache == null || !_cache.IsAlive)
                {
                    var currencies = ReadCurrencyFromXmlFile();
                    
                    command.Result = currencies.GetResults(val);

                    _cache = new WeakReference(currencies);
                }
                else
                {
                    var currencies = _cache.Target as Currencies;

                    Debug.Assert(currencies != null, "currencies != null");
                    command.Result = currencies.GetResults(val);
                }
            }
            else
            {
                command.Result = "Error";
            }
        }

        public void ClearCache()
        {
            _cache = null;
        }

        #endregion
    }
}
