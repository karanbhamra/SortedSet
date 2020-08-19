using System;
using System.Linq;

namespace Set
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedSet<string> set = new SortedSet<string>();
            Console.WriteLine("set = " + set);

            // insert some keys
            set.Add("www.cs.princeton.edu");
            set.Add("www.cs.princeton.edu");    // overwrite old value
            set.Add("www.princeton.edu");
            set.Add("www.math.princeton.edu");
            set.Add("www.yale.edu");
            set.Add("www.amazon.com");
            set.Add("www.simpsons.com");
            set.Add("www.stanford.edu");
            set.Add("www.google.com");
            set.Add("www.ibm.com");
            set.Add("www.apple.com");
            set.Add("www.slashdot.com");
            set.Add("www.whitehouse.gov");
            set.Add("www.espn.com");
            set.Add("www.snopes.com");
            set.Add("www.movies.com");
            set.Add("www.cnn.com");
            set.Add("www.iitb.ac.in");


            Console.WriteLine(set.Contains("www.cs.princeton.edu"));
            Console.WriteLine(!set.Contains("www.harvardsucks.com"));
            Console.WriteLine(set.Contains("www.simpsons.com"));
            Console.WriteLine();

            Console.WriteLine("Ceiling(www.simpsonr.com) = " + set.Ceiling("www.simpsonr.com"));
            Console.WriteLine("Ceiling(www.simpsons.com) = " + set.Ceiling("www.simpsons.com"));
            Console.WriteLine("Ceiling(www.simpsont.com) = " + set.Ceiling("www.simpsont.com"));
            Console.WriteLine("Floor(www.simpsonr.com)   = " + set.Floor("www.simpsonr.com"));
            Console.WriteLine("Floor(www.simpsons.com)   = " + set.Floor("www.simpsons.com"));
            Console.WriteLine("Floor(www.simpsont.com)   = " + set.Floor("www.simpsont.com"));
            Console.WriteLine();

            Console.WriteLine("set = " + set);
            Console.WriteLine();

            // print out all keys in this set in lexicographic order
            foreach (string s in set)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine();
            SortedSet<string> set2 = new SortedSet<string>();
            set2.AddRange(set);
            Console.WriteLine(set.Equals(set2));


        }
    }
}
