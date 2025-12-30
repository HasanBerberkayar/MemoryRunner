using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class NoteManager : MonoBehaviour
{
    public GameObject notePanel; // UI Paneli
    public Text noteTextDisplay; // 
    public bool isNoteOpen = false;
    public TextMeshProUGUI counterText;
    public int totalNotes = 10; //Tüm notlar
    public int collectedNotes = 0; //toplanan notlar 
    // YENİ: 9. notta açılacak olan özel kapı
    public DoorController specialDoor;
    void Start()
    {
        UpdateCounterUI();
    }
    void Update()
    {
        // Not açıkken E tuşuna basılırsa kapat
        if (isNoteOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseNote();
        }
    }

    public void ShowNote(string content)
    {
        
        //Gelen içeriği Text bileşenine yazdır 
        if (noteTextDisplay != null)
        {
            noteTextDisplay.text = content;
        }

        collectedNotes++;
        UpdateCounterUI();

        if (collectedNotes == 9 && specialDoor != null)
        {
            specialDoor.OpenDoor();
            Debug.Log("Final door has been opened after collecting 9 notes!");
        }

        //Paneli gösterimi ve oyunu durdurma
        notePanel.SetActive(true);
        isNoteOpen = true;
        Time.timeScale = 0f; 

        // Mouse imlecini göster
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseNote()
    {
        notePanel.SetActive(false);
        isNoteOpen = false;
        Time.timeScale = 1f; 

        // Mouse imlecini gizleme kısmı
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        //Tüm notlar toplanınca burda birşey olabilir
        if(collectedNotes >= totalNotes)
        {
            Debug.Log("Tüm Notlar Toplandı! Gizli birşeyi tetikleyebilirsin!");
        }
    }

    //Sayacı ekranda güncelleyen yardımı fonksiyon
    void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = "Notes: " + collectedNotes + " / " + totalNotes; 
        }
    }
}