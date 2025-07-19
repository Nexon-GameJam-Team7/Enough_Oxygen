// Unity
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] checkObject;
    [SerializeField] private Image[] itemImages;

    [SerializeField] private TextMeshProUGUI tmp;
    private int lastMoney;

    private void Start()
    {
        lastMoney = GameManager.Data.data.money;
    }

    public void Update()
    {
        if (lastMoney != GameManager.Data.data.money)
        {
            lastMoney = GameManager.Data.data.money;
            tmp.text = lastMoney.ToString() + "원";
        }
    }

    public void CheckToDoList(int _idx)
    {
        if (_idx < checkObject.Length)
            checkObject[_idx].gameObject.SetActive(true);
    }

    public void DisplayGetItem(int _idx)
    {
        itemImages[_idx].gameObject.SetActive(true);
    }
}
