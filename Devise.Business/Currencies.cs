using System.Collections.Generic;
using System.Text;

namespace Devise.Business
{
    public class Currencies
    {
        #region Properties

        public Dictionary<string, double> CurrencyDictionary { get; }

        #endregion

        #region Constructor

        public Currencies()
        {
            CurrencyDictionary = new Dictionary<string, double>();
        }

        #endregion

        #region Members

        public string GetResults(double source)
        {
            var stringBuilder = new StringBuilder();

            foreach (var currency in CurrencyDictionary)
            {
                stringBuilder.Append(currency.Key);
                stringBuilder.Append(": ");
                stringBuilder.Append(source*currency.Value);
                stringBuilder.Append("\t");
            }

            return stringBuilder.ToString();
        }

        #endregion
    }
}
