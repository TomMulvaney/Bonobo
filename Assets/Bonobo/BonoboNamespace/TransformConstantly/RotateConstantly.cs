using UnityEngine;
using System.Collections;

namespace Bonobo
{
    public class RotateConstantly : MonoBehaviour
    {
        [SerializeField]
        private float m_rotateSpeed;
        [SerializeField]
        private float m_randomRange = 20;
        [SerializeField]
        private bool m_startRandom;
        [SerializeField]
        private bool m_clamp;

        private float m_rotateSpeedInternal;

        void Start()
        {
            if (m_startRandom)
            {
                transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
            }
        }

        void OnEnable()
        {
            m_rotateSpeedInternal = m_rotateSpeed + Random.Range(-m_randomRange, m_randomRange);
        }

    	void Update () 
        {
            transform.Rotate(Vector3.forward, m_rotateSpeedInternal * Time.deltaTime);
    	}
    }
}