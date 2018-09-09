using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TweenParent : MonoBehaviour
{
    public float _Duration = 1;
    public UIGlobal.TweenType _TweenType = UIGlobal.TweenType.Forward;
    public AnimationCurve _TweenCurve;
    public bool _PlayOnAwake = false;
    public float _DelayedTime = 0;
    public bool _Loop = false;
    public GameObject _TweenTarget;
    public UIGlobal.CallOption _CallOption;
    public EventDelegate onFinished;

    public bool _IsProgress = false;
    protected WaitForSeconds _DelayTime;

    protected virtual void Awake()
    {
        if (_PlayOnAwake == true)
        {
            TweenStart();
        }
    }

    public void Reset()
    {
        _TweenCurve = new AnimationCurve();
        _TweenCurve.AddKey(0, 0);
        _TweenCurve.AddKey(1, 1);
    }
    
    public void TweenStart()
    {
        _DelayTime = new WaitForSeconds(_DelayedTime);
        if (_CallOption == UIGlobal.CallOption.IgnoreWhileProgress)
        {
            if (_IsProgress == true) return;
        }
        else
        {
            if (_IsProgress == true)
                StopCoroutine("ProcessTween");
        }
        if (_IsProgress == false)
            _IsProgress = true;
        StartCoroutine("ProcessTween");
    }
    
    public void TweenStart(bool Forward = true)
    {
        _TweenType = (Forward == true) ? UIGlobal.TweenType.Forward : UIGlobal.TweenType.Backward;
        TweenStart();
    }

    public virtual void ResetTween() { }

    public abstract IEnumerator ProcessTween();
}
