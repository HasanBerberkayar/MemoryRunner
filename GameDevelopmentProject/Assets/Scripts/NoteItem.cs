using UnityEngine;

public class NoteItem : MonoBehaviour
{
    [TextArea(3, 10)]
    public string noteContent;

    private NoteManager noteManager;
    public AudioSource pickupSound;
    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(
                pickupSound.clip,
                transform.position
            );
            noteManager.ShowNote(noteContent);
            Destroy(gameObject);
        }
    }
}