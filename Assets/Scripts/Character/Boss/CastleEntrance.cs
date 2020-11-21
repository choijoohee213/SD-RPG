using UnityEngine;

public class CastleEntrance : MonoBehaviour {
    private BossQuest bossQuest;

    private void Awake() {
        bossQuest = BossQuest.Instance;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer.Equals(9)) {
            bossQuest.EnterCastle();
        }
    }
}