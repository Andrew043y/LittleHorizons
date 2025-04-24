using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject buildingObject;
    public LayerMask ground;
    public Building building;
    public GameObject designatedPlot=null;
    public ResourceManager resourceManager;
    public bool buildingInConstruction;
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
        buildingInConstruction=false;
        building = buildingObject.GetComponent<Building>();
        ground=LayerMask.GetMask("Ground");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && buildingInConstruction){        //if right click while building under construction
            Destroy(designatedPlot);
            buildingInConstruction=false;
        }
        
        if(Input.GetMouseButtonDown(0) && buildingInConstruction){        //if left click while building under construction
            int woodAvailable=resourceManager.getNumWood();
            int foodAvailable=resourceManager.getNumFood();
            int stoneAvailable=resourceManager.getNumStone();
            requirements.decrement(resourceManager);

            designatedPlot.GetComponent<Building>().inConstruction=false;
            buildingInConstruction=false;
        }
    }

    public void woodHouseButton(){
        requirements = new ResourceRequirements(5,0,0);
        int woodAvailable=resourceManager.getNumWood();
        int foodAvailable=resourceManager.getNumFood();
        int stoneAvailable=resourceManager.getNumStone();


        if(buildingInConstruction==false && requirements.compareTo(woodAvailable,foodAvailable,stoneAvailable)){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                designatedPlot = Instantiate(buildingObject, hit.point, Quaternion.identity);
                designatedPlot.GetComponent<Building>().inConstruction=true;
                buildingInConstruction=true;
            }
        }
    }
}
