using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TransicaoManeger : MonoBehaviour
{
    [SerializeField] private Text texto;
    [SerializeField] private Animator anim;
    private bool permitirToque;
   
    private DialogosManeger dialogos;
    private string cena;

    void Awake()
    {
        permitirToque = true;
        dialogos = FindObjectOfType<DialogosManeger>();
    }

  
    public void IniciarTransicao(string frase)
    {
        texto.text = frase;
        anim.Play("Escurecer", 0, 0);
    }

    public void TransicaoInicio(string frase)
    {
        texto.text = frase;
        anim.Play("Inicio",0,0);
    }

    public void TransicaoFim(string proximaCena)
    {
        texto.text = "";
        permitirToque = false;
        anim.Play("Fim",0,0);
        cena = proximaCena;
    }

    public void CarregarCena()
    {
        SceneManager.LoadScene(cena);
    }

    public void ToqueFalse()
    {
        permitirToque = false;
    }

    public void ToqueTrue()
    {
        permitirToque = true;

    }

    public void ProximaLinha()
    {
        dialogos.ProximaLinha();
    }


    //Gets e Sets

    public bool GetToque()
    {
        return permitirToque;
    }
}
