using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Camera Cam { get; set; }
    public ObjectPool objectPool;
    public Player player;
    public Vector3 StartPos { get { return new Vector3(74, 21.79191f, 44); } }

    [SerializeField]
    GameObject ResurrectUI;

    [SerializeField]
    Text CountdownText;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void PlayerResurrect() {
        player.gameObject.SetActive(false);
        ResurrectUI.SetActive(true);
        StartCoroutine(ResurrectTimer(3f));
    }

    IEnumerator ResurrectTimer(float seconds) {
        while(seconds-- != 0) {
            CountdownText.text = (seconds + 1).ToString();
            yield return new WaitForSeconds(1f);
        }
        ResurrectUI.SetActive(false);
        player.transform.position = StartPos;
        player.gameObject.SetActive(true);
    }

}
