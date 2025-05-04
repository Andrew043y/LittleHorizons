using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed=10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = 10;
        Vector3 moveDirection = Vector3.zero;

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            moveSpeed=5f;
        }
        else{
            moveSpeed=10f;
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(transform.position.y<=4){
                // moveDirection.y*=-1;
            }
            else{
                moveDirection += 4*moveSpeed*transform.forward;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            moveDirection -= 4*moveSpeed*transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if(transform.position.z<=-250){
                // moveDirection.y*=-1;
            }
            else{
                moveDirection -= moveSpeed/2*transform.right;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if(transform.position.z>=250){
                // moveDirection.y*=-1;
            }
            else{
                moveDirection += moveSpeed/2*transform.right;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if(transform.position.x<=-245){
                // moveDirection.y*=-1;
            }
            else{
                moveDirection -= moveSpeed/2*new Vector3(1f,0f,0f);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if(transform.position.x>=275){
                // moveDirection.y*=-1;
            }
            else{
                moveDirection += moveSpeed/2*new Vector3(1f,0f,0f);
            }
        }

        if (moveDirection.magnitude > 0.1f)
        {
            // moveDirection.Normalize();
            transform.position += moveDirection * currentSpeed * Time.deltaTime;
        }
    }
}
