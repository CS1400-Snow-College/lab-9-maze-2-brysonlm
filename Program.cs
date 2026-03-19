// Bryson Leon, 3/18/26, Maze 2

Console.WriteLine("Welcome to the Maze Game!");
Console.WriteLine("Use the arrow keys to move collect ^, avoid %, find # to win.");
Console.WriteLine("Press any key to start...");
Console.ReadKey();
Console.Clear();

string[] mapRows = File.ReadAllLines("map.txt");

for (int i = 0; i < mapRows.Length; i++)
{
    Console.WriteLine(mapRows[i]);
}