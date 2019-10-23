using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterCombat : MonoBehaviour
{
    private Stats myStats;
    private Stats enemyStats;
    private float attackCooldown = 0;
    public float attackSpeed = 1f;
    public float totalDamage = 0f;
    private AnimationManager animationManager;
    [HideInInspector] public Transform equipPoint;
    [HideInInspector] public GameObject assignedWeapon;
    [HideInInspector] public bool equippedWeapon = false;

    private void Awake()
    {
        myStats = GetComponent<Stats>();
        animationManager = GetComponent<AnimationManager>();
    }

    void Start()
    {
        Transform r_hand = animationManager.animator.GetBoneTransform(HumanBodyBones.RightHand);

        if (r_hand != null)
            if (r_hand.childCount > 0)
                equipPoint = animationManager.animator.GetBoneTransform(HumanBodyBones.RightHand).GetChild(0);
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (myStats.HP <= 0)
            myStats.isDead = true;
    }

    public void Attack(Stats targetStats)
    {
        enemyStats = targetStats;
        if (attackCooldown <= 0)
        {
            animationManager.animator.SetBool("attack", true);
            attackCooldown = attackSpeed;
        }
    }
    public void Attack(Stats targetStats, float damage)
    {
        enemyStats = targetStats;
        totalDamage = damage;

        if (attackCooldown <= 0)
        {
            animationManager.animator.SetBool("attack", true);
            animationManager.animator.SetFloat("moveSpeed", 0);
            attackCooldown = attackSpeed;
        }
    }

    public void doDamage()
    {
        if (enemyStats != null)
        {
            if (totalDamage > 0)
            {
                Debug.Log(name + " Takes Damage of: " + totalDamage);
                enemyStats.TakeDamage(totalDamage);
                totalDamage = 0;
                return ;
            }

            enemyStats.TakeDamage(UnityEngine.Random.Range(myStats.MINDMG, myStats.MAXDMG));
        }
    }

    public void ResetCooldown()
    {
        attackCooldown = 0.1f;
    }

    public void EquipWeapon(GameObject weapon)
    {
        UnequipWeapon();
        assignedWeapon = GameObject.Instantiate(weapon);

        CollectableItems item = assignedWeapon.GetComponent<CollectableItems>();
        item.enabled = false;

        Rigidbody rigid = assignedWeapon.GetComponent<Rigidbody>();
        rigid.isKinematic = true;
        rigid.useGravity = false;

        BoxCollider boxCollider = assignedWeapon.GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        SphereCollider sphereCollider = assignedWeapon.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;

        assignedWeapon.transform.SetParent(equipPoint);
        assignedWeapon.transform.localPosition = Vector3.zero;
        assignedWeapon.transform.localRotation = Quaternion.identity;
        assignedWeapon.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);

        assignedWeapon.layer = 8;//Set layer to the players layer for equip camera to render the weapon as well
    }

    public void UnequipWeapon()
    {
        equippedWeapon = false;

        if (assignedWeapon != null)
        {
            GameObject.Destroy(assignedWeapon);
            assignedWeapon = null;
        }
    }
}
