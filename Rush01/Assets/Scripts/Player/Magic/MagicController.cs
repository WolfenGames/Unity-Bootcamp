using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    #region Singleton Access
    private static MagicController instance;//Use of a singleton here, needs to be static in order for other scripts to access it.

    public static MagicController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MagicController>();
            }

            return MagicController.instance;
        }
    }
    #endregion

    public GameObject magicCircle;
    private Projector projector;

    public void Start()
    {
        projector = transform.GetChild(0).GetComponent<Projector>();
    }

    /*
    void Update()
    {
        AimSpell();
    }
    */

    public Vector3 AimSpell()
    {
        projector.enabled = true;
        Ray targetPos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 hitPoint = Vector3.zero;

        if (Physics.Raycast(targetPos, out RaycastHit hit, Mathf.Infinity))
        {
            var offset = (hit.point - Player.p.transform.position);
            magicCircle.transform.position = Player.p.transform.position + Vector3.ClampMagnitude(offset, 10) + new Vector3(0, 20, 0);
            hitPoint = hit.point;
        }
        magicCircle.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime, Space.World);
        return hitPoint;
    }

    public void CancelSpell()
    {
        projector.enabled = false;
    }

    public void CastSpell()
    {

    }
}
