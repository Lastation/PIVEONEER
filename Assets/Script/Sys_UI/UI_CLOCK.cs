using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_CLOCK : MonoBehaviour
{
    [SerializeField] Text TEXT_DAY;
    [SerializeField] Text TEXT_TIME;

    public void Update()
    {
        System.DateTime TIME_NowDay     = System.DateTime.Today;
        System.DateTime TIME_NowTime    = System.DateTime.Now;

        if (TIME_NowDay.DayOfWeek.ToString()    != TEXT_DAY.text  )         TEXT_DAY.text = TIME_NowDay.DayOfWeek.ToString();
        if (TIME_NowTime.ToString("t")          != TEXT_TIME.text )         TEXT_TIME.text = TIME_NowTime.ToString("t");
    }
}