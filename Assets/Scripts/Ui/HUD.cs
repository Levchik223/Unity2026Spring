using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image SoulsImage;

    private int SizeSoulImage = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateScore(int score)
    {
        scoreText.text = "score: " + score.ToString(); 
    }

    public void UpdateSouls(int amount)
    {
        SoulsImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SizeSoulImage * amount);
    }
}
