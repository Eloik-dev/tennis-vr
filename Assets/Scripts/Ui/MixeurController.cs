using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixeurController : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameterName = "Volume";

    void Start()
    {
        Slider slider = GetComponent<Slider>();

        if (mixer != null && slider != null)
        {
            float currentValue;
            if (mixer.GetFloat(parameterName, out currentValue))
            {
                slider.value = currentValue;
            }

            slider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        if (mixer != null)
        {
            mixer.SetFloat(parameterName, value);
        }
    }
}
