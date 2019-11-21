using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NPC_Gen_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<user name=\"John Doe\" age=\"42\" name=\"Jane Doe\" age=\"31\">></user>");
            if (xmlDoc.DocumentElement.Attributes["name"] != null)
                Console.WriteLine(xmlDoc.DocumentElement.Attributes["name"].Value);
            if (xmlDoc.DocumentElement.Attributes["age"] != null)
                Console.WriteLine(xmlDoc.DocumentElement.Attributes["age"].Value);
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
