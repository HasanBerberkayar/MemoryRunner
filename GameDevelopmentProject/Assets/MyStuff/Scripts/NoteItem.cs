using UnityEngine;

public class NoteItem : MonoBehaviour
{
    [TextArea(3, 10)] // Inspector panelinde yazmak için alan 
    public string noteContent; 
    
    private NoteManager noteManager;

    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Eğer oyuncu (Player etiketli obje) çarparsa
        if (other.CompareTag("Player"))
        {
            noteManager.ShowNote(noteContent);
            Destroy(gameObject); // Notu yerden kaldırır
        }
    }
}