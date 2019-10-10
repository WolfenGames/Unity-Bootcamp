using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionFunctions
{
	public static Vector2 toVec2(this Vector3 vec3)
	{
		return new Vector2(vec3.x, vec3.y);
	}

	public static Vector2 Swap(this Vector2 vec2)
	{
		float		temp1;

		temp1 = vec2.x;
		vec2.x = vec2.y;
		vec2.y = temp1;
		return vec2;
	}
}