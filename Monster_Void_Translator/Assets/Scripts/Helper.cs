using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static string ConvertToMinuteSecond(int seconds)
    {
        string minu_sec = "";
        int minutes = seconds / 60;
        seconds = seconds - minutes * 60;
        if (minutes < 10) minu_sec = "0" + minutes;
        else minu_sec = minutes + "";

        minu_sec += ": ";

        if (seconds < 10) minu_sec += ("0" + seconds);
        else minu_sec += (seconds + "");
        return minu_sec;
    }

    public static string GetRecordDay()
    {
        DateTime now = DateTime.Now;
        return now.Day + "/" + now.Month + "/" + now.Year;
    }
}
