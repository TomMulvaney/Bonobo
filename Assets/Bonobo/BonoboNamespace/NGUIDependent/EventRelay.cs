using UnityEngine;
using System.Collections;
using System.Linq;

namespace Bonobo
{
	public class EventRelay : MonoBehaviour 
	{
		public delegate void SimpleRelayEventHandler(EventRelay relay);
		private event SimpleRelayEventHandler InternalClicked;
		public event SimpleRelayEventHandler Clicked
		{
			add
			{
				if(InternalClicked == null || !InternalClicked.GetInvocationList().Contains(value))
				{
					InternalClicked += value;
				}
			}
			remove
			{
				InternalClicked -= value;
			}
		}
		
		public delegate void BoolRelayEventHandler(EventRelay relay, bool pressed);
		
		private event BoolRelayEventHandler InternalPressed;
		public event BoolRelayEventHandler Pressed
		{
			add
			{
				if(InternalPressed == null || !InternalPressed.GetInvocationList().Contains(value))
				{
					InternalPressed += value;
				}
			}
			remove
			{
				InternalPressed -= value;
			}
		}
		
		private event BoolRelayEventHandler InternalHover;
		public event BoolRelayEventHandler Hover
		{
			add
			{
				if(InternalHover == null || !InternalHover.GetInvocationList().Contains(value))
				{
					InternalHover += value;
				}
			}
			remove
			{
				InternalHover -= value;
			}
		}
		
		public delegate void MouseOrTouchRelayEventHandler(EventRelay relay, UICamera.MouseOrTouch mot);
		private event MouseOrTouchRelayEventHandler InternalSwiped;
		public event MouseOrTouchRelayEventHandler Swiped
		{
			add
			{
				if(InternalSwiped == null || !InternalSwiped.GetInvocationList().Contains(value))
				{
					InternalSwiped += value;
				}
			}
			remove
			{
				InternalSwiped -= value;
			}
		}
		
		private event MouseOrTouchRelayEventHandler InternalSwipedHorizontal;
		public event MouseOrTouchRelayEventHandler SwipedHorizontal
		{
			add
			{
				if(InternalSwipedHorizontal == null || !InternalSwipedHorizontal.GetInvocationList().Contains(value))
				{
					InternalSwipedHorizontal += value;
				}
			}
			remove
			{
				InternalSwipedHorizontal -= value;
			}
		}
		
		
		[SerializeField]
		private float m_swipeDistanceThreshold = 20;
		
		bool m_hasSwiped = false;
		bool m_hasSwipedHorizontal = false;
		bool m_hasDragged = false;
		
		bool m_isPressed = false;
		public bool isPressed
		{
			get
			{
				return m_isPressed;
			}
		}
		
		void OnPress(bool pressed)
		{
			m_isPressed = pressed;
			
			if (!pressed) 
			{
				m_hasDragged = false;
				m_hasSwiped = false;
				m_hasSwipedHorizontal = false;
			}
			
			if (InternalPressed != null) 
			{
				InternalPressed(this, pressed);
			}
		}
		
		void OnDrag(Vector2 delta)
		{
			m_hasDragged = true;
			
			if (InternalSwipedHorizontal != null && Mathf.Abs(UICamera.currentTouch.totalDelta.x) > m_swipeDistanceThreshold && !m_hasSwiped) 
			{
				InternalSwipedHorizontal(this, UICamera.currentTouch);
			}
			
			if (UICamera.currentTouch.totalDelta.magnitude > m_swipeDistanceThreshold) 
			{
				m_hasSwiped = true;
				
				if(InternalSwiped != null && !m_hasSwiped)
				{
					InternalSwiped(this, UICamera.currentTouch);
				}
			}
		}
		
		void OnClick()
		{
			if(InternalClicked != null && (InternalSwiped == null || !m_hasDragged))
			{
				InternalClicked(this);
			}
		}
		
		void OnHover(bool isOver)
		{
			if (InternalHover != null)
			{
				InternalHover(this, isOver);
			}
		}
		
		public void EnableCollider(bool enable)
		{
			collider2D.enabled = enable;
		}
	}
}