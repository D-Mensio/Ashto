using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetObjects;
    private List<Target> targets;

    private bool winCheckActive;

    public GameObject cam;
    public GameObject countdownObject;
    private TextMeshProUGUI countdown;

    private void Awake()
    {
        countdown = countdownObject.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targets = new List<Target>();
        foreach(GameObject targetObject in targetObjects)
        {
            //Debug.Log(targetObject);
            Target target = targetObject.GetComponent<Target>();
            //Debug.Log(target);
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
                countdown.text = time.ToString();
                //Play sound clip (clock ticking)
                //Debug.Log(time);
                time--;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                countdown.text = "";
                //Play wrong clip
                broken = true;
            }
        }
        if (broken)
        {
            //Debug.Log("Broken win condition");
            winCheckActive = false;
        }
        else
        {
            //Play right clip
            countdown.text = "";
            //Debug.Log("win");
            cam.GetComponent<CameraController>().LevelTransition();
        }
       
    }
}
