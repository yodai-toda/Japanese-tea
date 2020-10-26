using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Description : MonoBehaviour
{
    

     GameObject sousa= null;
    public void HideSousa()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        HideSousa();
    }
}
