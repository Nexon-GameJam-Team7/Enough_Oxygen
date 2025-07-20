using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSellerMovement : MonoBehaviour
{
    private Vector3 startPoint = new Vector3(-9.6f, 56.4f, 0);
    private Vector3 waitingPoint = new Vector3(0, 56.4f, 0);
    private Vector3 endPoint = new Vector3(9.6f, 56.4f, 0);

    int pos = 0;    // 0: s->w �����̱�, 1: ���, 2: w->e �����̱�

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float bounceHeight = 0.2f;
    [SerializeField]
    private float bounceSpeed = 5f;

    private float journeyLength;
    private float startTime;

    [SerializeField] private GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = 9;
        pos = 0;
    }

    public void Init()
    {
        Debug.Log("초기화");

        transform.position = startPoint;

        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }

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

        // �⺻ ��ġ (X�� ���� �̵�)
        Vector3 flatPos = Vector3.Lerp(from, to, frac);

        // Y�� �ݵ� �߰� (���� �)
        float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        Vector3 finalPos = new Vector3(flatPos.x, flatPos.y + bounce, flatPos.z);

        // ��ġ ����
        transform.position = finalPos;

        if (Vector3.Distance(transform.position, to) < 0.01f)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].SetActive(true);
            }


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
