using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LetraMusica : MonoBehaviour
{
    [SerializeField] private Text karaokeVermelho;
    [Header("BACKGROUND :")]
    [SerializeField] private Image background;
    [Header("LETRA MUSICA :")]
    [TextArea]
    [SerializeField] private string[] versos;   
    [Header("TEMPOS :")]
    [SerializeField] private int tempoIniciarMusica;
    [SerializeField] private float tempoLeituraPadraoLetra;
    [SerializeField] private float[] tempoEntrePalavras;
    [SerializeField] private float[] tempoEntreEntreVersos;

    private CanvasManeger canvas;
    private string[] palavras;
    private bool iniciarLetra=false;
    private bool bgColorido;
    private Text  textoUI;
    private int indexPalavra;
    private int indexVerso;
    private int indexTempoPalavras;
    private int indexTempoVersos;
    private float tempoMusica;

    private void Awake()
    {
        canvas = FindObjectOfType<CanvasManeger>();
        textoUI = GetComponent<Text>();
        indexPalavra = 0;
        indexVerso = 0;
        indexTempoPalavras = 0;
        indexTempoVersos = 0;
        palavras = versos[0].Split(" "[0]);
        bgColorido = true;
        StartCoroutine(IniciarLetra(tempoIniciarMusica));
    }


    IEnumerator IniciarLetra(float tempo)
    {

        yield return new WaitForSeconds(tempo);
        background.GetComponent<Animator>().Play("Background PB");
        bgColorido = false;
        iniciarLetra = true;
        textoUI.text = versos[indexVerso];
        StartCoroutine(TempoEntrePalavrasTimer(tempoEntrePalavras[indexTempoPalavras]));
        StopCoroutine(IniciarLetra(tempoIniciarMusica));
    }


    IEnumerator TempoEntrePalavrasTimer(float tempo)
    {

        //textoUI.text = textoUI.text.Replace(palavras[indexPalavra], "<color=red>" + palavras[indexPalavra] + "</color>");
        StartCoroutine(TextoVermelho(palavras[indexPalavra], tempo));
        canvas.SetPalavraAtual(palavras[indexPalavra]);


        if (tempo == 0) // se tempo for igual a zero o calculo para passar para a proxima letra sera definido pelo tamanho da letra multiplicado por tempoPadraoLetra 
        {           
            tempo = palavras[indexPalavra].Length * tempoLeituraPadraoLetra;
        }

        yield return new WaitForSeconds(tempo);

        indexPalavra++;
        if (indexPalavra < palavras.Length) // ir para a proxima palavra
        {
            karaokeVermelho.text += " ";
            StartCoroutine(TempoEntrePalavrasTimer(tempoEntrePalavras[indexTempoPalavras]));
            StopCoroutine(TempoEntrePalavrasTimer(tempoEntrePalavras[indexTempoPalavras]));


        }
        else //ir para o proximo verso
        {
            indexVerso++;
            if (indexVerso < versos.Length) //ir para o proximo verso
            {
                karaokeVermelho.text = "";
                indexTempoVersos++;
                if (tempoEntreEntreVersos[indexTempoVersos]>1.5f)
                {
                    background.GetComponent<Animator>().Play("Backgroun colorido");
                    bgColorido = true;
                }
                StopAllCoroutines();
                StartCoroutine(TempoEntreVersosTimer(tempoEntreEntreVersos[indexTempoVersos]));               
            }
            else // termina karaoke
            {
                karaokeVermelho.text = "";
                textoUI.text = "";
                StopAllCoroutines();
            }      
        }

    }


    IEnumerator TempoEntreVersosTimer(float tempo)
    {
        //print(tempo);
        indexTempoPalavras++;     
        indexPalavra = 0;
       // limpar array
        string[] tamanho = versos[indexVerso].Split(" "[0]);
        palavras = new string[tamanho.Length];
        palavras = versos[indexVerso].Split(" "[0]);
        textoUI.text = "";

        yield return new WaitForSeconds(tempo);
        if (bgColorido)
        {
            background.GetComponent<Animator>().Play("Background PB");
            bgColorido = false;
        }
        textoUI.text = versos[indexVerso];
        StartCoroutine(TempoEntrePalavrasTimer(tempoEntrePalavras[indexTempoPalavras]));
        StopCoroutine(TempoEntreVersosTimer(indexTempoVersos));
        
    }

    IEnumerator TextoVermelho(string palavra,float tempo)
    {
        float velocidadeTexto = (tempo / palavra.Length)-0.0111f;

        foreach (char letra in palavra.ToCharArray())
        {
            karaokeVermelho.text += letra;
            yield return new WaitForSeconds(velocidadeTexto);
        }
        StopCoroutine(TextoVermelho(palavra, tempo));
    }




}
