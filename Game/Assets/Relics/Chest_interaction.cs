using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Chest_interaction : MonoBehaviour
{
    public List<GameObject> prefabList = new List<GameObject>();
    public Vector3 offSet = new Vector3(0, -2, 0);
    
    void Update(){
        if (Input.GetKey("e")){
            
            int prefabIndex = UnityEngine.Random.Range(0,prefabList.Count-1);
            print(prefabIndex);
            print(prefabList);
            Instantiate(prefabList[prefabIndex],transform.position + offSet, transform.rotation);
            gameObject.GetComponent<ChestOpening>().isOpen = true;
            gameObject.GetComponent<Chest_interaction>().enabled = false;
            
        }
    }

}
