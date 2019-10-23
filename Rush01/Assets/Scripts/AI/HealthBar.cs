using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public GameObject EnemyHealthBar;
    Transform target;
    GameObject EnemyHealth;
    private Emeny enenmy;
    private RectTransform healthRect;

    void Start ()
    {
        enenmy = GetComponent<Emeny>();
        EnemyHealth = Instantiate(EnemyHealthBar);
        EnemyHealth.transform.SetParent(GameObject.FindGameObjectWithTag("Healthbar Canvas").transform, false);
        healthRect = EnemyHealth.transform.GetChild(0).GetComponent<RectTransform>();
    }
 
    private void Awake()
    {
        target = gameObject.transform;
    }
 
    void Update ()
    {
        EnemyHealth.transform.position  = Camera.main.WorldToScreenPoint(target.position + new Vector3(0, gameObject.transform.localScale.y * 2f, 0));
        healthRect.sizeDelta = new Vector2((enenmy.HP / enenmy.MAXHP) * 56, 10);
    }
}
