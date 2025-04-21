using UnityEngine;

public class VillagerAI : BaseAI
{
    public Creature creature;
    public GameObject foodReturnObject; //stockpile
    public GameObject targetGatherable; //piece of food
    public float checkFoodRadius = 1.25f;           // May want to make the radius smaller for full version, like 1f or lower 

    protected void Awake()
    {
        base.Awake();
        creature=GetComponent<Creature>();
        ChangeState(WanderState);
    }
    void CheckForFood(){
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

        CheckForFood();
        if(targetGatherable!=null){
            creature.isSelected=false;
            ChangeState(getGatherableState);
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
            ChangeState(ReturnFoodState);
            return;
        }
        
     }
    void ReturnFoodState(){
        stateImIn="Return Food State";
        if(creature.isSelected==false){     //I want to change state of villagerAI to be wandering if it is selected while returning food   **
            creature.MoveToward(foodReturnObject.transform.position);
        }
        if(Vector3.Distance(targetGatherable.transform.position, foodReturnObject.transform.position)<2f){
            Destroy(targetGatherable);
            ChangeState(WanderState);
            return;
        }
    }
    public void MovingState(Vector3 destination){
        stateImIn="Moving State";
    }

}
