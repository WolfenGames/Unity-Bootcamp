using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
	public SpriteRenderer body;
	public SpriteRenderer head;
	public Sprite[] bodies;
	public Sprite[] heads;

	public GameObject	bullet_prefab;
	public Transform	target;
	NavMeshAgent  		nma;
	float				cd = 0;
	float				oneFuckTon = 6;
	public float		timeToShoot;
	float				holdTimeToShoot;
	[SerializeField]
	List<GameObject>	path;
	bool				heard = false;
	[SerializeField]
	Vector3				heard_Target;
	Vector3				pathTarget;
	int					pathTaken = 0;
	[SerializeField]
	string				pathtag;
    public AudioSource aBullet;
    public AudioSource aReload;
    public AudioSource aInquisitive;
	float inquisitivecd;
	int clip = 20;
    void Start()
    {
		body.sprite = bodies[Random.Range(0, bodies.Length - 1)];
		head.sprite = heads[Random.Range(0, heads.Length - 1)];
		aBullet.clip = bullet_prefab.GetComponent<Bullet>().clip;
		path = GameObject.FindGameObjectsWithTag(pathtag).ToList();
		if (path.Count > 0)
			pathTarget = path[pathTaken].transform.position;
		holdTimeToShoot = timeToShoot;
		timeToShoot = 0;
		if (!target)
			target = GameObject.FindGameObjectWithTag("Player").transform;
		nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Display.display.dead == true)
			return;
		Vector3 t = target.transform.position;
		t.y = transform.position.y;
		RaycastHit[] raycast =	Physics.BoxCastAll(this.transform.position, new Vector3(0.0f, 0, 0.3f), target.transform.position - this.transform.position, Quaternion.LookRotation(transform.position, t), Vector3.Distance(this.transform.position, target.transform.position));
		bool LOS = true;
		foreach(RaycastHit hit in raycast)
			if (hit.transform.gameObject.tag != "Enemy" && hit.transform.gameObject.tag != "Player" && hit.transform.gameObject.tag != "Bullet" && hit.transform.gameObject.tag != "Gun")
				LOS = false;

		if (PlayerInView() && LOS)
				cd = 4;
		if (cd > 0)
 		{
			if (Vector3.Distance(this.transform.position, target.transform.position) < 15 && LOS)
			{
				//Mkae look at
				nma.SetDestination(transform.position);
				transform.LookAt(t);
				Attack();
			}
			else
			{
				inquisitivecd = -1;
       			nma.SetDestination(target.position);
			}
			cd -= Time.deltaTime;
		}
		else
		{
			if (heard && Vector3.Distance(heard_Target, this.transform.position) > 0.5f)
			{
				if (inquisitivecd < 0.0f)
				{
					inquisitivecd = 4.0f;
					aInquisitive.Play();
				}
				nma.SetDestination(heard_Target);
			}else
			{
				heard = false;
				if (path.Count > 0)
				{
					if (Vector3.Distance(this.transform.position, pathTarget) < 1f)
					{
						if (pathTaken > path.Count - 1)
							pathTaken = 0;
						pathTarget = path[pathTaken++].transform.position;
						Debug.Log("new Path");
					}
					nma.SetDestination(pathTarget);
				}else
					nma.SetDestination(transform.position);
				this.transform.Rotate(new Vector3(0, 12 * Time.deltaTime, 0));
			}
		}
		Debug.DrawRay(this.transform.position, this.transform.forward, LOS ? Color.blue : Color.red, 0.1f);
    }

	public void oShit()
	{
		if (Vector3.Distance(target.position, this.transform.position) < oneFuckTon)
		{
			heard_Target = new Vector3(target.position.x, target.position.y, target.position.z);
			heard = true;
		}
	}

	bool	PlayerInView()
	{
		Vector3 dir = target.transform.position - transform.position;
		dir.y = 0;
		dir.Normalize();
		return (
			Vector3.Dot(dir, transform.forward) > 0.75f
		);
	}

	void	Attack()
	{
		// GetComponent<Shoot>().shootBullets(true);
		if (timeToShoot < 0)
		{
			Vector3 pos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
			GameObject t = GameObject.Instantiate(bullet_prefab, pos, this.transform.rotation);
			Bullet b = t.GetComponent<Bullet>();
			b.canPass = new string[]{"Enemy", "Gun", "Floor"};
			aBullet.Play();
			timeToShoot = holdTimeToShoot; // Replace with weapon property
			clip--;
			if (clip < 0)
			{
				aReload.Play();
				clip = 20;
				timeToShoot = 2.0f;
			}
		}
		timeToShoot -= Time.deltaTime; // Replace with weapon property
	}
}
