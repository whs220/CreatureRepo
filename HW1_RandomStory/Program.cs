// Jake, Josie, Leah, Dylan, Will
// 2/1/2023
// HW #1: Random Story generator
// Purpose: Creates random stories!!

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

            // -- menu ------------------------------------------------------------
            string menuChoice = "";
            Random rng = new Random();

            while (menuChoice != "no")
            {
                // -- Start chossing story info ---------------------------------------
                //choose the actors
                int actorNum1 = rng.Next(0, 6);
                int actorNum2 = rng.Next(0, 6);
                //make sure it's 2 different actors
                while (actorNum1 == actorNum2)
                {
                    actorNum2 = rng.Next(0, 6);
                }
                Actor actor1 = actors[actorNum1];
                Actor actor2 = actors[actorNum2];

                //choose the setting
                Setting setting = settings[rng.Next(0, settings.Count)];

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

                //pick conflict, default is none
                int conflictNum = -1;
                switch (menuChoice)
                {
                    case "happy":
                        conflictNum = rng.Next(0, 12);
                        while (conflicts[conflictNum].end != Ending.Happy)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "tragic":
                        conflictNum = rng.Next(0, 12);
                        while (conflicts[conflictNum].end != Ending.Tragic)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "twist":
                        conflictNum = rng.Next(0, 12);
                        while (conflicts[conflictNum].end != Ending.Twist)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "cliffhanger":
                        conflictNum = rng.Next(0, 12);
                        while (conflicts[conflictNum].end != Ending.Cliffhanger)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "strange":
                        conflictNum = rng.Next(0, 12);
                        while (conflicts[conflictNum].end != Ending.Strange)
                        {
                            conflictNum = rng.Next(0,12);
                        }
                        break;
                    case "lame":
                        conflictNum = rng.Next(0, 12);
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

                // If one was chosen...
                if (conflictNum != -1)
                {
                    //print conflict, replacing the text with the data
                    string output = conflicts[conflictNum].Dialouge.Replace("{Actor1.Name}", actor1.Name);
                    output = output.Replace("{Actor1.Pronoun1}", actor1.Pronoun1);
                    output = output.Replace("{Actor1.Pronoun2}", actor1.Pronoun2);
                    output = output.Replace("{Actor1.Pronoun3}", actor1.Pronoun3);
                    output = output.Replace("{Actor1.Occupation}", actor1.Occupation);
                    output = output.Replace("{Actor1.Description}", actor1.Description);
                    output = output.Replace("{Actor2.Name}", actor2.Name);
                    output = output.Replace("{Actor2.Pronoun1}", actor2.Pronoun1);
                    output = output.Replace("{Actor2.Pronoun2}", actor2.Pronoun2);
                    output = output.Replace("{Actor2.Pronoun3}", actor2.Pronoun3);
                    output = output.Replace("{Actor2.Occupation}", actor2.Occupation);
                    output = output.Replace("{Actor2.Description}", actor2.Description);
                    output = output.Replace("{Setting.Location}", setting.Location);
                    output = output.Replace("{Setting.Weather}", setting.Weather);
                    Console.WriteLine(output);
                    Console.WriteLine();

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
            }
            Console.WriteLine("Thanks for playing!");
        }
    }
}