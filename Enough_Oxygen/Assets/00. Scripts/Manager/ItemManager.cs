// Unity
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int[] itemPrice = { 10000, 20000, 30000, 50000 };

    [SerializeField] private Button[] buttons;

    private void Start()
    {
        buttons[0].onClick.AddListener(GetHackUSB);
        buttons[1].onClick.AddListener(GetRope);
        buttons[2].onClick.AddListener(GetOxygenRoomKey);
        buttons[3].onClick.AddListener(GetMasterKey);
    }

    public void GetHackUSB()
    {
        if (GameManager.Data.data.money > itemPrice[0])
        {
            GameManager.Data.data.haveItem[0] = true;
            uiManager.DisplayGetItem(0);
            buttons[0].gameObject.SetActive(false);
        }
        Debug.Log("돈 부족");
    }

    public void GetRope()
    {
        if (GameManager.Data.data.money > itemPrice[1])
        {
            GameManager.Data.data.haveItem[1] = true;
            uiManager.DisplayGetItem(1);
            buttons[1].gameObject.SetActive(false);
        }
        Debug.Log("돈 부족");
    }

    public void GetOxygenRoomKey()
    {
        if (GameManager.Data.data.money > itemPrice[2])
        {
            GameManager.Data.data.haveItem[2] = true;
            uiManager.DisplayGetItem(2);
            buttons[2].gameObject.SetActive(false);
        }
        Debug.Log("돈 부족");
    }

    public void GetMasterKey()
    {
        if (GameManager.Data.data.money > itemPrice[3])
        {
            GameManager.Data.data.haveItem[3] = true;
            uiManager.DisplayGetItem(3);
            buttons[3].gameObject.SetActive(false);
        }
        Debug.Log("돈 부족");
    }
}
