using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toque : MonoBehaviour
{
    private bool tocandoTela;
   
    void Awake()
    {
        tocandoTela = false;
    }
   
    public void ToqueTrue()
    {
        tocandoTela = true;
    }

    public void ToqueFalse()
    {
        tocandoTela = false;
    }

    public bool GetToqueTela()
    {
        return tocandoTela;
    }
}
