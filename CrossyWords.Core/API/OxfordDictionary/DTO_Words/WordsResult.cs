using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.API.OxfordDictionary.DTO_Words
{
    public class WordsResult
    {
        public int Id { get; set; }
        public List<ResultItem> Results { get; set; }
    }
}
