using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance;

    public GameObject ProofImageUIPrefab;
    public GameObject ProofImagesLayer;

    public TextMeshProUGUI timeText;

    public List<GameObject> proofImages;

    private void Awake()
    {
        instance = this;
        proofImages = new List<GameObject>();
    }

    public void Start()
    {      
       ParseTime();
    }

    public void Update()
    {
        ParseTime();
    }

    public void CreateProofButton(ProofInfo proof)
    {
        GameObject proofImage= Instantiate(ProofImageUIPrefab, ProofImagesLayer.transform);
        proofImages.Add(proofImage);
        proofImage.GetComponent<Image>().sprite = proof.proofUISprite;
        proofImage.GetComponent<UIProofImage>().info = proof;
    }

    public void CheckProofImage(ProofInfo proof)
    {
        foreach (GameObject imageGO in proofImages)
        {
            UIProofImage uIProofImage = imageGO.GetComponent<UIProofImage>();
            if (uIProofImage.info == proof && !uIProofImage.hasBeenFound)
            {
                uIProofImage.hasBeenFound = true;
                //Cambiar a sprite de completado
                imageGO.GetComponent<Image>().color = Color.green;
                break;
            }
        }
    }

    public void ParseTime()
    {
        float currentTime = GameController.instance.currentTime;

        //parseo del tiempo a formato contador
        int minutos = Mathf.Max(Mathf.FloorToInt(currentTime / 60), 0);     //Extraemos los minutos
        int segundos = Mathf.Max(Mathf.FloorToInt(currentTime % 60), 0);    //Extraemos los segundos
        if (segundos >= 10) timeText.text = minutos + ":" + segundos;       //Si los segundos son mayores o iguales que 10, concatenamos y construimos el texto
        else timeText.text = minutos + ":" + "0" + segundos;                //Si los segundos son menors que 10, añadimos un 0 a nuestros segundos, concatenamos
    }

}
