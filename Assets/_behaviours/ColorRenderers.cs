using UnityEngine;
using System.Collections;

namespace Bonobo
{
    public class ColorRenderers : ColorObjects 
    {
        [SerializeField]
        private Renderer[] m_renderers;

        protected override void RefreshColors()
        {
            if (m_renderers != null)
            {
                Color color = ColorHelpers.GetColor(m_bonoboColor);
                for (int i = 0; i < m_renderers.Length; ++i)
                {
                    if (m_renderers [i] != null && m_renderers[i].renderer != null)
                    {
                        if(Application.isPlaying)
                        {
                            float alpha = m_renderers[i].material.color.a;
                            m_renderers [i].material.color = new Color(color.r, color.g, color.b, alpha);
                        }
                        else // Using sharedMaterial prevents material leaking into the scene
                        {
                            float alpha = m_renderers[i].sharedMaterial.color.a;
                            m_renderers [i].sharedMaterial.color = new Color(color.r, color.g, color.b, alpha);
                        }
                    }
                }
            }
        }
    }
}