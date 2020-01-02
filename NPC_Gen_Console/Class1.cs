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
        XmlDocument dragonbornFirstnamesXML = new XmlDocument();
        XmlDocument dwarvenFirstnamesXML = new XmlDocument();
        XmlDocument elvishFirstnamesXML = new XmlDocument();
        XmlDocument gnomishFirstnamesXML = new XmlDocument();
        XmlDocument halflingFirstnamesXML = new XmlDocument();
        XmlDocument orcishFirstnamesXML = new XmlDocument();

        XmlDocument dragonbornLastnamesXML = new XmlDocument();
        XmlDocument dwarvenLastnamesXML = new XmlDocument();
        XmlDocument elvishLastnamesXML = new XmlDocument();
        XmlDocument gnomishLastnamesXML = new XmlDocument();
        XmlDocument halflingLastnamesXML = new XmlDocument();

        XmlDocument firstnamesKannamereXML = new XmlDocument();
        XmlDocument firstnamesHerzamarkXML = new XmlDocument();
        XmlDocument firstnamesStavroXML = new XmlDocument();
        XmlDocument firstnamesZolbonneXML = new XmlDocument();
        XmlDocument firstnamesUnitedCoast = new XmlDocument();

        XmlDocument lastnamesKannamereXML = new XmlDocument();
        XmlDocument lastnamesHerzamarkXML = new XmlDocument();
        XmlDocument lastnamesStavroXML = new XmlDocument();
        XmlDocument lastnamesZolbonneXML = new XmlDocument();
        XmlDocument lastnamesUnitedCoast = new XmlDocument();

        int i_selectedFirstnameNode;
        int i_selectedLastnameNode;
        int i_stepCount = 0; //"state machine", ensures program doesn't continue before each step is correct.

        public Class1() //Constructor
        {
            LoadXMLFiles(); //loads the actual XML docs into memory.

            Console.WriteLine("Welcome to NPC Gen, console version.");

            while (i_stepCount != -1)
            {
                MainLoop();
            }
        }

        public void MainLoop() 
        {
            List<string> li_npcInput = null;

            XmlDocument pointerFirstname = new XmlDocument();
            XmlDocument pointerLastname = new XmlDocument();

            while (i_stepCount == 0)//takes & validates input of name sex & origin
            {
                Console.WriteLine("");
                Console.WriteLine("State sex & origin of name (E.G. 'm,k,k' or 'f,s,z')");
                String str_npcInput = Console.ReadLine();
                li_npcInput = str_npcInput.Split(',').ToList();

                if (InputValidation(li_npcInput) == true)
                {
                    if (li_npcInput[1].Equals("k")) { pointerFirstname = firstnamesKannamereXML; }
                    else if (li_npcInput[1].Equals("h")) { pointerFirstname = firstnamesHerzamarkXML; }
                    else if (li_npcInput[1].Equals("s")) { pointerFirstname = firstnamesStavroXML; }
                    else if (li_npcInput[1].Equals("z")) { pointerFirstname = firstnamesZolbonneXML; }
                    else if (li_npcInput[1].Equals("u")) { pointerFirstname = firstnamesUnitedCoast; }
                    else if (li_npcInput[1].Equals("dr")) { pointerFirstname = dragonbornFirstnamesXML; }
                    else if (li_npcInput[1].Equals("dw")) { pointerFirstname = dwarvenFirstnamesXML; }
                    else if (li_npcInput[1].Equals("el")) { pointerFirstname = elvishFirstnamesXML; }
                    else if (li_npcInput[1].Equals("gn")) { pointerFirstname = gnomishFirstnamesXML; }
                    else if (li_npcInput[1].Equals("ha")) { pointerFirstname = halflingFirstnamesXML; }
                    else if (li_npcInput[1].Equals("or")) { pointerFirstname = orcishFirstnamesXML; }

                    if (li_npcInput[2].Equals("k")) { pointerLastname = lastnamesKannamereXML; }
                    else if (li_npcInput[2].Equals("h")) { pointerLastname = lastnamesHerzamarkXML; }
                    else if (li_npcInput[2].Equals("s")) { pointerLastname = lastnamesStavroXML; }
                    else if (li_npcInput[2].Equals("z")) { pointerLastname = lastnamesZolbonneXML; }
                    else if (li_npcInput[2].Equals("u")) { pointerLastname = lastnamesUnitedCoast; }
                    else if (li_npcInput[2].Equals("dr")) { pointerLastname = dragonbornLastnamesXML; }
                    else if (li_npcInput[2].Equals("dw")) { pointerLastname = dwarvenLastnamesXML; }
                    else if (li_npcInput[2].Equals("el")) { pointerLastname = elvishLastnamesXML; }
                    else if (li_npcInput[2].Equals("gn")) { pointerLastname = gnomishLastnamesXML; }
                    else if (li_npcInput[2].Equals("ha")) { pointerLastname = halflingLastnamesXML; }

                    i_stepCount = 1;
                }
            }

            while (i_stepCount == 1)
            {
                while (PickFirstname(pointerFirstname, li_npcInput[0]) == false)
                { /*PickFirstname(pointerFirstname, li_npcInput[0]);*/ }//KEEP EMPTY, otherwise f(x) runs twice and program has to find 2 available names in a row.

                while (PickLastname(pointerLastname) == false) { /*PickLastname(pointerLastname);*/ }//KEEP EMPTY, same as above while loop.

                i_stepCount = 2;
            }

            while (i_stepCount == 2)//asks to save & acts accordingly
            {
                Console.WriteLine("Change names' ''used before'' status? (yy, yn, ny, nn)");
                String str_saveInput = Console.ReadLine();

                if (SaveChangeToUsedBeforeFlag(pointerFirstname, pointerLastname, str_saveInput, li_npcInput) == true) { i_stepCount = 0; }
            }
        }

        public Boolean InputValidation(List<String> li_input)
        {
            if (li_input.Count == 3)
            {
                if (li_input[0] == "m" || li_input[0] == "f")
                {
                    if (li_input[1] == "k" || li_input[1] == "h" || li_input[1] == "s" || li_input[1] == "z" || li_input[1] == "u" || li_input[1] == "dr" || li_input[1] == "dw" || li_input[1] == "el" || li_input[1] == "gn" || li_input[1] == "ha" || li_input[1] == "or")
                    {
                        if (li_input[2] == "k" || li_input[2] == "h" || li_input[2] == "s" || li_input[2] == "z" || li_input[2] == "u" || li_input[2] == "dr" || li_input[2] == "dw" || li_input[2] == "el" || li_input[2] == "gn" || li_input[2] == "ha")
                        {
                            return true;
                        }
                        else { Console.WriteLine("Invalid surname origin, type (k)annamere, (h)erzamark, (s)tavro, (z)olbonne, (u)nited coast, (dr)agonborn, (dw)arf, (el)ven, (gn)omish, (ha)lfling, & (or)cish."); return false; }
                    }
                    else { Console.WriteLine("Invalid firstname origin, type (k)annamere, (h)erzamark, (s)tavro, (z)olbonne, (u)nited coast, (dr)agonborn, (dw)arf, (el)ven, (gn)omish, & (ha)lfling."); return false; }
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
            firstnamesUnitedCoast.Load("firstnames, United Coast.xml");
            dragonbornFirstnamesXML.Load("dragonborn firstnames, Seamir.xml");
            dwarvenFirstnamesXML.Load("dwarven firstnames, Seamir.xml");
            elvishFirstnamesXML.Load("elven firstnames, Seamir.xml");
            gnomishFirstnamesXML.Load("gnomish firstnames, Seamir.xml");
            halflingFirstnamesXML.Load("halfling firstnames, Seamir.xml");
            orcishFirstnamesXML.Load("orcish firstnames, Seamir.xml");

            lastnamesKannamereXML.Load("lastnames, Kannamere.xml");
            lastnamesHerzamarkXML.Load("lastnames, Herzamark.xml");
            lastnamesStavroXML.Load("lastnames, Stavro.xml");
            lastnamesZolbonneXML.Load("lastnames, Zolbonne.xml");
            lastnamesUnitedCoast.Load("lastnames, United Coast.xml");
            dragonbornLastnamesXML.Load("dragonborn lastnames, Seamir.xml");
            dwarvenLastnamesXML.Load("dwarven lastnames, Seamir.xml");
            elvishLastnamesXML.Load("elven lastnames, Seamir.xml");
            gnomishLastnamesXML.Load("gnomish lastnames, Seamir.xml");
            halflingLastnamesXML.Load("halfling lastnames, Seamir.xml");
        }

        public Boolean SaveChangeToUsedBeforeFlag(XmlDocument firstName,XmlDocument lastName,String str_userSaveInput,List<String> li_npcInput2)//Change & save the is_used_before flag to true.
        {
            if (str_userSaveInput.Equals("yy"))//save first & last name
            {
                Console.WriteLine("Saving first- and lastnames.");
                SaveFirstname(firstName,li_npcInput2);
                SaveLastname(lastName,li_npcInput2);
                Console.WriteLine("Saved!");
                return true;
            }
            else if (str_userSaveInput.Equals("yn"))//save first name
            {
                Console.WriteLine("Saving fistname.");
                SaveFirstname(firstName, li_npcInput2);
                Console.WriteLine("Saved!");
                return true;
            }
            else if (str_userSaveInput.Equals("ny"))//save last name
            {
                Console.WriteLine("Saving lastname.");
                SaveLastname(lastName, li_npcInput2);
                Console.WriteLine("Saved!");
                return true;
            }
            else if (str_userSaveInput.Equals("nn"))//no save
            {
                Console.WriteLine("No saving.");
                return true;
            }
            else { Console.WriteLine("Unrecognized command. Valid commands: 'yy', 'yn', 'ny', & 'nn'."); return false; }
        }

        public void SaveFirstname(XmlDocument xmlDoc, List<String> li_npcInput3)
        {
            if (li_npcInput3[1].Equals("k"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Kannamere.xml");
            }
            else if (li_npcInput3[2].Equals("h"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Herzamark.xml");
            }
            else if (li_npcInput3[2].Equals("s"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Stavro.xml");
            }
            else if (li_npcInput3[2].Equals("z"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Zolbonne.xml");
            }
        }

        public void SaveLastname(XmlDocument xmlDoc,List<String> li_npcInput4)
        {
            if (li_npcInput4[2].Equals("k"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, Kannamere.xml");
            }
            else if(li_npcInput4[2].Equals("h"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, Herzamark.xml");
            }
            else if (li_npcInput4[2].Equals("s"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, Stavro.xml");
            }
            else if (li_npcInput4[2].Equals("z"))
            {
                xmlDoc.DocumentElement.ChildNodes[i_selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, Zolbonne.xml");
            }
        }

        public Boolean PickFirstname(XmlDocument xmlDoc, String str_sex)//returns bool due to sex & availability checking
        {
            int i_randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["name"].Value + " "
                            + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["sex"].Value + " used before: "
                            + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value);

            if (xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["sex"].Value.Equals(str_sex))
            {
                Console.WriteLine("correct sex...");
                if (xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value.Equals("false"))
                {
                    Console.WriteLine("...and available.");
                    i_selectedFirstnameNode = i_randomlySelectedNode;
                    return true;
                }
                else
                {
                    Console.WriteLine("...but used before.");
                    return false;
                }     
            }
            else { Console.WriteLine("Wrong sex."); return false; }
        }

        public Boolean PickLastname(XmlDocument xmlDoc)//returns bool due to availability checking
        {
            int i_randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["name"].Value + " used before: "
                + xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value);
            
            if (xmlDoc.DocumentElement.ChildNodes[i_randomlySelectedNode].Attributes["is_used_before"].Value.Equals("false"))
            {
                i_selectedLastnameNode = i_randomlySelectedNode;
                return true;
            }
            else { return false; }
        }
    }
}
