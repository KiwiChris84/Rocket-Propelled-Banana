using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioSource m_AudioSource; //The thing to play the audio
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
             m_AudioSource.PlayOneShot(m_AudioSource.clip);
            Destroy(this.gameObject);
        }
    }
}
