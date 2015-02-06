using UnityEngine;
using System.Collections;

namespace Bonobo
{
    [ExecuteInEditMode]
    public class ColorWidgets : MonoBehaviour 
    {
        [SerializeField]
        private BonoboColor m_bonoboColor;
        [SerializeField]
        private UIWidget[] m_widgets;
        [SerializeField]
        private UILabel[] m_labels;
        
        void Start()
        {
            RefreshLabels();
            RefreshWidgetColors();
        }
        
#if UNITY_EDITOR
        BonoboColor m_lastBonoboColor;
        
        void Update()
        {
            if (m_bonoboColor != m_lastBonoboColor)
            {
                RefreshLabels();
                RefreshWidgetColors();
            }
            
            m_lastBonoboColor = m_bonoboColor;
        }
#endif
        
        public BonoboColor GetBonoboColor()
        {
            return m_bonoboColor;
        }
        
        public void SetBonoboColor(string colorName)
        {
            try
            {
                SetBonoboColor(ColorHelpers.GetBonoboColor(colorName));
            }
            catch(BonoboColorNotFoundException ex)
            {
                Debug.Log(ex.Message);
            }
        }
        
        public void SetBonoboColor(BonoboColor myBonoboColor)
        {
            m_bonoboColor = myBonoboColor;
            
            RefreshLabels();
            RefreshWidgetColors();
        }
        
        void RefreshLabels()
        {
            if (m_labels != null)
            {
                for (int i = 0; i < m_labels.Length; ++i)
                {
                    if (m_labels [i] != null)
                    {
                        m_labels [i].text = m_bonoboColor.ToString();
                    }
                }
            }
        }
        
        void RefreshWidgetColors()
        {
            if (m_widgets != null)
            {
                Color color = ColorHelpers.GetColor(m_bonoboColor);
                for (int i = 0; i < m_widgets.Length; ++i)
                {
                    if (m_widgets [i] != null)
                    {
                        float alpha = m_widgets[i].color.a;
                        m_widgets [i].color = new Color(color.r, color.g, color.b, alpha);
                    }
                }
            }
        }
    }
}