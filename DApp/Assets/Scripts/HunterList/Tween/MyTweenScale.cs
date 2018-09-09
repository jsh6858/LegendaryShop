using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTweenScale : TweenParent
{
    private Vector3 _LocalScale;
    public Vector3 _From;
    public Vector3 _To;
    protected override void Awake()
    {
        _LocalScale = _TweenTarget.transform.localScale;
        base.Awake();
    }

    public override IEnumerator ProcessTween()
    {
        yield return _DelayTime;
        do
        {
            float Factor = 0;
            float Amount = (_TweenType == UIGlobal.TweenType.Pingpong) ? Time.deltaTime / (_Duration * 0.5f) : Time.deltaTime / _Duration;
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
                LerpResult.x *= _LocalScale.x;
                LerpResult.y *= _LocalScale.y;
                LerpResult.z *= _LocalScale.z;
                _TweenTarget.transform.localScale = LerpResult;
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
                    LerpResult.x *= _LocalScale.x;
                    LerpResult.y *= _LocalScale.y;
                    LerpResult.z *= _LocalScale.z;
                    _TweenTarget.transform.localScale = LerpResult;
                    Factor += Amount;
                    yield return UIGlobal._Time_For_FrameRate;
                }
            }
        } while (_Loop == true);
        _IsProgress = false;

        if (onFinished != null)
        {
            onFinished.Execute();
        }
    }

    public override void ResetTween()
    {
        _TweenTarget.transform.localScale = _LocalScale;
        if (onFinished != null)
        {
            onFinished.Execute();
        }
    }
}
