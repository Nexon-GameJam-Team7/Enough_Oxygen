using UnityEngine;

[DisallowMultipleComponent]
public class RestaurantObject : InteractiveObject
{
    protected override void ObjectEvent()
    {
        Debug.Log("Restaurant Object");
        Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI", "Base Canvas"));
    }
}
