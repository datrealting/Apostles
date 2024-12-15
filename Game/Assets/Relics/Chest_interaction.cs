using System.Collections.Generic;
using UnityEngine;

public class Chest_interaction : MonoBehaviour
{
    public List<GameObject> prefabList = new List<GameObject>();

    void Update(){
        if (Input.GetKey("e")){
            int prefabIndex = UnityEngine.Random.Range(0,prefabList.Count-1);
            print(prefabIndex);
            print(prefabList);
            Instantiate(prefabList[prefabIndex]);
            gameObject.GetComponent<ChestOpening>().isOpen = true;
            gameObject.GetComponent<Chest_interaction>().enabled = false;
            
        }
    }

}
