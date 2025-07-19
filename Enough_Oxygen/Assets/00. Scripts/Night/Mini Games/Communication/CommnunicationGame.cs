using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CommnunicationGame : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Transform lineParent;
    [SerializeField] private int gridSize = 8;

    private Cell[,] grid;
    private Dictionary<string, List<Cell>> symbolGroups = new();
    private Cell dragStartCell = null;
    private Cell lastCell = null;
    private bool isDragging = false;
    private List<Vector3> currentLinePoints = new();
    private LineRenderer currentLine;
    private GameObject currentLineObj;
    private ObjectManager objectManager;

    private Dictionary<string, Color> symbolColors = new()
    {
        { "★", Color.yellow },
        { "♥", Color.red },
        { "♦", new Color(1f, 0.4f, 0.7f) },  // 연분홍
        { "♣", Color.green },
        { "♠", Color.black },
        { "●", Color.blue },
    };

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

        lineParent = GameObject.Find("Line Parent").transform;

        objectManager = FindObjectOfType<ObjectManager>();

        GenerateGrid();
        PlaceSymbols();
        PlaceObstacles();
    }

    private void GenerateGrid()
    {
        grid = new Cell[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject obj = Instantiate(cellPrefab, gridParent);
                Cell cell = obj.GetComponent<Cell>();
                cell.Init(x, y, this);
                grid[x, y] = cell;
            }
        }
    }

    private void PlaceSymbols()
    {
        PlaceSymbol("★", 6, 6);
        PlaceSymbol("★", 4, 5);
        PlaceSymbol("♥", 1, 0);
        PlaceSymbol("♥", 5, 7);
        PlaceSymbol("♦", 2, 0);
        PlaceSymbol("♦", 5, 2);
        PlaceSymbol("♣", 0, 3);
        PlaceSymbol("♣", 5, 6);
        PlaceSymbol("♠", 0, 0);
        PlaceSymbol("♠", 1, 6);
        PlaceSymbol("●", 1, 3);
        PlaceSymbol("●", 2, 5);
    }

    private void PlaceSymbol(string symbol, int x, int y)
    {
        Cell cell = grid[x, y];
        cell.SetSymbol(symbol);

        if (!symbolGroups.ContainsKey(symbol))
            symbolGroups[symbol] = new List<Cell>();

        symbolGroups[symbol].Add(cell);
    }

    private void PlaceObstacles()
    {
        SetObstacle(5, 1);
        SetObstacle(6, 1);
        SetObstacle(1, 4);
        SetObstacle(1, 5);
        SetObstacle(5, 5);
    }

    private void SetObstacle(int x, int y)
    {
        Cell cell = grid[x, y];
        cell.SetObstacle();
    }

    private void Update()
    {
        if (!isDragging || currentLine == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        currentLine.SetPosition(currentLine.positionCount - 1, mouseWorld);

        if (Input.GetMouseButtonUp(0))
        {
            EndDrag(null);
        }
    }

    public void StartDrag(Cell cell)
    {
        if (!cell.HasSymbol || cell.IsObstacle || cell.IsBlockedByLine || cell.IsMatched) return;

        dragStartCell = cell;
        lastCell = cell;
        isDragging = true;

        currentLineObj = Instantiate(linePrefab, lineParent);
        currentLine = currentLineObj.GetComponent<LineRenderer>();

        Color lineColor = symbolColors[cell.Symbol];
        currentLine.startColor = lineColor;
        currentLine.endColor = lineColor;
        currentLine.material.color = lineColor;

        currentLinePoints.Clear();

        Vector3 start = cell.transform.position;
        start.z = 0f;
        currentLinePoints.Add(start);
        currentLinePoints.Add(start);

        currentLine.positionCount = 2;
        currentLine.widthMultiplier = 0.1f;
        currentLine.SetPositions(currentLinePoints.ToArray());

        GameManager.Sound.SFXPlay("rope");
    }

    public void DragOver(Cell cell)
    {
        if (!isDragging || cell == lastCell || cell.IsObstacle || cell.IsBlockedByLine) return;

        if (cell.HasSymbol && cell.Symbol != dragStartCell.Symbol) return;

        if (Mathf.Abs(cell.X - lastCell.X) + Mathf.Abs(cell.Y - lastCell.Y) == 1)
        {
            if (cell != dragStartCell && cell.Symbol == dragStartCell.Symbol && !cell.IsMatched)
            {
                dragStartCell.IsMatched = true;
                cell.IsMatched = true;
                cell.IsBlockedByLine = true;

                bool isClear = CheckClearCondition();

                if (isClear)
                {
                    UIBase uiBase = GetComponent<UIBase>();

                    for (int i = lineParent.childCount - 1; i >= 0; i--)
                    {
                        DestroyImmediate(lineParent.GetChild(i).gameObject);
                    }

                    uiBase.Close();
                    return;
                }

                Vector3 _pos = cell.transform.position;
                _pos.z = 0f;
                currentLinePoints[currentLinePoints.Count - 1] = _pos;
                currentLinePoints.Add(_pos);
                currentLine.positionCount = currentLinePoints.Count;
                currentLine.widthMultiplier = 0.1f;
                currentLine.SetPositions(currentLinePoints.ToArray());

                EndDrag(cell);  // 자동으로 드래그 종료
                return;
            }

            Vector3 pos = cell.transform.position;
            pos.z = 0f;
            currentLinePoints[currentLinePoints.Count - 1] = pos;
            currentLinePoints.Add(pos);
            currentLine.positionCount = currentLinePoints.Count;
            currentLine.widthMultiplier = 0.1f;
            currentLine.SetPositions(currentLinePoints.ToArray());

            cell.IsBlockedByLine = true;
            lastCell = cell;
        }
    }

    public void EndDrag(Cell endCell)
    {
        isDragging = false;

        if (!dragStartCell.IsMatched)
        {
            // 매칭 실패: 선 제거
            foreach (Vector3 point in currentLinePoints)
            {
                Vector2 worldPos = point;
                foreach (var cell in grid)
                {
                    if ((Vector2)cell.transform.position == worldPos)
                    {
                        cell.IsBlockedByLine = false;
                    }
                }
            }

            Destroy(currentLineObj);
        }
    }

    private bool CheckClearCondition()
    {
        foreach (var pair in symbolGroups)
        {
            foreach (var cell in pair.Value)
            {
                if (!cell.IsMatched) return false;
            }
        }

        objectManager.CompleteCommunicationMission();
        return true;
    }

    private void OnDestroy()
    {
        for (int i = lineParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(lineParent.GetChild(i).gameObject);
        }
    }
}
