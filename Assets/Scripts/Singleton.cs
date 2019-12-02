using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static public Singleton<T> instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as Singleton<T>;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
