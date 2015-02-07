using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Bonobo
{
	public class IPositionTweener : Tweener 
	{
	    [SerializeField]
	    protected iTween.EaseType m_onEaseType = iTween.EaseType.easeOutQuad;
	    [SerializeField]
	    protected iTween.EaseType m_offEaseType = iTween.EaseType.linear;
		[SerializeField]
		private Transform m_offLocation;

		GameObject m_moveable;
		Transform m_onLocation;
		
		protected override void Start()
		{
	        base.Start();

	        m_moveable = TransformHelpers.CreateSubParent(transform, new Transform[] { m_offLocation } ).gameObject;
			
			m_onLocation = new GameObject ("OnLocation").transform;
	        m_onLocation.parent = transform;
	        m_onLocation.localPosition = Vector3.zero;
	        m_onLocation.localScale = Vector3.one;
	        m_onLocation.localRotation = Quaternion.identity;

	        switch (m_startState)
	        {
	            case StartState.StartOff:
	                m_isOn = false;
	                m_moveable.transform.position = m_offLocation.position;
	                TryEnableColliders(false);
	                InvokeCompletedOff();
	                break;
	            case StartState.StartOn:
	                m_isOn = true;
	                TryEnableColliders(true);
	                InvokeCompletedOn();
	                break;
	            case StartState.TweenOn:
	                m_isOn = false;
	                m_moveable.transform.position = m_offLocation.position;
	                On();
	                break;
	            case StartState.TweenOff:
	                m_isOn = true;
	                Off();
	                break;
	        }
		}

		public override void On()
		{
			if (!m_isOn && gameObject.activeInHierarchy) 
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

			iTween.Stop(m_moveable);
			
			Hashtable tweenArgs = new Hashtable();
			tweenArgs.Add("position", m_onLocation);
			tweenArgs.Add("time", m_onDuration);
			tweenArgs.Add("easetype", m_onEaseType);

			iTween.MoveTo (m_moveable, tweenArgs);

	        base.On();

	        yield return new WaitForSeconds(m_onDuration);

	        InvokeCompletedOn();
		}

	    public override void Off(bool immediate = false)
		{
			if (m_isOn && gameObject.activeInHierarchy) 
			{
				m_isOn = false;
				StopCoroutine("OnCo");

	            if(immediate)
	            {
	                m_moveable.transform.position = m_offLocation.position;
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

			iTween.Stop(m_moveable);
			
			Hashtable tweenArgs = new Hashtable();
			tweenArgs.Add("position", m_offLocation);
			tweenArgs.Add("time", m_offDuration);
			tweenArgs.Add("easetype", m_offEaseType);
			
			iTween.MoveTo (m_moveable, tweenArgs);

	        base.Off();

	        yield return new WaitForSeconds(m_offDuration);
	        
	        InvokeCompletedOff();
		}

	    public GameObject GetMoveable()
	    {
	        return m_moveable;
	    }
	}
}
