// Unity
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]

public class UpdateGridSize : MonoBehaviour
{
    [SerializeField] private int cellSize;
    [SerializeField] private int space;

    public int ComputeOffset()
    {
        int childCount = transform.childCount;

        if (childCount == 0) return 0;

        int totalWidth = (cellSize * childCount) + (space * (childCount - 1));
        int centerOffset = totalWidth / 2;

        return centerOffset;
    }

    public void UpdateOffset(int _offset)
    {
        RectTransform rt = GetComponent<RectTransform>();

        rt.anchoredPosition = new Vector2(-_offset + (cellSize / 2f), rt.anchoredPosition.y);
    }
}
