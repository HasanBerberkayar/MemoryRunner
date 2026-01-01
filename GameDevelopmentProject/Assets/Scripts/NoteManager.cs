using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class NoteManager : MonoBehaviour
{
    public GameObject notePanel;
    public UnityEngine.UI.Text noteTextDisplay;
    public bool isNoteOpen = false;
    public TextMeshProUGUI counterText;
    public int totalNotes = 10;
    public int collectedNotes = 0;
    public DoorController specialDoor;
    public GameObject note9CollectedText;
    public float textTimer;
    public GameObject winScreen;
    void Start()
    {
        UpdateCounterUI();
    }
    void Update()
    {
        if (isNoteOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseNote();
        }

        if (note9CollectedText.active == true && textTimer > 0)
        {
            textTimer -= Time.deltaTime;
        }
        else if (note9CollectedText.active == true && textTimer <= 0)
        {
            note9CollectedText.SetActive(false);
        }
    }

    public void ShowNote(string content)
    {

        if (noteTextDisplay != null)
        {
            noteTextDisplay.text = content;
        }

        collectedNotes++;
        UpdateCounterUI();

        if (collectedNotes == 9 && specialDoor != null)
        {
            specialDoor.OpenDoor();
            note9CollectedText.SetActive(true);
        }

        notePanel.SetActive(true);
        isNoteOpen = true;
        Time.timeScale = 0f;
    }

    public void CloseNote()
    {
        notePanel.SetActive(false);
        isNoteOpen = false;
        Time.timeScale = 1f;

        if (collectedNotes >= totalNotes)
        {
            winScreen.SetActive(true);
        }
    }

    void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = "Notes: " + collectedNotes + " / " + totalNotes;
        }
    }
}