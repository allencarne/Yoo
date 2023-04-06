using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] Vector3 offset;

    private void Start()
    {
        transform.position += offset;
    }
}
