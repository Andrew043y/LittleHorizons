using System.Collections;
using UnityEngine;

public class GroundMarker : MonoBehaviour
{
    public bool isMoving=false;
    public bool isAttacking=false;
    public bool isGathering=false;
    float currentScale = 1.25f;
    private float checkCollisionRadius=2f;
    public float scaleSpeed = 1;
    public Material movingMaterial;
    public Material attackMaterial;
    public Material gatherMaterial;

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
            transform.GetComponent<Renderer>().material=gatherMaterial;
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
            transform.localScale = Vector3.one * currentScale;
            yield return null;
        }
        // isMoving=false;

        Destroy(gameObject);
    }

    public void checkForGatherable(){
        Collider[] gatherColliders = Physics.OverlapSphere(transform.position, checkCollisionRadius, LayerMask.GetMask("Gatherable"));
        if(gatherColliders.Length>0){
            setGathering();
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag("Gatherable")){
    //         setGathering();
    //     }
    //     else if(other.CompareTag("Attackable")){
    //         setAttacking();
    //     }
    //     else{
    //         setMoving();
    //     }
    // }
}
