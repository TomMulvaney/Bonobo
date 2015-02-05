using UnityEngine;
using System.Collections;

namespace Bonobo
{
	public class ScaleFollower : MonoBehaviour 
	{
		[SerializeField]
		private Transform m_target;
		[SerializeField]
		private float m_scalar = 1;
		
		void Update () 
		{
			if (m_target != null)
			{
				transform.localScale = m_target.transform.localScale * m_scalar;
			}
		}
		
		public void SetTarget(Transform myTarget)
		{
			m_target = myTarget;
		}
		
		public Transform GetTarget()
		{
			return m_target;
		}
	}
}