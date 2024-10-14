using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
            other.GetComponent<timeScore>().gemsCollected += 1;

            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetBool("IsCollected", true);
            Destroy(gameObject, 1f);
        }
    }
}
