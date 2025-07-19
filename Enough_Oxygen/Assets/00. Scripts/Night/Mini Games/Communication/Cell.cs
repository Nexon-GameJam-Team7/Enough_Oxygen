using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public string Symbol { get; private set; }
    public bool HasSymbol => !string.IsNullOrEmpty(Symbol);
    public bool IsObstacle { get; private set; }
    public bool IsBlockedByLine { get; set; } = false;
    public bool IsMatched { get; set; } = false;

    private TextMeshProUGUI text;
    private Image image;
    private CommnunicationGame game;

    public void Init(int x, int y, CommnunicationGame game)
    {
        X = x;
        Y = y;
        this.game = game;

        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponent<Image>();
    }

    public void SetSymbol(string symbol)
    {
        Symbol = symbol;
        text.text = symbol;
    }

    public void SetObstacle()
    {
        IsObstacle = true;
        image.color = Color.black;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        game.StartDrag(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        game.DragOver(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        game.EndDrag(this);
    }
}
