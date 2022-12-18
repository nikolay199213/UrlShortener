using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    [Table("urls")]
    public class Url
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("targeturl")]
        public string TargetUrl { get; set; }
        [Column("shortguid")]
        public string ShortGuid { get; set; }
    }
}
