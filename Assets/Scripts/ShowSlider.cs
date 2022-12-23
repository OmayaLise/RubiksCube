using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ShowSlider : MonoBehaviour
{
    public Slider sliderUI;
    public TextMeshProUGUI textSliderValue;
    Text stored;

    void Start()
    {
        stored = textSliderValue.GetComponent<Text>();
        ShowSliderValue();
    }

    private void Update()
    {
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        string sliderMessage = "" + sliderUI.value;
        textSliderValue.text = sliderMessage;
    }
}