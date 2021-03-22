# step-7
Contains the solution to question. The Main method calls the Iterate method of the QuestionClass which you can find in the root directory of the project.

The code for the solution is presented here for your convenience.

```
    public static class QuestionClass
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
```
