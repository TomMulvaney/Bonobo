using UnityEngine;
using System.Collections;

public class TweenerPerma : MonoBehaviour 
{
    [SerializeField]
    private Tweener m_tweenBehaviour;

    void OnEnable()
    {
        m_tweenBehaviour.CompletedOn += OnTweenOnComplete;
        m_tweenBehaviour.CompletedOff += OnTweenOffComplete;

        if (m_tweenBehaviour.isOn)
        {            
            m_tweenBehaviour.Off();
        } 
        else
        {            
            m_tweenBehaviour.On();
        }
    }

    void OnDisable()
    {
        m_tweenBehaviour.CompletedOn -= OnTweenOnComplete;
        m_tweenBehaviour.CompletedOff -= OnTweenOffComplete;
    }

    void OnTweenOnComplete(Tweener tweenBehaviour)
    {
        m_tweenBehaviour.Off();
    }

    void OnTweenOffComplete(Tweener tweenBehaviour)
    {
        m_tweenBehaviour.On();
    }
}
