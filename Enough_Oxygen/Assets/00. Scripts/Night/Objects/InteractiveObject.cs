// Unity
using UnityEngine;

[DisallowMultipleComponent]
public class InteractiveObject : MonoBehaviour
{
    [SerializeField] protected int itemID;
    private bool isComplete = false;

    protected virtual void ObjectEvent()
    {
        // @@ Overriding @@
        Debug.Log("Base Object");
        //Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI", "Base Canvas"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isComplete) return;

        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();
        player.Interactive(ObjectEvent);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isComplete) return;

        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();
        player.UnableToInteract();
    }

    public void MissionComplete()
    {
        isComplete = true;

        Player player = FindObjectOfType<Player>();
        player.UnableToInteract();
    }
}
