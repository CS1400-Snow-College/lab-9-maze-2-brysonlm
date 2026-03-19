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
Random rand = new Random();
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

        if (nextCell != '*' && nextCell != '|') 
        {
            cursorTop = proposedTop;
            cursorLeft = proposedLeft;
            Console.SetCursorPosition(cursorLeft, cursorTop);

            if (nextCell == '^')
            {
                score += 100;
                
                char[] playerRow = mapRows[cursorTop].ToCharArray();
                playerRow[cursorLeft] = ' ';
                mapRows[cursorTop] = new string(playerRow);
                
                Console.Write(" ");
                Console.SetCursorPosition(cursorLeft, cursorTop); 

                if (score >= 1000)
                {
                    for (int i = 0; i < mapRows.Length; i++)
                    {
                        mapRows[i] = mapRows[i].Replace('|', ' ');
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

    // move bad guys
    for (int r = mapRows.Length - 1; r >= 0; r--) 
    {
        for (int c = mapRows[r].Length - 1; c >= 0; c--)
        {
            if (mapRows[r][c] == '%')
            {
                // choosing left or right
                int direction = rand.Next(0, 2) == 0 ? -1 : 1;
                int newCol = c + direction;

                // moving
                if (mapRows[r][newCol] == ' ' || (r == cursorTop && newCol == cursorLeft)) 
                {
                    char[] enemyRow = mapRows[r].ToCharArray();
                    enemyRow[c] = ' ';
                    enemyRow[newCol] = '%';
                    mapRows[r] = new string(enemyRow);
                    
                    Console.SetCursorPosition(c, r);
                    Console.Write(" ");
                    Console.SetCursorPosition(newCol, r);
                    Console.Write("%");
                }
            }
        }
    }

    // see if the bad guys hit you
    if (mapRows[cursorTop][cursorLeft] == '%')
    {
        Console.Clear();
        Console.WriteLine("You Lose! A bad guy caught you.");
        break;
    }
    
    Console.SetCursorPosition(cursorLeft, cursorTop); // put cursor back on player

} while (input != ConsoleKey.Escape);