using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BotaoArrastar : MonoBehaviour
{
    [SerializeField] private bool botaoCorreto;
    [SerializeField] private float autoDestruirTempo;
    [SerializeField] private GameObject[] posicoesLinhas;  
    [SerializeField] private CircleCollider2D colider;
    private Vector2[] pontosLinha;
    private Vector2[] posicaoFinal;
    private Vector2 posicaoInicial;
    private int indexPosicao;
    private float tamanhoObjeto;
    private bool podeClicar;
    private bool jaIniciado;
    private int posicoesRemovidas;
    private CanvasManeger canvas;
    private ValoresDefinidos valores;
    private string palavra;
    private bool movimentoBotaoIniciado;
    private bool reset;
    private bool linerendererON;


    private void Start()
    {
        ResetarBotao();
    }

    void Update()
    {
       
            if ((Vector2)transform.position == posicaoFinal[indexPosicao])
            {
                if (indexPosicao != posicaoFinal.Length - 1)
                {

                    if (jaIniciado == false)
                    {
                        jaIniciado = true;
                        posicaoInicial = posicaoFinal[indexPosicao];
                        indexPosicao++;

                        Vector2 direcao = (posicaoFinal[indexPosicao] - posicaoInicial);
                        for (int i = 0; i < 20; i++)
                        {
                            float pontox = direcao.x / 20;
                            float pontoy = direcao.y / 20;

                            pontosLinha[i] = new Vector2(transform.position.x + ((pontox) * (1 + i)), transform.position.y + ((pontoy) * (1 + i)));
                        }

                    }
                }
                else
                {
                    canvas.ApertouBotao(botaoCorreto, transform.position, 100);
                    StopAllCoroutines();
                    Destroy(gameObject.transform.parent.gameObject);
               
                }

                jaIniciado = false;
            }




            Debug.DrawLine(posicaoInicial, posicaoFinal[indexPosicao]);

            if (podeClicar)
            {
                if (linerendererON == false)
                {
                    ConfigurarLineRender();
                }

                if (FuncoesToque.ToqueDragNoBotao(transform.position, tamanhoObjeto) == true)
                {
                    canvas.BotaoAtivo();
                    colider.radius += 3;
                    tamanhoObjeto = colider.radius;
                    ArrastarBotao();
                }

                if (canvas.GetBotaoAtivo()== true && FuncoesToque.ToqueUpNoBotao(transform.position, tamanhoObjeto) == true)
                {
                    canvas.ApertouBotao(botaoCorreto, transform.position, (100 * (indexPosicao)) / posicaoFinal.Length);
                    Destroy(gameObject.transform.parent.gameObject);
                }

               if (canvas.GetBotaoTerminado() == true)
               {
                  Destroy(gameObject.transform.parent.gameObject);
               }

            }
        
    }

    public void ResetarBotao()
    {
        StopAllCoroutines();
        linerendererON = false;
        reset = false;
        movimentoBotaoIniciado = false;
        podeClicar = false;
        jaIniciado = false;
        posicoesRemovidas = 0;
        indexPosicao = 0;
        canvas = FindObjectOfType<CanvasManeger>();
        valores = FindObjectOfType<ValoresDefinidos>();
        if (botaoCorreto)
        {
            palavra = canvas.GetPalavraBotao();
        }
        else
        {
            DefinirTexto();
        }


        posicaoInicial = transform.position;

        tamanhoObjeto = colider.radius;

        //Adicionar a letra
        for (int i = 0; i < posicoesLinhas.Length; i++)
        {
            if (i < palavra.Length)
            {
                //comfigurar Text mesh


                if (transform.rotation.z > 0.5f && transform.rotation.z < 0.9f)
                {
                    posicoesLinhas[i].GetComponent<TextMeshPro>().text = palavra[(palavra.Length) - (i + 1)].ToString();
                }
                else
                {
                    posicoesLinhas[i].GetComponent<TextMeshPro>().text = palavra[i].ToString();
                }
            }

        }

        posicaoFinal = new Vector2[posicoesLinhas.Length];

        //Pegar todoas a posicões finais
        for (int i = 0; i < posicaoFinal.Length; i++)
        {

            posicaoFinal[i] = posicoesLinhas[i].transform.position;

        }

        pontosLinha = new Vector2[20];
        Vector2 direcao = (posicaoFinal[indexPosicao] - posicaoInicial);
        for (int i = 0; i < 20; i++)
        {
            float pontox = direcao.x / 20;
            float pontoy = direcao.y / 20;
            pontosLinha[i] = new Vector2(posicaoInicial.x + ((pontox) * (1 + i)), transform.position.y + ((pontoy) * (1 + i)));
        }
    }

    IEnumerator AutoDestruir(float tempo)
    {
       
        yield return new WaitForSeconds(tempo);
        if (canvas.GetBotaoAtivo()==false)
        {
            if (botaoCorreto)
            {
                canvas.ApertouBotao(botaoCorreto, transform.position, 0);
            }
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            StartCoroutine(AutoDestruirPorTempo(1.5f));
        }
        StartCoroutine(AutoDestruir(autoDestruirTempo));
    }

    IEnumerator AutoDestruirPorTempo(float tempo)
    {
        yield return new WaitForSeconds(tempo);

        canvas.ApertouBotao(botaoCorreto, transform.position, (100 * (indexPosicao + 1)) / posicaoFinal.Length);

        Destroy(gameObject.transform.parent.gameObject);

        StopAllCoroutines();
    }

    private void DefinirTexto()
    {
        int tamanho = posicoesLinhas.Length;

        switch (tamanho)
        {
            case 1:
                palavra = PalavrasDataBase.palavras1[Random.Range(0, PalavrasDataBase.palavras1.Length - 1)];
                break;
            case 2:
                palavra = PalavrasDataBase.palavras2[Random.Range(0, PalavrasDataBase.palavras2.Length - 1)];
                break;
            case 3:
                palavra = PalavrasDataBase.palavras3[Random.Range(0, PalavrasDataBase.palavras3.Length - 1)];
                break;
            case 4:
                palavra = PalavrasDataBase.palavras4[Random.Range(0, PalavrasDataBase.palavras4.Length - 1)];
                break;
            case 5:
                palavra = PalavrasDataBase.palavras5[Random.Range(0, PalavrasDataBase.palavras5.Length - 1)];
                break;
            case 6:
                palavra = PalavrasDataBase.palavras6[Random.Range(0, PalavrasDataBase.palavras6.Length - 1)];
                break;
            case 7:
                palavra = PalavrasDataBase.palavras7[Random.Range(0, PalavrasDataBase.palavras7.Length - 1)];
                break;
            case 8:
                palavra = PalavrasDataBase.palavras8[Random.Range(0, PalavrasDataBase.palavras8.Length - 1)];
                break;
            case 9:
                palavra = PalavrasDataBase.palavras9[Random.Range(0, PalavrasDataBase.palavras9.Length - 1)];
                break;
            case 10:
                palavra = PalavrasDataBase.palavras10[Random.Range(0, PalavrasDataBase.palavras10.Length - 1)];
                break;
        }
    }

    private void ArrastarBotao()
    {
        DetectarToque toqueTela = GameObject.FindObjectOfType<DetectarToque>();
        Vector2 direcao1 = posicaoFinal[indexPosicao] - (Vector2)transform.position;
        Vector2 direcao2 = toqueTela.GetPosicaoSegurarToque() - (Vector2)transform.position;

        if (Vector2.Dot(direcao2.normalized, direcao1.normalized) >= 0.5f)
        {
            float menorDistancia = 10;
            int index=0;

           for(int i=0;i<pontosLinha.Length;i++)
            {               
                float distancia= Vector2.Distance(pontosLinha[i], toqueTela.GetPosicaoSegurarToque());
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    index = i;
                }
            }

            transform.position = pontosLinha[index];
           
           
        } 

    }

    private void ConfigurarLineRender()
    {
        //Adicionar e comfigurar line renderer
        for (int i = 0; i < posicoesLinhas.Length; i++)
        {
            if (i < palavra.Length - 1)
            {
                posicoesLinhas[i].GetComponent<LineRenderer>().SetWidth(0.2f, 0.2f);
                posicoesLinhas[i].GetComponent<LineRenderer>().SetPosition(0, posicoesLinhas[i].transform.position);
                posicoesLinhas[i].GetComponent<LineRenderer>().SetPosition(1, posicoesLinhas[i + 1].transform.position);
            }
        }
        //Line renderer do botao principal
        GetComponent<LineRenderer>().SetPosition(0, posicaoInicial);
        GetComponent<LineRenderer>().SetPosition(1, posicaoFinal[0]);
        GetComponent<LineRenderer>().SetWidth(0.2f, 0.2f);
        linerendererON = true;
    }
  
    public void SetInteragivel()
    {       
        podeClicar = true;
        StartCoroutine(AutoDestruir(autoDestruirTempo));
    }

    public void SetDesativado()
    {
        podeClicar = false;
        
    }
}
