using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CelularManeger : MonoBehaviour
{
    [SerializeField] private DialogosManeger dialogos;
    [SerializeField] private Image foto;
    [SerializeField] private Sprite amanda;
    [SerializeField] private Sprite daniel;
    [SerializeField] private AudioSource som;
    [SerializeField] private GameObject fundo;
    [SerializeField] private GameObject fundoMensagens;
    [SerializeField] private GameObject nome;
    [SerializeField] private GameObject mensagem;
    [SerializeField] private GameObject horario;
    [SerializeField] private GameObject dataFundo;
    [SerializeField] private GameObject data;
    [SerializeField] private GameObject comentario;
    [SerializeField] private GameObject comentarioTexto;
    [SerializeField] private float velocidadeTexto;
    [SerializeField] private TextAsset[] celularTxt;

    private DetectarToqueVN toque;
    private VNManager maneger;
    private Animator anim;
    private GameObject caixaDialogo;
    private string[] textoRecortado;
    private string[] todasLinhas;
    private string nomePerfil;
    private int indexLinhas;
    private int indexCelularTxt;
    private int linha;
    private bool primeiraLinha;
    private bool tocou;
    private bool primeiraVez;
    // Start is called before the first frame update
    void Awake()
    {
        toque = FindObjectOfType<DetectarToqueVN>();
        maneger = FindObjectOfType<VNManager>();
        caixaDialogo = GameObject.FindGameObjectWithTag("CaixaDialogos");
        anim = fundoMensagens.GetComponent<Animator>();
        tocou = false;
        primeiraLinha = true;
        primeiraVez = true;
        indexCelularTxt = 0;       
        fundo.SetActive(false);
        fundoMensagens.SetActive(false);
        dataFundo.SetActive(false);
        data.SetActive(false);
        
        ZerarTodosoIndex();
        if (celularTxt.Length!=0)
        {
            TodasAsLinhasTexto();
        }
    }

    
    void Update()
    {

        if (maneger.GetPause() == false)
        {
            if (dialogos.GetCelular() == true)
            {
                if (primeiraVez)
                {
                    if (nomePerfil.Contains("Amanda"))
                    {
                        foto.sprite = amanda;
                    }
                    else if (nomePerfil.Contains("Daniel"))
                    {
                        foto.sprite = daniel;
                    }
                    StartCoroutine(DelayParaCelular());
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

                    fundo.SetActive(true);
                    fundoMensagens.SetActive(true);
                    caixaDialogo.SetActive(false);
                    if (primeiraLinha)
                    {
                        ExibirTexto();
                        if (todasLinhas[linha].Contains("[C]"))
                        {
                            Comentario();
                        }
                    }
                    else if (tocou)
                    {
                        tocou = false;
                        linha++;
                        ExibirTexto();
                        if (todasLinhas[linha].Contains("[C]"))
                        {
                            Comentario();
                        }
                        else if (todasLinhas[linha].Contains("[FIM]"))
                        {
                            print("OIIIII");
                            FinalizarCelular();
                        }
                    }
                }
            }
            else // Quando celular for false
            {
                fundo.SetActive(false);
                fundoMensagens.SetActive(false);
                dataFundo.SetActive(false);
                data.SetActive(false);
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
        linha = 0;
        indexLinhas = 0;
    }

    private void TodasAsLinhasTexto()
    {
        textoRecortado = celularTxt[indexCelularTxt].text.Split('\n');

        foreach (string s in textoRecortado)
        {
            if (s.Contains("[N1]") || s.Contains("[F1]") || s.Contains("[N2]") || s.Contains("[F2]") || s.Contains("[D]") || s.Contains("[H]") || s.Contains("[FIM]") || s.Contains("[C]"))
            {
                indexLinhas++;
            }
        }
        todasLinhas = new string[indexLinhas];
        indexLinhas = 0;

        for (int i = 0; i < textoRecortado.Length; i++)
        {
            if (textoRecortado[i].Contains("[N1]") || textoRecortado[i].Contains("[F1]") || textoRecortado[i].Contains("[N2]") || textoRecortado[i].Contains("[F2]") || textoRecortado[i].Contains("[D]") || textoRecortado[i].Contains("[H]") || textoRecortado[i].Contains("[FIM]") || textoRecortado[i].Contains("[C]"))
            {
                if (textoRecortado[i].Contains("[N2]"))
                {
                    nomePerfil = textoRecortado[i].Substring(4);
                }
               
                todasLinhas[indexLinhas] = textoRecortado[i];
                indexLinhas++;
            }
        }
    }

    private void ExibirTexto()
    {
        comentario.SetActive(false);
        if (primeiraLinha)
        {
            if (todasLinhas[0].Contains("[N1]"))
            {
                anim.Play("Jogador",0,0);
                linha++;
                Mensagem();
                linha++;
                horario.GetComponent<Text>().text = todasLinhas[linha].Substring(3);
                linha++;
                if (todasLinhas[linha].Contains("null") == false)
                {
                    data.SetActive(true);
                    dataFundo.SetActive(true);
                    data.GetComponent<Text>().text = todasLinhas[linha].Substring(3);
                }
                else
                {
                    data.SetActive(false);
                    dataFundo.SetActive(false);
                }               

            }
            else if (todasLinhas[0].Contains("[N2]"))
            {
                som.Play();
                anim.Play("Personagem",0,0);
                nome.GetComponent<Text>().text = todasLinhas[linha].Substring(4);               
                linha++;
                Mensagem();
                linha++;
                horario.GetComponent<Text>().text = todasLinhas[linha].Substring(3);
                linha++;
                if (todasLinhas[linha].Contains("null") == false)
                {
                    data.SetActive(true);
                    dataFundo.SetActive(true);
                    data.GetComponent<Text>().text = todasLinhas[linha].Substring(3);
                }
                else
                {
                    data.SetActive(false);
                    dataFundo.SetActive(false);
                }
            }
           
            primeiraLinha = false;
        }
        else
        {          
            if (todasLinhas[linha].Contains("[N1]"))
            {
                
                anim.Play("Jogador", 0, 0);
                linha++;
                Mensagem();
                linha++;
                horario.GetComponent<Text>().text = todasLinhas[linha].Substring(3);
                linha++;
                if (todasLinhas[linha].Contains("null") == false)
                {
                    data.SetActive(true);
                    dataFundo.SetActive(true);
                    data.GetComponent<Text>().text = todasLinhas[linha].Substring(3);
                }
                else
                {
                    data.SetActive(false);
                    dataFundo.SetActive(false);
                }
            }
            else if ((todasLinhas[linha].Contains("[N2]")))
            {
                som.Play();
                anim.Play("Personagem", 0, 0);
                nome.GetComponent<Text>().text = todasLinhas[linha].Substring(4);
                
                linha++;
                Mensagem();
                linha++;
                horario.GetComponent<Text>().text = todasLinhas[linha].Substring(3);
                linha++;
                if (todasLinhas[linha].Contains("null") == false)
                {
                    data.SetActive(true);
                    data.GetComponent<Text>().text = todasLinhas[linha].Substring(3);
                }
                else
                {
                    data.SetActive(false);
                }
            }
            

        }
    }

    private void Mensagem()
    {
        mensagem.GetComponent<Text>().text = todasLinhas[linha].Substring(4);
        if (mensagem.GetComponent<Text>().text.Contains("<NOME>"))
        {
            mensagem.GetComponent<Text>().text = mensagem.GetComponent<Text>().text.Replace("<NOME>", PlayerSave.GetNome());
        }

    }

    private void FinalizarCelular()
    {
        comentario.SetActive(false);
        fundo.SetActive(false);
        fundoMensagens.SetActive(false);
        data.SetActive(false);
        caixaDialogo.SetActive(true);
        dialogos.FinalizarCelular();
        indexCelularTxt++;
        if (indexCelularTxt < celularTxt.Length)
        {
            
            primeiraLinha = true;
            ZerarTodosoIndex();
            TodasAsLinhasTexto();
        }
    }

    IEnumerator DelayParaCelular()
    {
        yield return new WaitForSeconds(0.3f);
        primeiraVez = false;
        StopCoroutine(DelayParaCelular());
    }
}
