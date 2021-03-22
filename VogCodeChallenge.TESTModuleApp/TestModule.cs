using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VogCodeChallenge.TESTModuleApp
{
    class TestModule
    {
        public static dynamic TESTModule(dynamic input)
        {
            dynamic result = input switch
            {
                int num when num >= 1 && num <= 4 => num * 2,
                int num when num >= 5 => num * 3,
                int num when num < 1 => throw new ArgumentOutOfRangeException("Value cannot be less than 1"),
                float num when num == 1.0f || num == 2.0f => 3.0f,
                string str => str.ToUpper(),
                _ => input,
            };
            return result;
        }

        public static void Driver()
        {
            var testData = new ArrayList()
            {
                1,2,3,4,5,1.0f,2.0f,"a",true,0
            };
            var iterator = testData.GetEnumerator();
            while (iterator.MoveNext())
            {
                try
                {
                    Console.WriteLine(TESTModule(iterator.Current));
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
