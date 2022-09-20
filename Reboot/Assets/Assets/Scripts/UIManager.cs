using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
        SetTimer();
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
