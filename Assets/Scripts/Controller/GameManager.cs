using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public Camera Cam;
    public ObjectPool objectPool;
    public PlayerBase player;

    private void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}