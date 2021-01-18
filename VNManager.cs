using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class VNManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject voltarMenuPanel;
    [SerializeField] private AudioSource som;
    private bool pause;

    void Awake()
    {
        AudioListener.volume = PlayerSave.GetMute();
        pausePanel.SetActive(false);
        voltarMenuPanel.SetActive(false);
        pause = false;
       
    }

    public void Pause()
    {
        som.Play();
        pause = true;
        voltarMenuPanel.SetActive(false);
        pausePanel.SetActive(true);
        if (PlayerSave.GetMute() != 0)
        {
            AudioListener.volume = 0.1f;
        }
    }

    public void Continuar()
    {
        
        som.Play();
        pause = false;
        pausePanel.SetActive(false);
        voltarMenuPanel.SetActive(false);
        if (PlayerSave.GetMute() != 0)
        {
            AudioListener.volume = 1;
        }
    }

    public void Reiniciar()
    {
        som.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarMenu()
    {
        som.Play();
        pause = true;
        voltarMenuPanel.SetActive(true);
        if (PlayerSave.GetMute() != 0)
        {
            AudioListener.volume = 0.1f;
        }
    }

    public void Comfirmar()
    {
        som.Play();
        SceneManager.LoadScene("Menu principal");
    }
   

    //Get

    public bool GetPause()
    {
        return pause;
    }
}
