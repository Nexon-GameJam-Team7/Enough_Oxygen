using UnityEngine;

[DisallowMultipleComponent]
public class ObjectManager : MonoBehaviour
{
    [SerializeField] private SecurityObject securityObject;
    [SerializeField] private CommunicationObject communicationObject;
    [SerializeField] private ExitObject exitObject;

    public void CompleteSecurityMission()
    {
        GameManager.Data.data.clearCount++;
        securityObject.MissionComplete();
    }

    public void CompleteCommunicationMission()
    {
        GameManager.Data.data.clearCount++;
        communicationObject.MissionComplete();
    }

    public void CompleteExitMission()
    {
        GameManager.Data.data.clearCount++;
        exitObject.MissionComplete();
    }
}
