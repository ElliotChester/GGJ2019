using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public AudioClip music;

    // Start is called before the first frame update
    void Start()
    {
		this.GetComponent<AudioSource>().PlayOneShot(music);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
