using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractiblePiece))]
[CanEditMultipleObjects]
public class InteractiblePieceEditor : Editor
{
    SerializedProperty phase;

    private void OnEnable()
    {
        serializedObject.FindProperty("phase");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(phase);
        serializedObject.ApplyModifiedProperties();
        /*switch (phase)
        {
            case 0:
                yAngle = 0f;
                break;
            case 1:
                yAngle = 90f;
                break;
            case 2:
                yAngle = 180f;
                break;
            case 3:
                yAngle = 270f;
                break;
        }
        transform.rotation = Quaternion.Euler(0, 0, yAngle);*/
    }
}
