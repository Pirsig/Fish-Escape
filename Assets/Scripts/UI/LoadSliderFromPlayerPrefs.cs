using UnityEngine;
using UnityEngine.UI;

public class LoadSliderFromPlayerPrefs : MonoBehaviour
{
    public string keyToLoad; 

    private void OnEnable()
    {
        Slider slider = GetComponentInChildren<Slider>();
        slider.value = PlayerPrefs.GetFloat(keyToLoad);
    }
}
