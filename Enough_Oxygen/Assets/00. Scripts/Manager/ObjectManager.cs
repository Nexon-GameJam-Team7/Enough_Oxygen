using UnityEngine;

[DisallowMultipleComponent]
public class ObjectManager : MonoBehaviour
{
    [SerializeField] private SecurityObject securityObject;
    [SerializeField] private CommunicationObject communicationObject;
    [SerializeField] private ExitObject exitObject;

    [SerializeField] private UIManager uiManager;

    private bool isClear = false;

    public void CompleteSecurityMission()
    {
        GameManager.Data.data.clearCount++;
        securityObject.MissionComplete();
        uiManager.CheckToDoList(0);

        if (GameManager.Data.data.clearCount == 3) GameClear();
    }

    public void CompleteCommunicationMission()
    {
        GameManager.Data.data.clearCount++;
        communicationObject.MissionComplete();
        uiManager.CheckToDoList(1);

        if (GameManager.Data.data.clearCount == 3) GameClear();
    }

    public void CompleteExitMission()
    {
        GameManager.Data.data.clearCount++;
        exitObject.MissionComplete();
        uiManager.CheckToDoList(2);

        if (GameManager.Data.data.clearCount == 3) GameClear();
    }

    public void GameClear()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player != null) player.gameObject.SetActive(false);

        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager != null) timeManager.Pause();

        isClear = true;

        Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI", "Game Clear"));
    }

    private void Update()
    {
        if (isClear && Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Scene.ConvertScene("Main Title");
        }
    }
}
