using UnityEngine;

[DisallowMultipleComponent]
public class SecurityObject : InteractiveObject
{
    protected override void ObjectEvent()
    {
        Debug.Log("Security Object");
        Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI/MiniGame/Security", "Security Canvas"));
    }
}
