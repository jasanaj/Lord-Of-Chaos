using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public GameObject[] menuItems; // drag semua GameObject lagu ke sini

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

    void SelectMenuItem()
    {
        Debug.Log("Selected: " + menuItems[selectedIndex].name);
        // Ganti sesuai kebutuhan
        switch (menuItems[selectedIndex].name)
        {
            case "SelectSong":
                SceneManager.LoadScene("SelectSong");// SelectSong
                break;
            case "Tutorial":
                SceneManager.LoadScene("Tutorial");// Tutorial
                break;
            case "Credit":
                SceneManager.LoadScene("Credit");// Credit
                break;
            case "Leaderboard":
                SceneManager.LoadScene("Leaderboard");
                break;
        }
    }
}
