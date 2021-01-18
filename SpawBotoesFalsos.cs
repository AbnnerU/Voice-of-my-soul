using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawBotoesFalsos : MonoBehaviour
{
    [SerializeField] private SpawBotoes spawnBotoes;
    [SerializeField] private bool mostrarPosicoes;
    [SerializeField] private Vector2[] botao1;
    [SerializeField] private Vector2[] botao2;
    [SerializeField] private Vector2[] botao3;
    [SerializeField] private Vector2[] botao4;
    [SerializeField] private Text texto;
    
    private GameObject[] botoesReferencia;
    private ValoresDefinidos valores;
    private float ultimoInstanciado; 
    private float tempoAtual;   
    private bool musicaIniciada;
    private float compensacao;
    private string[] tipoBotao;
    private float botaoAtual;
    
    private void Awake()
    {
        botaoAtual = -1;
        valores = FindObjectOfType<ValoresDefinidos>();
        
        tempoAtual = 0;
        
        compensacao = spawnBotoes.GetCompensacao();
        botoesReferencia = spawnBotoes.GetBotoesReferencia();
        tipoBotao = new string[botoesReferencia.Length];
        for(int i = 0; i < tipoBotao.Length; i++)
        {
            tipoBotao[i] = botoesReferencia[i].tag;
        }
    }

    private void Update()
    {
        musicaIniciada = spawnBotoes.GetIniciar();
        if (musicaIniciada)
        {
            if (botaoAtual < spawnBotoes.GetUltimoInstanciado())
            {
                botaoAtual++;
                if (tipoBotao[(int)botaoAtual] == "BotaoToque")
                {
                    SpawnarBotaoToque((int)botaoAtual);
                }
                else if(tipoBotao[(int)botaoAtual] == "BotaoSegurar")
                {
                    SpawnarBotaoSegurar((int)botaoAtual);
                }
                else
                {
                    SpawnarBotaoArrastar((int)botaoAtual);
                }

            }
            
        }
    }

    private void SpawnarBotaoToque(int index)
    {
        if(botao1[index] != new Vector2(0,0))
        {
            //PoolManager.SpawnObject(valores.prefabBotaoToqueF, botao1[index], Quaternion.identity);
            Instantiate(valores.prefabBotaoToqueF, botao1[index], Quaternion.identity);
        }
        if (botao2[index] != new Vector2(0, 0))
        {
            //PoolManager.SpawnObject(valores.prefabBotaoToqueF, botao2[index], Quaternion.identity);
            Instantiate(valores.prefabBotaoToqueF, botao2[index], Quaternion.identity);
        }
        if (botao3[index] != new Vector2(0, 0))
        {
            //PoolManager.SpawnObject(valores.prefabBotaoToqueF, botao3[index], Quaternion.identity);
            Instantiate(valores.prefabBotaoToqueF, botao3[index], Quaternion.identity);
        }
        if (botao4[index] != new Vector2(0, 0))
        {
            //PoolManager.SpawnObject(valores.prefabBotaoToqueF, botao4[index], Quaternion.identity);
            Instantiate(valores.prefabBotaoToqueF, botao4[index], Quaternion.identity);
        }
    }

    private void SpawnarBotaoSegurar(int index)
    {
        if (botao1[index] != new Vector2(0, 0))
        {
            if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 1)
            {
                Instantiate(valores.prefabBotaoSeguraF[0], botao1[index], Quaternion.identity);
            }
            else if(botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 2)
            {
                Instantiate(valores.prefabBotaoSeguraF[1], botao1[index], Quaternion.identity);
            }
            else if(botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 3)
            {
                Instantiate(valores.prefabBotaoSeguraF[2], botao1[index], Quaternion.identity);
            }
        }
        if (botao2[index] != new Vector2(0, 0))
        {
            if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 1)
            {
                Instantiate(valores.prefabBotaoSeguraF[0], botao2[index], Quaternion.identity);
            }
            else if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 2)
            {
                Instantiate(valores.prefabBotaoSeguraF[1], botao2[index], Quaternion.identity);
            }
            else if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 3)
            {
                Instantiate(valores.prefabBotaoSeguraF[2], botao2[index], Quaternion.identity);
            }
        }
        if (botao3[index] != new Vector2(0, 0))
        {
            if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 1)
            {
                Instantiate(valores.prefabBotaoSeguraF[0], botao3[index], Quaternion.identity);
            }
            else if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 2)
            {
                Instantiate(valores.prefabBotaoSeguraF[1], botao3[index], Quaternion.identity);
            }
            else if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 3)
            {
                Instantiate(valores.prefabBotaoSeguraF[2], botao3[index], Quaternion.identity);
            }
        }
        if (botao4[index] != new Vector2(0, 0))
        {
            if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 1)
            {
                Instantiate(valores.prefabBotaoSeguraF[0], botao4[index], Quaternion.identity);
            }
            else if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 2)
            {
                Instantiate(valores.prefabBotaoSeguraF[1], botao4[index], Quaternion.identity);
            }
            else if (botoesReferencia[index].GetComponent<BotaoSegurar>().GetTempoEncherBarra() == 3)
            {
                Instantiate(valores.prefabBotaoSeguraF[2], botao4[index], Quaternion.identity);
            }
        }
    }

    private void SpawnarBotaoArrastar(int index)
    {
        int tamanhoTexto = texto.text.Length;

        if (botao1[index] != new Vector2(0, 0))
        {
            switch (tamanhoTexto)
            {
                case 1:
                    Instantiate(valores.prefabBotaoArrastaF[0], botao1[index], Quaternion.identity);
                    break;
                case 2:
                    Instantiate(valores.prefabBotaoArrastaF[1], botao1[index], Quaternion.identity);
                    break;
                case 3:
                    Instantiate(valores.prefabBotaoArrastaF[2], botao1[index], Quaternion.identity);
                    break;
                case 4:
                    Instantiate(valores.prefabBotaoArrastaF[3], botao1[index], Quaternion.identity);
                    break;
                case 5:
                    Instantiate(valores.prefabBotaoArrastaF[4], botao1[index], Quaternion.identity);
                    break;
                case 6:
                    Instantiate(valores.prefabBotaoArrastaF[5], botao1[index], Quaternion.identity);
                    break;
                case 7:
                    Instantiate(valores.prefabBotaoArrastaF[6], botao1[index], Quaternion.identity);
                    break;
                case 8:
                    Instantiate(valores.prefabBotaoArrastaF[7], botao1[index], Quaternion.identity);
                    break;
                case 9:
                    Instantiate(valores.prefabBotaoArrastaF[8], botao1[index], Quaternion.identity);
                    break;
                case 10:
                    Instantiate(valores.prefabBotaoArrastaF[9], botao1[index], Quaternion.identity);
                    break;

            }
        }

        if (botao2[index] != new Vector2(0, 0))
        {
            switch (tamanhoTexto)
            {
                case 1:
                    Instantiate(valores.prefabBotaoArrastaF[0], botao2[index], Quaternion.identity);
                    break;
                case 2:
                    Instantiate(valores.prefabBotaoArrastaF[1], botao2[index], Quaternion.identity);
                    break;
                case 3:
                    Instantiate(valores.prefabBotaoArrastaF[2], botao2[index], Quaternion.identity);
                    break;
                case 4:
                    Instantiate(valores.prefabBotaoArrastaF[3], botao2[index], Quaternion.identity);
                    break;
                case 5:
                    Instantiate(valores.prefabBotaoArrastaF[4], botao2[index], Quaternion.identity);
                    break;
                case 6:
                    Instantiate(valores.prefabBotaoArrastaF[5], botao2[index], Quaternion.identity);
                    break;
                case 7:
                    Instantiate(valores.prefabBotaoArrastaF[6], botao2[index], Quaternion.identity);
                    break;
                case 8:
                    Instantiate(valores.prefabBotaoArrastaF[7], botao2[index], Quaternion.identity);
                    break;
                case 9:
                    Instantiate(valores.prefabBotaoArrastaF[8], botao2[index], Quaternion.identity);
                    break;
                case 10:
                    Instantiate(valores.prefabBotaoArrastaF[9], botao2[index], Quaternion.identity);
                    break;

            }
        }
    }

    private void OnDrawGizmos()
    {
        if (mostrarPosicoes)
        { 
            for (int i = 0; i < botao1.Length; i++)
            {
                if (botao1[i] != new Vector2(0, 0))
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(botao1[i], 0.5f);
                }
            }

            for (int i = 0; i < botao2.Length; i++)
            {
                if (botao2[i] != new Vector2(0, 0))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(botao2[i], 0.5f);
                }
            }

            for (int i = 0; i < botao3.Length; i++)
            {
                if (botao3[i] != new Vector2(0, 0))
                {
                    Gizmos.color = Color.gray;
                    Gizmos.DrawSphere(botao3[i], 0.5f);
                }
            }
            for (int i = 0; i < botao4.Length; i++)
            {
                if (botao4[i] != new Vector2(0, 0))
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(botao4[i], 0.5f);
                }
            }
        }
    }
}
