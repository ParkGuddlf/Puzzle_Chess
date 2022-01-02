using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTextEffect : MonoBehaviour
{
    Vector3 dir;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }

    public void resetPos()
    {        
        dir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
    }
}
