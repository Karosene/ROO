using System.Collections;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private bool _stopwatchActive;
    private float _currTime;
    [SerializeField] private TMP_Text _text;

    private static TimerScript instance; 

    void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        _currTime = 0;
        StartTimer();
    }

    void Update()
    {
        if (_stopwatchActive)
        {
            _currTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(_currTime / 60);
        int seconds = Mathf.FloorToInt(_currTime % 60);
        int milliseconds = Mathf.FloorToInt((_currTime * 100) % 100);

        _text.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public void StartTimer()
    {
        _stopwatchActive = true;
    }

    public static void StopTimer()
    {
        if (instance != null)
        {
            instance._stopwatchActive = false;
        }
        else
        {
            Debug.LogError("TimerScript instance not found!");
        }
    }   

    public void ResetTimer()
    {
        _currTime = 0;
        UpdateTimerUI();
    }
}
