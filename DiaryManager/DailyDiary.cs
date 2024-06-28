using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DiaryManager
{
    public class DailyDiary
    {
        private const string DiaryFilePath = "../../../mydiary.txt";
        private const string DateFormat = @"^\d{4}-\d{2}-\d{2}$";

        public void RunApp()
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("R. Read Diary");
                Console.WriteLine("A. Add New Entry");
                Console.WriteLine("D. Delete Entry");
                Console.WriteLine("C. Count All Lines");
                Console.WriteLine("S. Search Entries");
                Console.WriteLine("E. Exit");

                string choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "R":
                        ReadDiaryFile();
                        break;
                    case "A":
                        AddEntry();
                        break;
                    case "D":
                        Console.WriteLine("Enter the date (YYYY-MM-DD) of the entry to delete:");
                        string dateToDelete = Console.ReadLine();
                        DeleteEntry(dateToDelete);
                        break;
                    case "C":
                        CountLines();
                        break;
                    case "S":
                        Console.WriteLine("Enter the date (YYYY-MM-DD) to retrieve data:");
                        string dateToSearch = Console.ReadLine();
                        SearchEntries(dateToSearch);
                        break;
                    case "E":
                        continueRunning = false;
                        Console.WriteLine("Thank you for using the Daily Diary. Goodbye!");
                        continue;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("Do you want to perform another operation? (Y/N)");
                string continueChoice = Console.ReadLine().ToUpper();
                if (continueChoice != "Y")
                {
                    continueRunning = false;
                    Console.WriteLine("Thank you for using the Daily Diary. Goodbye!");
                }
            }
        }

        private bool IsValidDate(string date) => Regex.IsMatch(date, DateFormat);

        public void ReadDiaryFile()
        {
            try
            {
                if (File.Exists(DiaryFilePath))
                {
                    string[] lines = File.ReadAllLines(DiaryFilePath);
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading diary file: {ex.Message}");
            }
        }

        public void AddEntry()
        {
            try
            {
                Console.WriteLine("Enter the date (YYYY-MM-DD):");
                string date = Console.ReadLine();

                while (!IsValidDate(date))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    date = Console.ReadLine();
                }

                Console.WriteLine("Enter the content:");
                string content = Console.ReadLine();

                Entry newEntry = new Entry(date, content);
                AddEntry(newEntry); // Call overloaded method
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry: {ex.Message}");
            }
        }

        // Overloaded method to handle Entry objects
        public void AddEntry(Entry entry)
        {
            try
            {
                if (!IsValidDate(entry.Date))
                {
                    throw new ArgumentException("Invalid date format. Please use YYYY-MM-DD.");
                }

                string formattedEntry = $"{Environment.NewLine}{entry.Date}{Environment.NewLine}{entry.Content}{Environment.NewLine}";

                // Append entry to the diary file
                using (StreamWriter writer = File.AppendText(DiaryFilePath))
                {
                    writer.WriteLine(formattedEntry);
                }

                Console.WriteLine("Entry added successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry: {ex.Message}");
            }
        }

        public void DeleteEntry(string date)
        {
            try
            {
                while (!IsValidDate(date))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    date = Console.ReadLine();
                }

                if (File.Exists(DiaryFilePath))
                {
                    var lines = File.ReadAllLines(DiaryFilePath);
                    bool entryFound = false;
                    int indexToRemove = -1;

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains(date))
                        {
                            entryFound = true;
                            indexToRemove = i;
                            break;
                        }
                    }

                    if (entryFound)
                    {
                        string[] updatedLines = new string[lines.Length - 3];
                        Array.Copy(lines, 0, updatedLines, 0, indexToRemove);
                        Array.Copy(lines, indexToRemove + 3, updatedLines, indexToRemove, lines.Length - indexToRemove - 3);

                        File.WriteAllLines(DiaryFilePath, updatedLines);
                        Console.WriteLine("Entry deleted successfully.\n");
                    }
                    else
                    {
                        Console.WriteLine("No entry found for the specified date.");
                    }
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry: {ex.Message}");
            }
        }

        public void CountLines()
        {
            try
            {
                if (File.Exists(DiaryFilePath))
                {
                    int lineCount = File.ReadAllLines(DiaryFilePath).Length;
                    Console.WriteLine($"Total number of lines: {lineCount}\n");
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error counting lines: {ex.Message}");
            }
        }

        public void SearchEntries(string date)
        {
            try
            {
                while (!IsValidDate(date))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    date = Console.ReadLine();
                }

                if (File.Exists(DiaryFilePath))
                {
                    string[] lines = File.ReadAllLines(DiaryFilePath);
                    bool entryFound = false;

                    for (int i = 0; i < lines.Length; i += 3)
                    {
                        if (lines[i].Contains(date))
                        {
                            entryFound = true;
                            Console.WriteLine("Data retrieved:");
                            Console.WriteLine(lines[i]); // Print the date
                            Console.WriteLine(lines[i + 1]); // Print the content
                            Console.WriteLine(); // Add a blank line between entries
                        }
                    }

                    if (!entryFound)
                    {
                        Console.WriteLine("No entries found for the specified date.");
                    }
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching entries: {ex.Message}");
            }
        }
    }
}
