using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Buttons : MonoBehaviour
{
	enum ButtonType
	{
		None,
		Standard,
		Absorb,
		Platform,
		Enable,
		Disable,
	}
	[SerializeField]
	ButtonType Type1;
	[SerializeField]
	ButtonType Type2;

	[SerializeField]
	GameObject[]	objectsAffected;
	SpriteRenderer	_sprite;
	enum RequiredColor
	{
		All,
		Red,
		Yellow,
		Blue
	}
	[SerializeField]
	RequiredColor requiredColor;	
    // Start is called before the first frame update
    void Start()
    {
		_sprite = this.GetComponentInChildren<SpriteRenderer>();
		ChangeColor();
    }

	void	ChangeColor()
	{
		switch (requiredColor)
		{
			case RequiredColor.Red:
				_sprite.color = Color.red;
				break;
			case RequiredColor.Blue:
				_sprite.color = Color.blue;
				break;
			case RequiredColor.Yellow:
				_sprite.color = Color.yellow;
				break;
			case RequiredColor.All:
			default:
				break;
		}
	}

	void	DoThaEnable()
	{
		foreach (GameObject go in objectsAffected)
			go.SetActive(!go.activeSelf);
	}

    // Update is called once per frame
    void Update()
    {
    }
}
