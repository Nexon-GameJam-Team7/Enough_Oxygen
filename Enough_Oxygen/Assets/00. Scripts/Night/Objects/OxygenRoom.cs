using UnityEngine;

public class OxygenRoom : MonoBehaviour
{
    [SerializeField] private GameObject openedJail;
    [SerializeField] private GameObject closedJail;

    private BoxCollider2D colider;

    private void Start()
    {
        openedJail.SetActive(false);
        closedJail.SetActive(true);

        colider = GetComponent<BoxCollider2D>();
    }

    private void OpenDoor()
    {
        openedJail.SetActive(true);
        closedJail.SetActive(false);

        Player player = FindObjectOfType<Player>();
        player.UnableToInteract();
        player.UnInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (!GameManager.Data.data.haveItem[2]) return;

        OpenDoor();
        GameManager.Sound.SFXPlay("automaticdoor");

        colider.enabled = false;
    }

}
