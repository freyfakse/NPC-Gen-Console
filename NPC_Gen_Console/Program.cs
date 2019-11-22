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
            XmlDocument firstnamesKannamereXML = new XmlDocument();
            firstnamesKannamereXML.Load("firstnames, Kannamere.xml");
            XmlDocument firstnamesHerzamarkXML = new XmlDocument();
            firstnamesHerzamarkXML.Load("firstnames, Herzamark.xml");
            XmlDocument firstnamesStavroXML = new XmlDocument();
            firstnamesStavroXML.Load("firstnames, Stavro.xml");
            XmlDocument firstnamesZolbonneXML = new XmlDocument();
            firstnamesZolbonneXML.Load("firstnames, Zolbonne.xml");

            XmlDocument lastnamesKannamereXML = new XmlDocument();
            lastnamesKannamereXML.Load("lastnames, Kannamere.xml");
            XmlDocument lastnamesHerzamarkXML = new XmlDocument();
            lastnamesHerzamarkXML.Load("lastnames, Herzamark.xml");
            XmlDocument lastnamesStavroXML = new XmlDocument();
            lastnamesStavroXML.Load("lastnames, Stavro.xml");
            XmlDocument lastnamesZolbonneXML = new XmlDocument();
            lastnamesZolbonneXML.Load("lastnames, Zolbonne.xml");

            Console.WriteLine("Hello World!");

            //Console.WriteLine(firstnamesKannamereXML.DocumentElement.ChildNodes[0].ChildNodes[0].Attributes["name"].Value);

            
            Console.WriteLine(firstnamesKannamereXML.DocumentElement.ChildNodes[0].Attributes["name"].Value +" " 
                + firstnamesKannamereXML.DocumentElement.ChildNodes[0].Attributes["sex"].Value +" " 
                + firstnamesKannamereXML.DocumentElement.ChildNodes[0].Attributes["is_used_before"].Value);
                
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
        
    }
}
