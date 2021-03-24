using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace k173722_Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            // creating a directory
            if (!Directory.Exists(args[1]))
            {
                Directory.CreateDirectory(args[1]);
            }

            DateTime dateTime = DateTime.UtcNow.Date;
            WebClient web1 = new WebClient();
            web1.DownloadFile(args[0], args[1] + @"\Summary" + DateTime.Now.ToString("ddMMMyy") + ".html");
        }
    }
}
