using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCamera : MonoBehaviour
{
    public GameObject[] ExclamationMarks;
    private Vector3 BossPos = new Vector3(-30.9f, 28.8f, 235.2f);
    private Vector3 PrincessPos = new Vector3(-49.1f, 26f, 262.3f);

    public void CameraMove() {
        transform.position = GameManager.Instance.Cam.transform.position;
        GameManager.Instance.Cam.gameObject.SetActive(false);

        StartCoroutine(CameraStop());
    }

    IEnumerator CameraStop() {
        yield return new WaitForSeconds(2f);
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

        yield return new WaitForSeconds(2f);
        StartCoroutine(MoveToPrincess());
    }

    IEnumerator MoveToPrincess() {
        while(transform.position != PrincessPos) {
            transform.position = Vector3.MoveTowards(transform.position, PrincessPos, 15f * Time.deltaTime);
            yield return null;
        }
    }
}
