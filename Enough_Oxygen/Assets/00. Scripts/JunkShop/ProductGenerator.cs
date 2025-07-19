using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductGenerator : MonoBehaviour
{
    private void Start()
    {
        // 상인 중간까지 걸어오면 실행되게 바꾸기
        showProducts();
    }

    public void showProducts()
    {
        for (int i = 0; i < 4; i++)
        {/*
            if (GameManager.Data.data.haveItem[i])
                transform.GetChild(i).gameObject.SetActive(false);
            else*/
                transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
