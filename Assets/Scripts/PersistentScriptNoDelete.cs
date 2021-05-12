using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentScriptNoDelete : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
