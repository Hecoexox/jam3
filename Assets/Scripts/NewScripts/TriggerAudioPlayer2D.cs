using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class TriggerAudioPlayer2D : MonoBehaviour
{
    public AudioClip soundClip; // Çalýnacak ses
    [TextArea] public string subtitleText; // Altyazý metni (birden fazla cümle olabilir)
    public TextMeshProUGUI subtitleUI; // UI'deki TextMeshPro öðesi
    public float subtitleDelay = 2f; // Her cümle arasýndaki gecikme süresi

    private AudioSource audioSource;
    private bool hasPlayed = false; // Sesin daha önce çalýp çalmadýðýný kontrol eder

    private void Start()
    {
        // AudioSource bileþenini ekle ve yapýlandýr
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = soundClip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed) // Oyuncu tetikleyiciye girerse ve ses çalmadýysa
        {
            audioSource.Play();
            hasPlayed = true; // Sesin bir kez çalmasýný saðla

            if (subtitleUI != null)
            {
                StartCoroutine(DisplaySubtitles());
            }
        }
    }

    private IEnumerator DisplaySubtitles()
    {
        string[] sentences = subtitleText.Split('.'); // Cümleleri noktalara göre ayýr
        foreach (string sentence in sentences)
        {
            if (!string.IsNullOrWhiteSpace(sentence))
            {
                subtitleUI.text = sentence.Trim() + "."; // Cümleyi temizleyip ekrana yaz
                yield return new WaitForSeconds(subtitleDelay); // Belirtilen süre kadar bekle
            }
        }
        subtitleUI.text = ""; // Altyazýyý temizle
    }
}
