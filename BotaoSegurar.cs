using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotãoSegurar : MonoBehaviour
{
    [SerializeField] private CircleCollider2D colider;
    private float tamanhoObjeto;
    private bool podeClicar;

    void Start()
    {
        podeClicar = false;
        tamanhoObjeto = colider.radius;
    }

    void Update()
    {
        
    }
}
