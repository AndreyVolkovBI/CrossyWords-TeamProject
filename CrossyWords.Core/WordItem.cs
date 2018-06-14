using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core
{
    [Table("Words")]
    public class WordItem
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }
}
