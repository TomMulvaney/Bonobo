using UnityEngine;
using System.Collections;

namespace Bonobo
{
	public class NPositionTweener : Tweener
	{
		[SerializeField]
		private UITweener.Method m_onMethod = UITweener.Method.EaseOut;
		[SerializeField]
		private UITweener.Method m_offMethod = UITweener.Method.Linear;
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

			TweenPosition tweenBehaviour = m_moveable.GetComponent<TweenPosition>() as TweenPosition;
			
			if(tweenBehaviour != null)
			{
				UnityEngine.Object.Destroy(tweenBehaviour);
			}
			
			tweenBehaviour = m_moveable.gameObject.AddComponent<TweenPosition>() as TweenPosition;
			
			tweenBehaviour.from = m_moveable.transform.localPosition;
			tweenBehaviour.to = m_onLocation.localPosition;
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
					m_moveable.transform.localPosition = m_offLocation.localPosition;
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
			
			TweenPosition tweenBehaviour = m_moveable.GetComponent<TweenPosition>() as TweenPosition;
			
			if(tweenBehaviour != null)
			{
				UnityEngine.Object.Destroy(tweenBehaviour);
			}
			
			tweenBehaviour = m_moveable.gameObject.AddComponent<TweenPosition>() as TweenPosition;
			
			tweenBehaviour.from = m_moveable.transform.localPosition;
			tweenBehaviour.to = m_offLocation.localPosition;
			tweenBehaviour.duration = m_offDuration;
			
			tweenBehaviour.method = m_offMethod;
			
			tweenBehaviour.Play ();
			
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