using UnityEngine;

public class GameManager : Manager
{
    public static GameManager singleton;

    public bool isGameStart; 

    private void Awake()
    {
        singleton = this;
    }
}
