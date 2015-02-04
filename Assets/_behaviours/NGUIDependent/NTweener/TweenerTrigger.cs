using UnityEngine;
using System.Collections;

public class TweenerTrigger : MonoBehaviour 
{
    [SerializeField]
    private TriggerType m_triggerType;
    [SerializeField]
    private TriggerEvent m_triggerEvent;
    [SerializeField]
    private Tweener m_tweenBehaviour;

    enum TriggerType
    {
        Off,
        On,
        Toggle
    }

    enum TriggerEvent
    {
        Click,
        PressOn,
        PressOff,
        Drag
    }

    bool m_isEnabled = true;
    public bool isEnabled
    {
        get
        {
            return m_isEnabled;
        }
    }

    public void Enable(bool myIsEnabled)
    {
        m_isEnabled = myIsEnabled;
    }

	void OnClick()
    {
        if (m_triggerEvent == TriggerEvent.Click)
        {
            Trigger();
        }
    }

    void OnPress(bool pressed)
    {
        if (m_triggerEvent == TriggerEvent.PressOff || m_triggerEvent == TriggerEvent.PressOn)
        {
            Trigger();
        }
    }

    void OnDrag(Vector2 delta)
    {
        if (m_triggerEvent == TriggerEvent.Drag)
        {
            Trigger();
        }
    }

    void Trigger()
    {
        if (m_isEnabled && m_tweenBehaviour != null)
        {
            if (m_triggerType == TriggerType.Off)
            {
                m_tweenBehaviour.Off();
            } 
            else if (m_triggerType == TriggerType.On)
            {
                m_tweenBehaviour.On();
            } 
            else
            {
                if (m_tweenBehaviour.isOn)
                {
                    m_tweenBehaviour.Off();
                }
                else
                {
                    m_tweenBehaviour.On();
                }
            }
        }
    }
}
