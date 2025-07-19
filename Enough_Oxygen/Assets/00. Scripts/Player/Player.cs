// Unity
using System;
using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    private bool interactive;   // 상호 작용 가능 여부
    private bool isInteracting; // 상호 작용 진행 중 여부

    private Action actionEvent;

    private Vector3 startPos;

    [SerializeField] private ItemManager itemManager;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Init()
    {
        transform.position = startPos;

        interactive = false;
        isInteracting = false;
    }

    private void Update()
    {
        InputHandler();
    }

    /// <summary>
    /// Input Handler
    /// </summary>
    private void InputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Interact();
            GameManager.Sound.SFXPlay("click");
        }
    }

    private void Interact()
    {
        if (interactive && !isInteracting)
        {
            isInteracting = true;
            actionEvent();
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
