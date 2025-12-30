using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public DoorController targetDoor; // Açılacak kapı referansı
    private bool isPlayerNearby = false; // Oyuncu butona yakın mı?
    private bool isPressed = false; // Butona daha önce basıldı mı?

    void Update()
    {
        // "E" tuşuna basıldıysa
        // kapıyı aç
        if (isPlayerNearby && !isPressed)
        {
            // Standart Input Sistemi kullanıyorsan:
            if (Input.GetKeyDown(KeyCode.E))
            {
                PressButton();
            }
        }
    }

    void PressButton()
    {
        isPressed = true; // Tekrar basılmasını engelle
        
        // Görsel geri bildirim (Butonu yeşil yap)
        GetComponent<Renderer>().material.color = Color.green;

        // Kapıyı aç
        if (targetDoor != null)
        {
            targetDoor.OpenDoor();
            Debug.Log("Butona basıldı, kapı açılıyor!");
        }
        else
        {
            Debug.LogError("Hata: Butona bir kapı (Target Door) atanmamış!");
        }
    }

    // Oyuncu butonun etki alanına girdiğinde
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Kapıyı açmak için 'E' tuşuna bas.");
        }
    }

    // Oyuncu butonun etki alanından çıktığında
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}