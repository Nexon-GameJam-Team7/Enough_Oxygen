// Unity
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Collections;

[DisallowMultipleComponent]
public class TimeManager : MonoBehaviour
{
    [SerializeField] private float unitTime = 150f; // 2분 30초
    [SerializeField] private float timer = 0f;

    [SerializeField] private int dayCount = 1;
    [SerializeField] private int maxDayCount = 3;

    [Range(0, 2), Header("낮: 0 | 오후: 1 | 밤: 2")]
    [SerializeField] private int timeOfDay;

    [SerializeField] private SearchLight[] searchLights;
    [SerializeField] private Player player;
    [SerializeField] private JunkSellerMovement seller;

    [SerializeField] private CoinGenerator coinGenerator;

    [SerializeField] private TextMeshProUGUI timerTMP;
    [SerializeField] private TextMeshProUGUI dayTMP;

    [SerializeField] private Sprite[] watchSprite;
    [SerializeField] private Image watchImage;

    [SerializeField] private Canvas watchCanvas;

    [SerializeField] private Camera dayCamera;
    [SerializeField] private Camera nightCamera;
    [SerializeField] private Camera twailCamera;

    [SerializeField] private GameObject dayEnvPrefab;
    [SerializeField] private GameObject dayEnv;

    private bool isStop = false;

    private void Start()
    {
        GameManager.Sound.BGMPlay("bgm1");

        StartCoroutine(DayAlertCoroutine());

        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(unitTime - timer);
        string formatted = string.Format("{0}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
        timerTMP.text = formatted;

        dayTMP.text = dayCount.ToString() + "일차 " + (timeOfDay == 0 ? "오전" : "오후");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timeOfDay == 1) SwapTime();

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
        if (dayCount > maxDayCount) return;

        if (timeOfDay == 0)
        {
            timeOfDay = 1;
            GameManager.Sound.BGMPlay("bgm2");

            watchImage.sprite = watchSprite[1];

            dayCamera.gameObject.SetActive(false);
            twailCamera.gameObject.SetActive(true);
            nightCamera.gameObject.SetActive(false);

            watchCanvas.worldCamera = twailCamera;

            Alert alert = Instantiate(GameManager.Resource.Load<Alert>("Prefabs/UI", "Alert Canvas"));
            alert.OpenAlert("오후가 되어 고물상이 찾아옵니다.");

            seller.Init();
            Destroy(dayEnv);
            dayEnv = null;

            Pause();
        }
        else if (timeOfDay == 1)
        {
            timeOfDay = 2;

            for (int i = 0; i < searchLights.Length; i++)
            {
                searchLights[i].gameObject.SetActive(true);
            }

            watchImage.sprite = watchSprite[2];

            dayCamera.gameObject.SetActive(false);
            twailCamera.gameObject.SetActive(false);
            nightCamera.gameObject.SetActive(true);

            watchCanvas.worldCamera = nightCamera;

            Alert alert = Instantiate(GameManager.Resource.Load<Alert>("Prefabs/UI", "Alert Canvas"));
            alert.OpenAlert("밤이 되었습니다.");

            player.gameObject.SetActive(true);
            coinGenerator.gameObject.SetActive(true);

            Resume();
        }
        else if (timeOfDay == 2)
        {
            UIBase uiBase = FindObjectOfType<UIBase>();
            if (uiBase != null) uiBase.Close();

            timeOfDay = 0;

            bool gameset = IncreaseDay();
            if (gameset)
            {
                StartCoroutine(GameOverCoroutine());
                return;
            }

            GameManager.Sound.BGMPlay("bgm1");

            for (int i = 0; i < searchLights.Length; i++)
            {
                searchLights[i].gameObject.SetActive(false);
            }

            watchImage.sprite = watchSprite[0];

            dayCamera.gameObject.SetActive(true);
            twailCamera.gameObject.SetActive(false);
            nightCamera.gameObject.SetActive(false);

            watchCanvas.worldCamera = dayCamera;

            player.Init();
            player.gameObject.SetActive(false);
            coinGenerator.gameObject.SetActive(false);

            dayEnv = Instantiate(dayEnvPrefab);

            StartCoroutine(DayAlertCoroutine());
        }

        timer = 0;  // Init Timer

        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(unitTime - timer);
        string formatted = string.Format("{0}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
        timerTMP.text = formatted;

        dayTMP.text = dayCount.ToString() + "일차 " + (timeOfDay == 0 ? "오전" : "오후");
    }

    private bool IncreaseDay()
    {
        dayCount++;

        if (dayCount > maxDayCount)
        {
            // Game Set
            Debug.Log("Game Set Time Over");
            isStop = true;   // Stop Timer

            return true;
        }

        return false;
    }

    public bool IsLasyDay()
    {
        return (dayCount == maxDayCount);
    }

    IEnumerator GameOverCoroutine()
    {
        Instantiate(GameManager.Resource.Load<Canvas>("Prefabs/UI", "Game Over"));

        Alert alert = Instantiate(GameManager.Resource.Load<Alert>("Prefabs/UI", "Alert Canvas"));
        alert.OpenAlert("탈출에 실패했습니다");

        yield return new WaitForSeconds(5f);

        GameManager.Scene.ConvertScene("Main Title");
    }

    IEnumerator DayAlertCoroutine()
    {
        Alert alert1 = Instantiate(GameManager.Resource.Load<Alert>("Prefabs/UI", "Alert Canvas"));

        if (4-dayCount == 1) alert1.OpenAlert("하루 남았습니다.");
        else alert1.OpenAlert((4 - dayCount) + "일 남았습니다.");

        yield return new WaitForSeconds(3f);

        Alert alert2 = Instantiate(GameManager.Resource.Load<Alert>("Prefabs/UI", "Alert Canvas"));
        alert2.OpenAlert("낮이 되어 식당을 오픈했습니다.");
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
