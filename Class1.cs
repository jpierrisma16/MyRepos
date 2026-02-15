using System;

namespace Module3Discussion
{
    public class Panda
    {
        // Field from your text file
        public string Name;
        public Panda Mate;

        // Adding a Property (Concept from the Stock example)
        public bool IsMatched => Mate != null;

        // Constructor (Concept from your text file)
        public Panda(string n)
        {
            Name = n;
        }

        // The "this" reference method from your text file
        public void Marry(Panda partner)
        {
            Mate = partner;
            partner.Mate = this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Creating instances
            Panda p1 = new Panda("Pan Zee");
            Panda p2 = new Panda("Pan Dah");

            // Using the Marry method
            p1.Marry(p2);

            Console.WriteLine($"{p1.Name} is married to {p1.Mate.Name}: {p1.IsMatched}");
        }
    }
}