using UnityEngine;

public class DoorController : MonoBehaviour
{


    public Vector3 openOffset = new Vector3(4, 0, 0); //Kapı şuan yukaru kayıyor
    public float speed = 2f;
    private bool shouldOpen = false;
    private Vector3 targetPosition; 
    void Start()
    {
        //Kapının açıldığında gideceği nihai konumu hesapla
        targetPosition = transform.position + openOffset;
    }

    void Update()
    {
        if (shouldOpen)
        {
            //Lerp ile yumuşakbir hareket sağlanıyor ama derste öğrenmemiş olabiliriz
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }
    public void OpenDoor()
    {
        shouldOpen = true;
        //Kapı sesi eklemek istersek buraya eklenecek
    }
}
