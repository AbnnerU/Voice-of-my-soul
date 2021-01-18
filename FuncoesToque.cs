using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncoesToque : MonoBehaviour
{

    public static bool ToqueNoBotao(Vector3 posicaoBotao, float tamanhoBotao)
    {

        DetectarToque toqueTela = GameObject.FindObjectOfType<DetectarToque>();
        if (toqueTela.telaPressionada==true && toqueTela.posicaoToque.x >= posicaoBotao.x - tamanhoBotao && toqueTela.posicaoToque.x <= posicaoBotao.x + tamanhoBotao && toqueTela.posicaoToque.y >= posicaoBotao.y - tamanhoBotao && toqueTela.posicaoToque.y <= posicaoBotao.y + tamanhoBotao)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ToqueUpNoBotao(Vector3 posicaoBotao, float tamanhoBotao)
    {
        DetectarToque toqueTela = GameObject.FindObjectOfType<DetectarToque>();
        if (toqueTela.telaPressionada==false && toqueTela.posicaoToqueUp.x >= posicaoBotao.x - tamanhoBotao && toqueTela.posicaoToqueUp.x <= posicaoBotao.x + tamanhoBotao && toqueTela.posicaoToqueUp.y >= posicaoBotao.y - tamanhoBotao && toqueTela.posicaoToqueUp.y <= posicaoBotao.y + tamanhoBotao)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ToqueDragNoBotao(Vector3 posicaoBotao, float tamanhoBotao)
    {
        DetectarToque toqueTela = GameObject.FindObjectOfType<DetectarToque>();
        if (toqueTela.telaPressionada == true && toqueTela.posicaoToqueDrag.x >= posicaoBotao.x - tamanhoBotao && toqueTela.posicaoToqueDrag.x <= posicaoBotao.x + tamanhoBotao && toqueTela.posicaoToqueDrag.y >= posicaoBotao.y - tamanhoBotao && toqueTela.posicaoToqueDrag.y <= posicaoBotao.y + tamanhoBotao)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
