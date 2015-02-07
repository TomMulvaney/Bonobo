using UnityEngine;
using System.Collections;

namespace Bonobo
{
    [ExecuteInEditMode]
    public class ColorWidgets : ColorObjects 
    {
        [SerializeField]
        private UIWidget[] m_widgets;
        [SerializeField]
        private UILabel[] m_labels;

        protected override void RefreshColors()
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
    }
}