using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple component used to avoid certain object destruction on scene load, delete if said objects are reimplemented as singleton
public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
