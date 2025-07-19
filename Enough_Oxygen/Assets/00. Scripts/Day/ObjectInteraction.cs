using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Vector3 initialPos;
    public bool isUsing = false;

    private void Start()
    {
        init();
    }

    public void init()
    {
        initialPos = gameObject.transform.position;
        isUsing = false;
    }

    public virtual void GoBack()
    {
        Debug.Log("���ڸ��� ���ư���");
        gameObject.transform.position = initialPos;
        Color newAlpha = gameObject.GetComponent<SpriteRenderer>().color;
        newAlpha.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = newAlpha;
    }

    // ��ȣ�ۿ� ���� ��ü�� ������
    public void OnDrop(GameObject target)
    {
        gameObject.transform.position = target.transform.position;
    }
}
