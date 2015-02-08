using UnityEngine;
using System.Collections;

public class ButtonSpriteSwap : MonoBehaviour 
{
    [SerializeField]
    private UISprite m_sprite;
    [SerializeField]
    private string m_pressedName;

    string m_unpressedName;

	// Use this for initialization
	void Awake () 
    {
        m_unpressedName = m_sprite.spriteName;
	}
	
	void OnPress(bool pressed)
    {
        m_sprite.spriteName = pressed ? m_pressedName : m_unpressedName;
    }
}
