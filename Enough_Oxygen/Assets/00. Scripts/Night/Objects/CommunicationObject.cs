using UnityEngine;

[DisallowMultipleComponent]
public class CommunicationObject : InteractiveObject
{
    protected override void ObjectEvent()
    {
        Debug.Log("Communication Object");
        Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI", "Base Canvas"));
    }
}
