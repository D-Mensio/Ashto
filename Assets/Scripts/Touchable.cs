using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(Touchable))]
public class Touchable_Editor : Editor
{ public override void OnInspectorGUI() { } }
#endif
public class Touchable : Graphic
{ 
    protected override void Awake() { 
        base.Awake(); 
    }
    protected override void UpdateGeometry() { }
}