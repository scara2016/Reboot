using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public int startScore = 1000;
    private float score;
    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score += value;
        }
    }
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private TMP_Text Meters;
    [SerializeField]
    private TMP_Text TimerText;
    [SerializeField]
    private float startDay;
    [SerializeField]
    private float startMonth;
    [SerializeField]
    private float startYear;
    [SerializeField]
    private float timeSpeed;
    private PlayerMeters playerMeters;
    

    private float wakeUpTime = 6;
    public float WakeUpTime{
        set
        {
            wakeUpTime = value;
        }
        get
        {
            return wakeUpTime;
        }
    }

    float tempTimer = 0;
    private int timer;
    public int Timer
    {
        get
        {
            return timer;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        timer = 0;
        playerMeters = FindObjectOfType<PlayerMeters>();
        SetTimer();
        SetMeters();
        ScoreText.text = "Score : " + (int)startScore;
        score = startScore;
        score += GameManager.Instance.SavedScore;
    }

    private void SetScore()
    {
        score -= Time.deltaTime;
        ScoreText.text = "Score : " + (int)score;
    }

    private void SetMeters()
    {
        Meters.text = "Blue : " + (int)playerMeters.BlueMeter + "\nGreen : " + (int)playerMeters.GreenMeter;
    }

    // Update is called once per frame
    void Update()
    {
        tempTimer += Time.deltaTime*timeSpeed;
        timer = (int)Mathf.Floor(tempTimer);
        if (timer >= 60)
        {
            tempTimer = 0;
            wakeUpTime++;
        }
        if (wakeUpTime >= 24)
        {
            startDay++;
            wakeUpTime = 0;
        }
        if (startDay >= 30)
        {
            startDay = 1;
            startMonth++;
        }
        SetTimer();
        SetMeters();
        SetScore();
    }


    void SetTimer()
    {
        if (wakeUpTime < 10)
            TimerText.text = startDay + "/" + startMonth + "/" + startYear + "    0" + wakeUpTime + ":";
        else
            TimerText.text = startDay + "/" + startMonth + "/" + startYear + "    " + wakeUpTime + ":";
        if (timer < 10)
            TimerText.text += "0" + timer;
        else
            TimerText.text += timer;
    }
}
