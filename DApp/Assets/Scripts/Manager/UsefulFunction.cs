using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class UsefulFunction {

	public static string TimeToString(float fTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(fTime);
        return string.Format("{0:D2}H {1:D2}M {2:D2}S", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
}
