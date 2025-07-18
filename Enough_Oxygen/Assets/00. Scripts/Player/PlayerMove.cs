// Unity
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rigid;
    private Player player;
    private Animator animator;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Move
        Move();
    }

    private void Move()
    {
        if (player.IsInteracting()) return;

        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveVector = inputVector.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + moveVector);

        // Temp Animation
        animator.SetFloat("DirX", inputVector.x);
        animator.SetFloat("DirY", inputVector.y);
    }
}
