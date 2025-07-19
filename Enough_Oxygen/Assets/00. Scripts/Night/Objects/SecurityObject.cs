using UnityEngine;

[DisallowMultipleComponent]
public class SecurityObject : InteractiveObject
{
    protected override void ObjectEvent()
    {
        if (GameManager.Data.data.haveItem[itemID])
        {
            Debug.Log("Security Object");
            Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI/MiniGame/Security", "Security Canvas"));
        }
        else
        {
            GameObject alertObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI", "Alert Canvas"));
            Alert alert = alertObj.GetComponent<Alert>();
            alert.OpenAlert("필요한 아이템이 없어 조작할 수 없습니다");
        }

    }
}
