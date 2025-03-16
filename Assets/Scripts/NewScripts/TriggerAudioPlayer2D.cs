using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class TriggerAudioPlayer2D : MonoBehaviour
{
    public AudioClip soundClip; // �al�nacak ses
    [TextArea] public string subtitleText; // Altyaz� metni (birden fazla c�mle olabilir)
    public TextMeshProUGUI subtitleUI; // UI'deki TextMeshPro ��esi
    public float subtitleDelay = 2f; // Her c�mle aras�ndaki gecikme s�resi

    private AudioSource audioSource;
    private bool hasPlayed = false; // Sesin daha �nce �al�p �almad���n� kontrol eder

    private void Start()
    {
        // AudioSource bile�enini ekle ve yap�land�r
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = soundClip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed) // Oyuncu tetikleyiciye girerse ve ses �almad�ysa
        {
            audioSource.Play();
            hasPlayed = true; // Sesin bir kez �almas�n� sa�la

            if (subtitleUI != null)
            {
                StartCoroutine(DisplaySubtitles());
            }
        }
    }

    private IEnumerator DisplaySubtitles()
    {
        string[] sentences = subtitleText.Split('.'); // C�mleleri noktalara g�re ay�r
        foreach (string sentence in sentences)
        {
            if (!string.IsNullOrWhiteSpace(sentence))
            {
                subtitleUI.text = sentence.Trim() + "."; // C�mleyi temizleyip ekrana yaz
                yield return new WaitForSeconds(subtitleDelay); // Belirtilen s�re kadar bekle
            }
        }
        subtitleUI.text = ""; // Altyaz�y� temizle
    }
}
