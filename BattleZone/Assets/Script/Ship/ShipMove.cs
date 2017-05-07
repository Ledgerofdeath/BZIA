using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipMove : System.Object {

    public float _sensitive;
    public Vector2 _sensitiveRotation = Vector2.zero;
    public Vector2 _orientationLimit = Vector2.zero;
    float _currentOrentation;

    public float Sensitive { get { return _sensitive; } }
    public Vector2 OrentationLimit { get { return _orientationLimit; } }

    public GameObject _bodyShip = null;

    public Transform transform;

    float _currentSpeed = 0.0f;
    float _currentSpeedLateral = 0.0f;
    public Vector2 _speedSensitive = Vector2.zero;

    public void StartMove()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _currentOrentation = _bodyShip.transform.rotation.eulerAngles.x;
    }

    public void UpdateMove () {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float rHorizontal = Input.GetAxis("Mouse X");
        float rVertical = Input.GetAxis("Mouse Y");

            MoveVertical(vertical);
            MoveHorizontal(horizontal);
        if (rHorizontal != 0)
            HorizontalRotation(rHorizontal);
        if (rVertical != 0)
            VerticalRotation(rVertical);

    }

    public void MoveVertical(float speed)
    {
        _currentSpeed = SpeedTransfer(speed, _currentSpeed);
        transform.Translate(Vector3.forward * _currentSpeed * _sensitive * Time.deltaTime);
    }

    public void MoveHorizontal(float speed)
    {
        _currentSpeedLateral = SpeedTransfer(speed, _currentSpeedLateral);
        transform.Translate(Vector3.right * _currentSpeedLateral * _sensitive * Time.deltaTime);
    }

    float SpeedTransfer(float speed, float currentSpeed)
    {
        if (speed > 0)
        {
            if (speed > currentSpeed)
                currentSpeed += _speedSensitive.x;
        }
        else if (speed < 0)
        {
            if (speed < currentSpeed)
                currentSpeed -= _speedSensitive.x;
        }
        if (speed == 0)
        {
            if ((currentSpeed > 0 && currentSpeed < _speedSensitive.y) ||
                (currentSpeed < 0 && currentSpeed > -_speedSensitive.y))
                currentSpeed = 0.0f;
            else if (currentSpeed > 0)
                currentSpeed -= _speedSensitive.y;
            else if (currentSpeed < 0)
                currentSpeed += _speedSensitive.y;
        }
        return currentSpeed;
    }

    public void HorizontalRotation(float speed)
    {
        transform.Rotate(Vector3.up, speed * _sensitiveRotation.x * Time.deltaTime);
    }

    public void VerticalRotation(float speed)
    {
        float orentation = speed * _sensitiveRotation.y * Time.deltaTime;

        if ((speed < 0 && _currentOrentation > _orientationLimit.x) ||
            (speed > 0 && _currentOrentation < _orientationLimit.y))
        {
            _bodyShip.transform.Rotate(Vector3.left, orentation);
            _currentOrentation += orentation;
        }
    }

}
