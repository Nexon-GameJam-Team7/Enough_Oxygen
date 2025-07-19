using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSellerMovement : MonoBehaviour
{
    private Vector3 startPoint = new Vector3(-9, 2.0f, 0);
    private Vector3 waitingPoint = new Vector3(0, 2.0f, 0);
    private Vector3 endPoint = new Vector3(9, 2.0f, 0);

    int pos = 0;    // 0: s->w 움직이기, 1: 대기, 2: w->e 움직이기

    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private float bounceHeight = 0.2f;
    [SerializeField]
    private float bounceSpeed = 5f;

    private float journeyLength;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = 9;
        pos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pos == 0)
        {
            MoveTo(startPoint, waitingPoint);
        }
        else if (pos == 2)
        {
            MoveTo(waitingPoint, endPoint);
        }
    }

    private void MoveTo(Vector3 from, Vector3 to)
    {
        float distCovered = (Time.time - startTime) * speed;
        float frac = Mathf.Clamp01(distCovered / journeyLength);

        // 기본 위치 (X축 선형 이동)
        Vector3 flatPos = Vector3.Lerp(from, to, frac);

        // Y축 반동 추가 (사인 곡선)
        float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        Vector3 finalPos = new Vector3(flatPos.x, flatPos.y + bounce, flatPos.z);

        // 위치 적용
        transform.position = finalPos;

        if (Vector3.Distance(transform.position, to) < 0.01f)
        {
            if (pos == 0)
            {
                pos++;

            }
            else if (pos == 2)
            {
                Debug.Log("x" + to.x + " y" + to.y + " z" + to.z);
                transform.position = startPoint;
            }
        }
    }
}
