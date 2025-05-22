using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectSongController : MonoBehaviour
{
    public GameObject[] menuItems; // drag semua GameObject lagu ke sini
    public AudioSource previewAudioSource;   // <<--- POINT 2: drag AudioSource ke sini dari Inspector

    private int selectedIndex = 0;

    void Start()
    {
        UpdateMenuSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % menuItems.Length;
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Return)) // Enter
        {
            SelectMenuItem();
        }
    }

    void UpdateMenuSelection()  // <<--- POINT 3: ubah fungsi ini
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            menuItems[i].SetActive(i == selectedIndex);
        }

        PlaySongPreview(menuItems[selectedIndex]); // <<-- tambahkan ini di akhir
    }
    public void MoveUp()
    {
        selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;
        UpdateMenuSelection();
    }

    public void MoveDown()
    {
        selectedIndex = (selectedIndex + 1) % menuItems.Length;
        UpdateMenuSelection();
    }

    void PlaySongPreview(GameObject item) // <<--- POINT 4: tambahkan fungsi ini
    {
        SongPreviewPlayer preview = item.GetComponent<SongPreviewPlayer>();

        if (preview != null && preview.previewClip != null)
        {
            if (previewAudioSource.isPlaying)
                previewAudioSource.Stop();

            previewAudioSource.clip = preview.previewClip;
            previewAudioSource.Play();
        }
    }

    void SelectMenuItem()
    {
        Debug.Log("Selected: " + menuItems[selectedIndex].name);
        // Ganti sesuai kebutuhan
        switch (menuItems[selectedIndex].name)
        {
            case "Song1":
                SceneManager.LoadScene("COTW");// SelectSong
                break;
            case "Song2":
                SceneManager.LoadScene("ISWD");// Tutorial
                break;
            case "Song3":
                SceneManager.LoadScene("NT");// Credit
                break;
        }
    }
}
