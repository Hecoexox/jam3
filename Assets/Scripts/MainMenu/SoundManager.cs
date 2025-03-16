using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; 


public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Audio Mixer referansý

    public void SetVolume(float volume)
    {
        // dB cinsine çevirerek sesi ayarla
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Kullanýcýnýn ses seviyesini kaydet
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
