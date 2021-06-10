using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BakeNeighboors
{
    [MenuItem("Pieces/Bake")]
    static void BakePieces()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("PieceWNeighboors");
        foreach (GameObject p in pieces)
        {           
            PieceConnections pc = p.GetComponent<PieceConnections>();
            Debug.Log("Baked " + pc.GetType());
            Vector3 pos = p.transform.position;
            //Debug.Log(pos);
            if(GetObjAtPos(pos + Vector3.up))
                pc.northNeighbor = GetObjAtPos(pos + Vector3.up).GetComponent<PieceConnections>();
            if (GetObjAtPos(pos + Vector3.down))
                pc.southNeighbor = GetObjAtPos(pos + Vector3.down).GetComponent<PieceConnections>();
            if (GetObjAtPos(pos + Vector3.right))
                pc.eastNeighbor = GetObjAtPos(pos + Vector3.right).GetComponent<PieceConnections>();
            if(GetObjAtPos(pos + Vector3.left))
                pc.westNeighbor = GetObjAtPos(pos + Vector3.left).GetComponent<PieceConnections>();
            PrefabUtility.RecordPrefabInstancePropertyModifications(pc);
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.targets = new List<TargetActivation>();
        foreach (GameObject t in targets)
        {
            levelManager.targets.Add(t.GetComponent<TargetActivation>());
            TargetConnections pc = t.GetComponent<TargetConnections>();
            Debug.Log("Baked " + pc.GetType());
            Vector3 pos = t.transform.position;
            if (GetObjAtPos(pos + Vector3.up))
                pc.northNeighbor = GetObjAtPos(pos + Vector3.up).GetComponent<PieceConnections>();
            if (GetObjAtPos(pos + Vector3.down))
                pc.southNeighbor = GetObjAtPos(pos + Vector3.down).GetComponent<PieceConnections>();
            if (GetObjAtPos(pos + Vector3.right))
                pc.eastNeighbor = GetObjAtPos(pos + Vector3.right).GetComponent<PieceConnections>();
            if (GetObjAtPos(pos + Vector3.left))
                pc.westNeighbor = GetObjAtPos(pos + Vector3.left).GetComponent<PieceConnections>();
            PrefabUtility.RecordPrefabInstancePropertyModifications(pc);
        }
        PrefabUtility.RecordPrefabInstancePropertyModifications(levelManager);
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    }

    public static GameObject GetObjAtPos(Vector3 p)
    {
        //Debug.Log(p);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(p, 0);
        if (hitColliders.Length < 1)
            return null;
        //Debug.Log("Hit!");
        return hitColliders[0].gameObject;
    }
}
