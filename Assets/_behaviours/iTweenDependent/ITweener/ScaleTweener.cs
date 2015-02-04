using UnityEngine;
using System.Collections;

public class ScaleTweener : Tweener
{
    [SerializeField]
    protected iTween.EaseType m_onEaseType = iTween.EaseType.easeOutQuad;
    [SerializeField]
    protected iTween.EaseType m_offEaseType = iTween.EaseType.linear;
    [SerializeField]
    private Vector3 m_onScale = Vector3.one;
    [SerializeField]
    private Vector3 m_offScale = Vector3.zero;

    Transform m_scaleable;

    protected override void Start()
    {
        base.Start();

        m_scaleable = TransformHelpers.CreateSubParent(transform);
        
        switch (m_startState)
        {
            case StartState.StartOff:
                m_isOn = false;
                m_scaleable.localScale = m_offScale;
                TryEnableColliders(false);
                InvokeCompletedOff();
                break;
            case StartState.StartOn:
                m_isOn = true;
                m_scaleable.localScale  = m_onScale;
                TryEnableColliders(true);
                InvokeCompletedOn();
                break;
            case StartState.TweenOn:
                m_isOn = false;
                m_scaleable.localScale = m_offScale;
                On();
                break;
            case StartState.TweenOff:
                m_isOn = true;
                m_scaleable.localScale = m_onScale;
                Off();
                break;
        }
    }
    
    public override void On()
    {
        if (!m_isOn) 
        {
            m_isOn = true;
            StopCoroutine("OffCo");

            if(gameObject.activeInHierarchy)
            {
                StartCoroutine("OnCo");
            }
        }
    }
    
    IEnumerator OnCo()
    {
        yield return new WaitForSeconds (Random.Range(m_onDelay.x, m_onDelay.y));

        Hashtable tweenArgs = new Hashtable();
        tweenArgs.Add("scale", m_onScale);
        tweenArgs.Add("time", m_onDuration);
        tweenArgs.Add("easetype", m_onEaseType);
        iTween.ScaleTo(m_scaleable.gameObject, tweenArgs);

        base.On();
        
        yield return new WaitForSeconds(m_onDuration);
        
        InvokeCompletedOn();
    }
    
    public override void Off(bool immediate = false)
    {
        if (m_isOn) 
        {
            m_isOn = false;
            StopCoroutine("OnCo");
            
            if(immediate)
            {
                m_scaleable.localScale = m_offScale;
                base.Off();
                InvokeCompletedOff();
            }
            else if (gameObject.activeInHierarchy)
            {
                StartCoroutine("OffCo");
            }
        }
    }
    
    IEnumerator OffCo()
    {
        yield return new WaitForSeconds (Random.Range(m_offDelay.x, m_offDelay.y));

        Hashtable tweenArgs = new Hashtable();
        tweenArgs.Add("scale", m_offScale);
        tweenArgs.Add("time", m_offDuration);
        tweenArgs.Add("easetype", m_offEaseType);
        iTween.ScaleTo(m_scaleable.gameObject, tweenArgs);

        base.Off();
        
        yield return new WaitForSeconds(m_offDuration);
        
        InvokeCompletedOff();
    }
}
