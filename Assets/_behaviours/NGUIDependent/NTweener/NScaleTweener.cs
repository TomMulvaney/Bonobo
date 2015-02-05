using UnityEngine;
using System.Collections;

namespace Bonobo
{
	public class NScaleTweener : Tweener
	{
		[SerializeField]
		private UITweener.Method m_onMethod = UITweener.Method.EaseOut;
		[SerializeField]
		private UITweener.Method m_offMethod = UITweener.Method.Linear;
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
			
			TweenScale tweenBehaviour = m_scaleable.GetComponent<TweenScale>() as TweenScale;
			
			if(tweenBehaviour != null)
			{
				UnityEngine.Object.Destroy(tweenBehaviour);
			}

			tweenBehaviour = m_scaleable.gameObject.AddComponent<TweenScale>() as TweenScale;
			
			tweenBehaviour.from = m_scaleable.transform.localScale;
			tweenBehaviour.to = m_onScale;
			tweenBehaviour.duration = m_onDuration;
			
			tweenBehaviour.method = m_onMethod;
			
			tweenBehaviour.Play ();
			
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
					m_scaleable.transform.localScale = m_offScale;
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
			
			TweenScale tweenBehaviour = m_scaleable.GetComponent<TweenScale>() as TweenScale;
			
			if(tweenBehaviour != null)
			{
				UnityEngine.Object.Destroy(tweenBehaviour);
			}
			
			tweenBehaviour = m_scaleable.gameObject.AddComponent<TweenScale>() as TweenScale;
			
			tweenBehaviour.from = m_scaleable.transform.localScale;
			tweenBehaviour.to = m_offScale;
			tweenBehaviour.duration = m_offDuration;
			
			tweenBehaviour.method = m_offMethod;
			
			tweenBehaviour.Play ();
			
			base.Off();
			
			yield return new WaitForSeconds(m_offDuration);
			
			InvokeCompletedOff();
		}
		
		public Transform GetScaleable()
		{
			return m_scaleable;
		}
	}
}