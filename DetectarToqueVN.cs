using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetectarToqueVN : MonoBehaviour
{
    [HideInInspector]
    public bool telaPressionada;
    private bool primeiraVez;
    // Start is called before the first frame update
    void Awake()
    {
        primeiraVez = false;
        telaPressionada = false;
    }

    public void Toque()
    {
        telaPressionada = true;                
    }

    public void ToqueUp()
    {
        
        telaPressionada = false;
    }
}
