using UnityEngine;
using System.Collections;

namespace Bonobo
{
	public class PositionFollower : MonoBehaviour
	{
		[SerializeField]
		private Transform m_target;
		[SerializeField]
		private bool m_useLocal;
		[SerializeField]
		private Vector3 m_offset = Vector3.zero;
		
		void Update () 
		{
			if (m_target != null)
			{
				if(m_useLocal)
				{
					transform.localPosition = m_target.transform.localPosition + m_offset;
				}
				else
				{
					transform.position = m_target.transform.position + m_offset;
				}
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