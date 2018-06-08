using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.API.OxfordDictionary.DTO_Categories
{
    public class WordsResult
    {
        public Dictionary<string, ResultItem> Results { get; set; }
    }
}
