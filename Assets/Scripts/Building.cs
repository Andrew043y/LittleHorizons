using UnityEngine;

public class Building : MonoBehaviour
{
    public bool inConstruction;
    public LayerMask ground;
    public LayerMask gatherable;
    public LayerMask reservedGatherable;
    public LayerMask villager;
    public LayerMask building;
    void Awake()
    {
        inConstruction=false;
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
            }
            else if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                transform.position = hit.point;
            }
        }
    }
}
