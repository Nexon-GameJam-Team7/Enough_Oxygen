// Unity
using System;
using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    private bool interactive;   // 상호 작용 가능 여부
    private bool isInteracting; // 상호 작용 진행 중 여부

    private Action actionEvent;

    private void Update()
    {
        InputHandler();

        if (Input.GetKeyDown(KeyCode.Z)) GameManager.Data.data.haveItem[0] = true;
        else if (Input.GetKeyDown(KeyCode.X)) GameManager.Data.data.haveItem[1] = true;
        else if (Input.GetKeyDown(KeyCode.V)) GameManager.Data.data.haveItem[3] = true;
    }

    /// <summary>
    /// Input Handler
    /// </summary>
    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            Interact();
        }
    }

    private void Interact()
    {
        if (interactive && !isInteracting)
        {
            actionEvent();
            isInteracting = true;
        }
    }

    /// <summary>
    /// Set Interactive
    /// </summary>
    /// <param name="_event"></param>
    public void Interactive(Action _event)
    {
        interactive = true;
         
        // Get Action from interactive object
        actionEvent = _event;
    }

    /// <summary>
    /// Unable to interact
    /// </summary>
    public void UnableToInteract()
    {
        interactive = false;
        actionEvent = null;
    }

    public void UnInteraction()
    {
        isInteracting = false;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }
}
