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
        Translator _trnaslator;
        public TranslateController()
        {
            _trnaslator = new Translator();
        }
        #region Ajax
        [HttpPost]
        public async Task<IActionResult> Start([FromBody]UserText userText)
        {
            var jsonResult = await _trnaslator.TranslateAsync(userText.text);
            var (lang, toText) = ParsedText(jsonResult);
            //Detect languge first()

            //Check parsed keywords if contain doctor keyword or not
            //if(userText.text.Contains("doctors")|| userText.text.Contains("doctor"))
            //{
            //    return RedirectToAction("Health", "Translate");
            //}
            var textBody = await CheckLangSourceAsync(lang, userText.text);
            if (textBody != null) //Mean the text is arabic and we need to translate it
                return Json(textBody);

            //set  the trnaslation result
            return Json(new
            {
                to = "ar",
                from = lang,
                text = toText
            });
        }
        [HttpPost]
        public async Task<IActionResult> SmartMessage([FromBody]UserText userText)
        {
            var jsonResult = await _trnaslator.TranslatAllAsync(userText.text);
            var langList= JsonConvert.DeserializeObject<List<JsonBody>>(jsonResult);
            List<Translations> translations = new List<Translations>();
            //Return list of translations
            return Json(langList.First().Translations);
        }
        #endregion

        #region Helper
        private async Task<object> CheckLangSourceAsync(string lang, string userText)
        {
            if (lang == "ar") //We need to translate it for english
            {
                var jsonResult = await _trnaslator.TranslateAsync(userText, "en");
                var (lng, toText) = ParsedText(jsonResult);
                var body = new
                {
                    to = lng,
                    from = lang,
                    text = toText
                };
                return body;
            }
            return null;
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
        #endregion

        public IActionResult Health()
        {
            return View();
        }
    }
}