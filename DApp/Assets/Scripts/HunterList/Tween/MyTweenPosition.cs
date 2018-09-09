using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTweenPosition : TweenParent
{
    public enum MoveType
    {
        MoveBySpeed = 0, MoveByTime = 1
    }
    public Vector3 _From;
    public Vector3 _To;
    public MoveType _MoveType = MoveType.MoveBySpeed;
    public float _MoveSpeed = 1;

    public override IEnumerator ProcessTween()
    {
        yield return _DelayTime;
        do
        {
            float Factor = 0;
            float Amount = 0;
            if (_MoveType == MoveType.MoveBySpeed)
            {
                Amount = _MoveSpeed * Time.deltaTime;
            }
            else
            {
                Amount = (_TweenType == UIGlobal.TweenType.Pingpong) ? Time.deltaTime / (_Duration * 0.5f) : Time.deltaTime / _Duration;
            }
            float EndAmount = 1;
            Vector3 StartVector = _From;
            Vector3 EndVector = _To;
            Vector3 LerpResult;
            if (_TweenType == UIGlobal.TweenType.Backward)
            {
                Factor = 1;
                Amount *= -1;
            }
            while (true)
            {
                LerpResult = UIGlobal.Lerp(StartVector, EndVector, _TweenCurve.Evaluate(Factor));
                _TweenTarget.transform.localPosition = LerpResult;
                Factor += Amount;
                if (_TweenType == UIGlobal.TweenType.Backward)
                {
                    if (Factor < 0)
                        break;
                }
                else
                {
                    if (Factor >= EndAmount)
                        break;
                }
                yield return UIGlobal._Time_For_FrameRate;
            }
            if (_TweenType == UIGlobal.TweenType.Pingpong)
            {
                Amount *= -1;
                while (Factor > 0)
                {
                    LerpResult = UIGlobal.Lerp(StartVector, EndVector, _TweenCurve.Evaluate(Factor));
                    _TweenTarget.transform.localPosition = LerpResult;
                    Factor += Amount;
                    yield return UIGlobal._Time_For_FrameRate;
                }
            }
        } while (_Loop == true);
        
        if(onFinished != null)
        {
            onFinished.Execute();
        }
    }
}
