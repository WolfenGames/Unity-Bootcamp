using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{
	public float x, y;
	public float		f ,g ,h;
	public List<Node> Neighbors;
	public List<float> Distances;
	public Node		previous;
	public void Init()
	{
		Neighbors = new List<Node>();
		Distances = new List<float>();
		foreach(Node n in transform.parent.GetComponent<AStar>().nodes)
		{
			if (n != this)
			{
				RaycastHit2D	raycastHit = Physics2D.Raycast(this.transform.position, n.transform.position - this.transform.position, Vector2.Distance(n.transform.position ,this.transform.position));
				if (!raycastHit)
				{
					Neighbors.Add(n);
					Distances.Add(Vector2.Distance(n.transform.position ,this.transform.position));
				}
			}
		}
		f = Mathf.Infinity;
		g = 0;
		h = 0;
		x = this.transform.position.x;
		y = this.transform.position.y;
	}

	private void OnDrawGizmos() {
		Gizmos.DrawCube(transform.position, Vector3.one * 0.25f);
		foreach(Node n in Neighbors)
		{
			if (n == previous)
				Gizmos.color = Color.red;
			else
				Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, n.transform.position);
		}
	}
}
