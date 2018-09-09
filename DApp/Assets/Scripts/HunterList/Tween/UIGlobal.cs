using UnityEngine;

public class UIGlobal {
    public enum TweenType
    {
        Forward=0,Backward=1,Pingpong=2
    }
    public enum CallOption
    {
        ForceRestart=0,IgnoreWhileProgress=1
    }
    public static WaitForSeconds _Time_For_FrameRate = new WaitForSeconds(0.016f);

    public static Vector3 Lerp(Vector3 StartVector,Vector3 EndVector,float Time)
    {
        Vector3 ReturnValue;
        if (Time <= 1.0f)
            ReturnValue = Vector3.Lerp(StartVector, EndVector, Time);
        else
        {
            float TimeOver = Time - 1.0f;
            ReturnValue = EndVector + (Vector3.Lerp(StartVector, EndVector, TimeOver) - StartVector);
        }
        return ReturnValue;
    }

    public static float Lerp(float Start,float End,float Time)
    {
        float ReturnValue;
        ReturnValue = (1 - Time) * Start + Time * End;
        return ReturnValue;
    }

    public static float Clamp(float Value,float Min,float Max)
    {
        float ReturnValue;
        if (Value > Max)
            ReturnValue = Max;
        else if (Value < Min)
            ReturnValue = Min;
        else
            ReturnValue = Value;
        return ReturnValue;
    }

    public static float Clamp01(float Value)
    {
        return Clamp(Value, 0, 1);
    }
}