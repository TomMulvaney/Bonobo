using UnityEngine;
using System.Collections;

public class TweenerRotator : MonoBehaviour
{
    [SerializeField]
    private Tweener m_tweenBehaviour;
    [SerializeField]
    private bool m_useLocal;
    [SerializeField]
    private Vector3 m_onRotation;
    [SerializeField]
    private Vector3 m_offRotation;
    [SerializeField]
    private float m_rotateTime = 0.2f;
    
    // Use this for initialization
    void Awake () 
    {
        m_tweenBehaviour.TweeningOn += OnTweenOn;
        m_tweenBehaviour.TweeningOff += OnTweenOff;
    }
    
    void OnTweenOn(Tweener behaviour)
    {
        Hashtable tweenArgs = new Hashtable();
        tweenArgs.Add("rotation", m_onRotation);
        tweenArgs.Add("time", m_rotateTime);
        tweenArgs.Add("islocal", m_useLocal);

        iTween.RotateTo(gameObject, tweenArgs);
    }
    
    void OnTweenOff(Tweener behaviour)
    {
        Hashtable tweenArgs = new Hashtable();
        tweenArgs.Add("rotation", m_offRotation);
        tweenArgs.Add("time", m_rotateTime);
        tweenArgs.Add("islocal", m_useLocal);
        
        iTween.RotateTo(gameObject, tweenArgs);
    }
}
