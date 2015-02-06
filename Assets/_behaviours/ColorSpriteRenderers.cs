using UnityEngine;
using System.Collections;

namespace Bonobo
{
    [ExecuteInEditMode]
    public class ColorSpriteRenderers : ColorObjects
    {
        [SerializeField]
        private SpriteRenderer[] m_spriteRenderers;

        protected override void RefreshColors()
        {
            if (m_spriteRenderers != null)
            {
                Color color = ColorHelpers.GetColor(m_bonoboColor);
                for (int i = 0; i < m_spriteRenderers.Length; ++i)
                {
                    if (m_spriteRenderers [i] != null && m_spriteRenderers[i].renderer != null)
                    {
                        if(Application.isPlaying)
                        {
                            float alpha = m_spriteRenderers[i].material.color.a;
                            m_spriteRenderers [i].material.color = new Color(color.r, color.g, color.b, alpha);
                        }
                        else // Using sharedMaterial prevents material leaking into the scene
                        {
                            float alpha = m_spriteRenderers[i].sharedMaterial.color.a;
                            m_spriteRenderers [i].sharedMaterial.color = new Color(color.r, color.g, color.b, alpha);
                        }
                    }
                }
            }
        }
    }
}