using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeCloth : MonoBehaviour
{
    [SerializeField]
    private bool isTitleScene = false;

    [SerializeField]
    private Sprite[] idleCutscene = new Sprite[15];
    [SerializeField]
    private Sprite[] morningCutscene = new Sprite[15];
    [SerializeField]
    private Sprite[] eveningCutscene = new Sprite[13];
    [SerializeField]
    private float speed = 0.1f;

    [SerializeField]
    private Image display = null;
    private int sceneCnt = 0;

    public void PlayCutScene(int type)
    {
        Debug.Log("play");
        // 0: Idle, 1: Morning, 2: Evening
        display.gameObject.SetActive(true);
        if (type == 0)
        {
            sceneCnt = idleCutscene.Length;
            StartCoroutine("IdleScene");
        }
        else if (type == 1)
        {
            sceneCnt = morningCutscene.Length;
            StartCoroutine("MorningScene");
        } else if (type == 2)
        {
            sceneCnt = eveningCutscene.Length;
            StartCoroutine("EveningScene");
        }
    }
    
    IEnumerator IdleScene()
    {
        int curScene = 0;
        while (curScene < sceneCnt)
        {
            yield return new WaitForSeconds(speed);
            display.sprite = idleCutscene[curScene];
            curScene++;
        }
        display.gameObject.SetActive(false);
        PlayCutScene(0);
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
        if (!isTitleScene)
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
