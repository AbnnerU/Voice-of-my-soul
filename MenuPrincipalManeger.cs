using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuPrincipalManeger : MonoBehaviour
{
    [SerializeField] private Button carregarJogoBotao;
    [SerializeField] private GameObject comfirmacaoPanel;
    [SerializeField] private GameObject creditosPanel;
    private AudioSource som;
    private string nomeFaseSave;
    private bool faseSave;
    private void Awake()
    {
        creditosPanel.SetActive(false);
        som = gameObject.GetComponent<AudioSource>();
        AudioListener.volume = PlayerSave.GetMute();
        comfirmacaoPanel.SetActive(false);
        if (PlayerPrefs.HasKey("Fase")==true)
        {
            faseSave = true;
            nomeFaseSave = PlayerSave.GetFase();
        }
        else
        {
            faseSave = false;
        }
    }

    private void Update()
    {
        if (faseSave == true)
        {
            carregarJogoBotao.interactable = true;
        }
        else
        {
            carregarJogoBotao.interactable = false;
        }
    }

    public void NovoJogo()
    {
        som.Play();
        if (PlayerPrefs.HasKey("Fase") == true)
        {
            comfirmacaoPanel.SetActive(true);
        }
        else
        {    
            PlayerSave.ResetSaves();
            SceneManager.LoadScene("Capitulo 1 pt 1");
        }
    }

    public void Comfirmar()
    {
        som.Play();
        PlayerSave.ResetSaves();
        SceneManager.LoadScene("Capitulo 1 pt 1");
    }

    public void Negar()
    {
        som.Play();
        comfirmacaoPanel.SetActive(false);
    }

    public void CarregarJogo()
    {
        som.Play();
        SceneManager.LoadScene(PlayerSave.GetFase());
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Creditos()
    {
        creditosPanel.SetActive(true);
    }

    public void Voltar()
    {
        creditosPanel.SetActive(false);
    }
}
