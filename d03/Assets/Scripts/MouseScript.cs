using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
	public Texture2D	mouseTex;
	public CursorMode 	cursorMode = CursorMode.Auto;
	public Vector2		hotspot = Vector2.zero;
	
	private void Awake() {
		GameObject.DontDestroyOnLoad(this.gameObject);
		Cursor.SetCursor(mouseTex, hotspot, cursorMode);	
	}

}
