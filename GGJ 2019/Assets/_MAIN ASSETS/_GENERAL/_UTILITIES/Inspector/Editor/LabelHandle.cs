/* using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject))]
public class LabelHandle : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
    {   
        GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.black;

        if (transform.gameObject.GetComponent<NameHandle>() != null)
            Handles.Label(transform.position, transform.gameObject.name, style);
}
} */