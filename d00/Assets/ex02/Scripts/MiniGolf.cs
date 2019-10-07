using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGolf : MonoBehaviour
{
	enum					Dir
	{
							left,
							right
	}

	[SerializeField]
	GameObject				Ball;
	[SerializeField]
	Vector3					oriBall;
	[SerializeField]
	GameObject				Exit;
	[SerializeField]
	GameObject				Club;
	[SerializeField]
	private KeyCode			code;
	[SerializeField]
	private float			spoed;
	private Dir				dir;
	private	bool			moving;
	[SerializeField]
	private float			decayRate;
	[SerializeField]		
	private float			minSpoed;
	private int				score;
	private	bool			flagWin;
    // Start is called before the first frame update
    void Start()
    {
        spoed = 0f;
		dir = Dir.right;
		moving = false;
		score = -3;
		flagWin = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (spoed <= 0.1f)
		{
			Vector3	pos = Ball.transform.position;
			switch(dir)
			{
				case Dir.left:
					pos.x += 1;
					pos.y += 0.5f;
					Club.transform.localScale = new Vector3(-1, 1, 1);
				break;
				case Dir.right:
					pos.x -= 1;
					pos.y += 0.5f;
					Club.transform.localScale = new Vector3(1, 1, 1);
				break;
			}
			Club.transform.position = pos;
			if (moving == true && !flagWin)
			{
				Debug.Log("Score " + (score * 5));
				score += 1;
			}
			moving = false;
			flagWin = false;
		}
		if (spoed >= 0.1f && !Input.GetKey(code))
		{
			moving = true;
			Ball.transform.Translate(new Vector3(((dir == Dir.right) ? spoed : -spoed) * Time.deltaTime , 0, 0));
			spoed -= (spoed * decayRate) * Time.deltaTime;
			if (Ball.transform.position.x >= 10)
				dir = Dir.left;
			if (Ball.transform.position.x <= -10)
				dir = Dir.right;
			if (spoed <= minSpoed)
			{
				float	IsardIsALizard = Ball.transform.position.x - Exit.transform.position.x;
				if (IsardIsALizard <= 0.4f && IsardIsALizard >= -0.4f)
				{
					// We did a win;
					flagWin = true;
					Ball.transform.position = oriBall;
					dir = Dir.right;
					spoed = 0;
				}
			}
		}
		else if (!moving && Input.GetKey(code))
				spoed += (8 * Time.deltaTime);
		spoed = Mathf.Clamp(spoed, 0, 60);
    }
}
