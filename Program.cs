// Bryson Leon, 3/18/26, Maze 2

Console.WriteLine("Welcome to the Maze Game!");
Console.WriteLine("Use the arrow keys to move collect ^, avoid %, find # to win.");
Console.WriteLine("Press any key to start...");
Console.ReadKey();
Console.Clear();

string[] mapRows = File.ReadAllLines("map.txt");

//map put into the terminal
for (int i = 0; i < mapRows.Length; i++)
{
    Console.WriteLine(mapRows[i]);
}

int cursorTop = 1;
int cursorLeft = 1;
int score = 0;
Console.SetCursorPosition(cursorLeft, cursorTop);
DateTime startTime = DateTime.Now;

// adding the movement
ConsoleKey input;
do
{
    input = Console.ReadKey(true).Key;

    int proposedTop = cursorTop;
    int proposedLeft = cursorLeft;

    switch (input)
    {
        case ConsoleKey.UpArrow: proposedTop--; break;
        case ConsoleKey.DownArrow: proposedTop++; break;
        case ConsoleKey.LeftArrow: proposedLeft--; break;
        case ConsoleKey.RightArrow: proposedLeft++; break;
    }

    if (proposedTop >= 0 && proposedTop < mapRows.Length && 
        proposedLeft >= 0 && proposedLeft < mapRows[proposedTop].Length)
    {
        char nextCell = mapRows[proposedTop][proposedLeft];

        // walls and the locked doors
        if (nextCell != '*' && nextCell != '|') 
        {
            cursorTop = proposedTop;
            cursorLeft = proposedLeft;
            Console.SetCursorPosition(cursorLeft, cursorTop);

            // to collect the coins
            if (nextCell == '^')
            {
                score += 100;
                
                // get rid of coins
                char[] row = mapRows[cursorTop].ToCharArray();
                row[cursorLeft] = ' ';
                mapRows[cursorTop] = new string(row);
                
                // get rid of coins from the screen
                Console.Write(" ");
                Console.SetCursorPosition(cursorLeft, cursorTop); 
                
                // to open the doors
                if (score >= 1000)
                {
                    for (int i = 0; i < mapRows.Length; i++)
                    {
                        mapRows[i] = mapRows[i].Replace('|', ' ');
                        
                        // redraw to get rid of doors
                        if (mapRows[i].Contains("$") || mapRows[i].Contains("#")) 
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write(mapRows[i]);
                            Console.SetCursorPosition(cursorLeft, cursorTop);
                        }
                    }
                }
            }

            if (nextCell == '#') 
            {
                TimeSpan timeTaken = DateTime.Now - startTime;
                Console.Clear();
                Console.WriteLine("Congrats! You got it!!!!");
                Console.WriteLine("Score: " + score);
                Console.WriteLine("Time: " + Math.Round(timeTaken.TotalSeconds) + " seconds");
                break;
            }
        }
    }
} while (input != ConsoleKey.Escape);