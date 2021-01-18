using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacao : MonoBehaviour
{
  
    [Header("Primeira linha")]
    [SerializeField] private float a1;
    [SerializeField] private float a2;
    [SerializeField] private float a3;
    [SerializeField] private float a4;
    [Header("Segunda linha")]
    [SerializeField] private float b1;
    [SerializeField] private float b2;
    [SerializeField] private float b3;
    [SerializeField] private float b4;
    [Header("Terceira linha")]
    [SerializeField] private float c1;
    [SerializeField] private float c2;
    [SerializeField] private float c3;
    [SerializeField] private float c4;
    [Header("Quarta linha")]
    [SerializeField] private float d1;
    [SerializeField] private float d2;
    [SerializeField] private float d3;
    [SerializeField] private float d4;
    private float altura;
    private float largura;
    private Vector2 limites1;
    private Vector2 limites2;
    private GameObject objetoReferencia;
    private void Start()
    {
        objetoReferencia = GameObject.FindGameObjectWithTag("AreaSpawn");
        altura = objetoReferencia.GetComponent<BoxCollider2D>().size.y/2;
        largura = objetoReferencia.GetComponent<BoxCollider2D>().size.x/2;
        limites1 = new Vector2(-largura, altura + objetoReferencia.transform.position.y);
        limites2 = new Vector2(objetoReferencia.transform.position.x,objetoReferencia.transform.position.y);
        DefinirPosicao();      
    }

    public void DefinirPosicao()
    {
        Vector2 posicaoAtual = gameObject.transform.position;
        print("Posicao: " + posicaoAtual);
        string quadrante = "";

        if (posicaoAtual.x < 0)
        {
            float posicaoX = 0;
            float posicaoXSeguinte = 0;

            int colunaPosicao;

            for (int coluna = 0; coluna < 2; coluna++)
            {
                int teste = coluna + 1;
                posicaoX = limites1.x + ((largura / 2) * coluna);
                posicaoXSeguinte = limites1.x + ((largura / 2) * teste);
                if (posicaoAtual.x > posicaoX && posicaoAtual.x <  posicaoXSeguinte)
                {
                    colunaPosicao = coluna + 1;

                    quadrante = ":" + colunaPosicao;
                }
                
            }
        }

        if(posicaoAtual.x > 0 )
        {
            float posicaoX = 0;
            float posicaoXSeguinte = 0;

            int colunaPosicao;

            for (int coluna = 0; coluna < 2; coluna++)
            {
                int teste = coluna + 1;
                posicaoX = limites2.x + ((largura / 2) * coluna);
                posicaoXSeguinte = limites2.x + ((largura / 2) * teste);
                print(posicaoX);
                print(posicaoXSeguinte);
                if (posicaoAtual.x >+ posicaoX && posicaoAtual.x <  posicaoXSeguinte)
                {
                    colunaPosicao = (coluna+1) + (1*2);

                    quadrante = ":" + colunaPosicao;
                }
            }
        }


        if (posicaoAtual.y > 0)
        {
            float posicaoY = 0;
            float posicaoYSeguinte = 0;
            int linhaPosicao;

            for (int linha = 0; linha < 2; linha++)
            {
                int teste2 = linha + 1;
                posicaoY = limites1.y - ((altura / 2) * linha);
                posicaoYSeguinte = limites1.y - ((altura / 2) * teste2);
                //print(posicaoY);
                //print(posicaoYSeguinte);
                if (posicaoAtual.y < posicaoY && posicaoAtual.y >  posicaoYSeguinte)
                {
                    linhaPosicao = linha + 1;
                    quadrante = linhaPosicao + quadrante;
                }

            }
        }

        if (posicaoAtual.y < 0)
        {
            float posicaoY = 0;
            float posicaoYSeguinte = 0;
            int linhaPosicao;

            for (int linha = 0; linha < 2; linha++)
            {
                int teste2 = linha + 1;
                posicaoY = limites2.y - ((altura / 2) * linha);
                posicaoYSeguinte = limites2.y - ((altura / 2) * teste2);
                //print(posicaoY);
                //print(posicaoYSeguinte);
                if (posicaoAtual.y <  posicaoY && posicaoAtual.y > posicaoYSeguinte)
                {
                    print("mano");
                    linhaPosicao = (linha+1) + (1*2);
                    quadrante = linhaPosicao + quadrante;
                }

            }
        }

        
        print(quadrante);
        DefinirRotacao(quadrante);
    }

    private void DefinirRotacao(string quadrante)
    {
        switch (quadrante)
        {
            case "1:1":
                print("1");
                transform.rotation = Quaternion.Euler(0,0,a1);
                break;
            case "1:2":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, a2);
                break;
            case "1:3":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, a3);
                break;
            case "1:4":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, a4);
                break;

            case "2:1":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, b1);
                break;
            case "2:2":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, b2);
                break;
            case "2:3":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, b3);
                break;
            case "2:4":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, b4);
                break;

            case "3:1":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, c1);
                break;
            case "3:2":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, c2);
                break;
            case "3:3":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, c3);
                break;
            case "3:4":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, c4);
                break;

            case "4:1":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, d1);
                break;
            case "4:2":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, d2);
                break;
            case "4:3":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, d3);
                break;
            case "4:4":
                print("1");
                transform.rotation = Quaternion.Euler(0, 0, d4);
                break;

        }
    }
}
