using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronBarCode;

namespace UrlShortener.Services
{
    public interface IBarCodeService
    {
        public void SaveBarcode(string url, string path); 
    }
}
