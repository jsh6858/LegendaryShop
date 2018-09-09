using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTweenTextColor : TweenParent
{
    private UILabel _TweenTargetForThis;
    public Color _From = Color.white;
    public Color _To = Color.white;
    protected override void Awake()
    {
        _TweenTargetForThis = _TweenTarget.GetComponent<UILabel>();
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
            Color StartColor = _From;
            Color EndColor = _To;
            if (_TweenType == UIGlobal.TweenType.Backward)
            {
                Factor = 1;
                Amount *= -1;
            }
            while (true)
            {
                _TweenTargetForThis.color = Color.Lerp(StartColor, EndColor, _TweenCurve.Evaluate(Factor));
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
                    _TweenTargetForThis.color = Color.Lerp(StartColor, EndColor, _TweenCurve.Evaluate(Factor));
                    Factor += Amount;
                    yield return UIGlobal._Time_For_FrameRate;
                }
            }
        } while (_Loop == true);

        if (onFinished != null)
        {
            onFinished.Execute();
        }
    }
}
