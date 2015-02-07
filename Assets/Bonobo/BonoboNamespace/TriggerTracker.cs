using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Bonobo
{
	public class TriggerTracker : MonoBehaviour 
	{
	    public delegate void TriggerTrackerEventHandler (TriggerTracker tracker, GameObject other);
	    public event TriggerTrackerEventHandler Entered;
	    public event TriggerTrackerEventHandler Exited;

		List<GameObject> m_trackedObjects = new List<GameObject>();

		// Removal and Defence
	    void Start()
	    {
	        StartCoroutine(RemoveNull());
	    }

	    IEnumerator RemoveNull()
	    {
	        yield return new WaitForSeconds(0.5f);
	        m_trackedObjects.RemoveAll(x => x == null);
	        StartCoroutine(RemoveNull());
	    }

		void OnDisable()
		{
			m_trackedObjects.Clear();
		}

		// Enter
		void OnTriggerEnter2D (Collider2D other) 
		{
			OnEnter (other.gameObject);
		}
		
		void OnTriggerEnter(Collider other)
		{
			OnEnter (other.gameObject);
		}

		void OnEnter(GameObject other)
		{
			if (!m_trackedObjects.Contains(other))
			{
				m_trackedObjects.Add(other);
				
				if(Entered != null)
				{
					Entered(this, other);
				}
			}
		}

		// Exit
		void OnTriggerExit2D (Collider2D other) 
		{
			OnExit (other.gameObject);
		}

		void OnTriggerExit(Collider other)
		{
			OnExit (other.gameObject);
		}

		void OnExit(GameObject other)
		{
			if(m_trackedObjects.Contains(other))
			{
				m_trackedObjects.Remove(other);
				
				if(Exited != null)
				{
					Exited(this, other);
				}
			}
		}

		// Getters
		public bool IsTracking(GameObject go)
		{
			return m_trackedObjects.Contains(go);
		}
		
		public int GetNumTrackedObjects()
		{
			return m_trackedObjects.Count;
		}

		// Logger
	    public void LogTrackedObjectNames()
	    {
	        foreach (GameObject go in m_trackedObjects)
	        {
	            Debug.Log(go.name);
	        }
	    }
	}
}