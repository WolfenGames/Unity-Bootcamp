using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    public GameObject target;
    public Vector3 playerPos => target.transform.position;
    public Firearm weapon;
    public float visibilityFOV = 90;
    public float minVisibilityDistance = 1.0f;

    private NavMeshAgent agent;
    private float halfFOV;

    private bool hasTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (weapon != null) {
            agent.stoppingDistance = weapon.range;
        }
        halfFOV = visibilityFOV / 2;
    }

    void Update()
    {
        if (hasTarget) {
            if (agent.destination != playerPos && Vector3.Distance(transform.position, playerPos) > agent.stoppingDistance)
                agent.SetDestination(playerPos);
            else {
                Debug.Log("RATATATA");
                weapon.Attack();
            }

            transform.forward = -(playerPos - transform.position);
        }
    }

    private bool TargetIsVisible(Transform target) {
        Vector3 direction = target.position - transform.position;
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= minVisibilityDistance)
            return TargetInLOS(target, direction, distance);

        float angleBetween = Vector3.Angle(direction, -transform.forward);

        if (angleBetween <= halfFOV)
            return TargetInLOS(target, direction, distance);

        return false;
    }

    private bool TargetInLOS(Transform target, Vector3 direction, float distance) {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance);
        return hits.Length == 2;
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            if (TargetIsVisible(collider.transform)) {
                hasTarget = true;
            }
        }
    }
}
