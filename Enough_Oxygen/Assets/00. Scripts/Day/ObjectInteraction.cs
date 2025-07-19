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

    public virtual void init()
    {
        initialPos = gameObject.transform.position;
        isUsing = false;
    }

    public virtual void GoBack()
    {
        Debug.Log("제자리로 돌아가기");
        gameObject.transform.position = initialPos;
        Color newAlpha = gameObject.GetComponent<SpriteRenderer>().color;
        newAlpha.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = newAlpha;
    }

    // 상호작용 가능 물체에 떨구기
    public void OnDrop(GameObject target)
    {
        gameObject.transform.position = target.transform.position;
    }
}
