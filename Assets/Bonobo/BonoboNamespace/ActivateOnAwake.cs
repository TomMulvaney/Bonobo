using UnityEngine;
using System.Collections;

namespace Bonobo
{
    public class ActivateOnAwake : MonoBehaviour 
    {
        [SerializeField]
        private GameObject[] m_gameObjects;

        void Awake()
        {
            for (int i = 0; i < m_gameObjects.Length; ++i)
            {
                m_gameObjects[i].SetActive(true);
            }
        }
    }
}