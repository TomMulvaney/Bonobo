using UnityEngine;
using System.Collections;
using System;

namespace Bonobo
{
    public class TriggerDestroy : MonoBehaviour
    {
        public delegate void TriggerDestroyEventHandler(TriggerDestroy destroyBehaviour, GameObject destructable);
        public event TriggerDestroyEventHandler Destroying;

        [SerializeField]
        private string[] m_excludedTags;

    	void OnTriggerEnter (Collider other)
        {
    	    if (Array.IndexOf(m_excludedTags, other.tag) == -1)
            {
                Destroy(other.gameObject);
            }
    	}

        void OnTriggerEnter2D (Collider2D other)
        {
            if (Array.IndexOf(m_excludedTags, other.tag) == -1)
            {
                if(Destroying != null)
                {
                    Destroying(this, other.gameObject);
                }

                Destroy(other.gameObject);
            }
        }
    }
}