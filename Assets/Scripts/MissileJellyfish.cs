using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class MissileJellyfish : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    
    public Sprite[] sprites;
    public Sprite[] runSprites;
    private int spriteIndex;

    public float size = 1f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50f;
    public float maxLifetime = 30f;
  

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Assign random properties to make each MissileJellyfish feel unique
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        // Set the scale and mass of the MissileJellyfish based on the assigned size so
        // the physics is more realistic
        transform.localScale = Vector3.one * size;
        rigidbody.mass = size;

        // Destroy the MissileJellyfish after it reaches its max lifetime
        Destroy(gameObject, maxLifetime);
        
    }

    public void SetTrajectory(Vector2 direction)
    {
        // The MissileJellyfish only needs a force to be added once since they have no
        // drag to make them stop moving
        rigidbody.AddForce(direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Check if the MissileJellyfish is large enough to split in half
            // (both parts must be greater than the minimum size)
            if ((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            
         
            FindObjectOfType<GameManager>().MissileJellyfishDestroyed(this);

            // Destroy the current MissileJellyfish since it is either replaced by two
            // new MissileJellyfishs or small enough to be destroyed by the bullet
            Destroy(gameObject);
            
        }
    }

    private MissileJellyfish CreateSplit()
    {
        // Set the new MissileJellyfish poistion to be the same as the current MissileJellyfish
        // but with a slight offset so they do not spawn inside each other
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // Create the new MissileJellyfish at half the size of the current
        MissileJellyfish half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;

        // Set a random trajectory
        half.SetTrajectory(Random.insideUnitCircle.normalized);

        return half;
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSprite), 1f/12f, 1f/12f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void AnimateSprite(){
if (spriteIndex >= runSprites.Length) {
                spriteIndex = 0;
            } else {
        spriteRenderer.sprite = runSprites[spriteIndex];
        spriteIndex++;
    }
}}
