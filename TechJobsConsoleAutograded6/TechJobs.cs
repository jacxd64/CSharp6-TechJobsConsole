using System;
using System.Collections.Generic;

namespace TechJobsConsoleAutograded6
{
    public class TechJobs
    {
        public void RunProgram()
        {
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            while (true)
            {
                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice == null)
                {
                    break;
                }
                else if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine(Environment.NewLine + "*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else
                {
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    Console.WriteLine(Environment.NewLine + "Search term: ");
                    string searchTerm = Console.ReadLine();
                    Console.WriteLine();

                    if (columnChoice.Equals("all"))
                    {
                        List<Dictionary<string, string>> searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }
                    else
                    {
                        List<Dictionary<string, string>> searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        public string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                if (choiceHeader.Equals("View Jobs"))
                {
                    Console.WriteLine(Environment.NewLine + choiceHeader + " by (type 'x' to quit):");
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + choiceHeader + " by:");
                }

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string input = Console.ReadLine();
                if (input.Equals("x") || input.Equals("X"))
                {
                    return null;
                }
                else
                {
                    choiceIdx = int.Parse(input);
                }

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        public void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            if (someJobs.Count == 0)
            {
                Console.WriteLine("No results");
            }
            else
            {
                for (int i = 0; i < someJobs.Count; i++)
                {
                    var job = someJobs[i];
                    Console.WriteLine("*****");
                    foreach (KeyValuePair<string, string> field in job)
                    {
                        Console.WriteLine($"{field.Key}: {field.Value}");
                    }
                    Console.WriteLine("*****");
                    if (i < someJobs.Count - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
