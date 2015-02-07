using UnityEngine;
using System.Collections;

public class BuildDependentDisabler : MonoBehaviour
{
	[SerializeField]
	private GameObject[] m_gameObjects;
	[SerializeField]
	private MonoBehaviour[] m_behaviours;
	[SerializeField]
	private Platform[] m_disabledPlatforms;
	[SerializeField]
	private DebugSetting m_disabledDebugSetting;

	enum Platform
	{
		Standalone,
		PC,
		Mac,
		Linux,
		iPhone,
		Android,
		Web,
		Editor
	}

	enum DebugSetting
	{
		Neither,
		Debug,
		Production
	}

	void Awake () 
	{
		bool disable = (m_disabledDebugSetting == DebugSetting.Debug && Debug.isDebugBuild) || (m_disabledDebugSetting == DebugSetting.Production && !Debug.isDebugBuild);

#if UNITY_STANDALONE
		if(IsDisabled(Platform.Standalone))
		{
			disable = true;
		}
#elif UNITY_PC
		if(IsDisabled(Platform.PC))
		{
			disable = true;
		}
#elif UNITY_MAC
		if(IsDisabled(Platform.Mac))
		{
			disable = true;
		}
#elif UNITY_LINUX
		if(IsDisabled(Platform.Linux))
		{
			disable = true;
		}
#elif UNITY_IPHONE
		if(IsDisabled(Platform.iPhone))
		{
			disable = true;
		}
#elif UNITY_ANDROID
		if(IsDisabled(Platform.Android))
		{
			disable = true;
		}
#elif UNITY_WEBPLAYER
		if(IsDisabled(Platform.Web))
		{
			disable = true;
		}
#endif

#if UNITY_EDITOR
		if(IsDisabled(Platform.Editor))
		{
			disable = true;
		}
#endif

		if (disable) 
		{
			foreach(GameObject go in m_gameObjects)
			{
				go.SetActive(false);
			}

			foreach(MonoBehaviour behaviour in m_behaviours)
			{
				behaviour.enabled = false;
			}
		}
	}

	bool IsDisabled(Platform platform)
	{
		return System.Array.IndexOf(m_disabledPlatforms, platform) != -1;
	}
}
