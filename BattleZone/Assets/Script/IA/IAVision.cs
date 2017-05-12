using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAVision : MonoBehaviour {
    [SerializeField]
    float _range = 50.0f;
    /// <summary>
    /// Between 0 and 45
    /// </summary>
    [SerializeField]
    float _forwardRange = 15.0f;

    struct VisioData
    {
        public e_obstacle type;
        public e_direction direction;
        public float dist;
    }

    public enum e_obstacle {
        OBSTACLE,
        FREIND,
        FOE
    }

    public enum e_direction {
        FORWARD,
        FORWARD_LEFT,
        FORWARD_RIGHT,
        RIGHT,
        LEFT,
        BACK
    }

    List<VisioData> _vision;

    List<VisioData> Vision { get { return _vision; } }

    public IAVision()
    {
        _vision = new List<VisioData>();
    }

    public void Radar()
    {
        List<GameObject> ships = GameManager.Instance.Ships;
        _vision.Clear();
        float dist;

        foreach (GameObject ship in ships)
        {
            // need param current ship game object
            if (ship != gameObject && (dist = Vector3.Distance(ship.transform.position, gameObject.transform.position)) <= _range)
            {
                VisioData data = new VisioData();
                data.dist = dist;
                data.type = TargetType(gameObject, ship);
                data.direction = FindDirection(gameObject, ship);
                Debug.Log("type : "+ data.type.ToString() + " direction: " + data.direction.ToString() + " dist: " + dist);
                _vision.Add(data);
            }
        }
    }

    e_direction FindDirection(GameObject src, GameObject target)
    {
        Vector3 targetDir = target.transform.position - src.transform.position;

        float fwdAngle = Vector3.Angle(targetDir, src.transform.forward);
        float rightAngle = Vector3.Angle(targetDir, src.transform.right);
        if (fwdAngle >= 135.0f)
            return e_direction.BACK;
        if (rightAngle >= 135.0f)
            return e_direction.LEFT;
        else if (rightAngle <= 45.0f)
            return e_direction.RIGHT;
        if (fwdAngle <= _forwardRange)
            return e_direction.FORWARD;
        else if (rightAngle <= 90.0 - _forwardRange)
            return e_direction.FORWARD_RIGHT;
        return e_direction.FORWARD_LEFT;
    }

    e_obstacle TargetType(GameObject src, GameObject target)
    {
        ShipManager ship = target.GetComponent<ShipManager>();

        if (ship == null)
            return e_obstacle.OBSTACLE;
        if (ship.Team == src.GetComponent<ShipManager>().Team)
            return e_obstacle.FREIND;
        return e_obstacle.FOE;
    }

}
