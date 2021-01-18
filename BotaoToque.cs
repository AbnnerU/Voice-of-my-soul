using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoToque : MonoBehaviour
{
    [SerializeField] private bool botaoCorreto;
    [SerializeField] private float autoDestruirTempo;
    [SerializeField] private CircleCollider2D colider;
    private float tamanhoObjeto;
    private bool podeClicar;
    private CanvasManeger canvas;

    private void Awake()
    {
        canvas = FindObjectOfType<CanvasManeger>();
        podeClicar = false;
        tamanhoObjeto = colider.radius;
    }

    //private void OnEnable()
    //{
    //    StopAllCoroutines();
    //    podeClicar = false;
        
    //}

    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //}

    private void Update()
    {
        if (podeClicar)
        {
            if (FuncoesToque.ToqueNoBotao(transform.position, tamanhoObjeto) == true)
            {
                canvas.BotaoAtivo();
                canvas.ApertouBotao(botaoCorreto,transform.position,100);
                PoolManager.ReleaseObject(gameObject);
            }
            if (canvas.GetBotaoTerminado() == true)
            {
                //PoolManager.ReleaseObject(gameObject);
                Destroy(gameObject);
            }
        }

    }

    IEnumerator AutoDestruir(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        if (canvas.GetBotaoAtivo() == false)
        {
            if (botaoCorreto==true)
            {
                canvas.ApertouBotao(botaoCorreto, transform.position, 0);
            }
            //PoolManager.ReleaseObject(gameObject);
            Destroy(gameObject);
            StopCoroutine(AutoDestruir(autoDestruirTempo));
        }
    }

    public void SetInteragivel()
    {
        StartCoroutine(AutoDestruir(autoDestruirTempo));
        podeClicar = true;
    }

    public void SetDesativado()
    {
        podeClicar = false;
    }
}
