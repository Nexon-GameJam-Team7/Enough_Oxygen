using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    public static bool FindPath(Cell[,] grid, Cell start, Cell end, out List<Cell> path)
    {
        path = new List<Cell>();

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        bool[,] visited = new bool[width, height];
        Dictionary<Cell, Cell> cameFrom = new();

        Queue<Cell> queue = new();
        queue.Enqueue(start);
        visited[start.X, start.Y] = true;

        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == end)
                break;

            for (int i = 0; i < 4; i++)
            {
                int nx = current.X + dx[i];
                int ny = current.Y + dy[i];

                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    Cell neighbor = grid[nx, ny];
                    if (!visited[nx, ny] && !neighbor.IsObstacle && !neighbor.IsBlockedByLine)
                    {
                        visited[nx, ny] = true;
                        cameFrom[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        if (!cameFrom.ContainsKey(end)) return false;

        Cell curr = end;
        while (curr != start)
        {
            path.Add(curr);
            curr = cameFrom[curr];
        }
        path.Add(start);
        path.Reverse();

        return true;
    }

    public static void DrawLinePath(List<Cell> path, GameObject linePrefab, Transform parent)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            GameObject lineObj = GameObject.Instantiate(linePrefab, parent);
            LineRenderer lr = lineObj.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, path[i].transform.position);
            lr.SetPosition(1, path[i + 1].transform.position);
        }
    }
}