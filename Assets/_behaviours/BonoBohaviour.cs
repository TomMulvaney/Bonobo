using UnityEngine;
using System.Collections;

namespace Bonobo
{
	// Pure Syntactic Sugar
	public class BonoBohaviour : MonoBehaviour 
	{
		public virtual void On() {}
		public virtual void Off() {}

		public Vector3 pos
		{
			get
			{
				return transform.position;
			}
			set
			{
				transform.position = value;
			}
		}

		public Vector3 localPos
		{
			get
			{
				return transform.localPosition;
			}
		}

		public Vector3 euler
		{
			get
			{
				return transform.eulerAngles;
			}
			set
			{
				transform.eulerAngles = value;
			}
		}

		public Vector3 localEuler
		{
			get
			{
				return transform.localEulerAngles;
			}
			set
			{
				transform.localEulerAngles = value;			
			}
		}

		public Quaternion rot
		{
			get
			{
				return transform.rotation;
			}
		}

		public Quaternion localRot
		{
			get
			{
				return transform.localRotation;
			}
		}


		public Vector3 lossySca
		{
			get
			{
				return transform.lossyScale;
			}
		}

		public Vector3 localSca
		{
			get
			{
				return transform.localScale;
			}
		}
	}
}
