// using System.Diagnostics;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{

    private bool isDragging=false;
    private Vector3 mousePositionInitial;
    private Vector3 mousePositionEnd;
    public RectTransform selectionBox;
    private List<Creature> selectedCreatures=new List<Creature>();
    public LayerMask ground;
    public LayerMask villager;
    public GameObject groundMarker;

    void Awake()
    {
        ground=LayerMask.GetMask("Ground");
        villager=LayerMask.GetMask("Villager");
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, villager)){
                hit.collider.gameObject.GetComponent<Creature>().isSelected=true;
                Debug.Log(hit.transform.position);
                selectedCreatures.Add(hit.collider.gameObject.GetComponent<Creature>());
            }
            else{
                foreach(Creature creature in selectedCreatures){
                    creature.isSelected=false;
                    // selectedCreatures.Remove(creature);
                }
            }


            mousePositionInitial = Input.mousePosition;
            isDragging=false;
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

        // if(Input.GetMouseButtonDown(1)){  //if right click is pressed & released
        // RaycastHit hit;
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Camera.main.nearClipPlane)); //world position of mouse cursor
        //     foreach(Creature c in selectedCreatures){
        //         // c.GetComponentInParent<BaseAI>();
        //         // c.MoveToward(hit.point);
        //         c.unselectThis();   //unselect all creatures
        //     }
        //     // Debug.Log("Right Clicked");
        // }   
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
