using System;
using System.Collections.Generic;
using System.Globalization;

namespace ContractorDemo
{
    // Base class
    public class Contractor
    {
        // Fields / auto-properties
        public string Name { get; set; }
        public string ContractorNumber { get; set; }
        public DateTime StartDate { get; set; }

        // Constructors
        public Contractor() { }

        public Contractor(string name, string contractorNumber, DateTime startDate)
        {
            Name = name;
            ContractorNumber = contractorNumber;
            StartDate = startDate;
        }

        // Accessors / Mutators are provided via properties above
    }

    // Derived class
    public class Subcontractor : Contractor
    {
        // Shift: 1 = day, 2 = night
        public int Shift { get; private set; }
        public double HourlyPayRate { get; private set; }

        // Constructors
        public Subcontractor() : base() { }

        public Subcontractor(string name, string contractorNumber, DateTime startDate, int shift, double hourlyPayRate)
            : base(name, contractorNumber, startDate)
        {
            SetShift(shift);
            SetHourlyPayRate(hourlyPayRate);
        }

        // Mutators with basic validation
        public void SetShift(int shift)
        {
            if (shift != 1 && shift != 2)
                throw new ArgumentException("Shift must be 1 (day) or 2 (night).");
            Shift = shift;
        }

        public void SetHourlyPayRate(double rate)
        {
            if (rate < 0) throw new ArgumentException("Hourly pay rate must be non-negative.");
            HourlyPayRate = rate;
        }

        // Accessors already via properties and getter methods above

        // Compute pay: returns float. If night shift (2), apply 3% differential to hourly rate.
        // Parameter hoursWorked is a float (or double) representing hours in a pay period.
        public float ComputePay(float hoursWorked)
        {
            if (hoursWorked < 0) throw new ArgumentException("Hours worked must be non-negative.");
            double rate = HourlyPayRate;
            if (Shift == 2)
                rate *= 1.03; // 3% differential for night shift
            double pay = rate * hoursWorked;
            return (float)pay;
        }
    }

    class Program
    {
        static void Main()
        {
            var subcontractors = new List<Subcontractor>();
            Console.WriteLine("Subcontractor entry. Enter blank name to finish.");

            while (true)
            {
                Console.Write("Name (blank to finish): ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) break;

                Console.Write("Contractor number: ");
                string number = Console.ReadLine();

                Console.Write("Start date (MM/DD/YYYY): ");
                DateTime startDate;
                while (!DateTime.TryParseExact(Console.ReadLine(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate)
                       && !DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    Console.Write("Invalid. Enter start date (MM/DD/YYYY): ");
                }

                Console.Write("Shift (1=Day, 2=Night): ");
                int shift;
                while (!int.TryParse(Console.ReadLine(), out shift) || (shift != 1 && shift != 2))
                {
                    Console.Write("Invalid. Enter shift (1 or 2): ");
                }

                Console.Write("Hourly pay rate (e.g. 15.50): ");
                double rate;
                while (!double.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out rate) || rate < 0)
                {
                    Console.Write("Invalid. Enter hourly pay rate: ");
                }

                var sc = new Subcontractor(name, number, startDate, shift, rate);
                subcontractors.Add(sc);

                Console.Write("Enter hours worked for this subcontractor now? (y/n): ");
                if (Console.ReadLine()?.Trim().ToLower() == "y")
                {
                    Console.Write("Hours worked: ");
                    float hours;
                    while (!float.TryParse(Console.ReadLine(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out hours) || hours < 0)
                    {
                        Console.Write("Invalid. Enter hours worked: ");
                    }
                    float pay = sc.ComputePay(hours);
                    Console.WriteLine($"Pay for {sc.Name}: {pay:C}");
                }

                Console.WriteLine("Subcontractor added.\n");
            }

            Console.WriteLine("\nAll subcontractors entered:");
            foreach (var s in subcontractors)
            {
                Console.WriteLine($"Name: {s.Name}, Number: {s.ContractorNumber}, Start: {s.StartDate:MM/dd/yyyy}, Shift: {s.Shift}, Rate: {s.HourlyPayRate:C}");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
