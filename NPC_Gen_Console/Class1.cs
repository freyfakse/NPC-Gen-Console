using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NPC_Gen_Console
{
    class Class1
    {
        XmlDocument firstnamesKannamereXML = new XmlDocument();
        XmlDocument firstnamesHerzamarkXML = new XmlDocument();
        XmlDocument firstnamesStavroXML = new XmlDocument();
        XmlDocument firstnamesZolbonneXML = new XmlDocument();
        XmlDocument lastnamesKannamereXML = new XmlDocument();
        XmlDocument lastnamesHerzamarkXML = new XmlDocument();
        XmlDocument lastnamesStavroXML = new XmlDocument();
        XmlDocument lastnamesZolbonneXML = new XmlDocument();

        public Class1()
        {
            LoadXMLFiles();

            Console.WriteLine("Welcome to NPC Gen, console version.");
            Console.WriteLine("State sex & origin of name (E.G. 'm,k,k' or 'f,s,z')");
            String str_npcInput = Console.ReadLine();
            List<string> li_npcInput = str_npcInput.Split(',').ToList();

            if (li_npcInput[0].Equals("m")) { Console.WriteLine("test m"); }

            Firstname(firstnamesKannamereXML);
            Lastname(lastnamesKannamereXML);

            Console.WriteLine("Change to ......? (yy, yn,ny,nn)");
            String str_saveInput = "lorem ipsum";
            if (str_saveInput.Equals("yy")) { }//save first & last name
            if (str_saveInput.Equals("yn")) { }//save first name
            if (str_saveInput.Equals("ny")) { }//save last name
            if (str_saveInput.Equals("nn")) { }//no save
        }

        public int RandomInt(int i_size)
        {
            Random rng = new Random();
            int i_randomInt = rng.Next(0, i_size);
            return i_randomInt;
        }

        public void LoadXMLFiles()
        {
            firstnamesKannamereXML.Load("firstnames, Kannamere.xml");
            firstnamesHerzamarkXML.Load("firstnames, Herzamark.xml");
            firstnamesStavroXML.Load("firstnames, Stavro.xml");
            firstnamesZolbonneXML.Load("firstnames, Zolbonne.xml");

            lastnamesKannamereXML.Load("lastnames, Kannamere.xml");
            lastnamesHerzamarkXML.Load("lastnames, Herzamark.xml");
            lastnamesStavroXML.Load("lastnames, Stavro.xml");
            lastnamesZolbonneXML.Load("lastnames, Zolbonne.xml");
        }

        public void Firstname(XmlDocument xmlDoc)
        {
            int i_randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["name"].Value + " "
                + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["sex"].Value + " "
                + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value);
        }

        public void Lastname(XmlDocument xmlDoc)
        {
            int i_randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["name"].Value + " "
                + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value);
        }
    }

}
