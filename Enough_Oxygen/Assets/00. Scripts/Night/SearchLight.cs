// Unity
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class SearchLight : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform[] points;
    [SerializeField] private TimeManager timeManager;
    private int currentPointIdx;
    private int direction = 1;

    private bool canMove = true;

    private void OnEnable()
    {
        if (points.Length == 0) return;

        transform.position = points[0].position;
        currentPointIdx = 0;
        direction = 1;

        canMove = true;
    }

    private void Update()
    {
        if (!canMove || points.Length < 2) return;

        Move();
    }

    private void Move()
    {
        Transform target = points[currentPointIdx];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            // 방향 전환 조건
            if (currentPointIdx == points.Length - 1)
                direction = -1;
            else if (currentPointIdx == 0)
                direction = 1;

            currentPointIdx += direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")||!canMove) return;

        // Game Over
        canMove = false;
        GameManager.Sound.SFXPlay("siren");

        timeManager.Pause();

        StartCoroutine(AlertCoroutine());
    }

    private IEnumerator AlertCoroutine()
    {
        if (!timeManager.IsLasyDay())
        {
            Alert alert1 = Instantiate(GameManager.Resource.Load<Alert>("Prefabs/UI", "Alert Canvas"));
            alert1.OpenAlert("감시에 걸려 밤 활동이 금지되었습니다.");

            yield return new WaitForSeconds(4f);

            Alert alert2 = Instantiate(GameManager.Resource.Load<Alert>("Prefabs/UI", "Alert Canvas"));
            alert2.OpenAlert("다음 일차로 넘어갑니다.");

            yield return new WaitForSeconds(4f);
        }

        SwapTime();
    }

    private void SwapTime()
    {
        GameObject sfxs = GameObject.Find("SFXPlayer");
        Destroy(sfxs);

        timeManager.Resume();
        timeManager.SwapTime();
    }
}
