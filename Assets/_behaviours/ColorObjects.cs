using UnityEngine;
using System.Collections;

namespace Bonobo
{
    [ExecuteInEditMode]
    public class ColorObjects : MonoBehaviour
    {
        [SerializeField]
        protected BonoboColor m_bonoboColor;
        
        void Start()
        {
            RefreshColors();
        }
        
#if UNITY_EDITOR
        BonoboColor m_lastBonoboColor;
        
        void Update()
        {
            if (m_bonoboColor != m_lastBonoboColor)
            {
                RefreshColors();
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
            RefreshColors();
        }
        
        protected virtual void RefreshColors() {}
    }
}