using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField]
    private CustomerGenerator cg;

    private Vector3 startPoint = new Vector3(-59, 12.0f, 0);
    private Vector3 waitingPoint = new Vector3(-50, 12.0f, 0);
    private Vector3 endPoint = new Vector3(-39, 12.0f, 0);

    int pos = 0;    // 0: s->w �����̱�, 1: ���, 2: w->e �����̱�

    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private float bounceHeight = 0.2f;
    [SerializeField]
    private float bounceSpeed = 5f;

    private float journeyLength;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        journeyLength = 9;
        pos = 0;
        cg = GameObject.Find("CustomerGenerator").GetComponent<CustomerGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pos == 0)
        {
            MoveTo(startPoint, waitingPoint);
        } else if (pos == 2)
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
            if (pos == 0)
            {
                pos++;
                cg.isCustomerReady = true;
            } else if (pos == 2)
            {
                Debug.Log("x" + to.x + " y" + to.y + " z" + to.z);
                Destroy(this.gameObject);
            }
        }
    }

    public int GetFood(GameObject target)
    {
        int result = target.GetComponent<Interactor_Pot>().Rating();
        startTime = Time.time;
        journeyLength = 9;
        pos++;
        cg.GenerateCustomer();
        cg.isCustomerReady = false;

        return result;
    }
}
