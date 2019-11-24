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

        int i_stepCount = 0; //"state machine", ensures program doesn't continue before each step is correct.

        List<string> li_npcInput = null;

        public Class1()
        {
            LoadXMLFiles();


            Console.WriteLine("Welcome to NPC Gen, console version.");

            while (i_stepCount != 2)
            {
                MainLoop();
            }
            
        }

        public void MainLoop()
        {

            while (i_stepCount == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("State sex & origin of name (E.G. 'm,k,k' or 'f,s,z')");
                String str_npcInput = Console.ReadLine();
                li_npcInput = str_npcInput.Split(',').ToList();

                if (li_npcInput.Count == 3)
                {
                    if (li_npcInput[0] == "m" || li_npcInput[0] == "f")
                    {
                        if (li_npcInput[1] == "k" || li_npcInput[1] == "h" || li_npcInput[1] == "s" || li_npcInput[1] == "z")
                        {
                            if (li_npcInput[2] == "k" || li_npcInput[2] == "h" || li_npcInput[2] == "s" || li_npcInput[2] == "z")
                            {
                                i_stepCount = 1;
                            }
                            else { Console.WriteLine("Invalid surname origin, type (k)annamere, (h)erzamark, (s)tavro, or (z)olbonne."); }
                        }
                        else { Console.WriteLine("Invalid firstname origin, type (k)annamere, (h)erzamark, (s)tavro, or (z)olbonne."); }
                    }
                    else { Console.WriteLine("Invalid sex, type 'm' or 'f'."); }
                }
                else { Console.WriteLine("Error in input. Did you forget a comma?"); }
            }

            while (i_stepCount == 1)
            {
                if (li_npcInput[0].Equals("m")) { Console.WriteLine("test m"); }
                else if (li_npcInput[0].Equals("m")) { Console.WriteLine("test f"); }

                if (li_npcInput[1].Equals("k"))
                {
                    while (Firstname(firstnamesKannamereXML, li_npcInput[0]) == false)
                    { Firstname(firstnamesKannamereXML, li_npcInput[0]); }
                }
                else if (li_npcInput[1].Equals("h"))
                {
                    while (Firstname(firstnamesHerzamarkXML, li_npcInput[0]) == false)
                    { Firstname(firstnamesHerzamarkXML, li_npcInput[0]); }
                }
                else if (li_npcInput[1].Equals("s"))
                {
                    while (Firstname(firstnamesStavroXML, li_npcInput[0]) == false)
                    { Firstname(firstnamesStavroXML, li_npcInput[0]); }
                }
                else if (li_npcInput[1].Equals("z"))
                {
                    while (Firstname(firstnamesZolbonneXML, li_npcInput[0]) == false)
                    { Firstname(firstnamesZolbonneXML, li_npcInput[0]); }
                }

                if (li_npcInput[2].Equals("k")) { Lastname(lastnamesKannamereXML); }
                else if (li_npcInput[2].Equals("h")) { Lastname(lastnamesHerzamarkXML); }
                else if (li_npcInput[2].Equals("s")) { Lastname(lastnamesStavroXML); }
                else if (li_npcInput[2].Equals("z")) { Lastname(lastnamesZolbonneXML); }

                Console.WriteLine("Change names's ''used before'' status? (yy, yn, ny, nn)");
                String str_saveInput = Console.ReadLine();
                if (str_saveInput.Equals("yy")) { Console.WriteLine("test yy"); }//save first & last name
                if (str_saveInput.Equals("yn")) { Console.WriteLine("test yn"); }//save first name
                if (str_saveInput.Equals("ny")) { Console.WriteLine("test ny"); }//save last name
                if (str_saveInput.Equals("nn")) { Console.WriteLine("test nn"); }//no save

                i_stepCount = 0;
            }
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

        public void SaveChangeToUsedBeforeFlag(XmlDocument xmlDoc, int i_position)//Change & save the is_used_before flag to true.
        {

        }

        public Boolean Firstname(XmlDocument xmlDoc, String str_sex)
        {
            int i_randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["name"].Value + " "
                            + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["sex"].Value + " used before: "
                            + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value);

            if (xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["sex"].Value.Equals(str_sex))
            {
                Console.WriteLine("correct sex");
                return true;
            }
            else { Console.WriteLine("wrong sex"); return false; }
        }

        public void Lastname(XmlDocument xmlDoc)
        {
            int i_randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["name"].Value + " used before: "
                + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value);
        }
    }

}
