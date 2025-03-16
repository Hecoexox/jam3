using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; 


public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Audio Mixer referans�

    public void SetVolume(float volume)
    {
        // dB cinsine �evirerek sesi ayarla
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Kullan�c�n�n ses seviyesini kaydet
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
