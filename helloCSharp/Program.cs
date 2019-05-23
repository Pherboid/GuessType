using System;

namespace helloCSharp{
    class MainClass{
        public static void Main(string[] args){
            Console.WriteLine("Hello World!");
            Console.Write("Enter a value: ");
            string str = Console.ReadLine();

            GuessType guessType = new GuessType(str);
            var a = guessType.ConvertToType();
            Console.WriteLine("Your input " + str + " is of type " + a.GetType());
        }
    }
}
