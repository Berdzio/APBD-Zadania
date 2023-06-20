namespace cwiczenie2
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;

    class CSV
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Niepoprawna liczba argumentów. Użyj: adres_pliku_wejsciowego adres_pliku_wyjsciowego format");
                return;
            }

            string csvFilePath = args[0];
            string outputFilePath = args[1];
            string format = args[2];
            Console.WriteLine(csvFilePath);

            try
            {
                List<Student> students = ReadCsvFile(csvFilePath);
                List<Student> uniqueStudents = RemoveDuplicates(students);
                ExportData(uniqueStudents, outputFilePath, format);
                Console.WriteLine("Eksport danych zakończony sukcesem.");
            }
            catch (ArgumentException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Błąd: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                LogError(ex.Message);
                Console.WriteLine("Błąd: " + ex.Message);
            }
            
        }

        static List<Student> ReadCsvFile(string csvFilePath)
        {
            List<Student> students = new List<Student>();

            if (!File.Exists(csvFilePath))
            {
                throw new FileNotFoundException("Plik " + csvFilePath + " nie istnieje");
            }

            string[] lines = File.ReadAllLines(csvFilePath);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                bool check = true;

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == "")
                    {
                        check = false;
                    }
                }
                if (check && values.Length == 9)
                {
                    Student student = new Student
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        Program = values[2],
                        StudyMode = values[3],
                        IndexNumber = values[4],
                        BirthDate = DateTime.Parse(values[5]),
                        Email = values[6],
                        GuardianFirstName = values[7],
                        GuardianLastName = values[8]
                    };

                    students.Add(student);
                }
                else
                {
                    LogError("Niepoprawna liczba kolumn w wierszu: " + line);
                }
            }

            return students;
        }

        static List<Student> RemoveDuplicates(List<Student> students)
        {
            return students.GroupBy(s => new { s.FirstName, s.LastName, s.IndexNumber })
                           .Select(g => g.First())
                           .ToList();
        }

        static void ExportData(List<Student> students, string outputFilePath, string format)
        {
            if (format != "json")
            {
                throw new ArgumentException("Nieobsługiwany format danych: " + format);
            }

            string jsonData = JsonSerializer.Serialize(students, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(outputFilePath, jsonData);
        }

        static void LogError(string errorMessage)
        {
            string logFilePath = "log.txt";
            string logMessage = DateTime.Now.ToString() + " - " + errorMessage;

            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }

    class Student
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Program { get; set; }
        public string? StudyMode { get; set; }
        public string? IndexNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? GuardianFirstName { get; set; }
        public string? GuardianLastName { get; set; }

    }
}