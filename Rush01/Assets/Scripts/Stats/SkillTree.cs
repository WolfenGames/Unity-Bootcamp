using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//TODO: Skill Tree

public class SkillTree : MonoBehaviour
{
	public int						points;
	public static SkillTree			skill;
	public TextMeshProUGUI	textMeshPro;
	string							temp;


	void Awake()
	{
		skill = this;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
		temp = textMeshPro.text;
    }
	public void Inc()
	{
		points += 5;
	}

    // Update is called once per frame
    void Update()
    {
		textMeshPro.text = string.Format(temp, points);
    }
}
