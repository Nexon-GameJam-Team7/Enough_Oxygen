// Unity
using UnityEngine;

[DisallowMultipleComponent]
public class InteractiveObject : MonoBehaviour
{
    private bool interactve = false;

    protected virtual void ObjectEvent()
    {
        // @@ Overriding @@
        Debug.Log("Base Object");
        Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI", "Base Canvas"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();
        player.Interactive(ObjectEvent);

        Debug.Log("Interactive");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();
        player.UnableToInteract();

        Debug.Log("Unable to interact");
    }
}
