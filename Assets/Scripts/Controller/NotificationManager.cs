using UnityEngine.UI;

/// <summary>
/// 아이템 획득 시, 아이템을 획득하려 했을 때 인벤토리가 꽉찼을 시, 경험치 획득 시, 퀘스트 완료 가능 시
/// </summary>

public class NotificationManager : Singleton<NotificationManager> {
    private int count = 0;
    private int Count { get { return count; } set { if(value >= NotificationTexts.Length) count = 0; else count++; } }

    public bool ShowingMessage = false;

    public Text[] NotificationTexts;

    //알림 텍스트 생성
    private void Generate(string message) {
        Text textObj = NotificationTexts[count];

        if(textObj.gameObject.activeSelf)
            textObj.gameObject.SetActive(false);
        textObj.gameObject.SetActive(true);
        textObj.transform.SetAsLastSibling();  //최신에 생성된 알림이 맨 아래에 위치하기 위함.

        textObj.text = message;
        Count++;
    }

    public void Generate_GetItem(string ItemName, int count) {
        string str = "아이템을 획득하였습니다 (" + ItemName + " +" + count + "개)";
        Generate(str);
    }

    public void Generate_InventoryIsFull() {
        string str = "인벤토리가 꽉 찼습니다. 인벤토리를 비워주세요.";
        Generate(str);
    }

    public void Generate_GetExp(float Exp) {
        string str = "경험치를 얻었습니다. (+" + Exp.ToString() + ")";
        Generate(str);
    }

    public void Generate_CompletableQuest() {
        string str = "퀘스트 완료 가능! NPC를 찾아가세요.";
        Generate(str);
    }
}