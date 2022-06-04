using UnityEngine;
using WhiteRoom.Utils.Debug;

namespace WhiteRoom.HyperCasualGameCreator.Panels
{
    public class HierarchyPanel : Panel
    {
        public static void Draw()
        {
            SetStyles();
            
            DrawLabel("Hierarchy Panel");
            
            GUILayout.BeginVertical("HelpBox");
            
            if (GUILayout.Button("Setup Hierarchy"))
            {
                HierarchyLayout.SetupHierarchy();
                DebugClass.Log("Hierarchy is Ready");
            }
            
            GUILayout.EndVertical();
        }
    }
}