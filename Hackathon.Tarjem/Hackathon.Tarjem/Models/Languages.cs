using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Tarjem.Models
{
    public class Languages
    {
        private readonly Dictionary<string, string> _languageList;
        public Languages()
        {
            _languageList = new Dictionary<string, string>();
            //Fill Languages
            _languageList.Add("bg", "بلغاري");
            _languageList.Add("da", "دنماركي");
            _languageList.Add("pt", "برتغالي");
            _languageList.Add("id", "إندونيسي");
            _languageList.Add("ja", "ياباني");
            _languageList.Add("tr", "تركي");
            _languageList.Add("ur", "أوردو");
            _languageList.Add("hi", "هندي");
        }
        public string GetLanguageName(string key)
        {
            return _languageList[key];
        }
    }
}
