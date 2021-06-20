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

        int selectedFirstnameNode;
        int selectedLastnameNode;
        //int stepCount = 0; //"state machine", ensures program doesn't continue before each step is correct.
        Random rng = new Random();

        enum stateMachine
        {
            breakState,
            startState,
            inputNameState,
            inputSaveState
        }
        stateMachine state = stateMachine.startState;

        public Class1() //Constructor
        {
            LoadXMLFiles(); //loads the actual XML docs into memory.

            Console.WriteLine("Welcome to NPC Gen, console version.");

            while (state != stateMachine.breakState)
            {
                MainLoop();
            }
        }

        public void MainLoop() 
        {
            List<string> npcInput = null;

            XmlDocument pointerFirstname = new XmlDocument();
            XmlDocument pointerLastname = new XmlDocument();

            while (state == stateMachine.startState)//takes & validates input of name sex & origin
            {
                Console.WriteLine("");
                Console.WriteLine("State sex & origin of name (E.G. 'm,k,k' or 'f,s,z')");
                String npcConsoleInput = Console.ReadLine();
                npcInput = npcConsoleInput.Split(',').ToList();

                if (InputValidation(npcInput) == true)
                {
                    if (npcInput[1].Equals("k")) { pointerFirstname = firstnamesKannamereXML; }
                    else if (npcInput[1].Equals("h")) { pointerFirstname = firstnamesHerzamarkXML; }
                    else if (npcInput[1].Equals("p")) { pointerFirstname = firstnamesStavroXML; }
                    else if (npcInput[1].Equals("z")) { pointerFirstname = firstnamesZolbonneXML; }
                    else if (npcInput[1].Equals("u")) { pointerFirstname = firstnamesUnitedCoast; }
                    else if (npcInput[1].Equals("dr")) { pointerFirstname = dragonbornFirstnamesXML; }
                    else if (npcInput[1].Equals("dw")) { pointerFirstname = dwarvenFirstnamesXML; }
                    else if (npcInput[1].Equals("el")) { pointerFirstname = elvishFirstnamesXML; }
                    else if (npcInput[1].Equals("gn")) { pointerFirstname = gnomishFirstnamesXML; }
                    else if (npcInput[1].Equals("ha")) { pointerFirstname = halflingFirstnamesXML; }
                    else if (npcInput[1].Equals("or")) { pointerFirstname = orcishFirstnamesXML; }

                    if (npcInput[2].Equals("k")) { pointerLastname = lastnamesKannamereXML; }
                    else if (npcInput[2].Equals("h")) { pointerLastname = lastnamesHerzamarkXML; }
                    else if (npcInput[2].Equals("p")) { pointerLastname = lastnamesStavroXML; }
                    else if (npcInput[2].Equals("z")) { pointerLastname = lastnamesZolbonneXML; }
                    else if (npcInput[2].Equals("u")) { pointerLastname = lastnamesUnitedCoast; }
                    else if (npcInput[2].Equals("dr")) { pointerLastname = dragonbornLastnamesXML; }
                    else if (npcInput[2].Equals("dw")) { pointerLastname = dwarvenLastnamesXML; }
                    else if (npcInput[2].Equals("el")) { pointerLastname = elvishLastnamesXML; }
                    else if (npcInput[2].Equals("gn")) { pointerLastname = gnomishLastnamesXML; }
                    else if (npcInput[2].Equals("ha")) { pointerLastname = halflingLastnamesXML; }
                    //else if (npcInput[2].Equals("or")) { Console.WriteLine("No orcish last names"); }

                    //stepCount = 1;
                    state = stateMachine.inputNameState;
                }
            }

            while ( state == stateMachine.inputNameState)
            {
                while (PickFirstname(pointerFirstname, npcInput[0]) == false)
                { /*PickFirstname(pointerFirstname, npcInput[0]);*/ }//KEEP EMPTY, otherwise f(x) runs twice and program has to find 2 available names in a row.

                while (PickLastname(pointerLastname) == false) { /*PickLastname(pointerLastname);*/ }//KEEP EMPTY, same as above while loop.

                //stepCount = 2;
                state = stateMachine.inputSaveState;
            }

            while ( state == stateMachine.inputSaveState)//asks to save & acts accordingly
            {
                Console.WriteLine("Change names' ''used before'' status? (yy, yn, ny, nn)");
                String saveInput = Console.ReadLine();

                if (SaveChangeToUsedBeforeFlag(pointerFirstname, pointerLastname, saveInput, npcInput) == true) { state = stateMachine.startState; }
            }
        }

        public Boolean InputValidation(List<String> input)
        {
            if (input.Count == 3)
            {
                if (input[0] == "m" || input[0] == "f")
                {
                    if (input[1] == "k" || input[1] == "h" || input[1] == "p" || input[1] == "z" || input[1] == "u" || input[1] == "dr" || input[1] == "dw" || input[1] == "el" || input[1] == "gn" || input[1] == "ha" || input[1] == "or")
                    {
                        if (input[2] == "k" || input[2] == "h" || input[2] == "p" || input[2] == "z" || input[2] == "u" || input[2] == "dr" || input[2] == "dw" || input[2] == "el" || input[2] == "gn" || input[2] == "ha")
                        {
                            return true;
                        }
                        else { Console.WriteLine("Invalid surname origin, type (k)annamere, (h)erzamark, (p)olya, (z)olbonne, (u)nited coast, (dr)agonborn, (dw)arf, (el)ven, (gn)omish, (ha)lfling, & (or)cish."); return false; }
                    }
                    else { Console.WriteLine("Invalid firstname origin, type (k)annamere, (h)erzamark, (p)olya, (z)olbonne, (u)nited coast, (dr)agonborn, (dw)arf, (el)ven, (gn)omish, & (ha)lfling."); return false; }
                }
                else { Console.WriteLine("Invalid sex, type 'm' or 'f'."); return false; }
            }
            else { Console.WriteLine("Error in input. Did you forget a comma?"); return false; }
        }

        public int RandomInt(int size)
        {
            
            int i_randomInt = rng.Next(0, size);
            //Thread.Sleep(100);
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

        public Boolean SaveChangeToUsedBeforeFlag(XmlDocument firstName,XmlDocument lastName,String userSaveInput,List<String> npcInput2)//Change & save the is_used_before flag to true.
        {
            if (userSaveInput.Equals("yy"))//save first & last name
            {
                Console.WriteLine("Saving first- and lastnames.");
                SaveFirstname(firstName,npcInput2);
                SaveLastname(lastName,npcInput2);
                Console.WriteLine("Saved!");
                return true;
            }
            else if (userSaveInput.Equals("yn"))//save first name
            {
                Console.WriteLine("Saving firstname.");
                SaveFirstname(firstName, npcInput2);
                Console.WriteLine("Saved!");
                return true;
            }
            else if (userSaveInput.Equals("ny"))//save last name
            {
                Console.WriteLine("Saving lastname.");
                SaveLastname(lastName, npcInput2);
                Console.WriteLine("Saved!");
                return true;
            }
            else if (userSaveInput.Equals("nn"))//no save
            {
                Console.WriteLine("No saving.");
                return true;
            }
            else { Console.WriteLine("Unrecognized command. Valid commands: 'yy', 'yn', 'ny', & 'nn'."); return false; }
        }

        public void SaveFirstname(XmlDocument xmlDoc, List<String> npcInput3)
        {
            if (npcInput3[1].Equals("k"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Kannamere.xml");
            }
            else if (npcInput3[1].Equals("h"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Herzamark.xml");
            }
            else if (npcInput3[1].Equals("p"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Stavro.xml");
            }
            else if (npcInput3[1].Equals("z"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, Zolbonne.xml");
            }
            else if (npcInput3[1].Equals("u"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("firstnames, United Coast.xml");
            }
            else if (npcInput3[1].Equals("dr"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("dragonborn firstnames, Seamir.xml");
            }
            else if (npcInput3[1].Equals("dw"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("dwarven firstnames, Seamir.xml");
            }
            else if (npcInput3[1].Equals("el"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("elven firstnames, Seamir.xml");
            }
            else if (npcInput3[1].Equals("gn"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("gnomish firstnames, Seamir.xml");
            }
            else if (npcInput3[1].Equals("ha"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("halfling firstnames, Seamir.xml");
            }
            else if (npcInput3[1].Equals("or"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedFirstnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("orcish firstnames, Seamir.xml");
            }
            else
                Console.WriteLine("ERROR: Couldn't save first name");
        }

        public void SaveLastname(XmlDocument xmlDoc,List<String> npcInput4)
        {
            if (npcInput4[2].Equals("k"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, Kannamere.xml");
            }
            else if(npcInput4[2].Equals("h"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, Herzamark.xml");
            }
            else if (npcInput4[2].Equals("p"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, Stavro.xml");
            }
            else if (npcInput4[2].Equals("z"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, Zolbonne.xml");
            }
            else if (npcInput4[2].Equals("u"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("lastnames, United Coast.xml");
            }
            else if (npcInput4[2].Equals("dr"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("dragonborn lastnames, Seamir.xml");
            }
            else if (npcInput4[2].Equals("dw"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("dwarven lastnames, Seamir.xml");
            }
            else if (npcInput4[2].Equals("el"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("elven lastnames, Seamir.xml");
            }
            else if (npcInput4[2].Equals("gn"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("gnomish lastnames, Seamir.xml");
            }
            else if (npcInput4[2].Equals("ha"))
            {
                xmlDoc.DocumentElement.ChildNodes[selectedLastnameNode].Attributes["is_used_before"].Value = "true";
                xmlDoc.Save("halfling lastnames, Seamir.xml");
            }
            else
                Console.WriteLine("ERROR: Couldn't save last name");
        }

        public Boolean PickFirstname(XmlDocument xmlDoc, String str_sex)//returns bool due to sex & availability checking
        {
            int randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["name"].Value + " "
                            + xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["sex"].Value + " used before: "
                            + xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["is_used_before"].Value);

            if (xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["sex"].Value.Equals(str_sex))
            {
                Console.WriteLine("correct sex...");
                if (xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["is_used_before"].Value.Equals("false"))
                {
                    Console.WriteLine("...and available.");
                    selectedFirstnameNode = randomlySelectedNode;
                    return true;
                }
                else if(xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["is_used_before"].Value.Equals("true"))
                {
                    Console.WriteLine("...but used before.");
                    return false;
                }
                else
                {
                    Console.WriteLine("Error in XML-file. Neither 'true' or 'false'");
                    return false;
                }
            }
            else { Console.WriteLine("Wrong sex."); return false; }
        }

        public Boolean PickLastname(XmlDocument xmlDoc)//returns bool due to availability checking
        {
            int randomlySelectedNode = RandomInt(xmlDoc.DocumentElement.ChildNodes.Count);

            Console.WriteLine(xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["name"].Value + " used before: "
                + xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["is_used_before"].Value);
            
            if (xmlDoc.DocumentElement.ChildNodes[randomlySelectedNode].Attributes["is_used_before"].Value.Equals("false"))
            {
                selectedLastnameNode = randomlySelectedNode;
                return true;
            }
            else { return false; }
        }
    }
}
