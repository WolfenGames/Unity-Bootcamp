using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;
    public float dampTime = 0.1f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();    
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float currentSpeed = agent.velocity.magnitude / agent.speed;

        animator.SetFloat("moveSpeed", currentSpeed, dampTime, Time.deltaTime);
    }

    public void isDead()
    {
        animator.SetFloat("moveSpeed", 0);
        animator.SetBool("attack", false);
        animator.SetBool("dead", true);
    }

    public void resetAllAnimations()
    {
        animator.SetFloat("moveSpeed", 0);
        animator.SetBool("attack", false);
        animator.SetBool("dead", false);

        if (animator.GetBool("pickup"))
            animator.SetBool("pickup", false);
    }

}
