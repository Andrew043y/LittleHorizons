using System.Collections;
using UnityEngine;

public class GroundMarker : MonoBehaviour
{
    public bool isMoving=false;
    public bool isAttacking=false;
    public bool isGathering=false;
    public float currentScale=5;      //this is a vector 3 use that ! ! ! !! ! ! 
    private float checkCollisionRadius=2f;
    public float scaleSpeed = 0.5f;
    public Material movingMaterial;
    public Material attackMaterial;
    public Material gatherMaterial;
    void Awake()
    {
        // currentScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            transform.GetComponent<Renderer>().material=movingMaterial;
            StartCoroutine(Shrinking());
        }
        if(isAttacking){
            transform.GetComponent<Renderer>().material=attackMaterial;
            StartCoroutine(Shrinking());
        }
        if(isGathering){
            // transform.GetComponent<Renderer>().material=gatherMaterial;
            StartCoroutine(Shrinking());
        }
    }

    public void setMoving(){
        isMoving=true;
        isAttacking=false;
        isGathering=false;
    }
    public void setAttacking(){
        isAttacking=true;
        isMoving=false;
        isGathering=false;
    }
    public void setGathering(){
        isGathering=true;
        isAttacking=false;
        isMoving=false;
    }

    IEnumerator Shrinking(){
        while(currentScale >= 0f){
            currentScale -= scaleSpeed * Time.deltaTime/10;
            transform.localScale = new Vector3(currentScale, 1, currentScale);
            yield return null;
        }
        // isMoving=false;

        Destroy(gameObject);
    }
}
