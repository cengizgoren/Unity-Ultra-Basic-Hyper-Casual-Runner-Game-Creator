using UnityEditor;
using UnityEngine;
using WhiteRoom.Utils.Debug;

public class WayCreator : MonoBehaviour
{
    public static Object WayPrefab;
    public static Object Parent;
    public static Object SpawnPos;
    
    public static int WayLenght = 0;
    public static float PositionOffset = 0;
    public static Vector3 RotationOffset = Vector3.zero;

    private static Object previusObject;

    public static void CreateWay()
    {
        if (WayPrefab == null)
        {
            DebugClass.Error("Not Set a Prefab: Select Way Prefab in Scene Panel");
            return;
        }
        for (int i = 0; i < WayLenght; i++)
        {
            Vector3 pos;
            var s_pos = ((Transform) SpawnPos)?.position ?? Vector3.zero;
            if (previusObject != null)
            {
                
                pos = s_pos + ((GameObject) previusObject).transform.position + Vector3.forward * PositionOffset;
            }
            else
            {
                pos = s_pos;
            }
            var way = Instantiate((GameObject)WayPrefab, pos, Quaternion.identity, Parent != null ? (Transform)Parent : null);
            way.transform.eulerAngles += RotationOffset;
            previusObject = way;
            
            Undo.RecordObject(way,"Way");
        }
        
        DebugClass.Log("Created Way");
    }
}
