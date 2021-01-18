using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarraConfianca : MonoBehaviour
{
    [SerializeField] private GameObject[] barraSprite;
    [SerializeField] private int valorInicial;
    private CanvasManeger canvas;
    private int numeroBarra;
    private bool jaIniciado;
    private bool finalizar;
    void Start()
    {
        numeroBarra = valorInicial;
        jaIniciado = false;
        finalizar = false;
        canvas = FindObjectOfType<CanvasManeger>();
        for(int i = 0; i < numeroBarra; i++)
        {
            barraSprite[i].SetActive(true);
        }
        numeroBarra--;
    }

    // Update is called once per frame
    void Update()
    {
        if (finalizar==true)
        {
            if (jaIniciado == false)
            {
                canvas.FinalizarMusica(false);
                jaIniciado = true;
            }
        }
    }

    public void AumentarBarra(float valor)
    {
        if (numeroBarra <17)
        { 
            numeroBarra++;
            barraSprite[numeroBarra].SetActive(true);
        }
    }

    public void DiminuirBarra(float valor)
    {
        if (numeroBarra >= 0)
        {
            barraSprite[numeroBarra].SetActive(false);
            if (numeroBarra == 0)
            {
                finalizar = true;
            }
            if(numeroBarra>0)
            numeroBarra--;
        }
    }

    

   
}
