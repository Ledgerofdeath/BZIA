using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour {

    public float _sensitive;
    public Vector2 _orientationLimit = Vector2.zero;

    public float Sensitive { get { return _sensitive; } }
    public Vector2 OrentationLimit { get { return _orientationLimit; } }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update () {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical != 0)
            MoveVertical(vertical);
        if (horizontal != 0)
            MoveHorizontal(horizontal);

    }

    public void MoveVertical(float speed)
    {
        transform.Translate(Vector3.forward * speed * _sensitive * Time.deltaTime);
    }

    public void MoveHorizontal(float speed)
    {
        transform.Translate(Vector3.right * speed * _sensitive * Time.deltaTime);
    }
}
