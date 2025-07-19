using UnityEngine;

[DisallowMultipleComponent]
public class ObjectManager : MonoBehaviour
{
    [SerializeField] private SecurityObject securityObject;
    [SerializeField] private CommunicationObject communicationObject;
    [SerializeField] private ExitObject exitObject;

    [SerializeField] private UIManager uiManager;

    public void CompleteSecurityMission()
    {
        GameManager.Data.data.clearCount++;
        securityObject.MissionComplete();
        uiManager.CheckToDoList(0);
    }

    public void CompleteCommunicationMission()
    {
        GameManager.Data.data.clearCount++;
        communicationObject.MissionComplete();
        uiManager.CheckToDoList(1);
    }

    public void CompleteExitMission()
    {
        GameManager.Data.data.clearCount++;
        exitObject.MissionComplete();
        uiManager.CheckToDoList(2);
    }
}
