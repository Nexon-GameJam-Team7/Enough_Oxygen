using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform[] coinPoint;
    [SerializeField] private float minCoolTime;
    [SerializeField] private float maxCoolTime;

    private void OnEnable()
    {
        StopAllCoroutines();

        StartCoroutine(CoinGenCoroutine());
    }

    IEnumerator CoinGenCoroutine()
    {
        while (true)
        {
            int randomIdx = Random.Range(0, coinPoint.Length);
            float randomCool = Random.Range(minCoolTime, maxCoolTime);

            float elapsedTime = 0f;

            while (elapsedTime < randomCool)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            Transform coin = Instantiate(GameManager.Resource.Load<Transform>("Prefabs", "Coin Prefab"));
            coin.SetParent(transform);
            coin.localPosition = coinPoint[randomIdx].position;
        }
    }
}
