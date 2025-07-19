using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private PlayerCooking playerCooking;
    [SerializeField] private Interactor_CuttingBoard cuttingBoard;
    [SerializeField] private ObjectInteraction[] objectInteractions;

    private void Start()
    {
        DayInit();
    }

    public void DayInit()
    {
        playerCooking.Init();
        cuttingBoard.myFish = null;
        
        for (int i = 0; i < objectInteractions.Length; i++)
        {
            objectInteractions[i].GoBack();
        }
    }
}
