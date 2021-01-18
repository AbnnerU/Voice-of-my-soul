using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoTexto : MonoBehaviour
{
    [SerializeField] private DialogosManeger dialogos;
    [SerializeField] private TextManipulator manipuladorTexto;
    [SerializeField] private int[] linhaDoEvento;
    [SerializeField] private int[] novoTamanhoFonte;
    private int falaAnterior;
    private int eventoAnterior;
    private bool impedir;

    // Start is called before the first frame update
    void Start()
    {

        impedir = true;
        falaAnterior = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (impedir == false)
        {
            for (int i = 0; i < linhaDoEvento.Length; i++)
            {
                if (dialogos.GetNumeroFala() == linhaDoEvento[i])
                {
                    impedir = true;
                    NovoEvento(i);
                }
            }
            impedir = true;
        }

        if (falaAnterior != dialogos.GetNumeroFala())
        {
            impedir = false;
            falaAnterior = dialogos.GetNumeroFala();
        }
    }

    private void NovoEvento(int id)
    {
        manipuladorTexto.AumentarFonte(novoTamanhoFonte[id]);
    }
}
