using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlueberryBomb : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D _rigidbody;
    public Bullet BulletPrefab;
    public GameObject[] asteroids;
    public Sprite[] runSprites;
    private int spriteIndex;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Vector2 left = new Vector2(-1, 0);
    public Vector2 right = new Vector2(1, 0);
    public Vector2 down = new Vector2(0, -1);
    public Vector2 up = new Vector2(0, 1);
    public Vector2 tr = new Vector2(1,1);
    public Vector2 tl = new Vector2(-1,1);
    public Vector2 br = new Vector2(1,-1);
    public Vector2 bl = new Vector2(-1,-1);
    public AudioSource m_AudioSource; //The thing to play the audio
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet")||collision.gameObject.CompareTag("Player")){return;}else{

        Bullet Bullet1 = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet1.Project(this.left);

        Bullet Bullet2 = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet2.Project(this.right);

        Bullet Bullet3 = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet3.Project(this.down);

        Bullet Bullet4 = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet4.Project(this.up);

        Bullet Bullet5 = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet5.Project(this.tr);

        Bullet Bullet6 = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet6.Project(this.tl);

        Bullet Bullet7 = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet7.Project(this.br);

        Bullet Bullet8 = Instantiate(this.BulletPrefab, this.transform.position, this.transform.rotation);
        Bullet8.Project(this.bl);

m_AudioSource.PlayOneShot(m_AudioSource.clip);
        Destroy(this.gameObject);
        
    }}}