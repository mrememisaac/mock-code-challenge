using System;
using System.Collections.Generic;
using System.Text;

namespace VogCodeChallenge.ConsoleApp
{
    public class QuestionClass
    {
        static List<string> NamesList = new List<string>()
        {
            "Jimmy",
            "Jeffrey",
            "John",
        };

        public static void Iterate()
        {
            var enumerator = NamesList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
    }
}
