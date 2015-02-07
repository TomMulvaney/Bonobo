using UnityEngine;
using System.Collections;

namespace Bonobo
{
	public class TweenerSpriteSwapper : MonoBehaviour 
	{
	    [SerializeField]
	    private Tweener m_tweenBehaviour;
	    [SerializeField]
	    private UISprite m_sprite;
	    [SerializeField]
	    private string m_onSpriteName;
	    [SerializeField]
	    private string m_offSpriteName;

		// Use this for initialization
		void Awake () 
	    {
	        m_tweenBehaviour.CompletedOn += OnCompleteOn;
	        m_tweenBehaviour.CompletedOff += OnCompleteOff;
		}
		
		void OnCompleteOn(Tweener behaviour)
	    {
	        m_sprite.spriteName = m_onSpriteName;
	    }

	    void OnCompleteOff(Tweener behaviour)
	    {
	        m_sprite.spriteName = m_offSpriteName;
	    }
	}
}