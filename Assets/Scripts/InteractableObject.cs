using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractableObject : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    public Player player;
    
    Vector3 offset;
    Vector3 mousePosition;
    public float maxSpeed=10;
    Vector2 mouseForce;
    Vector3 lastPosition;
    
    void Start(){
        selectedObject = null;
    }
    void Update()
    {
        if(!player.telekinesis){
            selectedObject = null;
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (selectedObject && player.telekinesis)
        {
            mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
            mouseForce = Vector2.ClampMagnitude(mouseForce, maxSpeed);
            lastPosition = mousePosition;
        }
        if (Input.GetMouseButtonDown(0) && player.telekinesis)
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                if(targetObject.transform.gameObject.tag != "Player"){
                    selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                    offset = selectedObject.transform.position - mousePosition;
                }
                
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject.velocity = Vector2.zero;
            selectedObject.AddForce(mouseForce, ForceMode2D.Impulse);
            selectedObject = null;
        }
    }
    void FixedUpdate()
    {
        if (selectedObject && player.telekinesis)
        {
            selectedObject.MovePosition(mousePosition + offset);
        }
    }
}