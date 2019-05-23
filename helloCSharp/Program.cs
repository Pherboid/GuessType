using System;

namespace helloCSharp{
    class MainClass{
        private static void fromUser() {
            Console.WriteLine("Hello World!");

            bool stop = false;
            do
            {
                Console.Write("Enter a value: ");
                string str = Console.ReadLine();
                GuessType guessType = new GuessType(str);
                var a = guessType.ConvertToType();
                Console.WriteLine("Your input \"" + a + "\" is of type " + a.GetType());
                stop = string.Compare(a.ToString(), "End") == 0;
            } while (!stop);
        }

        private static void tests() { //test compliance of GuessType
            string[] test = { "1", "a", "Name", "256", "1.2", "43434534.3554334", "23.325432335253234323232235232352432", "1-32", "-1223.213", "-32", "3423fd3", "-32d34", "32132432432467587655465354246445768565454454534354343", "3244444444444322222222222222222333333333334444442343454645.886", "0..32", "...32", ".32", "+43", "23.43.43", "192.168.1.1", "32..43.43", "32-32-43"};

            foreach (string s in test) {
                GuessType guessType = new GuessType(s);
                var a = guessType.ConvertToType();
                Console.WriteLine("Test \"{0}\" is of type {1}", a, a.GetType());
            }
        }

        public static void Main(string[] args) {
            //fromUser();
            tests();
        }
    }
}