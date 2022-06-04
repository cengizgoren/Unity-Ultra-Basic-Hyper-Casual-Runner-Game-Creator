using UnityEditor;
using UnityEngine;
using WhiteRoom.Utils.Debug;

#pragma warning disable 618
namespace WhiteRoom.HyperCasualGameCreator.Panels
{
    public class ScenePanel : Panel
    {
        public static void Draw()
        {
            SetStyles();

            DrawLabel("Scene Panel");

            DrawWayCreator();
            DrawCreateNewScene();
        }

        private static void DrawWayCreator()
        {
            GUILayout.BeginVertical("HelpBox");
        
            DrawLabel("Way Creator",15);
        
            GUILayout.BeginHorizontal();
            GUILayout.Label("Way Prefab");
            WayCreator.WayPrefab = EditorGUILayout.ObjectField(WayCreator.WayPrefab,typeof(GameObject));
            GUILayout.EndHorizontal();
        
            GUILayout.BeginHorizontal();
            GUILayout.Label("Parent");
            WayCreator.Parent = EditorGUILayout.ObjectField(WayCreator.Parent,typeof(Transform));
            GUILayout.EndHorizontal();
        
            GUILayout.BeginHorizontal();
            GUILayout.Label("Spawn Position");
            WayCreator.SpawnPos = EditorGUILayout.ObjectField(WayCreator.SpawnPos,typeof(Transform));
            GUILayout.EndHorizontal();

            WayCreator.WayLenght = EditorGUILayout.IntField("Way Lenght", WayCreator.WayLenght);
            WayCreator.PositionOffset = EditorGUILayout.FloatField("Position Offset", WayCreator.PositionOffset);
            WayCreator.RotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", WayCreator.RotationOffset);
        
            if (GUILayout.Button("Create Way"))
            {
                WayCreator.CreateWay();
            }
        
            GUILayout.EndVertical();
        }

        private static void DrawCreateNewScene()
        {
            if (GUILayout.Button("Create New Scene"))
            {
                DebugClass.Warning("Şuanlık çalışmıyor");
            }
        }
    }
}