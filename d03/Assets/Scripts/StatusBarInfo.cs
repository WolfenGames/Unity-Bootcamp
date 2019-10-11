using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarInfo : MonoBehaviour
{
    public float        Speed;
    public GameObject[] textFields;
    // Start is called before the first frame update
    void Start()
    {
     Speed = 1;   
    }

    public void SetSpeed(float x)
    {
        Speed = x;
    }

    // Update is called once per frame
    void Update()
    {
        textFields[0].GetComponent<Text>().text = string.Format("X {0}\n", Speed);
        textFields[1].GetComponent<Text>().text = string.Format("{0}/{1}", gameManager.gm.playerHp, gameManager.gm.playerMaxHp);
        textFields[2].GetComponent<Text>().text = string.Format("{0}", gameManager.gm.playerEnergy);    
    }
}
