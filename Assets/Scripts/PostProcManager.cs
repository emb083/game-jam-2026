using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcManager : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;
    private Bloom bloom;
    private ColorAdjustments color;
    private FilmGrain film;
    private LensDistortion lens;

    public static PostProcManager Instance {get; private set;}
    
    void Awake(){
        Instance = this;

        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<Bloom>(out bloom);
        volume.profile.TryGet<ColorAdjustments>(out color);
        volume.profile.TryGet<FilmGrain>(out film);
        volume.profile.TryGet<LensDistortion>(out lens);
    }

    public void ApplyImagineVFX(){
        Color32 customColor = new Color32(252, 5, 255, 255);
        vignette.color.value = customColor;
        vignette.intensity.overrideState = true;
        vignette.intensity.value = .6f;

        bloom.active = true;
        bloom.intensity.value = 1.15f;

        color.postExposure.value = 1.5f;
        color.contrast.value = 40f;
        color.hueShift.value = -88f;
        color.saturation.value = 30f;

        film.active = false;
        lens.active = false;
    }

    public void ApplyInsanityVFX(){
        Color32 customColor = new Color32(252, 5, 255, 255);
        vignette.color.value = customColor;
        vignette.intensity.overrideState = true;
        vignette.intensity.value = 1f;

        bloom.active = true;
        bloom.intensity.value = 0.5f;

        color.postExposure.value = 2.5f;
        color.contrast.value = 100f;
        color.hueShift.value = 17f;
        color.saturation.value = 100f;

        film.active = true;
        film.type.Override(FilmGrainLookup.Large02);
        film.intensity.value = 1f;

        lens.active = true;
        lens.intensity.overrideState = true;
        lens.intensity.value = 0.4f;
    }

    public void ApplyRealityVFX(){
        Color32 customColor = new Color32(41, 36, 41, 255);
        vignette.color.value = customColor;
        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0.18f;

        bloom.active = false;

        color.postExposure.value = 0.9f;
        color.contrast.value = 81f;
        color.hueShift.value = 9f;
        color.saturation.value = -73f;

        film.active = true;
        film.type.Override(FilmGrainLookup.Large02);
        film.intensity.value = 0.7f;

        lens.active = false;
    }

    public void ApplyDepressionVFX(){
        Color32 customColor = new Color32(41, 36, 41, 255);
        vignette.color.value = customColor;
        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0.37f;

        bloom.active = false;

        color.postExposure.value = 0.9f;
        color.contrast.value = -4f;
        color.hueShift.value = -107f;
        color.saturation.value = -100f;

        film.active = true;
        film.type.Override(FilmGrainLookup.Large02);
        film.intensity.value = 1f;

        lens.active = false;
    }
}
