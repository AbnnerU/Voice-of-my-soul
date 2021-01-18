using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoSegurar : MonoBehaviour
{
    
    [SerializeField] private bool botaoCorreto;
    [SerializeField] private float animacaoFundoTempo;
    [SerializeField] private float autoDestruirTempo;
    [SerializeField] private GameObject fundo;
    [SerializeField] private GameObject barraSprite;
    [SerializeField] private CircleCollider2D colider;
    [SerializeField] private Sprite spriteCompleto;
    [Range(0,3)]
    [SerializeField] private float tempoParaEncherBarra;

    private CanvasManeger canvas;
    private float tamanhoObjeto;
    private bool podeClicar;
    private string botaoEstado;

    private bool animacaoFundoCompleta;
    private float valorAumentarFundoX=0;
    private float valorAumentarFundoY=0;


    private void Awake()
    {
        canvas = FindObjectOfType<CanvasManeger>();
        animacaoFundoCompleta = false;

        barraSprite.transform.localScale = new Vector2(barraSprite.transform.localScale.x, tempoParaEncherBarra+1);
        botaoEstado = BotoesEstado.botaoNaoPressionado;
        podeClicar = false;
        tamanhoObjeto = colider.radius;
    }

    
    void Update()
    {
        if (podeClicar == true)
        {
           
            if (FuncoesToque.ToqueNoBotao(transform.position, tamanhoObjeto) == true)
            {
                canvas.BotaoAtivo();
                StopCoroutine(AutoDestruir(autoDestruirTempo));
                botaoEstado = BotoesEstado.botaoPressionado;
            }

            if (FuncoesToque.ToqueUpNoBotao(transform.position, tamanhoObjeto) == true && canvas.GetBotaoAtivo() == true)
            {               
                botaoEstado = BotoesEstado.botaoCancelado;              
            }

            if (canvas.GetBotaoTerminado() == true)
            {
                Destroy(gameObject);
            }
        }


        //Animacao fundo
        if (animacaoFundoCompleta == false)
        {
            Animacao();
        }

    }

    IEnumerator AutoDestruir(float tempo)
    {
        yield return new WaitForSeconds(tempo);

        if (canvas.GetBotaoAtivo()==false)
        {
            StopCoroutine(AutoDestruir(autoDestruirTempo));
            if (botaoCorreto)
            {
                canvas.ApertouBotao(botaoCorreto, transform.position, 0);
            }
           Destroy(gameObject);            
        }
       
        StopCoroutine(AutoDestruir(autoDestruirTempo));
    }

    public void BotaoCompleto()
    {
        Destroy(gameObject);
    }


    private void Animacao()
    {
      
        valorAumentarFundoX += (1f / animacaoFundoTempo) * Time.deltaTime;
        valorAumentarFundoY += (tempoParaEncherBarra + 1 / animacaoFundoTempo) * Time.deltaTime;
        fundo.transform.localScale = new Vector2(Mathf.Clamp(valorAumentarFundoX, 0, 1f), Mathf.Clamp(valorAumentarFundoY, 0, tempoParaEncherBarra + 1));

        if (fundo.transform.localScale.y == tempoParaEncherBarra + 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteCompleto;
            podeClicar = true;
            animacaoFundoCompleta = true;
            StartCoroutine(AutoDestruir(autoDestruirTempo));
        }
    }


    public string GetBotaoEstado()
    {
        return botaoEstado;
    }
   
    public bool GetPodeClicar()
    {
        return podeClicar;
    }

    public float GetTempoEncherBarra()
    {
        return tempoParaEncherBarra;
    }
     
    public bool GetBotaoCorreto()
    {
        return botaoCorreto;
    }

    
    


}
