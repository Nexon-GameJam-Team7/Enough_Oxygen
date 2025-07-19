using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector2Int gridPosition;
    public CellType cellType;

    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite block;
    [SerializeField] private Sprite goal;

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
            case CellType.Empty: sr.sprite = baseSprite; break;
            case CellType.Wall: sr.sprite = block; break;
            case CellType.BallStart: sr.sprite = baseSprite; ; break;
            case CellType.Goal: sr.sprite = goal; break;
        }
    }
}