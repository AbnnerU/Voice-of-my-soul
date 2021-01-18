using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespostasManeger : MonoBehaviour
{
    [SerializeField] private DialogosManeger dialogos;
    [SerializeField] private GameObject[] botaoResposta;
    [SerializeField] private GameObject[] textoResposta;

    private string[] textoParaBotoes;
    private bool jaExecutado1;
    private bool jaExecutado2;
    // Start is called before the first frame update
    void Awake()
    {
        jaExecutado1 = false;
        jaExecutado2 = false;
        textoParaBotoes = new string[4];
        foreach(GameObject g in botaoResposta)
        {
            g.SetActive(false);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogos.GetAguardarResposta() == true)
        {
            if (jaExecutado1 == false)
            {
               
                textoParaBotoes = dialogos.GetOpcoesRespostas().Split('/');
                for (int i = 0; i < textoParaBotoes.Length; i++)
                {

                    if (textoParaBotoes[i].Contains("null") == false)
                    {
                        botaoResposta[i].SetActive(true);
                        botaoResposta[i].GetComponent<Button>().interactable = true;
                        textoResposta[i].GetComponent<Text>().text = textoParaBotoes[i];
                    }
                    else
                    {
                        botaoResposta[i].SetActive(false);
                    }
                }
                jaExecutado1 = true;
                jaExecutado2 = false;
            }
        }
        else
        {
            if (jaExecutado2 == false)
            {
                for (int i = 0; i < textoParaBotoes.Length; i++)
                {
                    botaoResposta[i].SetActive(false);
                    textoResposta[i].GetComponent<Text>().text = "";
                    
                }
                jaExecutado2 = true;
                jaExecutado1 = false;
            }
        }
    }
}
