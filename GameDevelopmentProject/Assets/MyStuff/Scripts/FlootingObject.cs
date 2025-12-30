using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float amplitude = 0.3f; // Ne kadar yukarı/aşağı gidecek?
    public float frequency = 1f;   // Ne kadar hızlı hareket edecek?


    private Vector3 startPos;

    void Start()
    {
        // Objenin dünyadaki ilk konumunu kaydet
        startPos = transform.position;
    }

    void Update()
    {
        // Formül: BaşlangıçY + (Sin(Zaman * Hız) * Genişlik)
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Objeyi yeni konum
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Yukarı aşağı hareket 
        newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Kendi ekseni etrafında dönme 
        transform.Rotate(Vector3.up, 50f * Time.deltaTime);
    }
}