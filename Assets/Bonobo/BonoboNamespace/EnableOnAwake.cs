using UnityEngine;
using System.Collections;

namespace Bonobo
{
    public class EnableOnAwake : MonoBehaviour 
    {
        [SerializeField]
        private GameObject[] m_gameObjects;
        [SerializeField]
        private MonoBehaviour[] m_behaviours;

        void Awake()
        {
            for (int i = 0; i < m_gameObjects.Length; ++i)
            {
                m_gameObjects[i].SetActive(true);
            }

            for(int i = 0; i < m_behaviours.Length; ++i)
            {
                m_behaviours[i].enabled = true;
            }
        }
    }
}