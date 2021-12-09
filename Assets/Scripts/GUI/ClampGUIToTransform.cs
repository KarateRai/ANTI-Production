using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampGUIToTransform : MonoBehaviour
{
    private Vector3 _position;
    void Update()
    {
        if (GameManager.instance.ActiveCamera != null)
        {
            _position = GameManager.instance.ActiveCamera.WorldToScreenPoint(this.transform.position);
        }
    }

    public Vector3 GetPos()
    {
        if (_position.z < 0)
        {
            return _position * -1;
        }
        else
        {
            return _position;
        }
    }
}
