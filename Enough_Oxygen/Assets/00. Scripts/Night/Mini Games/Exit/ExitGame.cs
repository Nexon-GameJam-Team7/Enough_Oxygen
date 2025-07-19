using UnityEngine;

[DisallowMultipleComponent]
public class ExitGame : MonoBehaviour
{
    public int width = 10, height = 10;
    public GameObject cellPrefab;
    public GridCell[,] grid;

    public Vector2Int ballStartPos;
    public Vector2Int goalPos;

    [SerializeField] private Transform cellParent;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new GridCell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject go = Instantiate(cellPrefab);
                go.transform.SetParent(cellParent);
                go.transform.localPosition = new Vector3(x, y, 0);

                var cell = go.GetComponent<GridCell>();
                CellType type = CellType.Empty;

                if ((x == 6 && y == 0) || (x == 3 && y == 1) || (x == 1 && y == 2) || (x == 4 && y == 3) || (x == 6 && y == 5) || (x == 2 && y == 6))
                    type = CellType.Wall;

                if (x == 5 && y == 5) type = CellType.BallStart;

                if (x == 3 && y == 3) type = CellType.Goal;

                cell.Init(new Vector2Int(x, y), type);
                grid[x, y] = cell;
            }
        }
    }

    public bool IsBlocked(Vector2Int pos)
    {
        if (!IsInside(pos)) return true;
        return grid[pos.x, pos.y].cellType == CellType.Wall;
    }

    public bool IsInside(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    public void Close()
    {
        Player player = FindObjectOfType<Player>();
        player.UnableToInteract();
        player.UnInteraction();

        ObjectManager objManager = FindObjectOfType<ObjectManager>();
        objManager.CompleteExitMission();

        Destroy(gameObject);
    }
}
