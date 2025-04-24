using UnityEngine;

public class Building : MonoBehaviour
{
    public bool inConstruction;
    public LayerMask ground;
    void Awake()
    {
        inConstruction=false;
        ground=LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if(inConstruction==true){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground)){
                transform.position = hit.point;
            }
        }
    }
}
