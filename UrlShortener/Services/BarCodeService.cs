using IronBarCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Services
{
    public class BarCodeService : IBarCodeService
    {
        public void SaveBarcode(string url, string path)
        {
            var barCode = BarcodeWriter.CreateBarcode(url, BarcodeEncoding.QRCode);
            barCode.SaveAsHtmlFile(path);
        }
    }
}
