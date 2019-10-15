using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class AStar : MonoBehaviour
{

	public int changeme;
	Node 				MyPos;
	public Node			start, end;
	public List<Node>	nodes;
	List<Node>			openSet;
	List<Node>			closedSet;
	Node				lastChecked;

	private void Awake() {
		nodes = GetComponentsInChildren<Node>().ToList();
		openSet = new List<Node>();
		foreach(Node n in nodes)
		{
			n.Init();
			n.h = Vector2.Distance(n.transform.position, end.transform.position);
			openSet.Add(n);
		}
		openSet.Clear();
		closedSet = new List<Node>();
	}


	public bool step()
	{
		if (openSet.Count > 0)
		{
			int		winner = 0;
			for(int i = 0; i < openSet.Count; i++)
			{
				if (openSet[i].f < openSet[winner].f)
				{
					winner = i;
				}
				
				if (openSet[i].f == openSet[winner].f)
				{
					if (openSet[i].g > openSet[winner].g)
					{
						winner = i;
					}
				}
			}
			var current = openSet[winner];
			lastChecked = current;
			if (current == lastChecked)
				return true;
			openSet.Remove(current);
			closedSet.Add(current);
			for (int i = 0; i < current.Neighbors.Count; i++)
			{
				var neighbor = current.Neighbors[i];
				if (!closedSet.Contains(neighbor))
				{
					var TempG = current.g + heuristics(current, neighbor);
					if (!openSet.Contains(neighbor))
						openSet.Add(neighbor);
					else if (TempG > neighbor.g)
						continue;

					neighbor.g = TempG;
					neighbor.h = heuristics(neighbor, end);
					neighbor.f = neighbor.g + neighbor.h;
					neighbor.previous = current;
				}
			}
		}
		return SoekHom();
	}

	public AStar(Node Start, Node End)
	{
	}

	void Update()
	{
		Debug.Log(step());
	}

	public List<Node> findPath(Vector2 myposthing, Vector2 end)
	{
		List<Node> ret = new List<Node>();
		// Node start;
		//TODO: Be Better


		return ret;
	}

	public bool SoekHom()
	{
		return (MyPos == end);
	}

	float heuristics(Node A, Node B)
	{
		return Mathf.Abs(A.x - B.x) + Mathf.Abs(A.y - B.y);
	}

	private void OnValidate() {
		nodes.Clear();
		openSet.Clear();
		closedSet.Clear();
		Awake();
	}
}
