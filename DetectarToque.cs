using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarToque : MonoBehaviour
{
    [HideInInspector]
    public Vector2 posicaoToque;
    [HideInInspector]
    public Vector2 posicaoToqueDrag;
    [HideInInspector]
    public bool telaPressionada;
    [HideInInspector]
    public Vector2 posicaoToqueUp;

    private void Awake()
    {
        telaPressionada = false;
    }

    public void PosicaoPrimeiroToque()
    {
        telaPressionada = true;
        posicaoToque = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SoltoTela()
    {
        telaPressionada = false;
        posicaoToqueUp= Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void PosicaoSegurarToque()
    {
        telaPressionada = true;
        posicaoToqueDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    public Vector2 GetPosicaoSegurarToque()
    {
        return posicaoToqueDrag;
    }
}
