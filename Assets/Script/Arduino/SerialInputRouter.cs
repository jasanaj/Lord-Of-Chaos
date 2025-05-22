using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SerialInputRouter : MonoBehaviour
{
    public SerialController serialController;
    public GameObject resultPanel; // ← drag dari Inspector

    private ButtonController[] allButtons;

    void Start()
    {
        allButtons = FindObjectsOfType<ButtonController>();
    }

    void Update()
    {
        string message = serialController.ReadSerialMessage();

        // --- 1. Kirim ke InputReceiver
        if (!string.IsNullOrEmpty(message))
        {
            NoteKey parsedKey;
            if (System.Enum.TryParse(message, out parsedKey))
            {
                InputReceiver.instance?.ReceiveInput(parsedKey);
            }

            // --- 2. Jalankan animasi tombol
            DrumKey? drumKey = ParseDrumKey(message);
            if (drumKey != null)
            {
                foreach (var btn in allButtons)
                {
                    if (btn.GetDrumKey() == drumKey)
                    {
                        btn.Press();
                        StartCoroutine(AutoRelease(btn, 0.1f));
                    }
                }
            }

            // --- 3. Cek ENTER dari ESP32 hanya jika resultPanel aktif
            if (resultPanel.activeSelf && message == "ENTER")
            {
                LoadLeaderboard();
            }
        }

        // --- 4. Cek ENTER dari keyboard
        if (resultPanel.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            LoadLeaderboard();
        }
    }

    DrumKey? ParseDrumKey(string msg)
    {
        switch (msg)
        {
            case "Z": return DrumKey.Z;
            case "X": return DrumKey.X;
            case "C": return DrumKey.C;
            case "B": return DrumKey.B;
            case "N": return DrumKey.N;
            case "M": return DrumKey.M;
            default: return null;
        }
    }

    IEnumerator AutoRelease(ButtonController btn, float delay)
    {
        yield return new WaitForSeconds(delay);
        btn.Release();
    }

    void LoadLeaderboard()
    {
        Debug.Log("Pindah ke scene Leaderboard");
        SceneManager.LoadScene("Leaderboard");
    }
}
