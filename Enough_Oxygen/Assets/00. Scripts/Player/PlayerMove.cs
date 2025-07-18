// Unity
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rigid;
    private Vector2 lastDir;
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

        bool isMoving = inputVector != Vector2.zero;

        animator.SetBool("isWalk", isMoving);

        if (isMoving)
        {
            lastDir = inputVector;
            animator.SetFloat("DirX", inputVector.x);
            animator.SetFloat("DirY", inputVector.y);
        }
        else
        {
            // 움직이지 않을 땐 마지막 방향 유지
            animator.SetFloat("DirX", lastDir.x);
            animator.SetFloat("DirY", lastDir.y);
        }

        rigid.MovePosition(rigid.position + moveVector);
    }
}
