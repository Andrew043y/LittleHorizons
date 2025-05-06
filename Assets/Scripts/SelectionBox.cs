// using System.Diagnostics;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{

    private bool isDragging=false;
    private Vector3 mousePositionInitial;
    public RectTransform uiElement;
    private Vector3 mousePositionEnd;
    public RectTransform selectionBox;
    public List<Creature> selectedCreatures=new List<Creature>();
    public Building selectedBuilding;
    public LayerMask ground, villager, building;
    public GameObject groundMarker;
    public UIHandler uiHandler;

    void Awake()
    {
        ground=LayerMask.GetMask("Ground");
        villager=LayerMask.GetMask("Villager");
        building = LayerMask.GetMask("Building");
    }

    // Update is called once per frame
    void Update()
    {
        if(uiHandler.gamePaused == false && uiHandler.gameLoading == false){
            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor

            if(Input.GetMouseButtonDown(0)){        //if left mouse is just pressed
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, villager)){
                    hit.collider.gameObject.GetComponent<Creature>().isSelected=true;
                    Debug.Log(hit.transform.position);
                    uiHandler.SpawnButton.SetActive(false);
                    selectedCreatures.Add(hit.collider.gameObject.GetComponent<Creature>());
                }
                else if(Physics.Raycast(ray, out hit, Mathf.Infinity, building)){
                    hit.collider.gameObject.GetComponent<Building>().isSelected=true;
                    selectedBuilding = hit.collider.gameObject.GetComponent<Building>();
                    if(selectedBuilding.isSpawner){
                        uiHandler.SpawnButton.SetActive(true);
                        Vector2 targetScreenPosition = Camera.main.WorldToScreenPoint(hit.collider.gameObject.transform.position);
                        // uiHandler.SpawnButton.transform.position = targetScreenPosition;
                        Vector3 viewportPoint = new Vector3(targetScreenPosition.x / Screen.width, targetScreenPosition.y / Screen.width, 0);
                        uiElement.anchoredPosition = viewportPoint * uiElement.rect.size;
                        // create a button to spawn creature at location of building in terms of screen
                    }
                }
                else{
                    foreach(Creature creature in selectedCreatures){
                        creature.isSelected=false;
                        // selectedCreatures.Remove(creature);
                        selectedCreatures = new List<Creature>();
                    }
                    uiHandler.SpawnButton.SetActive(false);
                }


                mousePositionInitial = Input.mousePosition;
                isDragging=false;
            }

            if(Input.GetMouseButtonDown(1)){        //if right mouse just pressed
                selectedCreatures = new List<Creature>();
                uiHandler.SpawnButton.SetActive(false);
            }

            if(Input.GetMouseButton(0)){
                if(!isDragging && (mousePositionInitial-Input.mousePosition).magnitude>2){
                    isDragging=true;
                }

                if(isDragging){
                    mousePositionEnd=Input.mousePosition;
                    UpdateSelectionBox();
                }
            }

            if(Input.GetMouseButtonUp(0)){          //0 is left click, 1 right click, 2 middle click
                if(isDragging){
                    isDragging=false;
                    UpdateSelectionBox();
                    SelectObjects();
                }
            }
        }
    }

    void UpdateSelectionBox(){
        selectionBox.gameObject.SetActive(isDragging);

        float width=mousePositionEnd.x-mousePositionInitial.x;
        float height=mousePositionEnd.y-mousePositionInitial.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = new Vector2(mousePositionInitial.x, mousePositionInitial.y) + new Vector2(width/2, height/2);
    }

    void SelectObjects(){
        Vector2 minVal = selectionBox.anchoredPosition-(selectionBox.sizeDelta/2);
        Vector2 maxVal = selectionBox.anchoredPosition+(selectionBox.sizeDelta/2);

        GameObject[] selectableObjects = GameObject.FindGameObjectsWithTag("Villager");

        foreach(GameObject selectableObj in selectableObjects){
            Vector3 objScreenPos = Camera.main.WorldToScreenPoint(selectableObj.transform.position);
            if(objScreenPos.x > minVal.x && objScreenPos.x< maxVal.x && objScreenPos.y>minVal.y && objScreenPos.y < maxVal.y){
                Debug.Log("Selected "+selectableObj.name);
                if(selectableObj.GetComponent<Creature>() !=null){
                    Creature creature=selectableObj.GetComponent<Creature>();
                    selectedCreatures.Add(creature);        //add the Creatures to selected Creatures list
                    creature.isSelected=true;               //and select them
                }
            }
        }
    }
}
