using UnityEngine;
using System.Collections;

namespace Bonobo
{
    public class NSetAlphasOnAwake : MonoBehaviour 
    {
        [SerializeField]
        private float m_alpha;
        [SerializeField]
        private UIPanel[] m_panels;
        [SerializeField]
        private UIWidget[] m_widgets;

        void Awake()
        {
            for (int i = 0; i < m_panels.Length; ++i)
            {
                m_panels[i].alpha = m_alpha;
            }

            for (int i = 0; i < m_widgets.Length; ++i)
            {
                m_widgets[i].alpha = m_alpha;
            }
        }
    }
}