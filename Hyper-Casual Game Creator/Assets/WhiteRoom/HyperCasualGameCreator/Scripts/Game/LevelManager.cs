using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else if (Singleton != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadLevel();
    }
    
    public void NextLevel()
    {
        var index = GetLevel() + 1;
        if (SceneManager.sceneCountInBuildSettings - 1 < index) index = 0;
        SceneManager.LoadScene(index);
        SaveLevel(index);
    }
    
    public void SaveLevel(int levelIndex)
    {
        CheckLevelSaveData();
        PlayerPrefs.SetInt("CurrentLevel",levelIndex);
    }

    private void LoadLevel()
    {
        var index = GetLevel();
        SceneManager.LoadScene(index);
    }

    private int GetLevel()
    {
        CheckLevelSaveData();
        return PlayerPrefs.GetInt("CurrentLevel");
    }

    private void CheckLevelSaveData()
    {
        if(!PlayerPrefs.HasKey("CurrentLevel"))
            PlayerPrefs.SetInt("CurrentLevel",0);
    }
}
