using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public AudioClip music;
	public AudioClip rain;

	// Start is called before the first frame update
	void Start()
    {
		this.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
