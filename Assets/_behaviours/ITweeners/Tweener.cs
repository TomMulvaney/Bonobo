using UnityEngine;
using System.Collections;

public class Tweener : MonoBehaviour 
{
    public delegate void TweenBehaviourEventHandler(Tweener tweenBehaviour);
    public event TweenBehaviourEventHandler TweeningOn;
    public event TweenBehaviourEventHandler CompletedOn;
    public event TweenBehaviourEventHandler TweeningOff;
    public event TweenBehaviourEventHandler CompletedOff;

    [SerializeField]
    private bool m_enableColliders;
    [SerializeField]
    protected StartState m_startState;
    [SerializeField]
    protected Vector2 m_onDelay = new Vector2(0f, 0f);
    [SerializeField]
    protected float m_onDuration = 0.2f;
    [SerializeField]
    protected Vector2 m_offDelay = new Vector2(0f, 0f);
    [SerializeField]
    protected float m_offDuration = 0.2f;

    protected bool m_isOn = true;
    public bool isOn
    {
        get
        {
            return m_isOn;
        }
    }

    public StartState startState
    {
        get
        {
            return m_startState;
        }
    }

    public enum StartState
    {
        StartOff,
        StartOn,
        TweenOff,
        TweenOn
    }

    protected void TryEnableColliders(bool enable)
    {
        if (m_enableColliders)
        {
            TransformHelpers.EnableCollider2DRecursive(transform, enable);
        }
    }

    public virtual void On() 
    {
        TryEnableColliders(true);

        if (TweeningOn != null)
        {
            TweeningOn(this);
        }
    }

    public virtual void Off(bool immediate = false) 
    {
        TryEnableColliders(false);
        
        if (TweeningOff != null)
        {
            TweeningOff(this);
        }
    }

    public void OnOff()
    {
        StartCoroutine(OnOffCo());
    }

    IEnumerator OnOffCo()
    {
        On();

        yield return new WaitForSeconds(GetMaxDurationOn());

        Off();
    }

    protected virtual void Start()
    {
        m_onDuration = Mathf.Max (m_onDuration, 0);
        m_offDuration = Mathf.Max (m_offDuration, 0);
        
        m_onDelay.x = Mathf.Max (m_onDelay.x, 0);
        m_onDelay.x = Mathf.Max (m_onDelay.y, m_onDelay.x);
        m_offDelay.x = Mathf.Max (m_offDelay.x, 0);
        m_offDelay.y = Mathf.Max (m_offDelay.y, m_offDelay.x);
    }

    protected void InvokeCompletedOn()
    {
        if (CompletedOn != null)
        {
            CompletedOn(this);
        }
    }

    protected void InvokeCompletedOff()
    {
        if (CompletedOff != null)
        {
            CompletedOff(this);
        }
    }

    public float GetMaxDurationOn()
    {
        return m_onDelay.y + m_onDuration;
    }
    
    public float GetMaxDurationOff()
    {
        return m_offDelay.y + m_offDuration;
    }

    public float GetMaxDurationOnOff()
    {
        return GetMaxDurationOn() + GetMaxDurationOff();
    }
}