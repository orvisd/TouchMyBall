using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float DeltaCoefficient = 0.01f;
    public float VelocityDecayCoefficient = 0.95f;

    bool held;
    Vector3 lastMousePosition;
    Vector3 velocity;

    void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    void Update()
    {
        if(held)
        {
            if(isHoldable())
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                velocity = delta * DeltaCoefficient;
                lastMousePosition = Input.mousePosition;
            }
            else
            {
                held = false;
            }
        }
        else
        {
            velocity *= VelocityDecayCoefficient;
        }

        if(transform.position.x < -5.5f)
        {
            transform.position = new Vector3(5.5f, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > 5.5f)
        {
            transform.position = new Vector3(-5.5f, transform.position.y, transform.position.z);
        }

        if(transform.position.y < -3)
        {
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
        }
        else if(transform.position.y > 3)
        {
            transform.position = new Vector3(transform.position.x, -3, transform.position.z);
        }

        if(Mathf.Approximately(velocity.x, 0) && Mathf.Approximately(velocity.y, 0))
        {
            Reset();
        }

        transform.position += velocity;
    }

    void OnMouseDown()
    {
        if(isHoldable())
        {
            held = true;
            lastMousePosition = Input.mousePosition;
        }
    }

    void OnMouseUp()
    {
        held = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Border"))
        {
            Reset();
        }
    }

    bool isHoldable()
    {
        return transform.position.x > -1 && transform.position.x < 1 &&
               transform.position.y > -1 && transform.position.y < 1;
    }

    public void Reset()
    {
        velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }
}
