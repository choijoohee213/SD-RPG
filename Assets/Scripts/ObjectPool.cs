using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour {

    [SerializeField]
    private GameObject[] objectPrefabs;

    private List<GameObject> pooledObjs = new List<GameObject>();

    [SerializeField]
    private Transform HPCanvas, Particle;

    private GameObject Generate(string type, bool isActive) {
        for(int i = 0; i < objectPrefabs.Length; i++) {
            if(objectPrefabs[i].name.Equals(type)) {
                GameObject newObject = Instantiate(objectPrefabs[i]);
                newObject.SetActive(isActive);

                if(objectPrefabs[i].name.Contains("HealthBar"))
                    newObject.transform.SetParent(HPCanvas);
                if(objectPrefabs[i].name.Contains("Particle"))
                    newObject.transform.SetParent(Particle);

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
                if(obj.name.Equals("HealthBar") && obj.transform.GetChild(0).GetComponent<Image>().fillAmount != 0)
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