using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetObjects;
    private List<Target> targets;
    // Start is called before the first frame update
    void Start()
    {
        targets = new List<Target>();
        foreach(GameObject targetObject in targetObjects)
        {
            Debug.Log(targetObject);
            Target target = targetObject.GetComponent<Target>();
            Debug.Log(target);
            targets.Add(target);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targets.All(x => x.active))
            WinLevel();
    }

    private void WinLevel()
    {
        Debug.Log("Win");
    }
}
