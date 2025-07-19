using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPrice : MonoBehaviour
{
    [SerializeField]
    private int myNum = -1;

    public void MouseEnter()
    {
        Debug.Log("enter");
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void MouseExit()
    {
        Debug.Log("exit");
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void MouseClick()
    {
        GameManager.Data.data.haveItem[myNum] = true;
        gameObject.SetActive(false);
    }
}
