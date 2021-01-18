using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManeger : MonoBehaviour
{
    [SerializeField] private GameObject perdeuUI;
    [SerializeField] private Text palavraBotoes;
    [SerializeField] private  GameObject resultadoBotao;
    [SerializeField] private GameObject particula;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject comfirmarPanel;
    [SerializeField] private AudioSource som;
    [SerializeField] private AudioClip clique1;
    [SerializeField] private AudioClip clique2;
    [SerializeField] private string cenaPerdeu;
    [SerializeField] private string cenaGahou;
    private TransicaoManeger transicao;
    private SpawBotoes spawner;
    private ValoresDefinidos valores;
    private BarraConfianca barra;
    private string palavraAtual;
    private bool manterPalavraAtual;
    private bool botaoAtivo;
    private bool botaoTerminado;
    private bool cancelaAnimacao;
    private bool fimJogo;
    private bool jogoPausado;

    void Awake()
    {
        Time.timeScale = 1;
        comfirmarPanel.SetActive(false);
        panelPause.SetActive(false);
        fimJogo = false;
        transicao = FindObjectOfType<TransicaoManeger>();
        barra = FindObjectOfType<BarraConfianca>();
        spawner = FindObjectOfType<SpawBotoes>();
        valores = FindObjectOfType<ValoresDefinidos>();
        botaoAtivo = false;
        botaoTerminado = false;
        jogoPausado = false;
        manterPalavraAtual = false;

    }

    void Start()
    {
        palavraAtual = "";
    }


    void Update()
    {
        if (botaoTerminado)
        {
            botaoAtivo = false;
            palavraBotoes.text = "";
        }
                                

    }

   
    public void BotaoInstanciado()
    {
        palavraBotoes.text = palavraAtual;
        botaoAtivo = false;
        botaoTerminado = false;
    }

    public void ApertouBotao(bool botaoCorreto, Vector3 posicaoBotao, float valorConclusaoBotao)
    {
        botaoTerminado = true;
        if (botaoCorreto)
        {
            if (valorConclusaoBotao == 0)
            {
                barra.DiminuirBarra(0.05f);
                perdeuUI.GetComponent<Animator>().Play("Tela vermelha",0,0);
                cancelaAnimacao = true;
                //perdeuUI.text = valores.resultadoBotao[0];
                //perdeuUI.color = valores.corResultadoBotao[0];
            }
            else if (valorConclusaoBotao < 30)
            {
                barra.AumentarBarra(0.03f);
                resultadoBotao.GetComponent<Text>().text = valores.resultadoBotao[2];
                resultadoBotao.GetComponent<Text>().color = valores.corResultadoBotao[1];
                resultadoBotao.GetComponent<Animator>().Play("BotoesResultado");

            }
            else if(valorConclusaoBotao>30 && valorConclusaoBotao < 80)
            {
                barra.AumentarBarra(0.08f);
                resultadoBotao.GetComponent<Text>().text = valores.resultadoBotao[3];
                resultadoBotao.GetComponent<Text>().color = valores.corResultadoBotao[2];

            }
            else
            {
                barra.AumentarBarra(0.1f);
                resultadoBotao.GetComponent<Text>().text = valores.resultadoBotao[4];
                resultadoBotao.GetComponent<Text>().color = valores.corResultadoBotao[3];
            }
            particula.transform.position = posicaoBotao;
            resultadoBotao.transform.position = posicaoBotao;
            if (cancelaAnimacao == false)
            {
                particula.GetComponent<ParticleSystem>().Play();
                resultadoBotao.GetComponent<Animator>().Play("BotoesResultado", 0, 0);
            }
            else
            {
                cancelaAnimacao = false;
            }
        }
        else
        {
            barra.DiminuirBarra(0.1f);
            particula.transform.position = posicaoBotao;
            particula.GetComponent<ParticleSystem>().Play();

            resultadoBotao.GetComponent<Text>().text = valores.resultadoBotao[1];
            resultadoBotao.GetComponent<Text>().color = valores.corResultadoBotao[0];
            resultadoBotao.transform.position = posicaoBotao;
            resultadoBotao.GetComponent<Animator>().Play("BotoesResultado",0,0);
        }
    } 

  

    public void BotaoAtivo()
    {
        botaoAtivo = true;
    }

    public void FinalizarMusica(bool ganhou)
    {
        if (ganhou)
        {
            transicao.TransicaoFim(cenaGahou);
        }
        else
        {
            print("foi");
            transicao.TransicaoFim(cenaPerdeu);
        }
        fimJogo = true;
    }
    
    public void Continuar()
    {
        som.clip = clique2;
        som.Play();
        spawner.RetomarMosica();
        panelPause.SetActive(false);
        comfirmarPanel.SetActive(false);
        jogoPausado = false;
        Time.timeScale = 1;
    }

    public void JogoPausado()
    {
        spawner.PausarMusica();
        jogoPausado = true;
        som.clip = clique1;
        som.Play();
        panelPause.SetActive(true);
        comfirmarPanel.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void PainelComfirmar()
    {
        som.clip = clique2;
        som.Play();
        comfirmarPanel.SetActive(true);
    }


    public void Comfirmar()
    {
        som.clip = clique2;
        som.Play();
        SceneManager.LoadScene("Menu principal");
    }

    private void OnApplicationPause(bool pause)
    {
        JogoPausado();
    }

    public void Cheat()
    {
        FinalizarMusica(true);
    }

    //GETS E SETS

    public void SetPalavraAtual(string palavra)
    {
        palavraAtual = palavra;
    }

    public string GetPalavraBotao()
    {
        return palavraAtual;
    }   
    
    public bool GetBotaoAtivo()
    {
        return botaoAtivo;
    }

    public bool GetBotaoTerminado()
    {
        return botaoTerminado;
    }

    public bool GetFimJogo()
    {
        return fimJogo;
    }

    public bool GetJogoPausado()
    {
        return jogoPausado;
    }


    
}
