using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{
    #region Singleton Access
    private static MagicSystem instance;//Use of a singleton here, needs to be static in order for other scripts to access it.

    public static MagicSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MagicSystem>();
            }

            return MagicSystem.instance;
        }
    }
    #endregion

    public bool castingSpell = false;
}
