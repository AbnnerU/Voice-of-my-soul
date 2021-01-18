using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PreMenuManeger : MonoBehaviour
{
    [SerializeField] private GameObject invalido;
    [SerializeField] private GameObject painel;
    private bool caixaInput;

    private void Awake()
    {
        invalido.SetActive(false);
        painel.SetActive(false);
        if (PlayerPrefs.HasKey("Nome") == true)
        {
            caixaInput = false;
            SceneManager.LoadScene("Menu principal");
        }
        else
        {
            caixaInput = true;
            painel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (caixaInput)
        {
            if (PlayerPrefs.HasKey("Nome") == true)
            {
                SceneManager.LoadScene("Menu principal");
            }
        }
    }

    public void DefinirNome(string nome)
    {
        if (nome.Length < 3)
        {
            invalido.SetActive(true);
        }
        else
        {
            PlayerSave.SetNome(nome);
            PlayerSave.SetMute(1);
        }
    }
}
