using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;

namespace k173722_Q2
{
    class Program
    {
        static void Main(string[] args)
        {

            string root = ConfigurationManager.AppSettings["rootFolder"];
            string filename = root + ConfigurationManager.AppSettings["fileName"];
            string file_path = "";
            Console.WriteLine(root);
            var doc = new HtmlDocument();
            doc.Load(filename);

            string date = doc.DocumentNode.SelectSingleNode("/html/body/div[7]/div/div/div/div/div[1]/h4").InnerText.Replace(":", ".");
            List<Scripts> script_list = new List<Scripts>();

            foreach (var item in doc.DocumentNode.SelectNodes("/html/body/div[7]/div/div/div/div/div[2]/div/table"))
            {
                string file_name = item.SelectSingleNode("./thead/tr/th/h4").InnerText.Replace("/", "-");
                file_path = (root + file_name);
                Console.WriteLine(file_path);

                if (!Directory.Exists(file_path))
                {
                    Directory.CreateDirectory(file_path);
                }

                foreach (var item1 in item.SelectNodes("./tr"))
                {
                    if (item1.SelectSingleNode("./td[1]").InnerText != "SCRIP")
                    {
                        script_list.Add(new Scripts(item1.SelectSingleNode("./td[1]").InnerText, item1.SelectSingleNode("./td[6]").InnerText));
                    }
                }
                
                Stream stream = File.OpenWrite(file_path + @"\" + file_name.ToLower() + " " + date + ".xml");
                XmlSerializer xmlSer = new XmlSerializer(typeof(List<Scripts>));
                xmlSer.Serialize(stream, script_list);
                script_list.Clear();
                stream.Close();
            }
        }
    }
}
