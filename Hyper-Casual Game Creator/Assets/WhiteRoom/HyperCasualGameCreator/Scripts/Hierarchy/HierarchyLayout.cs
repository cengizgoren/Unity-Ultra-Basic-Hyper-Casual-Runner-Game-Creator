using UnityEditor;
using UnityEngine;
using WhiteRoom.Utils;

public class HierarchyLayout : MonoBehaviour
{
    public static void SetupHierarchy()
    {
        var manager_obj = UtilsClass.FindOrCreateObject("--------Manager--------", "Hierarchy");
        Undo.RecordObject(manager_obj,"Hierarchy_Manager");
            
        var managers = FindObjectsOfType<Manager>();
        foreach (var m in managers)
        {
            m.transform.SetParent(manager_obj.transform);
        }
        
        var ui_obj = UtilsClass.FindOrCreateObject("-----------UI-----------", "Hierarchy");
        Undo.RecordObject(ui_obj,"Hierarchy_UI");

        var uı_objs = GameObject.FindGameObjectsWithTag("UI");
        foreach (var obj in uı_objs)
        {
            obj.transform.SetParent(ui_obj.transform);
        }
        
        var scene_obj = UtilsClass.FindOrCreateObject("---------Scene---------", "Hierarchy");
        Undo.RecordObject(scene_obj,"Hierarchy_Scene");
        
        var environment = UtilsClass.FindOrCreateObject("Environments","Hierarchy");
        environment.transform.SetParent(scene_obj.transform);
        Undo.RecordObject(environment,"Hierarchy_Environments");
        
        var ways = UtilsClass.FindOrCreateObject("Ways");
        ways.transform.SetParent(environment.transform);
        Undo.RecordObject(ways,"Ways_Transform");
        WayCreator.Parent = ways.transform;

        var lights = UtilsClass.FindOrCreateObject("Lights","Hierarchy");
        lights.transform.SetParent(scene_obj.transform);
        Undo.RecordObject(lights,"Lights_Transform");

        var other = UtilsClass.FindOrCreateObject("Other","Hierarchy");
        other.transform.SetParent(scene_obj.transform);
        Undo.RecordObject(other,"Other_Transform");
        
        var scene_objs = FindObjectsOfType<GameObject>();
        foreach (var obj in scene_objs)
        {
            if (obj.CompareTag("Hierarchy")) continue;
            
            if(obj.CompareTag("UI") || obj.CompareTag("UI_Child")) continue;
            
            if(obj.GetComponent<Manager>() != null) continue;
            
            if (obj.GetComponent<Camera>() != null)
            {
                obj.transform.SetParent(scene_obj.transform);
                continue;
            }
            
            if (obj.GetComponent<Light>() != null)
            {
                obj.transform.SetParent(lights.transform);
                continue;
            }

            if (obj.CompareTag("Player") || obj.CompareTag("MainCamera"))
            {
                obj.transform.SetParent(other.transform);
                continue;
            }
            obj.transform.SetParent(environment.transform);
        }
    }
}
