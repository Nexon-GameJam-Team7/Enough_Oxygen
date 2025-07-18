// Unity
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

    private bool isStop = false;

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

    private void SwapTime()
    {
        if (timeOfDay == 0) timeOfDay = 1;
        else if (timeOfDay == 1)
        {
            timeOfDay = 0;
            IncreaseDay();
        }

        timer = 0;  // Init Timer
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
}
