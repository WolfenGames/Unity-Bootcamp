using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Stats
{
    public float interactionRadius = 2f;
    public Transform interactionTransform = null;
    public bool inRange = false;
    private Transform player;
    private bool focused = false;
    private bool hasInteracted = false;

    void Awake()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
    }

    void Update()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        if (HP <= 0)
            isDead = true;
    
        if (focused && !hasInteracted)
        {
            var distanceBetween = Vector3.Distance(player.position, interactionTransform.position);

            if (distanceBetween <= interactionRadius)
            {
                Interact();
                inRange = true;
                hasInteracted = true;
            }
            else
            {
                inRange = false;
            }
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + interactionTransform.name);
    }

    //Disabled by Justin - as i was getting errors in the engine here for some reason :(
    /*
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, interactionRadius);
    }
    */

    public void Focus(Transform playerTrans)
    {
        focused = true;
        player = playerTrans;
        hasInteracted = false;
    }

    public void Defocus()
    {
        focused = false;
        player = null;
        hasInteracted = false;
    }

    public void ResetInteracted()
    {
        hasInteracted = false;
    }
}
