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
            //Check parsed keywords if contain doctor keyword or not
            if(userText.text.Contains("doctors")|| userText.text.Contains("doctor"))
            {
                return RedirectToAction("Health", "Translate");
            }
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
        public IActionResult Health()
        {
            return View();
        }
    }
}