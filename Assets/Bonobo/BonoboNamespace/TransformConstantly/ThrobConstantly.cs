using UnityEngine;
using System.Collections;

namespace Bonobo
{
    public class ThrobConstantly : MonoBehaviour 
    {
        [SerializeField]
        private Vector3 m_minSize = Vector3.one * 0.75f;
        [SerializeField]
        private Vector3 m_maxSize = Vector3.one * 1.25f;
        [SerializeField]
        private float m_throbDuration = 0.5f;

        float m_currentTime = 0;

        int m_state = 0;

        void OnEnable()
        {
            StartCoroutine(Throb());
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }
            	
    	IEnumerator Throb()
        {
            while (true)
            {
                m_state = 1;
                while (transform.localScale.magnitude < m_maxSize.magnitude)
                {
                    transform.localScale = Vector3.Lerp(m_minSize, m_maxSize, m_currentTime);
                    m_currentTime += Time.deltaTime / m_throbDuration;
                    m_state = 2;
                    yield return null;
                }

                m_state = 3;

                while (transform.localScale.magnitude > m_minSize.magnitude)
                {
                    transform.localScale = Vector3.Lerp(m_minSize, m_maxSize, m_currentTime);
                    m_currentTime -= Time.deltaTime / m_throbDuration;
                    m_state = 4;
                    yield return null;
                }

                m_state = 5;
                yield return null;
                m_state = 6;
            }
        }

        void OnGUI()
        {
            GUILayout.Label("state: " + m_state);
            GUILayout.Label("localScale: " + transform.localScale);
            GUILayout.Label("m_currentTime: " + m_currentTime);
        }
    }
}