using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    public Camera Cam;
    public ObjectPool objectPool;
    public PlayerBase player;


    //나중에에지워
    public Item monsteritem;
    public int monsterItemcount;
    

    // Start is called before the first frame update
    private void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update() {
        //지워
        if(Input.GetButtonDown("Jump")) {
            Inventory.Instance.AddMultiple(monsteritem, monsterItemcount);
            Debug.Log("iiiiii");
        }
    }
}