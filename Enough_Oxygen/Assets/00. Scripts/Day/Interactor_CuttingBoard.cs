using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor_CuttingBoard: MonoBehaviour
{
    public GameObject myFish = null;
    private GameObject knife = null;

    private void Start()
    {
        knife = transform.GetChild(0).gameObject;
    }

    public void Chopping()
    {
        // Į �ִϸ��̼� ��� �� Į �����
        knife.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Fish" && !myFish)
        {
            knife.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Fish" && !myFish)
        {
            knife.SetActive(false);
        }
    }
}
