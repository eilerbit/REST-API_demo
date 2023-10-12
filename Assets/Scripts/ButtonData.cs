using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonData: MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Toggle updated;    
    [SerializeField] private TMP_Text text;

    public TMP_Text id;

    public void Initialize(HSL color, bool updated, string id, string text)
    {        
        image.color = color.Color;
        this.updated.isOn = updated;
        this.id.text = id;
        this.text.text = text;                
    }        
}