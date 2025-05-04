using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject buildingObject;
    public GameObject UIPlay;
    public GameObject UILoad;
    public GameObject UIPause;
    public GameObject MainMenuHandler;
    public LayerMask ground;
    public Building building;
    public GameObject designatedPlot=null;
    public ResourceManager resourceManager;
    public bool buildingInConstruction;
    public bool gameLoading=true;
    public bool gamePaused=false;
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
        ground=LayerMask.GetMask("Ground");
        UIPlay.SetActive(false);
        UILoad.SetActive(true);
        Time.timeScale=0;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape) && gameLoading==false){
            gamePaused=togglePause(gamePaused);
            UIPause.SetActive(true);
            UIPlay.SetActive(false);
            // Time.timeScale=1;
        }

        if(gameLoading==false && gamePaused == false){
            UIPlay.SetActive(true);
            UILoad.SetActive(false);
            Time.timeScale=1;
            MainMenuHandler.GetComponent<MainMenuHandler>().resume();
        }
        else{
            Time.timeScale=0;
        }

        if(Input.GetMouseButtonDown(1) && buildingInConstruction){        //if right click while building under construction
            Destroy(designatedPlot);
            buildingInConstruction=false;
        }
        
        if(Input.GetMouseButtonDown(0) && buildingInConstruction && designatedPlot.GetComponent<Building>().placeable==true){        //if left click while building under construction
            int woodAvailable=resourceManager.getNumWood();
            int foodAvailable=resourceManager.getNumFood();
            int stoneAvailable=resourceManager.getNumStone();
            requirements.decrement(resourceManager);

            designatedPlot.GetComponent<Building>().inConstruction=false;
            designatedPlot.layer = LayerMask.NameToLayer("Building");
            buildingInConstruction=false;
        }
    }

    bool togglePause(bool val){
        if(val==true){
            return false;
        }
        else{
            return true;
        }
    }


    public void woodHouseButton(){
        requirements = new ResourceRequirements(5,0,0);     // wood, food, stone
        int woodAvailable=resourceManager.getNumWood();
        int foodAvailable=resourceManager.getNumFood();
        int stoneAvailable=resourceManager.getNumStone();
        buildingObject = GameObject.Find("Wood House");
        building = buildingObject.GetComponent<Building>();


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

    public void stoneHouseButton(){
        requirements = new ResourceRequirements(0,0,5);     // wood, food, stone
        int woodAvailable=resourceManager.getNumWood();
        int foodAvailable=resourceManager.getNumFood();
        int stoneAvailable=resourceManager.getNumStone();
        buildingObject = GameObject.Find("Stone House");
        building = buildingObject.GetComponent<Building>();


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

    public void stockpileButton(){
        requirements = new ResourceRequirements(3,0,0);     // wood, food, stone
        int woodAvailable=resourceManager.getNumWood();
        int foodAvailable=resourceManager.getNumFood();
        int stoneAvailable=resourceManager.getNumStone();
        buildingObject = GameObject.Find("Stockpile");
        building = buildingObject.GetComponent<Building>();


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

    public void barracksButton(){
        requirements = new ResourceRequirements(5,0,5);     // wood, food, stone
        int woodAvailable=resourceManager.getNumWood();
        int foodAvailable=resourceManager.getNumFood();
        int stoneAvailable=resourceManager.getNumStone();
        buildingObject = GameObject.Find("Barracks");
        building = buildingObject.GetComponent<Building>();


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

    public void castleButton(){
        requirements = new ResourceRequirements(0,0,0);     // wood, food, stone
        int woodAvailable=resourceManager.getNumWood();
        int foodAvailable=resourceManager.getNumFood();
        int stoneAvailable=resourceManager.getNumStone();
        buildingObject = GameObject.Find("Castle");
        building = buildingObject.GetComponent<Building>();


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
