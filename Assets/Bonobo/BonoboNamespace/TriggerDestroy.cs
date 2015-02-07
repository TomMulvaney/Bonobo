using UnityEngine;
using System.Collections;
using System;

public class TriggerDestroy : MonoBehaviour
{
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
            Destroy(other.gameObject);
        }
    }
}
