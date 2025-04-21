using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    public CharacterController controller;
    // public AnimationStateChanger animationStateChanger;  TODO
    public float speed = 5f;
    public Material defaultHeadMaterial;
    public Material defaultTorsoMaterial;
    public bool isSelected=false;
    public LayerMask ground;
    public LayerMask gatherable;
    public GameObject groundMarker;
    public Material gatherMaterial;
    public BaseAI AI;

    void Awake()
    {
        AI=GetComponent<VillagerAI>();
        controller = GetComponent<CharacterController>();
        ground=LayerMask.GetMask("Ground");
        gatherable=LayerMask.GetMask("Gatherable");

    }

    void Update()
    {
        if(isSelected){
            
            selectThis();

        }
        else{
            unselectThis();
        }

        // if(Input.GetMouseButtonDown(1)){        //right click movement
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor
        //     if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
        //             if(isSelected){
        //                 agent.SetDestination(hit.point);
        //                 GameObject groundMarkerClone=Instantiate(groundMarker, hit.point, Quaternion.identity);

        //                 groundMarkerClone.GetComponent<GroundMarker>().setMoving();
        //                 unselectThis();
        //             }
        //             // Debug.Log(hit.transform.position);
        //     }
        //     else if(Physics.Raycast(ray, out hit, Mathf.Infinity, food)){
        //         if(isSelected){
        //             agent.SetDestination(hit.point);
        //             GameObject groundMarkerClone=Instantiate(groundMarker, hit.point, Quaternion.identity);

        //             groundMarkerClone.GetComponent<GroundMarker>().setGathering();
        //             unselectThis();
        //         }
        //     }
        // }
    }

    public void Stop(){

    }

    public void Move(Vector3 direction)
    {
        if(direction == Vector3.zero){
            return;
        }
        direction.Normalize();
        direction.y=0f;
        controller.Move(direction*speed*Time.deltaTime);
    }

    public void MoveToward(Vector3 target){ //  make villagers move towards only x & z, remove y aspect so they dont float
        Vector3 direction = target - transform.position;
        direction.y=0f;
        Move(direction);
    }

    public void pickup(GameObject collectable){
        collectable.transform.parent = transform;
        collectable.transform.localPosition = new Vector3(0,2.5f,0);
    }

    public void selectThis(){
        for(int i=0; i<transform.childCount; i++){
            if(transform.GetChild(i).CompareTag("Gatherable")==false){      //check if child objects in the creature selected are not part of its body
                transform.GetChild(i).GetComponent<Renderer>().material.color=Color.green;
            }
        }
    }
    public void unselectThis(){
        isSelected=false;
        transform.GetChild(0).GetComponent<Renderer>().material=defaultHeadMaterial;    //reset head material
        transform.GetChild(1).GetComponent<Renderer>().material=defaultTorsoMaterial;   //reset torso material
    }
}
