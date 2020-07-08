using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // We might be dealing with characters from a broader encoding
            Dictionary<string, Contact> inputData = ReadInputData("in.txt");

            int choiceNumber = -1;
            while (choiceNumber != 0)
            {
                Console.WriteLine("1. Log out all contacts");
                Console.WriteLine("2. Add a new contact");
                Console.WriteLine("3. Delete a contact");
                Console.WriteLine("4. Update a contact");
                Console.WriteLine("0. Exit the application\n");
                Console.Write("Choose an option: ");
                try
                {
                    choiceNumber = int.Parse(Console.ReadLine());
                    switch(choiceNumber)
                    {
                        case 1:
                            PrintAllContacts(inputData);
                            break;
                        case 2:
                            AddNewContact(inputData);
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Keyboard input format not recognized.");
                }
            }
        }

        /// <summary>
        /// Reads the initial data from file
        /// </summary>
        /// <param name="fileName">Path to file to read from</param>
        /// <returns>Returns a standard Dictionary<K, V> collection where K is the phone number and V is the Contact</returns>
        static Dictionary<string, Contact> ReadInputData(string fileName)
        {
            Console.WriteLine("-- READING FILE: " + fileName + " --");
            Dictionary<string, Contact> dataToReturn = new Dictionary<string, Contact>();
            if (!File.Exists(fileName)) File.Create(fileName); // In case the file is not present
            using(StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                string line = null;
                int lineNumber = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    ++lineNumber;
                    try
                    {
                        string[] data = line.Split(';');
                        string fName = data[0];
                        string lName = data[1];
                        string phone = data[2];
                        string address = data[3];
                        dataToReturn.Add(phone, new Contact(fName, lName, address));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Error reading data at line {lineNumber}. Might be a duplicate phone number or consider checking the format.");
                    }
                }
            }
            Console.WriteLine("-- Finished reading --\n");
            return dataToReturn;
        }

        /// <summary>
        /// Handles the logic of printing
        /// </summary>
        /// <param name="toPrint">The Dictionary to be printed</param>
        static void PrintAllContacts(Dictionary<string, Contact> toPrint)
        {
            Console.WriteLine("\nWhere do You want the list to be printed?");
            Console.WriteLine("1. To the console");
            Console.WriteLine("2. To out.txt");
            Console.Write("Choice: ");
            int choiceNumber = 0;
            try
            {
                choiceNumber = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Keyboard input format not recognized.");
            }
            switch(choiceNumber)
            {
                case 1:
                    PrintToConsole(toPrint);
                    break;
                case 2:
                    PrintToFile(toPrint);
                    break;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints all the contacts to the console
        /// </summary>
        /// <param name="toPrint">The Dictionary to be printed</param>
        static void PrintToConsole(Dictionary<string, Contact> toPrint)
        {
            Console.WriteLine("\n-- Printing all contacts --");
            Console.WriteLine("First name, Last name, Address, Phone");
            foreach (KeyValuePair<string, Contact> kvp in toPrint)
            {
                Console.WriteLine($"{kvp.Value.ToString()}, {kvp.Key}");
            }
        }

        /// <summary>
        /// Prints all contacts to an out.txt file
        /// </summary>
        /// <param name="toPrint">The Dictionary to be printed</param>
        static void PrintToFile(Dictionary<string, Contact> toPrint)
        {
            Console.WriteLine("\n-- Printing all contacts --");
            using (StreamWriter writer = new StreamWriter("out.txt", false, Encoding.UTF8))
            {
                writer.WriteLine("First name, Last name, Address, Phone");
                foreach (KeyValuePair<string, Contact> kvp in toPrint)
                {
                    writer.WriteLine($"{kvp.Value.ToString()}, {kvp.Key}");
                }
            }
        }

        /// <summary>
        /// Adds a new contact to the dictionary
        /// </summary>
        /// <param name="toUpdate">Dictionary to which a new contact is added</param>
        static void AddNewContact(Dictionary<string, Contact> toUpdate)
        {
            Console.WriteLine("\nPlease fill out the information about the new contact.");
            Console.Write("First name: ");
            string fName = Console.ReadLine();
            Console.Write("Last name: ");
            string lName = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();
            Console.Write("Phone number: ");
            string phone = Console.ReadLine();
            try
            {
                toUpdate.Add(phone, new Contact(fName, lName, address));
            }
            catch(Exception)
            {
                Console.WriteLine("Error adding a new contact. Possibly a duplicate phone number?");
            }
            Console.WriteLine();
        }
    }
}
