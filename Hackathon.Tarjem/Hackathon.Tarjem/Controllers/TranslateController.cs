using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackathon.Tarjem.AI;
using Hackathon.Tarjem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hackathon.Tarjem.Controllers
{
    public class TranslateController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Start([FromBody]UserText userText)
        {
            Translator translator = new Translator();
            var jsonResult = await translator.TranslateAsync(userText.text);
            var (lang, toText) = ParsedText(jsonResult);
            //Start the trnaslation
            var body = new
            {
                to = "ar",
                from = lang,
                text = toText
            };
            return Json(body);
        }
        public (string lang, string text) ParsedText(string fromText)
        {
            var jsonText = JsonConvert.DeserializeObject<List<JsonBody>>(fromText);

            if (jsonText.Count() > 0)
            {
                return (jsonText.FirstOrDefault().DetectedLanguage.Language
                    , jsonText.FirstOrDefault().Translations.FirstOrDefault().Text);
            }
            else
                return ("Nan", "Unable to detect the language .. Please try again");
        }
    }
}