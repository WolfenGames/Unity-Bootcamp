using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float attackRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat enemyCombat;
    Stats myStats;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void Start()
    {
        target = Player.p.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<CharacterCombat>();
        myStats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myStats.isDead)
        {
            float currentDistance = Vector3.Distance(this.transform.position, target.transform.position);

            if (currentDistance <= attackRadius)
            {
                agent.SetDestination(target.position);
                
                if (currentDistance <= agent.stoppingDistance)
                {
                    Stats targetStats = target.GetComponent<Stats>();
                    if (targetStats != null && !targetStats.isDead)
                        enemyCombat.Attack(targetStats);
                    FaceTarget();
                }
                else
                {
                    enemyCombat.ResetCooldown();
                }
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
    }

}
