// Unity
using UnityEngine;

[DisallowMultipleComponent]
public class SearchLight : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;

    private Vector3 startPos;
    private bool canMove = true;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!canMove) return;

        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = startPos + new Vector3(0, offset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")||!canMove) return;

        // Game Over
        canMove = false;
        GameManager.Sound.SFXPlay("siren");
    }
}
