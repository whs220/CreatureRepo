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


        // ============================= Methods =============================


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
            string menuChoice = Console.ReadLine();
            Console.WriteLine();

            switch (menuChoice.ToLower())
            {
                case "happy":
                    Menu();
                    break;
                case "tragic":
                    Menu();
                    break;
                case "twist":
                    Menu();
                    break;
                case "cliffhanger":
                    Menu();
                    break;
                case "strange":
                    Menu();
                    break;
                case "lame":
                    Menu();
                    break;
                //Catch-all so program will not break with incorrect input
                default:
                    Console.WriteLine("Sorry. That's not an option\n");
                    Menu(); 
                    break;
            }
        }

        public static void AnotherStory()
        {
            //print options
            Console.WriteLine("Would you like another story? Choose 'yes' or 'no'");
            Console.WriteLine();
            //choose option
            Console.Write(">> ");
            string another = Console.ReadLine();
            Console.WriteLine();

            switch (another.ToLower())
            {
                case "yes":
                    Console.WriteLine();
                    Menu();
                    break;
                default:
                    break;
            }
        }
    }
}