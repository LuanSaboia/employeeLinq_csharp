using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using ExFixLambda.Entities;

namespace ExFixLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            List<Employee> list = new List<Employee>();

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        string email = fields[1];
                        double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, salary));
                    }
                }

                Console.Write("Enter salary: ");
                double sal = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                var emails = list.Where(e => e.Salary > sal).OrderBy(e => e.Email).Select(e => e.Email);
                Console.WriteLine("Email of people whose salary is more than " + sal.ToString("F2", CultureInfo.InvariantCulture) + ":");
                foreach (var item in emails)
                {
                    Console.WriteLine(item);
                }

                double sum = list.Where(e => e.Name[0] == 'M').Sum(e => e.Salary);
                Console.WriteLine($"Sum of salary of people whose name starts with 'M': " + sum.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
