using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServicesLibrary
{
    public class XMLReader
    {
        public static XDocument Doc { get; set; }

        public static bool CheckFile(string path)
        {
            return File.Exists(path) ? true : false;
        }

        public static string FetchValue(IEnumerable<string> query)
        {

            return "";
        }
    }
}
