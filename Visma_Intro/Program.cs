﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Visma_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // We might be dealing with characters from a broader encoding
            Console.WriteLine("-- Contact Manager --");
            Dictionary<string, Contact> inputData = ReadInputData("in.txt");

            int choiceNumber = -1;
            while (choiceNumber != 0)
            {
                Console.WriteLine("1. Log out all contacts");
                Console.WriteLine("2. Add a new contact");
                Console.WriteLine("3. Delete a contact");
                Console.WriteLine("4. Update a contact");
                Console.WriteLine("5. Help");
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
                            DeleteContact(inputData);
                            break;
                        case 4:
                            UpdateContact(inputData);
                            break;
                        case 5:
                            DisplayHelp();
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
            var contact = MakeNewContact();
            try
            {
                toUpdate.Add(contact.Key, contact.Value);
            }
            catch(Exception)
            {
                Console.WriteLine("Error adding a new contact. Possibly a duplicate phone number?");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Creates a new contact
        /// </summary>
        /// <returns>Returns a KeyValuePair<K, V> where the key is a phone number and value is a contact</returns>
        static KeyValuePair<string, Contact> MakeNewContact()
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
            return new KeyValuePair<string, Contact>(phone, new Contact(fName, lName, address));
        }

        /// <summary>
        /// Deletes a single contact according to its phone number
        /// </summary>
        /// <param name="toUpdate">The Dictionary to be updated</param>
        static void DeleteContact(Dictionary<string, Contact> toUpdate)
        {
            Console.Write("\nEnter the phone number of the contact You want to delete: ");
            string phone = Console.ReadLine();
            try
            {
                if (toUpdate.Remove(phone)) Console.WriteLine("Contact deleted successfully.");
                else Console.WriteLine("No contact with such phone number found.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error while deleting a contact.");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Updates a single contact according to its phone number
        /// </summary>
        /// <param name="toUpdate">The Dictionary to be updated</param>
        static void UpdateContact(Dictionary<string, Contact> toUpdate)
        {
            Console.Write("\nEnter the phone number of the contact You want to update: ");
            string phone = Console.ReadLine();
            try
            {
                var newContact = MakeNewContact();
                if(!toUpdate.ContainsKey(newContact.Key)) // In order to not remove a contact prematurely
                {
                    if (toUpdate.Remove(phone))
                    {
                        toUpdate.Add(newContact.Key, newContact.Value);
                        Console.WriteLine("Contact updated successfully.");
                    }
                    else Console.WriteLine("No contact with such phone number found.");
                }
                else Console.WriteLine("Contact with such phone number is already registered.");
            }
            catch(Exception)
            {
                Console.WriteLine("Error while updating a contact.");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays all the needed information about the program
        /// </summary>
        static void DisplayHelp()
        {
            Console.WriteLine("\nInstructions:");
            Console.WriteLine("* The input is stored in a file named \"in.txt\"");
            Console.WriteLine("* Each value in a single row is separated by a semicolon (;)");
            Console.WriteLine("* The program is navigated by typing out the number of Your choice and pressing Enter\n");
        }
    }
}
