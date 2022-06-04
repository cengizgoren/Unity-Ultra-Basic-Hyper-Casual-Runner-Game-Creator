using UnityEngine;
using UnityEditor;
using WhiteRoom.Utils.Debug;

namespace WhiteRoom.HyperCasualGameCreator.Panels
{
    public class GamePanel : Panel
    {

        private static GameObject ui_parent;
        public static void Draw()
        {
            SetStyles();

            DrawLabel("Game Panel");

            DrawManagers();
            DrawUI();
        }

        private static void DrawManagers()
        {
            GUILayout.BeginVertical("HelpBox");
        
            DrawLabel("Managers",15);
            if (GUILayout.Button("Create Managers"))
            {
                var gameManager = Resources.Load<GameObject>("Prefab/Manager/GameManager");
                var levelManager = Resources.Load<GameObject>("Prefab/Manager/LevelManager");
                var inputManager = Resources.Load<GameObject>("Prefab/Manager/InputManager");
                var prefabBrusher = Resources.Load<GameObject>("Prefab/Manager/PrefabBrusher");

                var parent = GameObject.Find("--------Manager--------");
            
                if(GameObject.Find("GameManager") && GameObject.Find("InputManager") && GameObject.Find("LevelManager"))
                    DebugClass.Log("Managers is already");
                else
                    DebugClass.Log("Created Managers");
            
                if (!GameObject.Find("GameManager"))
                {
                    var i_g_manager = Instantiate(gameManager,parent != null ? parent.transform : null);
                    i_g_manager.name = "GameManager";
                    Undo.RecordObject(i_g_manager,"GameManager");
                }
                
                if (!GameObject.Find("LevelManager"))
                {
                    var i_l_manager = Instantiate(levelManager,parent != null ? parent.transform : null);
                    i_l_manager.name = "LevelManager";
                    Undo.RecordObject(i_l_manager,"LevelManager");
                }
                
                if (!GameObject.Find("InputManager"))
                {
                    var i_i_manager = Instantiate(inputManager,parent != null ? parent.transform : null);
                    i_i_manager.name = "InputManager";
                    Undo.RecordObject(i_i_manager,"InputManager");
                }
            
                if (!GameObject.Find("PrefabBrusher"))
                {
                    var i_p_brusher = Instantiate(prefabBrusher,parent != null ? parent.transform : null);
                    i_p_brusher.name = "PrefabBrusher";
                    Undo.RecordObject(i_p_brusher,"PrefabBrusher");
                }
            }
        
            GUILayout.EndVertical();
        }

        private static void DrawUI()
        {
            GUILayout.BeginVertical("HelpBox");
            
            DrawLabel("UI",15);

            if (GUILayout.Button("Create Game Panel"))
            {
                CreateUIObject("Canvas_Game");
            }
            
            if (GUILayout.Button("Create Coin Panel"))
            {
                CreateUIObject("Canvas_Coin");
            }
            
            if (GUILayout.Button("Create Menu Panel"))
            {
               CreateUIObject("Canvas_menu");
            }

            GUILayout.EndVertical();
        }

        private static void CreateUIObject(string objectName)
        {
            if (ui_parent == null) ui_parent = GameObject.Find("-----------UI-----------");
            
            var pref = Resources.Load<GameObject>($"Prefab/UI/{objectName}");
            var obj = Instantiate(pref, ui_parent != null ? ui_parent.transform : null);
            obj.name = objectName;
        }
    }
}