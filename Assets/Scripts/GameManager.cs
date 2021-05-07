using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetObjects;
    private List<Target> targets;

    private bool winCheckActive;

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
        winCheckActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!winCheckActive && targets.All(x => x.active))
        {
            winCheckActive = true;
            StartCoroutine(WinLevel());
        }

    }

    private IEnumerator WinLevel()
    {
        int time = 5;
        bool broken = false;

        while (!broken && time > 0)
        {
            if (targets.All(x => x.active))
            {
                Debug.Log(time);
                time--;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                broken = true;
            }
        }
        if (broken)
        {
            Debug.Log("Broken win condition");
            winCheckActive = false;
        }
        else
            Debug.Log("Win!");
    }
}
