namespace HW1_RandomStory
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //Read in data using file io ==========================================
            StreamReader reader = null;
            List<Setting> settings = new List<Setting>();
            List<Actor> actors = new List<Actor>();
            List<Conflict> conflicts = new List<Conflict>();

            // -- Settings --------------------------------------------------------
            try
            {
                reader = new StreamReader("../../../settings.txt");

                //Creates a placeholder string
                string lineOfText = "";
                //Sets line of text equal to line and checks it's not null
                while ((lineOfText = reader.ReadLine()) != null)
                {

                    string[] splitString = lineOfText.Split('|');

                    settings.Add(new Setting(splitString[0], splitString[1]));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            reader.Close();

            // -- Conflicts -------------------------------------------------------
            try
            {
                reader = new StreamReader("../../../conflicts.txt");

                //Creates a placeholder string
                string lineOfText = "";
                //Sets line of text equal to line and checks it's not null
                while ((lineOfText = reader.ReadLine()) != null)
                {

                    string[] splitString = lineOfText.Split('|');

                    conflicts.Add(new Conflict(splitString[0], Enum.Parse<Ending>(splitString[1])));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            reader.Close();

            // -- Actors ----------------------------------------------------------
            try
            {
                reader = new StreamReader("../../../actors.txt");

                //Creates a placeholder string
                string lineOfText = "";
                //Sets line of text equal to line and checks it's not null
                while ((lineOfText = reader.ReadLine()) != null)
                {

                    string[] splitString = lineOfText.Split('|');

                    actors.Add(new Actor(splitString[0], splitString[1], splitString[2],
                        splitString[3], splitString[4], splitString[5]));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            reader.Close();

            //start generator =====================================================
            Console.WriteLine("Welcome to the story generator!\n");

            // -- Start chossing story info ---------------------------------------
            //choose the actors
            Random rng = new Random();
            int actorNum1 = rng.Next(0,3);
            int actorNum2 = rng.Next(0,3);
            //make sure it's 2 different actors
            while (actorNum1 == actorNum2)
            {
                actorNum2 = rng.Next(0, 3);
            }
            Actor Actor1 = actors[actorNum1];
            Actor Actor2 = actors[actorNum2];

            //choose the setting
            Setting Setting = settings[rng.Next(0,settings.Count)];

            // -- menu ------------------------------------------------------------
            string menuChoice = "";

            while (menuChoice != "no")
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
                menuChoice = Console.ReadLine().ToLower();
                Console.WriteLine();

                //pick conflict
                int conflictNum = rng.Next(0,12);
                switch (menuChoice)
                {
                    case "happy":

                        while (conflicts[conflictNum].end != Ending.Happy)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "tragic":
                        while (conflicts[conflictNum].end != Ending.Tragic)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "twist":
                        while (conflicts[conflictNum].end != Ending.Twist)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "cliffhanger":
                        while (conflicts[conflictNum].end != Ending.Cliffhanger)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "strange":
                        while (conflicts[conflictNum].end != Ending.Strange)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "lame":
                        while (conflicts[conflictNum].end != Ending.Lame)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    //Catch-all so program will not break with incorrect input
                    default:
                        Console.WriteLine("Sorry. That's not an option\n");
                        break;
                }

                //print conflict
                Console.WriteLine(conflicts[conflictNum].Dialouge);                    //NOT WORKING-----INTERPOLATION IS STUPID
                // -- go again ----------------------------------------------------
                //print options
                Console.WriteLine("Would you like another story? Choose 'yes' or 'no'");
                Console.WriteLine();
                //choose option
                Console.Write(">> ");
                menuChoice = Console.ReadLine();
                Console.WriteLine();

                switch (menuChoice.ToLower())
                {
                    case "yes":
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Thanks for playing!");
        }
    }
}