using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TestDomeCSharp
{
    public class Folders
    {
        public static IEnumerable<string> FolderNames(string xml, char startingLetter)
        {
            var final = new List<string>();
            var test = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList nodes = doc.GetElementsByTagName("folder");

            var current = doc.InnerXml.Remove(0, 38);
            StringBuilder result = new StringBuilder();
            int index = 0;
            bool foundFirst = false;
            bool foundSecond = false;
            var currentConcat = "";

            result.Append(current.Replace("<folder name=", ""));
            result = result.Replace("</folder>", "");
            result = result.Replace("/", "");
            result = result.Replace(">", "");

            foreach(var item in result.ToString().ToCharArray())
            {
                currentConcat += item;
                if (item == '"' && foundFirst) foundSecond = true;
                if (item == '"') foundFirst = true;
                
                if (foundSecond)
                {
                    foundFirst = false;
                    foundSecond = false;
                    var currentItem = currentConcat.Replace("\"", "").ToString().ToCharArray();
                    var thingy = currentConcat.Replace("\"", "").ToString();
                    thingy = thingy.Trim();
                    test.Add(thingy);
                    if (thingy.ToCharArray()[0] == startingLetter) final.Add(thingy);
                    currentConcat = "";
                }
                index++;
            }
            return final;
        }

        public static void Main(string[] args)
        {
            string xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<folder name=\"u\">" +
                    "<folder name=\"urogram files\">" +
                        "<folder name=\"uninstall information\" />" +
                    "</folder>" +
                    "<folder name=\"users\" />" +
                "</folder>";

            foreach (string name in Folders.FolderNames(xml, 'u'))
                Console.WriteLine(name);
        }
    }
}
//XmlSerializer serializer = new XmlSerializer(typeof(FolderStructure));

//var stream = new MemoryStream();
//TextWriter writer = new StreamWriter(stream);
//writer.Write(xml);

//var folders = serializer.Deserialize(stream);

//var bytes = stream.ToArray();
//var items = Encoding.UTF8.GetString(bytes);