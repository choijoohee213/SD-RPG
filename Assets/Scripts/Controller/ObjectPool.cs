using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour {

    public GameObject[] objectPrefabs;
    private List<GameObject> pooledObjs = new List<GameObject>();

    public Transform Canvas, HPCanvas, Particles, Items, QuestArea;

    public GameObject Generate(string type, bool isActive) {
        for(int i = 0; i < objectPrefabs.Length; i++) {
            if(objectPrefabs[i].name.Equals(type)) {
                GameObject newObject = Instantiate(objectPrefabs[i]);
                newObject.SetActive(isActive);

                if(objectPrefabs[i].name.Contains("HealthBar"))
                    newObject.transform.SetParent(HPCanvas);
                else if(objectPrefabs[i].name.Contains("Particle"))
                    newObject.transform.SetParent(Particles);
                else if(objectPrefabs[i].name.Contains("DamageText"))
                    newObject.transform.SetParent(Canvas);
                else if(objectPrefabs[i].name.Contains("Item"))
                    newObject.transform.SetParent(Items);
                else if(objectPrefabs[i].name.Contains("QuestListSlot"))
                    newObject.transform.SetParent(QuestArea);

                pooledObjs.Add(newObject);
                newObject.name = type;
                return newObject;
            }
        }
        return null;
    }

    public GameObject GetObject(string type) {
        foreach(GameObject obj in pooledObjs) {
            if(obj.name.Equals(type) && !obj.activeInHierarchy) {
                if(obj.name.Equals("HealthBar") && obj.transform.GetChild(0).GetComponent<Image>().fillAmount != 0
                    || obj.name.Equals("QuestListSlot") && obj.GetComponent<QuestSlot>().added)
                    continue;
                obj.SetActive(true);
                return obj;
            }
        }

        return Generate(type, true);
    }

    public GameObject Soldiers(string level) {
        foreach(GameObject obj in pooledObjs) {
            if(obj.name.Equals(level) && obj.activeSelf) {
                return obj;
            }
        }
        return null;
    }

    public void ReleaseObject(GameObject gameObject) {
        gameObject.SetActive(false);
    }
}