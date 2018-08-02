using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Tarjem.Models
{
    public class JsonBody
    {
        public DetectedLanguage DetectedLanguage { get; set; }
        public List<Translations> Translations { get; set; }
    }
}
