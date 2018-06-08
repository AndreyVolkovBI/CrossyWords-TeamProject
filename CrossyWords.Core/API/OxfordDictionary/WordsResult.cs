using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core.API.OxfordDictionary
{
    [Table("Words")]
    public class WordsResult
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }
}
