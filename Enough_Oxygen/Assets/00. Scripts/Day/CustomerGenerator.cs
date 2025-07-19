using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] CustomerImg = new Sprite[4];
    [SerializeField]
    private GameObject prefab;
    public GameObject curCustomer = null;
    
    public bool isCustomerReady = false;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCustomer();
    }

    public void GenerateCustomer()
    {
        curCustomer = Instantiate(prefab);
        curCustomer.transform.position = new Vector3(-9, 2.5f, 0);
        curCustomer.GetComponent<SpriteRenderer>().sprite = CustomerImg[Random.Range(0, 4)];
        curCustomer.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
}
