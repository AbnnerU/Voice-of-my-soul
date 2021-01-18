using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BilheteManeger : MonoBehaviour
{
    [SerializeField] private DialogosManeger dialogos;

    [SerializeField] private GameObject bilheteImage;
    [SerializeField] private GameObject bilheteTexto;
    [SerializeField] private GameObject fundo;
    [SerializeField] private GameObject comentario;
    [SerializeField] private GameObject comentarioTexto;
    [SerializeField] private float velocidadeTexto;
    [SerializeField] private TextAsset[] bilhetes;
    private DetectarToqueVN toque;
    private VNManager maneger;
    private Animator anim;
    private GameObject caixaDialogo;
    private string[] textoRecortado;
    private string[] todasLinhas;
    private int indexLinhas;
    private int indexBilhetes;
    private int linha;
    private bool iniciarBilhete;
    private bool primeiraLinha;
    private bool aguardarInput;
    private bool tocou;
    private bool primeiraVez;
    void Awake()
    {
      
        maneger = FindObjectOfType<VNManager>();
        anim = bilheteImage.GetComponent<Animator>();
        tocou = false;
        aguardarInput = false;
        primeiraLinha = true;
        primeiraVez = true;
        indexBilhetes = 0;
        caixaDialogo = GameObject.FindGameObjectWithTag("CaixaDialogos");
        ZerarTodosoIndex();
        if (bilhetes.Length!=0)
        {
            TodasAsLinhasTexto();
        }
        bilheteImage.SetActive(false);
        bilheteTexto.SetActive(false);

        fundo.SetActive(false);
        
    }

    void Update()
    {
        if (maneger.GetPause() == false)
        {
            if (dialogos.GetBilhete() == true)
            {
                if (primeiraVez)
                {
                    StartCoroutine(DelayParaBilhete());
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Input.mousePosition.y < (Screen.height / 2))
                        {
                            tocou = true;
                        }
                    }

                    bilheteImage.SetActive(true);
                    bilheteTexto.SetActive(true);
                    fundo.SetActive(true);
                    caixaDialogo.SetActive(false);
                    if (primeiraLinha)
                    {
                        if (todasLinhas[0].Contains("[F1]"))
                        {
                            comentario.SetActive(false);
                            bilheteTexto.GetComponent<Text>().text = todasLinhas[0].Substring(4);
                            anim.Play("Bilhete direita");
                        }
                        else if (todasLinhas[0].Contains("[F2]"))
                        {
                            comentario.SetActive(false);
                            bilheteTexto.GetComponent<Text>().text = todasLinhas[0].Substring(4);
                            anim.Play("Bilhete esquerda");
                        }
                        else if (todasLinhas[0].Contains("[C]"))
                        {
                            Comentario();
                        }
                        primeiraLinha = false;
                    }
                    else if (tocou && aguardarInput == false)
                    {
                        tocou = false;
                        linha++;
                        if (todasLinhas[linha].Contains("[F1]"))
                        {
                            comentario.SetActive(false);
                            bilheteTexto.SetActive(true);
                            Fala();
                            anim.Play("Bilhete direita");
                        }
                        else if (todasLinhas[linha].Contains("[F2]"))
                        {
                            comentario.SetActive(false);

                            bilheteTexto.SetActive(true);
                            Fala();
                            anim.Play("Bilhete esquerda");
                        }
                        else if (todasLinhas[linha].Contains("[INPUT]"))
                        {
                            comentario.SetActive(false);

                            bilheteTexto.SetActive(false);
                            aguardarInput = true;
                            anim.Play("Bilhete direita");
                        }
                        else if (todasLinhas[linha].Contains("[C]"))
                        {
                            Comentario();
                        }
                        else if (todasLinhas[linha].Contains("[FIM]"))
                        {
                            FinalizarBilhete();

                        }
                    }
                }
            }
            else
            {
                primeiraVez = true;
                bilheteImage.SetActive(false);
                bilheteTexto.SetActive(false);
                fundo.SetActive(false);

            }
        }
       
    }

    IEnumerator Digitar(string frase)
    {
        comentarioTexto.GetComponent<Text>().text = "";
        foreach (char letra in frase.ToCharArray())
        {
            comentarioTexto.GetComponent<Text>().text += letra;
            yield return new WaitForSeconds(velocidadeTexto);
        }
        StopAllCoroutines();
    }

    private void Comentario()
    {
        StopAllCoroutines();
        comentario.SetActive(true);
        //comentarioTexto.GetComponent<Text>().text = todasLinhas[0].Substring(3);
        string sentenca = todasLinhas[linha].Substring(3);
        StartCoroutine(Digitar(sentenca));
        comentario.GetComponent<Animator>().Play("Comentario", 0, 0);
    }

    private void ZerarTodosoIndex()
    { 
        linha=0;
        indexLinhas = 0;
    }

    private void TodasAsLinhasTexto()
    {
        textoRecortado = bilhetes[indexBilhetes].text.Split('\n');

        foreach (string s in textoRecortado)
        {
            if ( s.Contains("[F1]") || s.Contains("[F2]") || s.Contains("[FIM]") || s.Contains("[INPUT]") || s.Contains("[C]"))
            {
                indexLinhas++;
            }
        }
        todasLinhas = new string[indexLinhas];
        indexLinhas = 0;

        for (int i = 0; i < textoRecortado.Length; i++)
        {
            if (textoRecortado[i].Contains("[F1]") || textoRecortado[i].Contains("[F2]") || textoRecortado[i].Contains("[FIM]") || textoRecortado[i].Contains("[INPUT]") || textoRecortado[i].Contains("[C]"))
            {
                todasLinhas[indexLinhas] = textoRecortado[i];
                indexLinhas++;
            }
        }
    }

    private void FinalizarBilhete()
    {
        bilheteImage.SetActive(false);
        bilheteTexto.SetActive(false);
        fundo.SetActive(false);
        caixaDialogo.SetActive(true);
        dialogos.FinalizarBilhete();
        indexBilhetes++;
        if (indexBilhetes < bilhetes.Length)
        {
            
            primeiraLinha = true;
            ZerarTodosoIndex();
            TodasAsLinhasTexto();
        }
    }

    private void ProximaLinha()
    {
        linha++;
        if (todasLinhas[linha].Contains("[F1]"))
        {
            comentario.SetActive(false);
            bilheteTexto.SetActive(true);
            Fala();
            anim.Play("Bilhete direita");
        }
        else if (todasLinhas[linha].Contains("[F2]"))
        {
            comentario.SetActive(false);

            bilheteTexto.SetActive(true);
            Fala();
            anim.Play("Bilhete esquerda");
        }
        else if (todasLinhas[linha].Contains("[INPUT]"))
        {
            comentario.SetActive(false);
  
            bilheteTexto.SetActive(false);
            aguardarInput = true;
            anim.Play("Bilhete direita");
        }
        else if (todasLinhas[linha].Contains("[C]"))
        {
            Comentario();
        }
        else if (todasLinhas[linha].Contains("[FIM]"))
        {
            FinalizarBilhete();

        }
    }

    private void Fala()
    {
        bilheteTexto.GetComponent<Text>().text = todasLinhas[linha].Substring(4);
        if (bilheteTexto.GetComponent<Text>().text.Contains("<NOME>"))
        {
            bilheteTexto.GetComponent<Text>().text = bilheteTexto.GetComponent<Text>().text.Replace("<NOME>", PlayerSave.GetNome());
        }
    }

    IEnumerator DelayParaBilhete()
    {
        yield return new WaitForSeconds(0.3f);
        primeiraVez = false;
        StopCoroutine(DelayParaBilhete());
    }

    
}
