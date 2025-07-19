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

    protected void init()
    {
        initialPos = gameObject.transform.position;
    }

    public virtual void GoBack()
    {
        Debug.Log("���ڸ��� ���ư���");
        gameObject.transform.position = initialPos;
    }

    // ��ȣ�ۿ� ���� ��ü�� ������
    public void OnDrop(GameObject target)
    {
        gameObject.transform.position = target.transform.position;
    }
}
