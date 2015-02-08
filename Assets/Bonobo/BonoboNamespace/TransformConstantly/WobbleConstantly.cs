using UnityEngine;
using System.Collections;

namespace Bonobo
{
    public class WobbleConstantly : MonoBehaviour 
    {
        [SerializeField]
        private float m_amount;
        [SerializeField]
        private float m_speed;
        
        private float m_current;
        
        void Start()
        {
            m_current = Random.Range(0.0f, 100.0f);
        }
        
        // Update is called once per frame
        void Update () 
        {
            m_current += Time.deltaTime * m_speed;
            
            transform.localPosition = new Vector3(
                Mathf.Sin(m_current) + (Mathf.Cos(m_current * 1.42f) * 0.5f),
                Mathf.Cos(m_current) + (Mathf.Sin(m_current * 0.87f) * 0.8f), 0) * m_amount;
        }
        
        public void SetAmount(float myAmount)
        {
            m_amount = myAmount;
        }
        
        public void SetSpeed(float mySpeed)
        {
            m_speed = mySpeed;
        }
    }
}