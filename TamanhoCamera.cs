using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamanhoCamera : MonoBehaviour
{
    [SerializeField] private SpriteRenderer referencia;
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    // Start is called before the first frame update
    void Awake()
    {
        if (x)
        {
            float tamanho = referencia.bounds.size.x * Screen.height / Screen.width * 0.5f;
            Camera.main.orthographicSize = tamanho;
        }
        else
        {
            float tamanho = referencia.bounds.size.y / 2;
            Camera.main.orthographicSize = tamanho;
        }

    }
}
