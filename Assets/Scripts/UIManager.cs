using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance;

    public TextMeshProUGUI timeText;

    public void Start()
    {
        ParseTime();
    }

    public void Update()
    {
        ParseTime();
    }

    public void ParseTime()
    {
        float currentTime = GameController.instance.currentTime;

        //parseo del tiempo a formato contador
        int minutos = Mathf.Max(Mathf.FloorToInt(currentTime / 60), 0);     //Extraemos los minutos
        int segundos = Mathf.Max(Mathf.FloorToInt(currentTime % 60), 0);    //Extraemos los segundos
        if (segundos >= 10) timeText.text = minutos + ":" + segundos;                    //Si los segundos son mayores o iguales que 10, concatenamos y construimos el texto
        else timeText.text = minutos + ":" + "0" + segundos;                             //Si los segundos son menors que 10, añadimos un 0 a nuestros segundos, concatenamos
    }

}
