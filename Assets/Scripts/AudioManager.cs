using UnityEngine;

using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] EventReference MusicReference;
    [SerializeField] EventInstance MusicInstance;
    public string volcanoIntensity = "VolcanoIntensity";

    private void Start() {
        MusicInstance = RuntimeManager.CreateInstance(MusicReference);
        MusicInstance.start();
        MusicInstance.setParameterByName(volcanoIntensity, 0f);

    }
    private void OnDestroy() {
        MusicInstance.release();
    }
}