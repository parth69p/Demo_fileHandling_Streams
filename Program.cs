using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
//using System.Memory;

namespace Demo_fileHandling_Streams
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }

        public Employee(int id, string name, string department)
        {
            Id = id;
            Name = name;
            Department = department;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee(1, "John Doe", "HR"),
                new Employee(2, "Jane Smith", "IT"),
                new Employee(3, "Sam Brown", "Finance")
            };

            string filepath = @"C:\Users\parth\source\repos\Demo_fileHandling_Streams\bin\Debug\employees.json";

            try
            {
                using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
                {
                    // json serializer is used to serialize the employees list to json string 
                    // this can be used to share data between different systems or store it in a file
                    JsonSerializer.Serialize(fs, employees);
                }



                Console.WriteLine("Data written to file successfully at: " + filepath);

                // reading json string from the file
                FileStream read = new FileStream(filepath, FileMode.Open);

                List <Employee> deserializedEmployees = JsonSerializer.Deserialize<List<Employee>>(read);
                // we have include ststem.Memory as reference in the project file
                // displaying the deserializedEmployees data
                if (deserializedEmployees != null)
                {
                    foreach (var emp in deserializedEmployees)
                    {
                        Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Department: {emp.Department}");
                    }
                }
                else
                {
                    Console.WriteLine("No employees found in the file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

           
        }
    }
}
