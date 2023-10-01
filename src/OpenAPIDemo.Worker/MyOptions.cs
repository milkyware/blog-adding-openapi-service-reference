using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAPIDemo.Worker
{
    public class MyOptions
    {
        [Required]
        public Uri? CatFactsBaseUrl { get; set; }
    }
}
