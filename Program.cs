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

        // walls
        if (nextCell != '*') 
        {
            cursorTop = proposedTop;
            cursorLeft = proposedLeft;
            Console.SetCursorPosition(cursorLeft, cursorTop);

            // detect if win
            if (nextCell == '#') 
            {
                TimeSpan timeTaken = DateTime.Now - startTime;
                Console.Clear();
                Console.WriteLine("Congrats! You got it!!!!");
                Console.WriteLine("Time: " + Math.Round(timeTaken.TotalSeconds) + " seconds");
                break;
            }
        }
    }
} while (input != ConsoleKey.Escape);