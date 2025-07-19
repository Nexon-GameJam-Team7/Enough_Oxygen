using UnityEngine;

[DisallowMultipleComponent]
public class ExitObject : InteractiveObject
{
    protected override void ObjectEvent()
    {
        if (GameManager.Data.data.haveItem[itemID])
        {
            Debug.Log("Exit Object");
            Player player = FindObjectOfType<Player>();
            Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI/MiniGame/Exit", "Exit Game"), new Vector3(player.transform.position.x - 3, player.transform.position.y - 3, 0), Quaternion.identity);
        }
        else
        {
            GameObject alertObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI", "Alert Canvas"));
            Alert alert = alertObj.GetComponent<Alert>();
            alert.OpenAlert("필요한 아이템이 없어 조작할 수 없습니다");
        }
    }
}
