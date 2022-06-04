using UnityEditor;
using UnityEngine;
using WhiteRoom.HyperCasualGameCreator.Panels;

public class GameCreatorEditor : EditorWindow
{
    private string _selectedPanelName = "Scene";

    [MenuItem("WhiteRoom/Hyper-Casual Game Creator")]
    private static void Init()
    {
        var window = (GameCreatorEditor) GetWindow(typeof(GameCreatorEditor));
        window.minSize = new Vector2(400,350);
        window.Show();

        var parent = GameObject.Find("Ways");
        WayCreator.Parent = parent != null ? parent.transform : null;
    }

    private void OnGUI()
    {
        DrawPanelButtons();
        SetPanelActive(_selectedPanelName);
    }

    private void DrawPanelButtons()
    {
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Scene Panel")) _selectedPanelName = "Scene";
        if (GUILayout.Button("Game Panel")) _selectedPanelName = "Game";
        if (GUILayout.Button("Player Panel")) _selectedPanelName = "Player";
        if (GUILayout.Button("Hierarchy Panel")) _selectedPanelName = "Hierarchy";
        
        GUILayout.EndHorizontal();
    }

    private void SetPanelActive(string panelName)
    {
        switch (panelName)
        {
            case "Scene":
                ScenePanel.Draw();
                break;
            case "Game":
                GamePanel.Draw();
                break;
            case "Player":
                PlayerPanel.Draw();
                break;
            case "Hierarchy":
                HierarchyPanel.Draw();
                break;
        }
    }
}
