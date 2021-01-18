using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawBotoes : MonoBehaviour
{
    [SerializeField] private float tempoIniciarMusica;
    [SerializeField] private AudioSource playerMusica;
    [SerializeField] private AudioClip arquivoSom;
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private Vector2[] posicaoAparecer;
    [SerializeField] private bool mostrarPosicoes;
    [SerializeField] private Vector2[] tempoQueAparece;
    [SerializeField] private float compensacaoTempo;
    [SerializeField] private Text texto;

    private bool estaPausado;
    private bool iniciarMusica=false;
    private float tempo = 0;
    private float ultimoInstanciado; // evitar q dois objetos sejam instanciados
    private CanvasManeger canvas;
    private ValoresDefinidos valores;

    private Vector2[] posicoesBotaoFalso;
    
    
    private void Awake()
    {
        posicoesBotaoFalso = new Vector2[4];
        canvas = FindObjectOfType<CanvasManeger>();
        valores = FindObjectOfType<ValoresDefinidos>(); 
    }

    private void Start()
    {
        estaPausado = false;
        StartCoroutine(IniciarMusica(tempoIniciarMusica));
        ultimoInstanciado = -1;
        
        tempo += Time.deltaTime;
    }

    private void Update()
    {
        if (iniciarMusica)
        {
            if (canvas.GetFimJogo() == false)
            {
                //print(playerMusica.time);
                tempo += Time.deltaTime;
                for (int i = 0; i < tempoQueAparece.Length; i++)
                {
                    if (tempo >= tempoQueAparece[i].x * 60 + tempoQueAparece[i].y - compensacaoTempo && tempo <= tempoQueAparece[i].x * 60 + ((tempoQueAparece[i].y - 1) + 0.1f) && i != ultimoInstanciado)
                    {

                        canvas.BotaoInstanciado();
                        if (prefab[i].CompareTag("BotaoToque"))
                        {
                            //PoolManager.SpawnObject(prefab[i], posicaoAparecer[i], Quaternion.identity);
                            Instantiate(prefab[i], posicaoAparecer[i], Quaternion.identity);
                        }
                        else if (prefab[i].CompareTag("BotaoSegurar"))
                        {
                            Instantiate(prefab[i], posicaoAparecer[i], Quaternion.identity);
                        }
                        else
                        {
                            InstanciarBotaoArrasta(i);
                        }
                        ultimoInstanciado = i;
                    }

                }

                if (playerMusica.isPlaying==false && canvas.GetJogoPausado()==false && estaPausado==false) // Musica terminou
                {
                    canvas.FinalizarMusica(true);
                }
                
            }
            else
            { 
                playerMusica.Stop();
            }
        }
    }

   
    private void OnDrawGizmos()
    {
        if (mostrarPosicoes)
        {
            for (int i = 0; i < posicaoAparecer.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(posicaoAparecer[i], 0.5f);
            }
        }
    }

    IEnumerator IniciarMusica(float tempo)
    {

        yield return new WaitForSeconds(tempo);
        iniciarMusica = true;
        playerMusica.clip = arquivoSom;
        playerMusica.Play();
        //print(playerMusica.time);
        StopCoroutine(IniciarMusica(tempoIniciarMusica));

    }

    private void InstanciarBotaoArrasta(int index)
    {
        int tamanhoTexto = texto.text.Length;
        switch (tamanhoTexto)
        {
            case 1:
               GameObject botao= PoolManager.SpawnObject(valores.prefabBotaoArrasta[0], posicaoAparecer[index], Quaternion.identity);
                break;
            case 2:
                Instantiate(valores.prefabBotaoArrasta[1], posicaoAparecer[index], Quaternion.identity);
                break;
            case 3:
                Instantiate(valores.prefabBotaoArrasta[2], posicaoAparecer[index], Quaternion.identity);
                break;
            case 4:
                Instantiate(valores.prefabBotaoArrasta[3], posicaoAparecer[index], Quaternion.identity);
                break;
            case 5:
                Instantiate(valores.prefabBotaoArrasta[4], posicaoAparecer[index], Quaternion.identity);
                break;
            case 6:
                Instantiate(valores.prefabBotaoArrasta[5], posicaoAparecer[index], Quaternion.identity);
                break;
            case 7:
                Instantiate(valores.prefabBotaoArrasta[6], posicaoAparecer[index], Quaternion.identity);
                break;
            case 8:
                Instantiate(valores.prefabBotaoArrasta[7], posicaoAparecer[index], Quaternion.identity);
                break;
            case 9:
                Instantiate(valores.prefabBotaoArrasta[8], posicaoAparecer[index], Quaternion.identity);
                break;
            case 10:
                Instantiate(valores.prefabBotaoArrasta[9], posicaoAparecer[index], Quaternion.identity);
                break;

        }
    }

    public void PausarMusica()
    {
        estaPausado = true;
        playerMusica.Pause();
    }

    public void RetomarMosica()
    {
        estaPausado = false;
        playerMusica.UnPause();
    }

    //gets e sets

    public bool GetIniciar()
    {
        return iniciarMusica;
    }

    public Vector2[] GetTemposSpanw()
    {
        return tempoQueAparece;
    }

    public float GetCompensacao()
    {
        return compensacaoTempo;
    }

    public GameObject[] GetBotoesReferencia()
    {
        return prefab;
    }

    public float GetUltimoInstanciado()
    {
        return ultimoInstanciado;
    }
}
