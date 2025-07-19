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
            Debug.Log("집힘");
            // 들고 있던 재료 제자리로
            if (cookingInfo.holdingObj)
                cookingInfo.holdingObj.GetComponent<IngredientInteraction>().GoBack();

            // 현재 선택 재료 손에 들기
            cookingInfo.holdingObj = this.gameObject;
            isHolding = true;
        }
    }

    public void GoBack()
    {
        Debug.Log("제자리로 돌아가기");
        isHolding = false;
        cookingInfo.holdingObj = null;
        gameObject.transform.position = initialPos;
    }


    // 상호작용 가능 물체에 떨구기
    public void DropObj(GameObject target)
    {
        isHolding = false;
        cookingInfo.holdingObj = null;
        gameObject.transform.position = target.transform.position;
    }
}
