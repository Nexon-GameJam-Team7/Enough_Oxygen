using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeCloth : MonoBehaviour
{
    [SerializeField]
    private Sprite[] morningCutscene = new Sprite[15];
    [SerializeField]
    private Sprite[] eveningCutscene = new Sprite[13];
    [SerializeField]
    private float speed = 0.5f;

    [SerializeField]
    private Image display = null;
    private int sceneCnt = 0;

    public void PlayCutScene(bool isDay)
    {
        display.gameObject.SetActive(true);
        if (isDay)
        {
            sceneCnt = morningCutscene.Length;
            StartCoroutine("MorningScene");
        } else
        {
            sceneCnt = eveningCutscene.Length;
            StartCoroutine("EveningScene");
        }
    }

    IEnumerator MorningScene()
    {
        int curScene = 0;
        while (curScene < sceneCnt)
        {
            yield return new WaitForSeconds(speed);
            display.sprite = morningCutscene[curScene];
            curScene++;
        }
        display.gameObject.SetActive(false);
    }
    IEnumerator EveningScene()
    {
        int curScene = 0;
        while (curScene < sceneCnt)
        {
            yield return new WaitForSeconds(speed);
            display.sprite = eveningCutscene[curScene];
            curScene++;
        }
        display.gameObject.SetActive(false);
    }
}
