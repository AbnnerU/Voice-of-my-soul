using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogosManeger : MonoBehaviour
{
    [SerializeField] private float velocidadeTexto;
    [SerializeField] private Text nomeTextUI;
    [SerializeField] private Text falaTextUI;
    [SerializeField] private GameObject elementosDialogo;
    [SerializeField] private Animator pensamentos;
    [Header("Caixa dialogo")]
    [SerializeField] private Animator animatorCXDialogo;
    [SerializeField] private GameObject[] componetesCXDialogo;
    [SerializeField] private Sprite[] cxDialogo1;
    [SerializeField] private Sprite[] cxDialogo2;
    [SerializeField] private Sprite[] cxDialogo3;
    [SerializeField] private Sprite[] cxDialogo4;
    [Header("TXT")]
    [SerializeField] private TextAsset texto;
    [SerializeField] private string proximaCena;
    private TransicaoManeger transicao;
    private DetectarToqueVN toque;
    private VNManager maneger;
    private string[] textoRecortado;
    private string[] todasLinhas;
    private string[] respostasOpcoes;
    private string[] pontosIntimidade;
    private int[] respostas;
    private int[] r1;
    private int[] r2;
    private int[] r3;
    private int[] r4;
    private int[] def;
    private int[] tamanhoFala;
    private int[] somarFalas;
    private string opcoesRespostas;
    private string nomeAtual;
    private int indexRespostasOpcoes;
    private int indexPontosIntimidade;
    private int indexLinhas;
    private int indexFalas;
    private int indexNomes;
    private int indexRespostas;
    private int indexR1;
    private int indexR2;
    private int indexR3;
    private int indexR4;
    private int indexTamanho;
    private int respostaEscolhida;
    private int indexDefault;
    private int linha;
    private int numeroDaFala;
    private bool tocou;
    private bool celular;
    private bool bilhete;
    private bool continuarDialogo;
    private bool aguardarResposta;
    private bool fimDialogos;
    private bool textoDefinido;
    private bool pensamentoIniciado;
    // Start is called before the first frame update
    void Awake()
    {
        nomeAtual = "";
        toque=FindObjectOfType<DetectarToqueVN>();
        maneger = FindObjectOfType<VNManager>();
        fimDialogos = false;
        celular = false;
        bilhete = false;
        continuarDialogo = true;  
        aguardarResposta = false;
        pensamentoIniciado = false;
        linha = 0;
        opcoesRespostas = "";
        numeroDaFala = 0;
        if (GameObject.FindObjectOfType<TransicaoManeger>() != null)
        {
            transicao = FindObjectOfType<TransicaoManeger>();
        }

        if (texto)
        {
            textoDefinido = true;
            ConfigurarRoteiro();
        }
        else
        {
            textoDefinido = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(numeroDaFala);
        if (textoDefinido)
        {          
            if (maneger.GetPause() == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Input.mousePosition.y < (Screen.height/2))
                    {
                        tocou = true;
                    }
                }

                if (tocou && transicao.GetToque() == true)
                {
                    tocou = false;
                    if (aguardarResposta == false && continuarDialogo == true && fimDialogos == false)
                    {
                        linha++;
                        if (todasLinhas[linha].Contains("[N]"))
                        {
                            Fala();
                            numeroDaFala++;
                        }
                        else if (todasLinhas[linha].Contains("[RESPOSTAS]"))
                        {
                            RespostasOpcoes(respostasOpcoes[indexRespostasOpcoes]);
                            indexRespostasOpcoes++;
                        }
                        else if (todasLinhas[linha].Contains("[/DEFAULT]"))
                        {
                            RotomarTextoDefault();
                        }
                        else if (todasLinhas[linha].Contains("[BILHETE]"))
                        {
                            continuarDialogo = false;
                            bilhete = true;
                        }
                        else if (todasLinhas[linha].Contains("[CELULAR]"))
                        {
                            continuarDialogo = false;
                            celular = true;
                        }
                        else if (todasLinhas[linha].Contains("[CELULAR]"))
                        {
                            continuarDialogo = false;
                            celular = true;
                        }
                        else if (todasLinhas[linha].Contains("[TRANSICAO]"))
                        {
                            string frase = todasLinhas[linha].Substring(11);
                            transicao.IniciarTransicao(frase);

                        }
                        else if (todasLinhas[linha].Contains("[FIM]"))
                        {
                            fimDialogos = true;
                        }
                    }
                    else if (fimDialogos == true)
                    {
                        PlayerSave.SetFase(proximaCena);
                        transicao.TransicaoFim(proximaCena);
                    }

                }
            }
        }
    }

    IEnumerator Digitar(string frase)
    {
        falaTextUI.text = "";
        foreach (char letra in frase.ToCharArray())
        {
            falaTextUI.text += letra;
            yield return new WaitForSeconds(velocidadeTexto);
        }
        StopAllCoroutines();
    }

    private void Fala()
    {
        StopAllCoroutines();
        if (todasLinhas[linha].Contains("[N]"))
        {
            nomeTextUI.text = todasLinhas[linha].Substring(3);
            if (nomeTextUI.text.Contains("<NOME>"))
            {            
                nomeTextUI.text = nomeTextUI.text.Replace("<NOME>", PlayerSave.GetNome());
            }

            if (nomeTextUI.text.Length<=1)
            {
                if (pensamentoIniciado == false)
                {
                    componetesCXDialogo[0].GetComponent<Image>().sprite = cxDialogo1[0];
                    elementosDialogo.SetActive(false);
                    pensamentos.Play("Pensamento");
                    pensamentoIniciado = true;
                }
            }
            else
            {
                if (pensamentoIniciado == true)
                {
                    elementosDialogo.SetActive(true);
                    pensamentos.Play("Dialogo");
                    pensamentoIniciado = false;
                }

                if (nomeAtual != nomeTextUI.text)
                {
                    if (nomeTextUI.text.Contains(PlayerSave.GetNome()))
                    {
                        animatorCXDialogo.Play("Defalt", 0, 0);
                        componetesCXDialogo[0].GetComponent<Image>().sprite = cxDialogo1[0];
                        componetesCXDialogo[1].GetComponent<Image>().sprite = cxDialogo1[1];
                        componetesCXDialogo[2].GetComponent<Image>().sprite = cxDialogo1[2];
                        componetesCXDialogo[3].GetComponent<Image>().sprite = cxDialogo1[3];
                        nomeAtual = nomeTextUI.text;
                    }
                    else if (nomeTextUI.text.Contains("Amanda"))
                    {
                        animatorCXDialogo.Play("Defalt", 0, 0);
                        componetesCXDialogo[0].GetComponent<Image>().sprite = cxDialogo2[0];
                        componetesCXDialogo[1].GetComponent<Image>().sprite = cxDialogo2[1];
                        componetesCXDialogo[2].GetComponent<Image>().sprite = cxDialogo2[2];
                        componetesCXDialogo[3].GetComponent<Image>().sprite = cxDialogo2[3];
                        nomeAtual = nomeTextUI.text;
                    }
                    else if (nomeTextUI.text.Contains("Daniel"))
                    {
                        animatorCXDialogo.Play("Defalt", 0, 0);
                        componetesCXDialogo[0].GetComponent<Image>().sprite = cxDialogo3[0];
                        componetesCXDialogo[1].GetComponent<Image>().sprite = cxDialogo3[1];
                        componetesCXDialogo[2].GetComponent<Image>().sprite = cxDialogo3[2];
                        componetesCXDialogo[3].GetComponent<Image>().sprite = cxDialogo3[3];
                        nomeAtual = nomeTextUI.text;
                    }
                    else if (nomeTextUI.text.Contains("Mia"))
                    {
                        animatorCXDialogo.Play("Defalt", 0, 0);
                        componetesCXDialogo[0].GetComponent<Image>().sprite = cxDialogo4[0];
                        componetesCXDialogo[1].GetComponent<Image>().sprite = cxDialogo4[1];
                        componetesCXDialogo[2].GetComponent<Image>().sprite = cxDialogo4[2];
                        componetesCXDialogo[3].GetComponent<Image>().sprite = cxDialogo4[3];
                        nomeAtual = nomeTextUI.text;
                    }
                    else
                    {
                        animatorCXDialogo.Play("Defalt", 0, 0);
                        componetesCXDialogo[0].GetComponent<Image>().sprite = cxDialogo1[0];
                        componetesCXDialogo[1].GetComponent<Image>().sprite = cxDialogo1[1];
                        componetesCXDialogo[2].GetComponent<Image>().sprite = cxDialogo1[2];
                        componetesCXDialogo[3].GetComponent<Image>().sprite = cxDialogo1[3];
                        nomeAtual = nomeTextUI.text;
                    }
                }
            }
            linha++;

            //falaTextUI.text = todasLinhas[linha].Substring(3);

            //if (falaTextUI.text.Contains("<NOME>"))
            //{
            //    falaTextUI.text = falaTextUI.text.Replace("<NOME>", PlayerSave.GetNome());
            //}
            string sentenca = todasLinhas[linha].Substring(3);
            if (sentenca.Contains("<NOME>"))
            {
                sentenca = sentenca.Replace("<NOME>", PlayerSave.GetNome());
            }
            StartCoroutine(Digitar(sentenca));

        }
    }

    

    private void ConfigurarRoteiro()
    {
        
        ZerarTodosIndex();

        TodasAsLinhasTexto();

        //foreach (string s in todasLinhas)
        //{
        //    print(s);
        //}

        for (int i = 0; i < todasLinhas.Length; i++)// Definir o tamanho das arrays 
        {
            if (todasLinhas[i].Contains("[RESPOSTAS]"))
            {
                indexRespostas++;
            }
            else if (todasLinhas[i].Contains("[R1]"))
            {
                indexR1++;
            }
            else if (todasLinhas[i].Contains("[R2]"))
            {
                indexR2++;
            }
            else if (todasLinhas[i].Contains("[R3]"))
            {
                indexR3++;
            }
            else if (todasLinhas[i].Contains("[R4]"))
            {
                indexR4++;
            }
            else if (todasLinhas[i].Contains("[DEFAULT]"))
            {
                indexDefault++;
            }
            else if (todasLinhas[i].Contains("[R]"))
            {
                indexRespostasOpcoes++;
            }
            else if (todasLinhas[i].Contains("[P]"))
            {
                indexPontosIntimidade++;
            }
            else if (todasLinhas[i].Contains("[T]"))
            {
                indexTamanho++;
            }
        }

        respostas = new int[indexRespostas];
        respostasOpcoes = new string[indexRespostasOpcoes];
        pontosIntimidade = new string[indexPontosIntimidade];
        r1 = new int[indexR1];
        r2 = new int[indexR2];
        r3 = new int[indexR3];
        r4 = new int[indexR4];
        def = new int[indexDefault];
        tamanhoFala = new int[indexTamanho];
        ZerarTodosIndex();

        for (int i = 0; i < todasLinhas.Length; i++) // Atribuindo valores nas arrays baseado no separador definido
        {
            if (todasLinhas[i].Contains("[RESPOSTAS]"))
            {
                respostas[indexRespostas] = i;
                indexRespostas++;
            }
            else if (todasLinhas[i].Contains("[R1]"))
            {
                r1[indexR1] = i;
                indexR1++;
            }
            else if (todasLinhas[i].Contains("[R2]"))
            {
                r2[indexR2] = i;
                indexR2++;
            }
            else if (todasLinhas[i].Contains("[R3]"))
            {
                r3[indexR3] = i;
                indexR3++;
            }
            else if (todasLinhas[i].Contains("[R4]"))
            {
                r4[indexR4] = i;
                indexR4++;
            }
            else if (todasLinhas[i].Contains("[DEFAULT]"))
            {
                def[indexDefault] = i;
                indexDefault++;
            }
            else if (todasLinhas[i].Contains("[T]"))
            {
                tamanhoFala[indexTamanho] = i;
                indexTamanho++;
            }
            else if (todasLinhas[i].Contains("[R]"))
            {
                respostasOpcoes[indexRespostasOpcoes] = todasLinhas[i].Substring(3);
                indexRespostasOpcoes++;
            }
            else if (todasLinhas[i].Contains("[P]"))
            {
                pontosIntimidade[indexPontosIntimidade] = todasLinhas[i].Substring(3);
                indexPontosIntimidade++;
            }
        }


        if (todasLinhas[0].Contains("[INICIO]"))
        {
            string frase = todasLinhas[0].Substring(8);
            transicao.TransicaoInicio(frase);          
        }

        ZerarTodosIndex();
        textoDefinido = true;
    }

    private void ZerarTodosIndex()
    {
        indexNomes = 0;
        indexFalas = 0;
        indexRespostas = 0;
        indexR1 = 0;
        indexR2 = 0;
        indexR3 = 0;
        indexR4 = 0;
        indexTamanho = 0;
        indexDefault = 0;
        indexLinhas = 0;
        indexRespostasOpcoes = 0;
        indexPontosIntimidade = 0;
    }

    private void TodasAsLinhasTexto()
    {
        textoRecortado = texto.text.Split('\n');

        foreach (string s in textoRecortado)
        {
            if (s.Contains("[N]") || s.Contains("[F]") || s.Contains("[RESPOSTAS]") || s.Contains("[R1]") || s.Contains("[R2]") || s.Contains("[R3]") || s.Contains("[R4]") || s.Contains("[DEFAULT]") || s.Contains("[/DEFAULT]") || s.Contains("[R]") || s.Contains("[FIM]") || s.Contains("[BILHETE]") || s.Contains("[CELULAR]") || s.Contains("[TRANSICAO]") || s.Contains("[P]") || s.Contains("[INICIO]") || s.Contains("[T]"))
            {
                indexLinhas++;
            }
        }
        todasLinhas = new string[indexLinhas];
        indexLinhas = 0;

        for (int i = 0; i < textoRecortado.Length; i++)
        {
            if (textoRecortado[i].Contains("[N]") || textoRecortado[i].Contains("[F]") || textoRecortado[i].Contains("[RESPOSTAS]") || textoRecortado[i].Contains("[R1]") || textoRecortado[i].Contains("[R2]") || textoRecortado[i].Contains("[R3]") || textoRecortado[i].Contains("[R4]") || textoRecortado[i].Contains("[DEFAULT]") || textoRecortado[i].Contains("[/DEFAULT]") || textoRecortado[i].Contains("[R]") || textoRecortado[i].Contains("[FIM]") || textoRecortado[i].Contains("[BILHETE]") || textoRecortado[i].Contains("[CELULAR]") || textoRecortado[i].Contains("[TRANSICAO]") || textoRecortado[i].Contains("[P]") || textoRecortado[i].Contains("[INICIO]") || textoRecortado[i].Contains("[T]"))
            {
                todasLinhas[indexLinhas] = textoRecortado[i];
                indexLinhas++;
            }
        }
    }

    private void RespostasOpcoes(string opcoes)
    {
        string textoFormatado = todasLinhas[tamanhoFala[indexTamanho]].Substring(3);
        string[] numeros = textoFormatado.Split('/');
        somarFalas = new int[4];
        for (int i = 0; i < 4; i++)
        {
            somarFalas[i] = int.Parse(numeros[i]);
            //print(somarFalas[i]);
        }
        indexTamanho++;

        opcoesRespostas = opcoes;
        aguardarResposta = true;
        //print("RESPOSTAS");
    }

    private void RotomarTextoDefault()
    {
        linha = def[indexDefault];
        indexDefault++;       
        switch (respostaEscolhida)
        {
            case 1:
                numeroDaFala += somarFalas[1] + somarFalas[2] + somarFalas[3];
            break;
            case 2:
                numeroDaFala += somarFalas[2] + somarFalas[3];
                break;
            case 3:
                numeroDaFala += somarFalas[3];
                break;
        }
        

        ProximaLinha();
    }

    public void Resposta1()
    {
        respostaEscolhida = 1;
        AddPontosIntimidade(1);
        aguardarResposta = false;
        linha = r1[indexR1];
        indexR1++;
        indexR2++;
        indexR3++;
        indexR4++;
       
        ProximaLinha();
       
    }

    public void Resposta2()
    {
        respostaEscolhida = 2;
        AddPontosIntimidade(2);
        aguardarResposta = false;
        linha = r2[indexR2];
        indexR1++;
        indexR2++;
        indexR3++;
        indexR4++;

        numeroDaFala += somarFalas[0];

        ProximaLinha();
        
    }

    public void Resposta3()
    {
        respostaEscolhida = 3;
        AddPontosIntimidade(3);
        aguardarResposta = false;
        linha = r3[indexR3];
        indexR1++;
        indexR2++;
        indexR3++;
        indexR4++;

        numeroDaFala += somarFalas[0] + somarFalas[1];

        ProximaLinha();
        
    }

    public void Resposta4()
    {
        respostaEscolhida = 4;
        AddPontosIntimidade(4);
        aguardarResposta = false;
        linha = r4[indexR4];
        indexR1++;
        indexR2++;
        indexR3++;
        indexR4++;

        numeroDaFala += somarFalas[0] + somarFalas[1] + somarFalas[2];

        ProximaLinha();
        
    }

    private void AddPontosIntimidade(int botaoNumero)
    {
        string[] recorte = pontosIntimidade[indexPontosIntimidade].Split('/');
        switch (botaoNumero)
        {
            case 1:
                PlayerSave.SetPontosIntimidade(float.Parse(recorte[0]));
                break;
            case 2:
                PlayerSave.SetPontosIntimidade(float.Parse(recorte[1]));
                break;
            case 3:
                PlayerSave.SetPontosIntimidade(float.Parse(recorte[2]));
                break;
            case 4:
                PlayerSave.SetPontosIntimidade(float.Parse(recorte[3]));
                break;
        }
        print(PlayerSave.GetPontosIntimidade());
        indexPontosIntimidade++;
    }

    public void FinalizarCelular()
    {
        continuarDialogo = true;
        celular = false;
        nomeTextUI.text = "";
        falaTextUI.text = "";
      
        ProximaLinha();
        
    }

    public void FinalizarBilhete()
    {
        continuarDialogo = true;
        bilhete = false;
        nomeTextUI.text = "";
        falaTextUI.text = "";
       
         ProximaLinha();
        
    }
    
    public void ProximaLinha()
    {
        if (todasLinhas[linha + 1].Contains("[FIM]"))
        {
            fimDialogos = true;
        }
        else
        {
            linha++;
            if (todasLinhas[linha].Contains("[N]"))
            {
                Fala();
                numeroDaFala++;
            }
            else if (todasLinhas[linha].Contains("[RESPOSTAS]"))
            {
                RespostasOpcoes(respostasOpcoes[indexRespostasOpcoes]);
                indexRespostasOpcoes++;
            }
            else if (todasLinhas[linha].Contains("[BILHETE]"))
            {
                continuarDialogo = false;
                bilhete = true;
            }
            else if (todasLinhas[linha].Contains("[CELULAR]"))
            {
                continuarDialogo = false;
                celular = true;
            }
        }
    }


    //GETS E SETS 

    public string GetOpcoesRespostas()
    {
        return opcoesRespostas;
    }

    public int GetNumeroFala()
    {
        return numeroDaFala;
    }

    public int GetLinha()
    {
        return linha;
    }

    public bool GetAguardarResposta()
    {
        return aguardarResposta;
    }

    public bool GetBilhete()
    {
        return bilhete;
    }

    public bool GetCelular()
    {
        return celular;
    }

    public void SetRoteiro(TextAsset roteiro)
    {
        texto = roteiro;
        ConfigurarRoteiro();
    }

   
}

