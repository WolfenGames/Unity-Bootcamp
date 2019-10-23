using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterLocomotion : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    AnimationManager animationManager;
    private Vector3 cachedDesination;

    // Start is called before the first frame update
    void Start()
    {   
        agent = GetComponent<NavMeshAgent>();    
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 destination)
    {
        cachedDesination = destination;
        agent.SetDestination(destination);
    }

    public void FollowTarget(Interactable focusedTarget)
    {
        agent.stoppingDistance = focusedTarget.interactionRadius * .8f;
        agent.updateRotation = false;

        target = focusedTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }
    public void StopFollowingTargetFear()
    {
        agent.updateRotation = true;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
    }

    public void Fear()
    {
        if (transform.GetComponent<Player>() != null)
            return;
        
        StopFollowingTargetFear();
        /*
        */
        //var dir = (transform.position - Player.p.transform.position) * 5;
        //NavMesh.SamplePosition(dir, out NavMeshHit hit, 25, 0);
        //agent.SetDestination(hit.position);
    }

}
