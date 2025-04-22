using UnityEngine;
using UnityEngine.AI;

public class VillagerAI : BaseAI
{
    public Creature creature;
    public GameObject foodReturnObject; //stockpile
    public GameObject targetGatherable; //piece of food
    public GameObject gatherCircle;
    public GameObject carriedGatherable;
    NavMeshAgent agent;
    public LayerMask ground;
    public LayerMask gatherable;
    public GameObject groundMarker;
    public Material gatherMaterial;
    public Vector3 destination;
    public float checkFoodRadius = 1f;           // May want to make the radius smaller for full version, like 1f or lower 

    protected void Awake()
    {
        base.Awake();
        agent=GetComponent<NavMeshAgent>();
        creature=GetComponent<Creature>();
        ground=LayerMask.GetMask("Ground");
        gatherable=LayerMask.GetMask("Gatherable");
        ChangeState(WanderState);
    }

    void Update()
    {
        base.Update();
        if(creature.isSelected){
            if(Input.GetMouseButtonDown(1)){        //right click movement
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, gatherable)){
                    GameObject gatherableItem = hit.transform.gameObject;
                    ChangeState(MovingState);
                    agent.SetDestination(hit.point);
                    destination = hit.point;
                    Debug.Log("Gatherable hit!");
                    Vector3 gatherablePos = gatherableItem.transform.position;
                    gatherablePos.y=0.13f;
                    GameObject gatherCircleClone=Instantiate(gatherCircle, gatherablePos, Quaternion.identity);
                    // gatherCircleClone.GetComponent<GroundMarker>().setGathering();
                    creature.unselectThis();
            }
            else if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                Vector3 hitPoint = hit.point;
                        ChangeState(MovingState);
                        agent.SetDestination(hit.point);
                        Debug.Log("Raycast hit: "+ hit.transform.gameObject.name);
                        destination = hit.point;
                        Debug.Log("Ground hit!");
                        GameObject groundMarkerClone=Instantiate(groundMarker, hit.point, Quaternion.identity);

                        groundMarkerClone.GetComponent<GroundMarker>().setMoving();
                        creature.unselectThis();
                    // Debug.Log(hit.transform.position);
            }
        }
        }
    }

    void CheckForGatherable(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkFoodRadius, LayerMask.GetMask("Gatherable"));
        if(colliders.Length>0){
            targetGatherable=colliders[0].gameObject;
            targetGatherable.gameObject.layer = LayerMask.NameToLayer("ReservedGatherable");
        }
    }
    Vector3 wanderPosition=Vector3.zero;
    public void WanderState(){
        stateImIn = "Wander State";
        if(stateTick==1){
            wanderPosition = transform.position + new Vector3(Random.Range(-10f,10f), 0, Random.Range(-10f,10f));   //when villager idle, move slightly
        }
        // creature.MoveToward(wanderPosition);
        if(carriedGatherable==null){
            CheckForGatherable();
            if(targetGatherable!=null){
                creature.isSelected=false;
                ChangeState(getGatherableState);
                return;
            }
        }
        else if(carriedGatherable!=null && Vector3.Distance(transform.position, foodReturnObject.transform.position)<6f){
            ChangeState(ReturnFoodState);
            return;
        }

        // if(Vector3.Distance(transform.position, wanderPosition)<1f){
        //     ChangeState(PauseState);
        //     return;
        // }
    }
    float pauseTime=0f;
     void PauseState(){
        stateImIn="Pause State";
        creature.Stop();
        return;

        // if(stateTick==1){
        //     pauseTime=Random.Range(2f,5f);
        // }
        
        // if(stateTime>pauseTime){
        //     ChangeState(WanderState);
        //     return;
        // }
     }
     public void getGatherableState(){
        stateImIn="Get Food State";
        if(targetGatherable==null){
            ChangeState(PauseState);
            return;
        }
        creature.MoveToward(targetGatherable.transform.position);
        if(Vector3.Distance(transform.position, targetGatherable.transform.position)<2f){
            creature.pickup(targetGatherable);
            carriedGatherable=targetGatherable;
            ChangeState(ReturnFoodState);
            return;
        }
        
     }
    void ReturnFoodState(){
        stateImIn="Return Food State";
        if(creature.isSelected==false){     //I want to change state of villagerAI to be wandering if it is selected while returning food   **
            creature.MoveToward(foodReturnObject.transform.position);
        }
        if(Vector3.Distance(transform.position, foodReturnObject.transform.position)<6f){
            Destroy(carriedGatherable);
            carriedGatherable=null;
            ChangeState(WanderState);
            return;
        }
    }
    void MovingState(){
        stateImIn="Moving State";
        float distance = Vector3.Distance(transform.position, destination);
        if(distance<2f){
            ChangeState(WanderState);
        }
    }

}
