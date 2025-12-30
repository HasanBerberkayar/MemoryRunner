using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject text;
    public float textTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(text.active == true && textTimer>0)
        {
            textTimer -= Time.deltaTime;
        }
        else if(text.active == true && textTimer <= 0)
        {
            text.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            text.SetActive(true);
        }
    }
}
