using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int minPrice;
    [SerializeField] private int maxPrice;

    private int randPrice;

    public void Start()
    {
        randPrice = Random.Range(minPrice, maxPrice);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Data.data.money += randPrice;
            GameManager.Sound.SFXPlay("coin");

            Destroy(gameObject);
        }

    }
}
