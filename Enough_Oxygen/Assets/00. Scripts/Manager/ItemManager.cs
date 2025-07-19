// Unity
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    public void GetHackUSB()
    {
        GameManager.Data.data.haveItem[0] = true;
        uiManager.DisplayGetItem(0);
    }

    public void GetRope()
    {
        GameManager.Data.data.haveItem[1] = true;
        uiManager.DisplayGetItem(1);
    }

    public void GetOxygenRoomKey()
    {
        GameManager.Data.data.haveItem[2] = true;
        uiManager.DisplayGetItem(2);
    }

    public void GetMasterKey()
    {
        GameManager.Data.data.haveItem[3] = true;
        uiManager.DisplayGetItem(3);
    }
}
