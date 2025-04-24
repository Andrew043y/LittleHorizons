using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    public GameObject gatherableObject;
    public int gatherablesLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if(gatherablesLeft<=0){
            Destroy(gameObject);
        }
    }

    public void decrement(){
        gatherablesLeft--;
    }
}
