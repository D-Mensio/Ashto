using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject icon;
    public GameObject content;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < 50; i++)
        {
            GameObject newObj = Instantiate(icon);
            icon.transform.position = transform.position;
            newObj.transform.parent = content.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
