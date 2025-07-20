using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStart : MonoBehaviour
{
    [SerializeField]
    private ChangeCloth CutScenePlayer;

    // Start is called before the first frame update
    void Start()
    {
        CutScenePlayer.PlayCutScene(0);
    }
}
