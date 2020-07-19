using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour {
    public Quest quest;
    public Text questTitleText, questStateText;
    public QuestGiver QuestGiverNPC { get; set; }

    public int slotNum;

    private bool selected = false;

    public bool Selected {
        get { return selected; }
        set {
            if(value)
                questTitleText.color = Color.red;
            else
                questTitleText.color = Color.black;
        }
    }

    public void Init() {
        transform.SetAsFirstSibling();

        quest.state = QuestState.Progressing;
        transform.localScale = new Vector3(1, 1, 1);

        questTitleText.text = quest.title;
        questStateText.text = "";

        Selected = false;
    }

    public void OnSlotBtn() {
        if(!Selected) {
            QuestUIScript.Instance.ShowQuestContent(this);
            Selected = true;
        }
    }

    public void CheckCompletable() {
        if(quest.IsCompleteObjectives) {
            questStateText.text = "완료가능";
            quest.state = QuestState.Completable;
        }
        else
            questStateText.text = "";

        QuestGiverNPC.SetSpeechBubble();
    }

    public void RemoveQuestSlot() {
        quest = null;
        gameObject.SetActive(false);
    }
}