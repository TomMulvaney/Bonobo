using UnityEngine;
using System.Collections;

namespace Bonobo
{
    public class AppPauseChecker : Singleton<AppPauseChecker>
    {
        public delegate void AppPauseCheckerEventHandler();
        public event AppPauseCheckerEventHandler LaunchedAfterAppPause;

        bool m_hasStarted = false; // TODO: m_hasStarted is necessary when launching from XCode, find out if it is necessary in other situations
        bool m_hasPausedApp = false;

        void OnApplicationPause()
        {
            if (m_hasStarted)
            {
                m_hasPausedApp = true;
            }
        }

        void Start()
        {
            m_hasStarted = true;
            InvokeRepeating("CheckForAppPause", 10, 10);
        }

        void CheckForAppPause()
        {
            if (m_hasPausedApp)
            {
                Debug.Log("LAUNCHED AFTER PAUSE");

                m_hasPausedApp = false;

                if(LaunchedAfterAppPause != null)
                {
                    LaunchedAfterAppPause();
                }
            }
        }
    }
}