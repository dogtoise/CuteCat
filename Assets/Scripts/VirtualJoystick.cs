using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    public Transform player;
    public float speed = 0.1f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    public RectTransform circle;
    public RectTransform outerCircle;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            //spointA = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

            circle.transform.position = Camera.main.WorldToScreenPoint( pointA);// * -1;
            outerCircle.transform.position = Camera.main.WorldToScreenPoint(pointA);// * -1;
           // circle.transform.position = pointA;
           // outerCircle.transform.position = pointA;


            //circle.GetComponent<SpriteRenderer>().enabled = true;
            //outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            //pointB = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
        }
        else
        {
            touchStart = false;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);
            circle.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(new Vector2(pointA.x + direction.x, pointA.y + direction.y));
            // circle.transform.position = Camera.main.WorldToScreenPoint(new Vector2(pointA.x + direction.x, pointA.y + direction.y));

           // circle.GetComponent<RectTransform>().position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
           
        }
        else
        {
          //  circle.GetComponent<SpriteRenderer>().enabled = false;
            //outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
}
