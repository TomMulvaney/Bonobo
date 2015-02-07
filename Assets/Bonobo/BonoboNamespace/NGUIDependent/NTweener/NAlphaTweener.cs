using UnityEngine;
using System.Collections;

namespace Bonobo
{
	public class NAlphaTweener : Tweener 
	{
	    [SerializeField]
	    private UITweener.Method m_onMethod = UITweener.Method.EaseOut;
	    [SerializeField]
	    private UITweener.Method m_offMethod = UITweener.Method.EaseOut;
	    [SerializeField]
	    private float m_onAlpha = 1f;
	    [SerializeField]
	    private float m_offAlpha = 0f;

	    protected override void Start()
	    {
	        base.Start();

	        switch (m_startState)
	        {
	            case StartState.StartOff:
	                m_isOn = false;
	                NGUIHelpers.Alpha(transform, 0, m_offAlpha);
	                TryEnableColliders(false);
	                InvokeCompletedOff();
	                break;
	            case StartState.StartOn:
	                m_isOn = true;
	                NGUIHelpers.Alpha(transform, 0, m_onAlpha);
	                TryEnableColliders(true);
	                InvokeCompletedOn();
	                break;
	            case StartState.TweenOn:
	                m_isOn = false;
	                NGUIHelpers.Alpha(transform, 0, m_offAlpha);
	                On();
	                break;
	            case StartState.TweenOff:
	                m_isOn = true;
	                NGUIHelpers.Alpha(transform, 0, m_onAlpha);
	                Off();
	                break;
	        }
	    }

	    public override void On()
	    {
	        if (!m_isOn) 
	        {
	            StopCoroutine("OffCo");
	            m_isOn = true;

	            if(gameObject.activeInHierarchy)
	            {
	                StartCoroutine("OnCo");
	            }
	        }
	    }
	    
	    IEnumerator OnCo()
	    {
	        yield return new WaitForSeconds (Random.Range(m_onDelay.x, m_onDelay.y));

	        Hashtable ht = new Hashtable();
	        ht.Add("method", m_onMethod);
	        NGUIHelpers.Alpha(transform, m_onDuration, m_onAlpha, ht);

	        base.On();
	        
	        yield return new WaitForSeconds(m_onDuration);
	        
	        InvokeCompletedOn();
	    }
	    
	    public override void Off(bool immediate = false)
	    {
	        if (m_isOn) 
	        {
	            StopCoroutine("OnCo");
	            m_isOn = false;

	            if(immediate)
	            {
	                Hashtable ht = new Hashtable();
	                ht.Add("method", m_offMethod);
	                NGUIHelpers.Alpha(transform, 0, m_offAlpha, ht);

	                base.Off();
	                InvokeCompletedOff();
	            }
	            else if(gameObject.activeInHierarchy)
	            {
	                StartCoroutine("OffCo");
	            }
	        }
	    }
	    
	    IEnumerator OffCo()
	    {
	        yield return new WaitForSeconds (Random.Range(m_offDelay.x, m_offDelay.y));

	        Hashtable ht = new Hashtable();
	        ht.Add("method", m_onMethod);
	        NGUIHelpers.Alpha(transform, m_offDuration, m_offAlpha, ht);

	        base.Off();

	        yield return new WaitForSeconds(m_offDuration);
	        
	        InvokeCompletedOff();
	    }
	}
}