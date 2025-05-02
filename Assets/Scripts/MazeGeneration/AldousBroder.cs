using System;
using System.Collections.Generic;

public class AldousBroder {
    private int width;
    private int height;

    private readonly bool[,] visited;
    private readonly Random random = new Random();

    public AldousBroder(int width, int height)
    {
        this.width = width;
        this.height = height;
        visited = new bool[height, width];
    }

    public void Generate()
    {
        // Инициализация
        var current = (x: random.Next(width), y: random.Next(height));
        visited[current.y, current.x] = true;
        int unvisited = width * height - 1;

        while (unvisited > 0)
        {
            // Избор на произволен съсед
            var neighbors = GetNeighbors(current);
            var next = neighbors[random.Next(neighbors.Count)];

            if (!visited[next.y, next.x])
            {
                // Свързване на клетките (логика за премахване на стена)
                visited[next.y, next.x] = true;
                unvisited--;
            }

            current = next;
        }
    }

    private List<(int x, int y)> GetNeighbors((int x, int y) cell)
    {
        var neighbors = new List<(int x, int y)>();

        if (cell.y > 0) neighbors.Add((cell.x, cell.y - 1));            // Горен съсед
        if (cell.y < height - 1) neighbors.Add((cell.x, cell.y + 1));   // Долен съсед
        if (cell.x > 0) neighbors.Add((cell.x - 1, cell.y));            // Ляв съсед
        if (cell.x < width - 1) neighbors.Add((cell.x + 1, cell.y));    // Десен съсед

        return neighbors;
    }
}
