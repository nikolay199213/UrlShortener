using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Validators
{
    public interface IUrlValidator
    {
        bool Validate(string url);
    }
}
