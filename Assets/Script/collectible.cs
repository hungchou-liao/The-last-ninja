using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 private void OnTriggerEnter(Collider other)
{
    // Ensure you are comparing the name or tag of the GameObject properly
    if (other.gameObject.name == "Player")
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().SetBool("IsCollected", true);
        Destroy(gameObject, 2f);
    }
}
}
