using UnityEngine;
using System.Collections;

namespace Bonobo
{
	public class TweenerPerma : MonoBehaviour 
	{
	    Tweener m_tweener;

		void Awake()
		{
			m_tweener = GetComponent<Tweener> () as Tweener;

			if (m_tweener == null)
			{
				Debug.LogError(name + " m_tweener is null");			
			}
		}

	    void OnEnable()
	    {
	        m_tweener.CompletedOn += OnTweenOnComplete;
	        m_tweener.CompletedOff += OnTweenOffComplete;

	        if (m_tweener.isOn)
	        {            
	            m_tweener.Off();
	        } 
	        else
	        {            
	            m_tweener.On();
	        }
	    }

	    void OnDisable()
	    {
	        m_tweener.CompletedOn -= OnTweenOnComplete;
	        m_tweener.CompletedOff -= OnTweenOffComplete;
	    }

	    void OnTweenOnComplete(Tweener tweenBehaviour)
	    {
	        m_tweener.Off();
	    }

	    void OnTweenOffComplete(Tweener tweenBehaviour)
	    {
	        m_tweener.On();
	    }
	}
}