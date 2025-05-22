using System.Collections.Generic;
using UnityEngine;

public class InputReceiver : MonoBehaviour
{
    public static InputReceiver instance;

    private List<NoteObject> activeNotes = new List<NoteObject>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void RegisterNote(NoteObject note)
    {
        if (!activeNotes.Contains(note))
            activeNotes.Add(note);
    }

    public void UnregisterNote(NoteObject note)
    {
        if (activeNotes.Contains(note))
            activeNotes.Remove(note);
    }

    public void ReceiveInput(NoteKey key)
    {
        // Cari note yang bisa dipencet dan punya NoteKey sama
        foreach (var note in activeNotes.ToArray()) // salin list untuk aman saat dihapus
        {
            if (note.noteKey == key && note.canBePressed)
            {
                note.OnExternalInput();
                break; // hanya satu yang bisa ditekan per key
            }
        }
    }
}
