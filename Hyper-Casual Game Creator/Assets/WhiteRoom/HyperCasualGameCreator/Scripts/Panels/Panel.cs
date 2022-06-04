using UnityEditor;
using UnityEngine;

namespace WhiteRoom.HyperCasualGameCreator.Panels
{
    public abstract class Panel : EditorWindow
    {
        private static GUIStyle _panelTitleStyle;
        protected static void DrawLabel(string title,int fontSize = 20)
        {
            GUILayout.BeginVertical("box");
            _panelTitleStyle.fontSize = fontSize;
            GUILayout.Label(title,_panelTitleStyle);
            GUILayout.EndHorizontal();
        }
        
        public static void SetStyles()
        {
            _panelTitleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter,
                stretchWidth = true,
            };
        }
    }
}