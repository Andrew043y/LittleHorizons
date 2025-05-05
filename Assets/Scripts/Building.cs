using UnityEngine;

public class Building : MonoBehaviour
{
    public bool inConstruction, placeable, isSelected, isSpawner;
    private LayerMask ground, gatherable, reservedGatherable, villager, building;
    public UIHandler uiHandler;
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
                transform.position = hit.point;
                placeable=true;
            }
        }
    }
}
