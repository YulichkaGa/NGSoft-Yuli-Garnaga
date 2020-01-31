using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NGSearch.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace NGSearch.Controllers
{
    public class HomeController : Controller
    {
        List<Word> wordsList;
        List<WordCount> listCount;
        static readonly List<WordsForDropBox> listForDropBox = new List<WordsForDropBox>()
        {
            new WordsForDropBox{Name="duck"},
            new WordsForDropBox{Name="elephant"},
            new WordsForDropBox{Name="lion"}
        };


        // GET: Home
        public ActionResult Index()
        {
            wordsList = new List<Word>();
            wordsList.Add(new Word { Name = "Welcome to my app" });
            listCount = new List<WordCount>();
          
            listCount.Add(new WordCount { Name = "", Count = 0 });
            var model = new BothTable { words = wordsList, wordCount = listCount };
            return View(model);
        }
        public ActionResult Import(string importName)
        {
            try
            {
                string webApiAddress = "https://api.datamuse.com/words?ml=";
                wordsList = new List<Word>();
                WebClient _webClient = new WebClient { Encoding = Encoding.UTF8 };
                _webClient.Headers["User-Agent"] = "Mozilla/5.0";
                var json = _webClient.DownloadString(webApiAddress + importName);
                //  var obj = JObject.Parse(json);
                var obj = JArray.Parse(json);
                var jsonObject = new JObject();
                foreach (var arrayWord in obj)
                {
                    wordsList.Add(new Word { Name = arrayWord["word"].ToString(), Tag = arrayWord["tags"] });
                }
                var exists = wordsList.Any(c => c.Tag.ToString().Contains("prop"));
                listCount = new List<WordCount>();
                int countProp = wordsList.Select(x => x).Count(s => s.Tag.ToString().Contains("prop"));
                int countSyn = wordsList.Select(x => x).Count(s => s.Tag.ToString().Contains("syn"));
                int countN = wordsList.Select(x => x).Count(s => s.Tag.ToString().Contains("n"));
                int countV = wordsList.Select(x => x).Count(s => s.Tag.ToString().Contains("v"));
                listCount.Add(new WordCount { Name = "Prop: ", Count = countProp });
                listCount.Add(new WordCount { Name = "Syn: ", Count = countSyn });
                listCount.Add(new WordCount { Name = "V: ", Count = countV });
                listCount.Add(new WordCount { Name = "N: ", Count = countN });
                this.Session["MyWords"] = wordsList;
                var model = new BothTable { words = wordsList, wordCount = listCount, wordsForDropBox = listForDropBox };
                return View("index", model);
            }
            catch
            {
                return View("index");
            }
        }
        public ActionResult Search(string SearchText)
        {
            try
            {
                string webApiAddress = "https://api.datamuse.com/words?ml=";
                wordsList = new List<Word>();
                WebClient _webClient = new WebClient { Encoding = Encoding.UTF8 };
                _webClient.Headers["User-Agent"] = "Mozilla/5.0";
                var json = _webClient.DownloadString(webApiAddress + SearchText);
                //  var obj = JObject.Parse(json);
                var obj = JArray.Parse(json);
                var jsonObject = new JObject();
                foreach (var arrayWord in obj)
                {
                    wordsList.Add(new Word { Name = arrayWord["word"].ToString(), Tag = arrayWord["tags"] });
                }
                var exists = wordsList.Any(c => c.Tag.ToString().Contains("prop"));
                listCount = new List<WordCount>();
                int countProp = wordsList.Select(x => x).Count(s => s.Tag.ToString().Contains("prop"));
                int countSyn = wordsList.Select(x => x).Count(s => s.Tag.ToString().Contains("syn"));
                int countN = wordsList.Select(x => x).Count(s => s.Tag.ToString().Contains("n"));
                int countV = wordsList.Select(x => x).Count(s => s.Tag.ToString().Contains("v"));
                listCount.Add(new WordCount { Name = "Prop: ", Count = countProp });
                listCount.Add(new WordCount { Name = "Syn: ", Count = countSyn });
                listCount.Add(new WordCount { Name = "V: ", Count = countV });
                listCount.Add(new WordCount { Name = "N: ", Count = countN });
                this.Session["MyWords"] = wordsList;
                var model = new BothTable { words = wordsList, wordCount = listCount, wordsForDropBox = listForDropBox };
                return View("index", model);
            }
            catch
            {
                return View("index");
            }
            
            
        }
    }
}