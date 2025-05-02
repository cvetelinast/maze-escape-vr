using System;
using System.Collections.Generic;

class RandomizedDFS {

    // Размери на лабиринта
    private int width = 10;
    private int height = 10;
    private Random random = new Random();

    public RandomizedDFS(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void Generate()
    {
        // Инициализация на visited и stack
        bool[,] visited = new bool[height, width];
        Stack<(int x, int y)> stack = new Stack<(int x, int y)>();

        // Начална точка
        (int x, int y) start = (0, 0);
        stack.Push(start);

        // Генериране на лабиринта
        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (!visited[current.y, current.x])
            {
                visited[current.y, current.x] = true;

                // Вземане на съседите и разбъркване
                var neighbors = GetNeighbors(current);
                Shuffle(neighbors);

                foreach (var neighbor in neighbors)
                {
                    stack.Push(neighbor);
                }
            }
        }

        Console.WriteLine("Лабиринтът е генериран!");
    }

    // Метод за получаване на съседни клетки
    private List<(int x, int y)> GetNeighbors((int x, int y) cell)
    {
        var neighbors = new List<(int x, int y)>();

        if (cell.y > 0) neighbors.Add((cell.x, cell.y - 1));            // Горен съсед
        if (cell.y < height - 1) neighbors.Add((cell.x, cell.y + 1));   // Долен съсед
        if (cell.x > 0) neighbors.Add((cell.x - 1, cell.y));            // Ляв съсед
        if (cell.x < width - 1) neighbors.Add((cell.x + 1, cell.y));    // Десен съсед

        return neighbors;
    }

    // Метод за разбъркване на списък (Fisher-Yates Shuffle)
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
