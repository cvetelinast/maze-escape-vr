using System;
using System.Collections.Generic;
using System.Linq;

public class Wilson {
    private int width;
    private int height;
    private readonly Cell[,] grid;
    private readonly Random random = new Random();

    public Wilson(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new Cell[width, height];
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                grid[i, j] = new Cell(i, j);
    }

    public void Generate()
    {
        var visited = new HashSet<Cell>();
        var unvisited = grid.Cast<Cell>().ToList();

        // Начална клетка
        var first = unvisited[random.Next(unvisited.Count)];
        visited.Add(first);
        unvisited.Remove(first);

        while (unvisited.Count > 0)
        {
            var current = unvisited[random.Next(unvisited.Count)];
            var path = new List<Cell> { current };

            // Случайна разходка
            while (unvisited.Contains(current))
            {
                // Избор на произволен съсед
                var neighbors = GetNeighbors((x: current.Row, y: current.Col));
                (int row, int col) = neighbors[random.Next(neighbors.Count)];

                current = new Cell(row, col);
                var loopIndex = path.IndexOf(current);

                // Изтриване на цикъл
                if (loopIndex != -1)
                    path = path.Take(loopIndex + 1).ToList();
                else
                    path.Add(current);
            }

            // Добавяне на пътя
            for (int i = 0; i < path.Count - 1; i++)
            {
                path[i].Link(path[i + 1]);
                visited.Add(path[i]);
                unvisited.Remove(path[i]);
            }
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

public class Cell {
    public int Row { get; }
    public int Col { get; }
    public HashSet<Cell> Links { get; } = new HashSet<Cell>();

    public Cell(int row, int col) => (Row, Col) = (row, col);

    public void Link(Cell cell)
    {
        Links.Add(cell);
        cell.Links.Add(this);
    }
}
