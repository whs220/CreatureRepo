namespace HW1_RandomStory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Read in data using file io

            //start generator
            Console.WriteLine("Welcome to the story generator!\n");
            Menu();
            Console.WriteLine("Thanks for playing!");
        }

        public static void Menu()
        {
            //print menu options
            Console.WriteLine("Please choose a type of ending to generate a story: ");
            Console.WriteLine("Happy");
            Console.WriteLine("Tragic");
            Console.WriteLine("Twist");
            Console.WriteLine("Cliffhanger");
            Console.WriteLine("Strange");
            Console.WriteLine("Lame");
            Console.WriteLine();
            //choose menu option
            Console.WriteLine("Your choice: ");
            string menuChioce = Console.ReadLine();
            Console.WriteLine();

            switch (menuChioce.ToLower())
            {
                case "happy":

                    Console.WriteLine();
                    Menu();
                    break;
                case "tragic":

                    Console.WriteLine();
                    Menu();
                    break;
                case "twist":

                    Console.WriteLine();
                    Menu();
                    break;
                case "cliffhanger":

                    Console.WriteLine();
                    Menu();
                    break;
                case "strange":

                    Console.WriteLine();
                    Menu();
                    break;
                case "lame":

                    Console.WriteLine();
                    Menu();
                    break;
                //Catch-all so program will not break with incorrect input
                default:
                    Console.WriteLine("Sorry. That's not an option\n");
                    Menu();
                    break;
            }
        }
    }
}