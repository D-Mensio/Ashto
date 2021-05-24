using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BakeNeighboors
{
    [MenuItem("Pieces/Bake")]
    static void BakePieces()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("PieceWNeighboors");
        foreach(GameObject p in pieces)
        {
            if (p.GetComponent<Source>())
            {
                Debug.Log("Baked source");
                Source s = p.GetComponent<Source>();
                Vector3 pos = p.transform.position;
                Debug.Log(pos);
                s.northGameObject = GetObjAtPos(pos + Vector3.up);
                s.southGameObject = GetObjAtPos(pos + Vector3.down);
                s.eastGameObject = GetObjAtPos(pos + Vector3.right);
                s.westGameObject = GetObjAtPos(pos + Vector3.left);
                PrefabUtility.RecordPrefabInstancePropertyModifications(s);
            }
            else if (p.GetComponent<InteractiblePiece>())
            {
                Debug.Log("Baked Interactible piece");
                InteractiblePiece i = p.GetComponent<InteractiblePiece>();
                Vector3 pos = p.transform.position;
                Debug.Log(pos);
                i.northGameObject = GetObjAtPos(pos + Vector3.up);
                i.southGameObject = GetObjAtPos(pos + Vector3.down);
                i.eastGameObject = GetObjAtPos(pos + Vector3.right);
                i.westGameObject = GetObjAtPos(pos + Vector3.left);
                PrefabUtility.RecordPrefabInstancePropertyModifications(i);
            }
        }
    }

    private static GameObject GetObjAtPos(Vector3 p)
    {
        Debug.Log(p);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(p, 0);
        if (hitColliders.Length < 1)
            return null;
        Debug.Log("Hit!");
        return hitColliders[0].gameObject;
    }
}
