using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector2Int gridPosition;
    public CellType cellType;

    public void Init(Vector2Int position, CellType type)
    {
        gridPosition = position;
        cellType = type;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // 셀 타입에 따라 색상 또는 스프라이트 변경
        var sr = GetComponent<SpriteRenderer>();
        switch (cellType)
        {
            case CellType.Empty: sr.color = Color.white; break;
            case CellType.Wall: sr.color = Color.black; break;
            case CellType.BallStart: sr.color = Color.blue; break;
            case CellType.Goal: sr.color = Color.green; break;
        }
    }
}