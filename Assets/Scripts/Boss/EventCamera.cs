using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCamera : MonoBehaviour
{
    public GameObject[] ExclamationMarks;
    private Text DialogText;
    private GameObject BossDialogUI;

    private Vector3 BossPos = new Vector3(-30.9f, 28.8f, 235.2f);
    private Vector3 PrincessPos = new Vector3(-49.1f, 26f, 262.3f);

    private bool NextScene = false;

    private void Awake() {
        DialogText = UIManager.Instance.dialogText;
        BossDialogUI = UIManager.Instance.BossDialogUI;
    }

    public void StartAnimation() {
        //이벤트카메라의 시작위치를 메인카메라 위치로 지정
        transform.position = GameManager.Instance.Cam.transform.position;
        
        //메인카메라 비활성화
        GameManager.Instance.Cam.gameObject.SetActive(false);

        StartCoroutine(CameraStop());
    }

    IEnumerator CameraStop() {
        yield return new WaitForSeconds(2f);
        
        //느낌표 오브젝트 활성화
        ExclamationMarks[0].SetActive(true);
        ExclamationMarks[1].SetActive(true);
        ExclamationMarks[2].SetActive(true);

        yield return new WaitForSeconds(2f);
        StartCoroutine(MoveToBoss());
    }

    IEnumerator MoveToBoss() {
        while(transform.position != BossPos) {
            transform.position = Vector3.MoveTowards(transform.position, BossPos, 22f * Time.deltaTime);
            yield return null;
        }
        ExclamationMarks[0].SetActive(false);
        ExclamationMarks[1].SetActive(false);
        ExclamationMarks[2].SetActive(false);

        //다이얼로그창 활성화를 위해 해당 Canvas를 활성화
        BossDialogUI.SetActive(true);

        //사용자가 다이얼로그창을 누르기전까지 대기
        yield return new WaitUntil(() => NextScene);

        BossDialogUI.SetActive(false);
        StartCoroutine(MoveToPrincess());
    }

    IEnumerator MoveToPrincess() {
        NextScene = false;

        while(transform.position != PrincessPos) {
            transform.position = Vector3.MoveTowards(transform.position, PrincessPos, 15f * Time.deltaTime);
            yield return null;
        }
        BossDialogUI.SetActive(true);
        DialogText.text = "구해주세요..!!!흑흑..";

        yield return new WaitUntil(() => NextScene);
        BossDialogUI.SetActive(false);
        StartCoroutine(MoveToPlayer());
    }

    IEnumerator MoveToPlayer() {
        Vector3 playerPos = GameManager.Instance.Cam.transform.position;
        while(transform.position != playerPos) {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, 30f * Time.deltaTime);
            yield return null;
        }

        UIManager.Instance.OnOffCanvas(true, true, true);
        UIManager.Instance.BossUI.SetActive(true);
        BossDialogUI.SetActive(false);

        //메인카메라로 시점을 바꾸기 위해 메인카메라 활성화
        GameManager.Instance.Cam.gameObject.SetActive(true);
        gameObject.SetActive(false);

        BossQuest.Instance.OnAnimation = false;
    }

    public void OnNextDialogBtn() {
        NextScene = true;
    }
}
