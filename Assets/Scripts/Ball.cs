using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    bool held;
    Vector3 lastMousePosition;
    float DELTA_COEFFICIENT = 0.01f;
    Vector3 velocity;
    float VELOCITY_DECAY = 0.95f;

    // Use this for initialization
    void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(held)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            velocity = delta * DELTA_COEFFICIENT;
            lastMousePosition = Input.mousePosition;
        }
        else
        {
            velocity *= VELOCITY_DECAY;
        }

        if(transform.position.x < -5.5f)
        {
            transform.position = new Vector3(5.5f, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > 5.5f)
        {
            transform.position = new Vector3(-5.5f, transform.position.y, transform.position.z);
        }

        transform.position += velocity;
    }

    void OnMouseDown()
    {
        Debug.Log("WE HAVE A TOUCH");
        held = true;
        lastMousePosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        Debug.Log("TOUCH RELEASED");
        held = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Border"))
        {
            Debug.Log("WE HAVE A HIT");
            velocity = Vector3.zero;
        }
    }
}
