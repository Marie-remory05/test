using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static char[,] board;
    static int boardHeight, boardWidth;
    static string player1, player2;
    static int player1Flags, player2Flags;
    static string currentPlayer;

    static void Main(string[] args)
    {
        Game game = new Game();
        game.StartGame();
    }

    static void StartGame()
    {
        // Speler namen opvragen
        Console.WriteLine("Voer de naam van Speler 1 in:");
        player1 = Console.ReadLine();
        Console.WriteLine("Voer de naam van Speler 2 in:");
        player2 = Console.ReadLine();

        // Lijst van beschikbare .map bestanden tonen
        string[] mapFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.map");
        if (mapFiles.Length == 0)
        {
            Console.WriteLine("Geen .map bestanden gevonden in de applicatiefolder.");
            return;
        }

        Console.WriteLine("Kies een map:");
        for (int i = 0; i < mapFiles.Length; i++)
        {
            Console.WriteLine($"{i + 1}: {Path.GetFileName(mapFiles[i])}");
        }

        int mapChoice;
        while (!int.TryParse(Console.ReadLine(), out mapChoice) || mapChoice < 1 || mapChoice > mapFiles.Length)
        {
            Console.WriteLine("Ongeldige keuze. Kies een geldig nummer.");
        }

        // Kies de geselecteerde map en laad deze
        LoadMap(mapFiles[mapChoice - 1]);

        // Spel starten
        currentPlayer = player1;
        while (true) // Hoofdlus van het spel
        {
            DrawBoard();
            Console.WriteLine($"Aan de beurt: {currentPlayer} | Vlaggen - {player1}: {player1Flags}, {player2}: {player2Flags}");
            // Hier zou de logica van het spel komen, zoals zetten en vlaggen planten
            // Wissel van speler
            currentPlayer = currentPlayer == player1 ? player2 : player1;
        }
    }

    static void LoadMap(string mapFile)
    {
        var lines = File.ReadAllLines(mapFile);
        boardHeight = lines.Length;
        boardWidth = lines[0].Length;

        // Controleer op maximale afmetingen
        if (boardHeight > 15 || boardWidth > 30)
        {
            Console.WriteLine("De map is te groot. Maximaal 15 hoog en 30 breed.");
            return;
        }

        board = new char[boardHeight, boardWidth];

        // Rotsen en lege plekken inladen
        for (int i = 0; i < boardHeight; i++)
        {
            var line = lines[i];
            for (int j = 0; j < boardWidth; j++)
            {
                board[i, j] = line[j];
            }
        }

        // Plaats koning en paard op willekeurige posities
        PlacePieces();
    }

    static void PlacePieces()
    {
        Random random = new Random();
        // Plaats de koning en het paard voor beide spelers
        PlacePiece('K', player1);
        PlacePiece('P', player1);
        PlacePiece('K', player2);
        PlacePiece('P', player2);
    }

    static void PlacePiece(char piece, string player)
    {
        Random random = new Random();
        while (true)
        {
            int row = random.Next(boardHeight);
            int col = random.Next(boardWidth);
            if (board[row, col] == '.') // Controleer op lege cel
            {
                board[row, col] = piece; // Plaats het stuk
                break;
            }
        }
    }

    static void DrawBoard()
    {
        Console.Clear();
        // Coördinaten weergave
        Console.Write("  ");
        for (int j = 0; j < boardWidth; j++)
        {
            char coord = (char)('A' + j);
            Console.Write($"{coord} ");
        }
        Console.WriteLine();

        for (int i = 0; i < boardHeight; i++)
        {
            Console.Write($"{i + 1} "); // Rijen weergeven
            for (int j = 0; j < boardWidth; j++)
            {
                Console.Write($"{board[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}