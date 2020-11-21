using System.Collections.Generic;
using UnityEngine;

public class DropItemController : MonoBehaviour {
    public int DropProbability = 4;

    [SerializeField]
    private List<ItemProbability> itemProbabilities = null;

    private Transform player;

    private void Awake() {
        player = GameManager.Instance.player.transform;

        //확률이 높은 순으로 내림차순 정렬
        itemProbabilities.Sort(delegate (ItemProbability x, ItemProbability y) {
            return x.DropProbability.CompareTo(y.DropProbability);
        });
    }

    public void DropItem() {
        var dropNum = Random.Range(0, DropProbability);
        for(int i = 0; i < dropNum; i++)
            CreateItemObj();
    }

    private void CreateItemObj() {
        var randomItem = Random.Range(1, 11);
        int index = 0;

        for(int i = 0; i < itemProbabilities.Count; i++) {
            if(randomItem <= itemProbabilities[i].DropProbability) {
                index = i;
                break;
            }
        }

        ItemPickup itemPickup = GameManager.Instance.objectPool.GetObject(itemProbabilities[index].itemName).GetComponent<ItemPickup>();
        itemPickup.Init(transform);
    }
}