using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject buildingObject, UIPlay, UILoad, UIPause, MainMenuHandler, SpawnButton, UIWin;
    public TMP_Text spawnButtonText;
    public LayerMask ground;
    public Building building;
    public GameObject designatedPlot=null;
    public ResourceManager resourceManager;
    public AudioHandler audioHandler;
    public bool buildingInConstruction;
    public bool gameLoading=true;
    public bool gamePaused=false;
    public bool winCondition=false;
    public Building.ResourceRequirements spawnRequirements;
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
        audioHandler = GameObject.Find("AudioHandler").GetComponent<AudioHandler>();
        buildingInConstruction=false;
        ground=LayerMask.GetMask("Ground");
        UIPlay.SetActive(false);
        UILoad.SetActive(true);
        Time.timeScale=0;
    }

    void Update()
    {
        if(winCondition==true){
            UIPause.SetActive(false);
            UIPlay.SetActive(false);
            UIWin.SetActive(true);
            gamePaused=true;
            
        }

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
            if(designatedPlot.tag == "Stockpile"){
                designatedPlot.layer = LayerMask.NameToLayer("Stockpile");
            }
            else if(designatedPlot.tag == "Castle"){
                winCondition=true;
                audioHandler.PlayWinNoise(); 
            }
            else{
                designatedPlot.layer = LayerMask.NameToLayer("Building");
            }
            buildingInConstruction=false;
        }

        if(SpawnButton.activeSelf == false){
            spawnButtonText.text = "Spawn";
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
        buildingObject = GameObject.Find("WoodHouse");
        buildingObject.GetComponent<Building>().setSpawnMax(3);
        spawnRequirements = buildingObject.GetComponent<Building>().requirements = new Building.ResourceRequirements(0,5,0);
        building = buildingObject.GetComponent<Building>();


        if(buildingInConstruction==false && requirements.compareTo(woodAvailable,foodAvailable,stoneAvailable)){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                designatedPlot = Instantiate(buildingObject, hit.point+ new Vector3(0,2.2f,0), Quaternion.Euler(0,-180,0));
                designatedPlot.GetComponent<Building>().inConstruction=true;
                designatedPlot.GetComponent<Building>().isSpawner=true;
                buildingInConstruction=true;
            }
        }
    }

    public void stoneHouseButton(){
        requirements = new ResourceRequirements(0,0,5);     // wood, food, stone
        int woodAvailable=resourceManager.getNumWood();
        int foodAvailable=resourceManager.getNumFood();
        int stoneAvailable=resourceManager.getNumStone();
        buildingObject = GameObject.Find("StoneHouse");
        buildingObject.GetComponent<Building>().setSpawnMax(6);
        spawnRequirements = buildingObject.GetComponent<Building>().requirements = new Building.ResourceRequirements(0,5,0);
        building = buildingObject.GetComponent<Building>();


        if(buildingInConstruction==false && requirements.compareTo(woodAvailable,foodAvailable,stoneAvailable)){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                designatedPlot = Instantiate(buildingObject, hit.point+ new Vector3(0,2.2f,0), Quaternion.Euler(0,-180,0));
                designatedPlot.GetComponent<Building>().inConstruction=true;
                designatedPlot.GetComponent<Building>().isSpawner=true;
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
                designatedPlot = Instantiate(buildingObject, hit.point+ new Vector3(0,1.5f,0), buildingObject.transform.rotation);
                designatedPlot.GetComponent<Building>().inConstruction=true;
                designatedPlot.tag = "Stockpile";
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
        buildingObject.GetComponent<Building>().setSpawnMax(10);
        spawnRequirements = buildingObject.GetComponent<Building>().requirements = new Building.ResourceRequirements(0,5,0);
        building = buildingObject.GetComponent<Building>();


        if(buildingInConstruction==false && requirements.compareTo(woodAvailable,foodAvailable,stoneAvailable)){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                designatedPlot = Instantiate(buildingObject, hit.point+ new Vector3(0,3.7f,0), Quaternion.Euler(0,-180,0));
                designatedPlot.GetComponent<Building>().inConstruction=true;
                designatedPlot.GetComponent<Building>().isSpawner=true;
                buildingInConstruction=true;
            }
        }
    }

    public void castleButton(){
        requirements = new ResourceRequirements(20,0,30);     // wood, food, stone
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
                designatedPlot = Instantiate(buildingObject, hit.point+ new Vector3(0,5.5f,0), Quaternion.Euler(0,-180,0)); //-180 degree rotation needed
                designatedPlot.GetComponent<Building>().inConstruction=true;
                buildingInConstruction=true;
            }
        }
    }

    public void spawnButton(){
        int woodAvailable=resourceManager.getNumWood();
        int foodAvailable=resourceManager.getNumFood();
        int stoneAvailable=resourceManager.getNumStone();

        if(building.spawnMax>0 && spawnRequirements.compareTo(woodAvailable,foodAvailable,stoneAvailable) && spawnButtonText.text!="Spawn"){
            Instantiate(building.spawnCreature, building.transform.position + new Vector3(5f,0,0), buildingObject.transform.rotation);
            building.decrementSpawnCount();
            spawnRequirements.decrement(resourceManager);
        }
        else{
            spawnButtonText.text = "Spawn "+ building.spawnCreature.name + " \n("+spawnRequirements.foodReq+"F "+spawnRequirements.woodReq+"W " + spawnRequirements.stoneReq+"S)";
        }
        // Debug.Log("BUILDING: "+ building.ToString() + "SPAWN CREATURE: "+ building.spawnCreature.ToString());
    }

    public void quitApplication(){
        Application.Quit();
    }
}
