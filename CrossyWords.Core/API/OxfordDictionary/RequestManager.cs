using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CrossyWords.Core.API.OxfordDictionary
{
    public class RequestManager
    {
        // Login and Password to Oxford API Dictionary
        // AndreyVolkov
        // MyStudentAPIPassword0123!

        const string _key = "3c7de0fed12121b3bf32942871c0620a";
        const string _appId = "d11b3c45";

        string language = "en";
        string filters = "domains=Art";

        private string GetRequestUrl()
            //=> $"https://od-api.oxforddictionaries.com:443/api/v1/domains/en";
            => $"https://od-api.oxforddictionaries.com/api/v1/wordlist/{language}/{filters}";

        public List<WordsResult> GetWords()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("app_id", _appId);
                client.DefaultRequestHeaders.Add("app_key", _key);
                var result = client.GetAsync(GetRequestUrl()).Result;
                Console.WriteLine(result);

                if (!result.IsSuccessStatusCode)
                    throw new Exception("Invalid server reply");
                else
                {
                    var jsonResult = result.Content.ReadAsStringAsync().Result;
                    var searchResult = JsonConvert.DeserializeObject<DTO.WordsResult>(jsonResult);
                    return searchResult.Results
                        .Select(x => new WordsResult()
                        {
                            Word = x.Word,
                        }).ToList();
                }

            }
        }
    }
}
