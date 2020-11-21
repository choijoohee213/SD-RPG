using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour {
    public GameObject EndCursor;
    public int CharPerSeconds;
    private string targetMsg;
    private Text msgText;
    private int index;
    private float interval;

    private void Awake() {
        msgText = GetComponent<Text>();
    }

    public void SetMsg(string msg) {
        targetMsg = msg;
        EffectStart();
    }

    private void EffectStart() {
        EndCursor.SetActive(false);
        msgText.text = "";
        index = 0;

        interval = 1.0f / CharPerSeconds;
        Invoke("Effecting", interval);
    }

    private void Effecting() {
        if(msgText.text == targetMsg) {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        index++;

        Invoke("Effecting", interval);
    }

    private void EffectEnd() {
        EndCursor.SetActive(true);
    }
}