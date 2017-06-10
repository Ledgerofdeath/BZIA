using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapGenerator : MonoBehaviour {
    public TerrainCollider _collider;
    public GameObject _line;
    public int margin = 5;
    public GameObject _miniMap;

    private void Awake()
    {
        Vector3 size = _collider.terrainData.size;
        for (int i = 0; i < size.x - margin; i += margin)
        {
            for (int j = 0; j < size.z - margin; j += margin)
            {
                GameObject point = Instantiate(_line, _miniMap.transform) as GameObject;
                GameObject point2 = Instantiate(_line, _miniMap.transform) as GameObject;
                Vector3 p1 = new Vector3(i, _collider.terrainData.GetHeight(i, j), j);
                Vector3 p2 = new Vector3(i + margin, _collider.terrainData.GetHeight(i + margin, j), j);
                Vector3 p3 = new Vector3(i, _collider.terrainData.GetHeight(i, j + margin), j + margin);
                //Vector3 p1 = _collider.terrainData.GetInterpolatedNormal((float)i / size.x, (float)j / size.z);
                //Vector3 p2 = _collider.terrainData.GetInterpolatedNormal((float)(i + margin) / size.x, (float)j / size.z);
                //Vector3 p3 = _collider.terrainData.GetInterpolatedNormal((float)i / size.x, (float)(j + margin) / size.z);
                point.transform.localScale = new Vector3(0.2f, 0.2f, Vector3.Distance(p1, p2));
                point2.transform.localScale = new Vector3(0.2f, 0.2f, Vector3.Distance(p1, p3));
                point.transform.position = (p2 + p1) / 2;
                point2.transform.position = (p3 + p1) / 2;

                point.transform.LookAt(p2);
                point2.transform.LookAt(p3);

                //point.transform.position -= new Vector3(5f, 0.0f, 6.5f);
                //point2.transform.position -= new Vector3(5f, 0.0f, 6.5f);
                //_miniMap.transform.localScale = new Vector3(0.977f, 1f, 0.977f);
                //point.transform.SetParent(_miniMap.transform);
                //point2.transform.SetParent(_miniMap.transform);

            }
        }
        _miniMap.transform.localScale = new Vector3(0.977f, 1f, 0.977f);
    }
}
