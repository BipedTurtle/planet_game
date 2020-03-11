using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var vector = Vector3.up;
        var worldVector = transform.TransformDirection(vector);
        Debug.Log($"local: {vector}\nworld: {worldVector}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
