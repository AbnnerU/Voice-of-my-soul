using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerSave : MonoBehaviour
{   
    //nome
    public static string GetNome()
    {
        if (PlayerPrefs.HasKey("Nome") == true)
        {
            return PlayerPrefs.GetString("Nome");
        }
        else
        {
            PlayerPrefs.SetString("Nome", "PJ");
            return PlayerPrefs.GetString("Nome");
        }
    }

    public static void SetNome(string nome)
    {              
         PlayerPrefs.SetString("Nome", nome);             
    }

    //Mute

    public static int GetMute()
    {
        if (PlayerPrefs.HasKey("Mute"))
        {
            return PlayerPrefs.GetInt("Mute");
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 1);
            return PlayerPrefs.GetInt("Mute");
        }
    }

    public static void SetMute(int valor)
    {
        PlayerPrefs.SetInt("Mute", valor);
    }

    //Fase
    public static void SetFase(string fase)
    {
        PlayerPrefs.SetString("Fase", fase);
    }

    public static string GetFase()
    {
       return PlayerPrefs.GetString("Fase");
        
    }

    //pontos intimidade
    public static float GetPontosIntimidade()
    {
        if (PlayerPrefs.HasKey("Intimidade") == true)
        {
            return PlayerPrefs.GetFloat("Intimidade");
        }
        else
        {
            PlayerPrefs.SetFloat("Intimidade", 0);
            return PlayerPrefs.GetFloat("Intimidade");
        }
    }

    public static void SetPontosIntimidade(float valor)
    {
        float valorAtual;
        if (PlayerPrefs.HasKey("Intimidade") == true)
        {
            valorAtual = PlayerPrefs.GetFloat("Intimidade");
            PlayerPrefs.SetFloat("Intimidade", valorAtual + valor);
        }
        else
        {
            PlayerPrefs.SetFloat("Intimidade", 0);
            valorAtual = PlayerPrefs.GetFloat("Intimidade");
            PlayerPrefs.SetFloat("Intimidade", valorAtual + valor);
        }
    }

    public static void ResetSaves()
    {
        PlayerPrefs.DeleteKey("Fase");
        PlayerPrefs.DeleteKey("Intimidade");
    }
}


      


    
