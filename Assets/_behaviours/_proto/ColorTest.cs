using UnityEngine;
using System.Collections;
using Bonobo;

public class ColorTest : MonoBehaviour
{
    [SerializeField]
    private ColorWidgets m_colorWidgets;

	void Start () 
	{
        m_colorWidgets.SetBonoboColor("hello");
	}
}
