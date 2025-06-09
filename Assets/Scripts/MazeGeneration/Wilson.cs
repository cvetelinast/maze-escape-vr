using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wilson {
    private int width;
    private int height;
    private readonly Cell[,] grid;
    private int seed = 0;
    private System.Random random;

    public Wilson(int width, int height, int seed)
    {
        this.width = width;
        this.height = height;
        this.seed = seed;
        grid = new Cell[width, height];
        random = new System.Random(seed);
        InitMaze();
    }

    private void InitMaze()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                grid[i, j] = new Cell(i, j);
    }

    public void GenerateMaze()
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

            while (unvisited.Contains(current))
            {
                var neighbors = GetNeighbors((x: current.Row, y: current.Col));
                (int row, int col) = neighbors[random.Next(neighbors.Count)];

                // Вземане на клетка от грида вместо създаване на нова
                current = grid[row, col]; // Основната корекция

                var loopIndex = path.IndexOf(current);

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

    public void PrintPath()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var cell = grid[j, i];
                Debug.Log($"Cell ({cell.Row}, {cell.Col}) - Links: {string.Join(", ", cell.Links.Select(c => $"({c.Row}, {c.Col})"))}");
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

    public Cell GetCell(int row, int col)
    {
        if (row < 0 || row >= width || col < 0 || col >= height)
            return null;
        return grid[row, col];
    }

    public bool WallRight(Cell cell)
    {
        return cell.WallRight && (GetCell(cell.Row, cell.Col + 1)?.WallLeft ?? true);
    }

    public bool WallFront(Cell cell)
    {
        return cell.WallFront && (GetCell(cell.Row + 1, cell.Col)?.WallBack ?? true);
    }

    public bool WallLeft(Cell cell)
    {
        return cell.WallLeft && (GetCell(cell.Row, cell.Col - 1)?.WallRight ?? true);
    }

    public bool WallBack(Cell cell)
    {
        return cell.WallBack && (GetCell(cell.Row - 1, cell.Col)?.WallFront ?? true);
    }
}

public class Cell {
    public int Row { get; }
    public int Col { get; }
    public HashSet<Cell> Links { get; } = new HashSet<Cell>();

    public Cell(int row, int col) => (Row, Col) = (row, col);

    public override bool Equals(object obj)
    {
        return obj is Cell cell &&
               Row == cell.Row &&
               Col == cell.Col;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Col);
    }

    public void Link(Cell cell)
    {
        Links.Add(cell);
        cell.Links.Add(this);
    }

    public bool WallRight {
        get { return !Links.Any(cell => cell.Row == Row && cell.Col == Col + 1); }
    }

    public bool WallFront {
        get { return !Links.Any(cell => cell.Row == Row + 1 && cell.Col == Col); }
    }

    public bool WallLeft {
        get { return !Links.Any(cell => cell.Row == Row && cell.Col == Col - 1); }
    }
    public bool WallBack {
        get { return !Links.Any(cell => cell.Row == Row - 1 && cell.Col == Col); }
    }
}
