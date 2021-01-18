using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoRotacao : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, -transform.parent.gameObject.transform.rotation.z);
    }
}
