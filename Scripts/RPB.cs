using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPB : MonoBehaviour
{
    public float speed = 8;
    private Rigidbody2D _rigidbody;
    public float maxLifetime = 10.0f;
    public GameObject[] asteroids;
    public Sprite[] runSprites;
    private int spriteIndex;
    public SpriteRenderer spriteRenderer { get; private set; }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }


private void AnimateSprite(){
if (spriteIndex >= runSprites.Length) {
                spriteIndex = 0;
            } else {
        spriteRenderer.sprite = runSprites[spriteIndex];
        spriteIndex++;
    }}


}