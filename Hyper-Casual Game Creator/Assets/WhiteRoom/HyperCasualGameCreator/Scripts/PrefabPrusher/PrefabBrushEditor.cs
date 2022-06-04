using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CustomEditor(typeof(PrefabBrush))]
public class PrefabBrushEditor : Editor
{
    private Vector3 mouseOldPos;
    private Object source;

    private PrefabBrush pb;

    private Brush selectedBrush;

    private bool randomSizeChecker;

    private void Awake()
    {
        pb = target as PrefabBrush;
    }

    public override void OnInspectorGUI()
    {
        if (pb == null) return;
        InspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (pb == null) return;
        PrefabBrusher();

        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
    }
    
    private void InspectorGUI()
    {
        GUI.backgroundColor = Color.white;

        GUILayout.BeginVertical("Box");
        GUILayout.Label("General Settings", new GUIStyle(GUI.skin.label) {fontSize = 20});

        GUILayout.BeginHorizontal();
        GUILayout.Label("General Offset",new GUIStyle(GUI.skin.label) {fontSize = 15});
        pb.generalOffset = EditorGUILayout.Vector3Field("",pb.generalOffset);
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Random Size", new GUIStyle(GUI.skin.label) {fontSize = 15});
        randomSizeChecker = EditorGUILayout.Toggle(randomSizeChecker);
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        GUILayout.Label("Prefab Settings", new GUIStyle(GUI.skin.label) {fontSize = 20});

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Prefab"))
        { 
            Brush brush;
            if (pb.brushes.Count > 0)
            {
                var newBrush = pb.brushes[pb.brushes.Count - 1];
                brush = new Brush(newBrush.prefab, newBrush.size, newBrush.rotation,newBrush.localOffset);
            }
            else
                brush = new Brush(null, 1, 0,Vector3.zero);

            pb.brushes.Add(brush);
        }

        if (GUILayout.Button("Remove Prefab"))
        {
            if (pb.brushes.Count < 1) return;
            if (pb.brushes[pb.brushes.Count - 1] == selectedBrush) selectedBrush = null;
            pb.brushes.RemoveAt(pb.brushes.Count - 1);
        }

        if (GUILayout.Button("Remove All Prefab"))
        {
            if (pb.brushes.Count < 1) return;
            selectedBrush = null;
            pb.brushes.RemoveAll(brush => true);
        }

        GUILayout.EndHorizontal();
        
        DrawBrushes();

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private void PrefabBrusher()
    {
        var e = Event.current;
        var mouseRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

        var point = (0 - mouseRay.origin.y) / mouseRay.direction.y;
        var mousePos = mouseRay.GetPoint(point);
        
        PointBrusher(e,mouseRay,mousePos);
    }

    private void PointBrusher(Event e, Ray mouseRay, Vector3 mousePos)
    {
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Z)
        {
            if (pb.brushedPrefab.Count - 1 < 0) return;
            DestroyImmediate(pb.brushedPrefab[pb.brushedPrefab.Count - 1]);
        }

        if (selectedBrush == null || selectedBrush.prefab == null) return;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            if (pb.brushes == null) return;
            if (mouseOldPos == mousePos) return;

            Undo.RecordObject(pb, "Brushed Prefabs");

            GameObject pref;
            if (Physics.Raycast(mouseRay, out var hit))
            {
                InstantiateObjectUp(out pref, hit.point,hit.normal);
            }
            else
            {
                mouseOldPos = mousePos;
                InstantiateEmpty(out pref,mousePos);
            }

            if (pb.parent != null) pref.transform.SetParent(pb.parent);
            pb.brushedPrefab.Add(pref);
        }
    }

    private void InstantiateEmpty(out GameObject pref,Vector3 pos)
    {
        pref = Instantiate((GameObject) selectedBrush.prefab, pos + pb.generalOffset + selectedBrush.localOffset, Quaternion.identity);
        var rot = pref.transform.eulerAngles;
        rot.y = selectedBrush.rotation;
        pref.transform.eulerAngles = rot;
    }

    private void InstantiateObjectUp(out GameObject pref,Vector3 point,Vector3 normal)
    {
        pref = Instantiate((GameObject) selectedBrush.prefab, point + pb.generalOffset + selectedBrush.localOffset, Quaternion.FromToRotation(Vector3.up, normal));
        var parent = new GameObject("parent");
        parent.transform.position = pref.transform.position;
        parent.transform.rotation = pref.transform.rotation;
                
        pref.transform.SetParent(parent.transform);

        var localRot = pref.transform.localEulerAngles;
        localRot.y += selectedBrush.rotation;
        pref.transform.localEulerAngles = localRot;
        pref.transform.SetParent(null);
        DestroyImmediate(parent);

        pref.transform.position += pref.transform.up * (pref.transform.lossyScale.y / 2);
    }
    
    private void DrawBrushes()
    {
        if(pb.brushes.Count > 0) GUILayout.BeginVertical("HelpBox");
        
        foreach (var brush in pb.brushes)
        {
            GUILayout.BeginVertical("Box");

            GUILayout.BeginHorizontal();
            
            if (selectedBrush != null)
                GUI.backgroundColor = brush.isSelected ? new Color(0f, 1f, 0f) : new Color(1f, .5f, 0f);
            else GUI.backgroundColor = new Color(1f, .5f, 0f);

            if (GUILayout.Button("Select")) //select brush
            {
                if (brush.prefab == null) return;

                if (selectedBrush != null)
                    selectedBrush.isSelected = false;

                brush.isSelected = true;
                selectedBrush = brush;
            }

            GUI.backgroundColor = Color.white;
            GUILayout.BeginVertical();
            brush.prefab = EditorGUILayout.ObjectField("Prefab", brush.prefab, typeof(GameObject), false);
            GUILayout.BeginHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Size");
            brush.size = EditorGUILayout.FloatField(brush.size);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Rotation");
            brush.rotation = EditorGUILayout.FloatField(brush.rotation);
            GUILayout.EndHorizontal();
            
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Local Offset");
            brush.localOffset = EditorGUILayout.Vector3Field("",brush.localOffset);
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        if(pb.brushes.Count > 0)  GUILayout.EndVertical();
    }
}