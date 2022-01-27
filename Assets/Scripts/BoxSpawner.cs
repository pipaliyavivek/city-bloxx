using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box_Prefab;
    public Rigidbody2D floor_Rigid;
    // Start is called before the first frame update
    public void SpawnBox()
    {
        GameObject box_Obj = Instantiate(box_Prefab);
        floor_Rigid = box_Obj.GetComponent<Rigidbody2D>();
        Vector3 temp = transform.position;
        //To adjust z to keep at 0f
        temp.z = 0f;
        box_Obj.transform.position = temp;
    }
    
}
