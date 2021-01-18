using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoSegurarBarra : MonoBehaviour
{
   [SerializeField] private BotaoSegurar botaoPai;
    private CanvasManeger canvas;
    private float valorBarra=0;

    private void Awake()
    {
        canvas = FindObjectOfType<CanvasManeger>();
    }

    private void Update()
    {
        if (botaoPai.GetBotaoEstado()==BotoesEstado.botaoPressionado)
        {            
            AumentarBarra();
        }

        if (botaoPai.GetBotaoEstado() == BotoesEstado.botaoCancelado)
        {
            PararBotão();
        }
       
    }


   private void AumentarBarra()
    {
        valorBarra += (1/botaoPai.GetTempoEncherBarra()) * Time.deltaTime;
        gameObject.transform.localScale = new Vector2(1, Mathf.Clamp(valorBarra, 0, 1));

        if (gameObject.transform.localScale.y == 1)
            PararBotão();
    }

    private void PararBotão()
    {
        canvas.ApertouBotao(botaoPai.GetBotaoCorreto(), botaoPai.transform.position, gameObject.transform.localScale.y * 100);
        botaoPai.BotaoCompleto();
    }
    
}
