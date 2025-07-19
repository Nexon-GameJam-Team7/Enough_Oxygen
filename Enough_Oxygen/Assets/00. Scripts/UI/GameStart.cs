using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void StartButton()
    {
        Debug.Log("start");
        GameManager.Scene.ConvertScene("Main Scene");
    }
}
