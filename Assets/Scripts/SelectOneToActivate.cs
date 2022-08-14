using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOneToActivate : MonoBehaviour
{
    public List<GameObject> objects;

    void Start()
    {
        int randomIndex = Random.Range(0, objects.Count);
        objects[randomIndex].SetActive(true);

        for(int i = 0; i < objects.Count; i++)
        {
            if(i == randomIndex)
            {
                continue;
            }
            objects[i].SetActive(false);
        }
    }

}
