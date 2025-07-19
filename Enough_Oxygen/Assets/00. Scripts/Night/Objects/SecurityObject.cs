using UnityEngine;

[DisallowMultipleComponent]
public class SecurityObject : InteractiveObject
{
    private Canvas cutScene;

    protected override void ObjectEvent()
    {
        if (GameManager.Data.data.haveItem[itemID])
        {
            Debug.Log("Security Object");
            Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI/MiniGame/Security", "Security Canvas"));

            UIBase uiBase = cutScene.GetComponent<UIBase>();
            uiBase.Close();
        }
        else
        {
            GameObject alertObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI", "Alert Canvas"));
            Alert alert = alertObj.GetComponent<Alert>();
            alert.OpenAlert("필요한 아이템이 없어 조작할 수 없습니다");
        }

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (isComplete) return;

        if (!collision.CompareTag("Player")) return;

        cutScene = Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI/Cut Scene", "Security CutScene"));
    }
}
