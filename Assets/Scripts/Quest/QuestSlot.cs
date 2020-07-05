using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{    
    public Quest quest;
    public Text questTitleText, questStateText;
    
    Button slotBtn;
    ColorBlock cb;

    public bool selected { get; set; }
    int slotNum;

    private void Awake() {
        slotBtn = GetComponent<Button>();
        cb = slotBtn.colors;
        selected = false;
    }

    private void OnEnable() {
        if(selected) 
            StartSetColor(new Color(1, 0.9221995f, 0.5607843f, 1));
    }

    public void Init(int _slotNum) {
        quest.state = QuestState.Progressing;
        slotNum = _slotNum;
        transform.localScale = new Vector3(1, 1, 1);

        questTitleText.text = quest.title;
        questStateText.text = "";
    }

    public void OnSlotBtn() {
        if(!selected) {
            StartSetColor(new Color(1, 0.9221995f, 0.5607843f, 1));
            QuestUIScript.Instance.ShowQuestContent(slotNum);
        }
    }

    public void StartSetColor(Color newColor) {
        selected = true;
        StartCoroutine(SetColor(newColor));
    }

    IEnumerator SetColor(Color newColor) {
        while(selected) {
            //선택된 버튼이므로 노란색으로
            if(!selected) break;
            cb.normalColor = newColor;
            slotBtn.colors = cb;
            yield return new WaitForSeconds(0.3f);
        }

        //하얀색으로 다시 변경
        cb.normalColor = new Color(1, 1, 1, 0.6f);
        slotBtn.colors = cb;
    }

    public void CheckCompletable() {
        if(quest.IsCompleteObjectives) {
            questStateText.text = "완료가능";
            quest.state = QuestState.Completable;
        }
        else
            questStateText.text = "";
    }

}
