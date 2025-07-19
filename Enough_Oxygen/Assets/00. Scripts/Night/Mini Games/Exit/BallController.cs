using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public ExitGame exitGame;
    private Vector2Int currentPos;
    [SerializeField] private float moveDuration = 0.1f;

    private bool canMove = true;

    void Start()
    {
        currentPos = exitGame.ballStartPos;
        transform.localPosition = new Vector3(currentPos.x, currentPos.y, 0);
    }

    void Update()
    {
        if (!canMove) return;

        if (Input.GetKeyDown(KeyCode.UpArrow)) Move(Vector2Int.up);
        else if (Input.GetKeyDown(KeyCode.DownArrow)) Move(Vector2Int.down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) Move(Vector2Int.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) Move(Vector2Int.right);
    }

    void Move(Vector2Int dir)
    {
        StartCoroutine(MoveCoroutine(dir));
    }

    IEnumerator MoveCoroutine(Vector2Int dir)
    {
        canMove = false;

        Vector2Int nextPos = currentPos + dir;

        while (exitGame.IsInside(nextPos) && !exitGame.IsBlocked(nextPos))
        {
            Vector3 startWorldPos = new Vector3(currentPos.x, currentPos.y, 0);
            Vector3 endWorldPos = new Vector3(nextPos.x, nextPos.y, 0);

            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / moveDuration);

                transform.localPosition = Vector3.Lerp(startWorldPos, endWorldPos, t);

                yield return null;
            }

            // 위치 고정
            transform.localPosition = endWorldPos;
            currentPos = nextPos;
            nextPos += dir;
        }

        CheckGoal();
    }

    void CheckGoal()
    {
        if (currentPos == exitGame.goalPos)
        {
            exitGame.Close();
        }
        else canMove = true;
    }
}