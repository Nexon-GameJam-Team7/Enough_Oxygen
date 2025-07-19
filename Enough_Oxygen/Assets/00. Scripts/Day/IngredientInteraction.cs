using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInteraction : MonoBehaviour
{
    public GameObject player = null;
    private PlayerCooking cookingInfo = null;
    private Vector3 initialPos;

    private bool isHolding = false;

    private void Start()
    {
        cookingInfo = player.GetComponent<PlayerCooking>();
        initialPos = gameObject.transform.position;
    }

    public void init()
    {
        isHolding = false;
    }

    public void HoldObj()
    {
        if (!isHolding)
        {
            Debug.Log("����");
            // ��� �ִ� ��� ���ڸ���
            if (cookingInfo.holdingObj)
                cookingInfo.holdingObj.GetComponent<IngredientInteraction>().GoBack();

            // ���� ���� ��� �տ� ���
            cookingInfo.holdingObj = this.gameObject;
            isHolding = true;
        }
    }

    public void GoBack()
    {
        Debug.Log("���ڸ��� ���ư���");
        isHolding = false;
        cookingInfo.holdingObj = null;
        gameObject.transform.position = initialPos;
    }


    // ��ȣ�ۿ� ���� ��ü�� ������
    public void DropObj(GameObject target)
    {
        isHolding = false;
        cookingInfo.holdingObj = null;
        gameObject.transform.position = target.transform.position;
    }
}
