using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Pieces
{
    IPiece,
    XPiece,
    LPiece,
    Source,
    Target
}

public enum Dir
{
    Up,
    Down,
    Left,
    Right
}

[CustomEditor(typeof(PieceConnections), true)]
public class BaseClassEditor : Editor
{
    private Pieces p;
    private Dir d;
    private PieceConnections targetPiece;

    public void OnEnable()
    {
        targetPiece = (PieceConnections)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.BeginVertical("box");
        GUILayout.Label("Add piece:");
        p = (Pieces)EditorGUILayout.EnumPopup("Piece Type: ", p);
        d = (Dir)EditorGUILayout.EnumPopup("Direction: ", d);

        if (GUILayout.Button("Create"))
        {
            CreateObject(p, d);
        }
        GUILayout.EndVertical();
    }

    private void CreateObject(Pieces p, Dir d)
    {
        GameObject prefab = p switch
        {
            Pieces.IPiece => (GameObject)Resources.Load("Prefabs/Pieces/IPiece"),
            Pieces.XPiece => (GameObject)Resources.Load("Prefabs/Pieces/XPiece"),
            Pieces.LPiece => (GameObject)Resources.Load("Prefabs/Pieces/LPiece"),
            Pieces.Source => (GameObject)Resources.Load("Prefabs/Pieces/Source"),
            Pieces.Target => (GameObject)Resources.Load("Prefabs/Pieces/Target"),
            _ => null,
        };

        Vector3 offset = d switch
        {
            Dir.Up => Vector3.up,
            Dir.Down => Vector3.down,
            Dir.Left => Vector3.left,
            Dir.Right => Vector3.right,
            _ => Vector3.zero,
        };

        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        obj.transform.position = targetPiece.gameObject.transform.position + offset;
        obj.transform.parent = GameObject.Find("====PUZZLE PIECES====").transform;
    }
}
