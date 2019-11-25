using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        int i_selectedFirstnameNode;
        int i_selectedLastnameNode;
        int i_stepCount = 0; //"state machine", ensures program doesn't continue before each step is correct.

        public Class1() //Constructor
        {
            LoadXMLFiles(); //loads the actual XML docs into memory.

            Console.WriteLine("Welcome to NPC Gen, console version.");

            while (i_stepCount != 2)
            {
                MainLoop();
            }
        }

        public void MainLoop()
        {
            List<string> li_npcInput = null;

            XmlDocument pointerFirstname = new XmlDocument();
            XmlDocument pointerLastname = new XmlDocument();

            while (i_stepCount == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("State sex & origin of name (E.G. 'm,k,k' or 'f,s,z')");
                String str_npcInput = Console.ReadLine();
                li_npcInput = str_npcInput.Split(',').ToList();

                if (InputValidation(li_npcInput) == true) { i_stepCount = 1; }

                if (li_npcInput[1].Equals("k")) {pointerFirstname = firstnamesKannamereXML;}
                else if (li_npcInput[1].Equals("h")) {pointerFirstname = firstnamesHerzamarkXML;}
                else if (li_npcInput[1].Equals("s")) {pointerFirstname = firstnamesStavroXML;}
                else if (li_npcInput[1].Equals("z")) {pointerFirstname = firstnamesZolbonneXML;}

                if (li_npcInput[2].Equals("k")) { pointerLastname= lastnamesKannamereXML; }
                else if (li_npcInput[2].Equals("h")) { pointerLastname = lastnamesHerzamarkXML; }
                else if (li_npcInput[2].Equals("s")) { pointerLastname = lastnamesStavroXML; }
                else if (li_npcInput[2].Equals("z")) { pointerLastname = lastnamesZolbonneXML; }
            }

            while (i_stepCount == 1)
            {
                while (Firstname(pointerFirstname, li_npcInput[0]) == false)
                { Firstname(pointerFirstname, li_npcInput[0]); }

                Lastname(pointerLastname);

                Console.WriteLine("Change names' ''used before'' status? (yy, yn, ny, nn)");
                String str_saveInput = Console.ReadLine();

                SaveChangeToUsedBeforeFlag(pointerFirstname, pointerLastname, str_saveInput, li_npcInput);

                i_stepCount = 0;
            }
        }

        public Boolean InputValidation(List<String> li_input)
        {
            if (li_input.Count == 3)
            {
                if (li_input[0] == "m" || li_input[0] == "f")
                {
                    if (li_input[1] == "k" || li_input[1] == "h" || li_input[1] == "s" || li_input[1] == "z")
                    {
                        if (li_input[2] == "k" || li_input[2] == "h" || li_input[2] == "s" || li_input[2] == "z")
                        {
                            return true;
                        }
                        else { Console.WriteLine("Invalid surname origin, type (k)annamere, (h)erzamark, (s)tavro, or (z)olbonne."); return false; }
                    }
                    else { Console.WriteLine("Invalid firstname origin, type (k)annamere, (h)erzamark, (s)tavro, or (z)olbonne."); return false; }
                }
                else { Console.WriteLine("Invalid sex, type 'm' or 'f'."); return false; }
            }
            else { Console.WriteLine("Error in input. Did you forget a comma?"); return false; }
        }

        public int RandomInt(int i_size)
        {
            Random rng = new Random();
            int i_randomInt = rng.Next(0, i_size);
            Thread.Sleep(100);
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

        public void SaveChangeToUsedBeforeFlag(XmlDocument firstName,XmlDocument lastName,String str_userSaveInput,List<String> li_npcInput2)//Change & save the is_used_before flag to true.
        {
            
            
            if (str_userSaveInput.Equals("yy")) { Console.WriteLine("test yy"); SaveFirstname(firstName,li_npcInput2); }//save first & last name
            else if (str_userSaveInput.Equals("yn")) { Console.WriteLine("test yn"); }//save first name
            else if (str_userSaveInput.Equals("ny")) { Console.WriteLine("test ny"); }//save last name
            else if (str_userSaveInput.Equals("nn")) { Console.WriteLine("test nn"); }//no save
        }

        public void SaveFirstname(XmlDocument xmlDoc, List<String> li_npcInput3)
        {
            if (li_npcInput3[1].Equals("k"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Kannamere.xml");
            }
        }

        public void SaveLastname() { }

        public Boolean Firstname(XmlDocument xmlDoc, String str_sex)//returns bool due to sex checking
        {
            int i_randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["name"].Value + " "
                            + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["sex"].Value + " used before: "
                            + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value);

            if (xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["sex"].Value.Equals(str_sex))
            {
                Console.WriteLine("correct sex");
                i_selectedFirstnameNode = i_randomlySelectedNode;
                return true;
            }
            else { Console.WriteLine("wrong sex"); return false; }
        }

        public void Lastname(XmlDocument xmlDoc)
        {
            int i_randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["name"].Value + " used before: "
                + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value);
            i_selectedLastnameNode = i_randomlySelectedNode;
        }
    }

}
