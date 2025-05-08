using UnityEngine;

public class Building : MonoBehaviour
{
    public bool inConstruction, placeable, isSelected, isSpawner;
    private LayerMask ground, gatherable, reservedGatherable, villager, building;
    public GameObject spawnCreature;
    public int spawnMax=0;
    public UIHandler uiHandler;

    public struct ResourceRequirements{
        public int woodReq, foodReq, stoneReq;

        public ResourceRequirements(int wood, int food, int stone){
            woodReq=wood;
            foodReq=food;
            stoneReq=stone;
        }

        public bool compareTo(int w, int f, int s){
            if(w>=woodReq && f>=foodReq && s>=stoneReq){
                return true;
            }
            return false;
        }

        public void decrement(ResourceManager r){
            r.removeWood(woodReq);
            r.removeFood(foodReq);
            r.removeStone(stoneReq);
        }
    }
    public ResourceRequirements requirements;
    void Awake()
    {
        placeable=true;
        inConstruction=false;
        uiHandler = GameObject.Find("UIHandler").GetComponent<UIHandler>();
        ground=LayerMask.GetMask("Ground");
        gatherable = LayerMask.GetMask("Gatherable");
        reservedGatherable = LayerMask.GetMask("ReservedGatherable");
        villager = LayerMask.GetMask("Villager");
        building = LayerMask.GetMask("Building");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inConstruction==true){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, gatherable | reservedGatherable | villager | building)){
                Debug.Log("Obstruction hit!");
                placeable=false;
            }
            else if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                transform.position = new Vector3(hit.point.x,transform.position.y, hit.point.z);
                placeable=true;
            }
        }
    }

    public void setSpawnMax(int num){
        spawnMax = num;
    }

    public void decrementSpawnCount(){
        spawnMax--;
    }

    public void setSpawnCreature(GameObject creature){
        spawnCreature = creature;
    }
}
