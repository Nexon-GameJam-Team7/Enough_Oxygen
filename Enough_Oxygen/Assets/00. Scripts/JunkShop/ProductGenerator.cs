using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductGenerator : MonoBehaviour
{
    private void Start()
    {
        // ���� �߰����� �ɾ���� ����ǰ� �ٲٱ�
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
