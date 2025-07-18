// Unity
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.UnInteraction();
            Destroy(gameObject);
        }
    }
}
