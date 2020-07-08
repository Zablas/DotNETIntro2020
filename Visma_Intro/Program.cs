﻿using System;
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
                            break;
                        case 2:
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
    }
}
