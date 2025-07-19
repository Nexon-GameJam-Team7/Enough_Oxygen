// Unity
using UnityEngine;
using UnityEngine.UI;

using TMPro;

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

    [SerializeField] private TextMeshProUGUI timerTMP;
    [SerializeField] private TextMeshProUGUI dayTMP;

    [SerializeField] private Sprite[] watchSprite;
    [SerializeField] private Image watchImage;

    private bool isStop = false;

    private void Start()
    {
        GameManager.Sound.BGMPlay("bgm1");

        GameObject alertObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI", "Alert Canvas"));
        Alert alert = alertObj.GetComponent<Alert>();
        alert.OpenAlert(dayCount.ToString() + "일차 " + (timeOfDay == 0 ? "낮" : "밤"));

        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(unitTime - timer);
        string formatted = string.Format("{0}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
        timerTMP.text = formatted;

        dayTMP.text = dayCount.ToString() + "일차 " + (timeOfDay == 0 ? "낮" : "밤");
    }

    private void Update()
    {
        if (isStop) return;

        if (timer < unitTime)
        {
            timer += Time.deltaTime;

            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(unitTime - timer);
            string formatted = string.Format("{0}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            timerTMP.text = formatted;
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

        UIBase uiBase = FindObjectOfType<UIBase>();
        if (uiBase != null) uiBase.Close();

        if (timeOfDay == 0)
        {
            timeOfDay = 1;
            GameManager.Sound.BGMPlay("bgm2");
            searchLight.gameObject.SetActive(true);

            watchImage.sprite = watchSprite[2];
        }
        else if (timeOfDay == 1)
        {
            timeOfDay = 0;
            IncreaseDay();
            GameManager.Sound.BGMPlay("bgm1");
            searchLight.gameObject.SetActive(false);

            watchImage.sprite = watchSprite[0];
        }

        timer = 0;  // Init Timer

        GameObject alertObj = Instantiate(GameManager.Resource.Load<GameObject>("Prefabs/UI", "Alert Canvas"));
        Alert alert = alertObj.GetComponent<Alert>();
        alert.OpenAlert(dayCount.ToString() + "일차 " + (timeOfDay == 0 ? "낮" : "밤"));

        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(unitTime - timer);
        string formatted = string.Format("{0}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
        timerTMP.text = formatted;

        dayTMP.text = dayCount.ToString() + "일차 " + (timeOfDay == 0 ? "낮" : "밤");
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
