// Unity
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class TimeManager : MonoBehaviour
{
    [SerializeField] private float unitTime = 150f; // 2분 30초
    [SerializeField] private float timer = 0f;

    [SerializeField] private int dayCount = 1;
    [SerializeField] private int maxDayCount = 3;

    [Range(0, 1), Header("낮: 0 | 밤: 1")]
    [SerializeField] private int timeOfDay;

    [SerializeField] private SearchLight searchLight;
    [SerializeField] private Player player;

    private bool isStop = false;

    private void Start()
    {
        GameManager.Sound.BGMPlay("bgm1");

        GameObject alertObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI", "Alert Canvas"));
        Alert alert = alertObj.GetComponent<Alert>();
        alert.OpenAlert(dayCount.ToString() + "일차 " + (timeOfDay == 0 ? "낮" : "밤"));
    }

    private void Update()
    {
        if (isStop) return;

        if (timer < unitTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SwapTime();
        }
    }
    
    public int GetTimeOfDay()
    {
        return timeOfDay;
    }

    public void SwapTime()
    {
        player.Init();

        if (timeOfDay == 0)
        {
            timeOfDay = 1;
            GameManager.Sound.BGMPlay("bgm2");
            searchLight.gameObject.SetActive(true);
        }
        else if (timeOfDay == 1)
        {
            timeOfDay = 0;
            IncreaseDay();
            GameManager.Sound.BGMPlay("bgm1");
            searchLight.gameObject.SetActive(false);
        }

        timer = 0;  // Init Timer

        GameObject alertObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI", "Alert Canvas"));
        Alert alert = alertObj.GetComponent<Alert>();
        alert.OpenAlert(dayCount.ToString() + "일차 " + (timeOfDay == 0 ? "낮" : "밤"));
    }

    private void IncreaseDay()
    {
        dayCount++;

        if (dayCount > maxDayCount)
        {
            // Game Set
            Debug.Log("Game Set Time Over");
            isStop = true;   // Stop Timer
        }
    }

    public void Pause()
    {
        isStop = true;
    }

    public void Resume()
    {
        isStop = false;
    }
}
