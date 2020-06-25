using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public Camera Cam { get; set; }
    public ObjectPool objectPool;
    public PlayerBase player;

    // Start is called before the first frame update
    private void Awake() {
        Cam = Camera.main;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }


}