using UnityEditor;
using UnityEngine;
using WhiteRoom.Utils.Debug;

#pragma warning disable 618
namespace WhiteRoom.HyperCasualGameCreator.Panels
{
    public class PlayerPanel : Panel
    {
        private static Object playerPrefab;

        public static void Draw()
        {
            SetStyles();

            DrawLabel("Player Panel");
            
            DrawCreatePlayer();
            DrawCreateCamera();
        }

        private static void DrawCreatePlayer()
        {
            GUILayout.BeginVertical("HelpBox");
            
            DrawLabel("Create Player",15);
            
            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Player Prefab");
            playerPrefab = EditorGUILayout.ObjectField(playerPrefab,typeof(GameObject));
            
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Create Player with prefab"))
            {
                if (playerPrefab == null)
                {
                    DebugClass.Error("Not Set a Prefab","Select Player Prefabs in Player Panel or Create New Player");
                    return;
                }
                var player = Instantiate((GameObject)playerPrefab, Vector3.zero, Quaternion.identity);
                player.AddComponent<PlayerController>();
                Undo.RecordObject(player,"Player");
            
                DebugClass.Log("Created Player With Prefab");
            }
        
            if (GUILayout.Button("Create New Player"))
            {
                var prefab = Resources.Load<GameObject>("Prefab/Player/Player");
                var parent = GameObject.Find("Other");
                var player = Instantiate(prefab, Vector3.zero, Quaternion.identity,parent != null ? parent.transform : null);
                player.name = "Player";
                Undo.RecordObject(player,"Player");
            
                DebugClass.Log("Created New Player");
            }
            
            GUILayout.EndVertical();
        }

        private static void DrawCreateCamera()
        {
            GUILayout.BeginVertical("HelpBox");
            
            DrawLabel("Create Camera",15);

            if (GUILayout.Button("Create New Camera"))
            {
                var other = GameObject.Find("Other");
                var cam_pref = Resources.Load<GameObject>("Prefab/Player/PlayerCamera");
                var cam_obj = Instantiate(cam_pref, other != null ? other.transform : null);
                cam_obj.name = "PlayerCamera";
            }
            
            GUILayout.EndVertical();
        }
    }
}