using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] PostProcessVolume _volume;
    [SerializeField] PostProcessLayer _layer;

    string _fx = "FX";
    string _car = "Car";
    string _music = "Music";

    public void SetQuality(float sliderValue)
    {
        QualitySettings.SetQualityLevel((int)sliderValue);
    }
    public void SetAA(float sliderValue)
    {
        switch (sliderValue)
        {
            case 0:
                _layer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
            case 1:
                _layer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
                break;
            case 2:
                _layer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
                break;
            default:
                _layer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
        }
    }

    public void SetAO(float sliderValue)
    {
        AmbientOcclusion AO = _volume.profile.GetSetting<AmbientOcclusion>();
        AO.intensity.value = sliderValue;
    }
    public void SetMB(float sliderValue)
    {
        MotionBlur MB = _volume.profile.GetSetting<MotionBlur>();
        MB.shutterAngle.value = sliderValue;
    }
    public void SetBloom(float sliderValue)
    {
        Bloom bloom = _volume.profile.GetSetting<Bloom>();
        bloom.intensity.value = sliderValue;
    }
    public void SetSSR(float sliderValue)
    {
        ScreenSpaceReflections reflections = _volume.profile.GetSetting<ScreenSpaceReflections>();
        reflections.maximumMarchDistance.value = sliderValue;
    }

    public void SetTemperature(float sliderValue)
    {
        ColorGrading colorGrading = _volume.profile.GetSetting<ColorGrading>();
        colorGrading.gamma.value = new Vector4(sliderValue, sliderValue/5);
    }
}
