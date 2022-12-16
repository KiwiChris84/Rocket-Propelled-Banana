using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public Bullet bulletPrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite[] runSprites;
public Sprite[] RedSprites;
public Sprite[] OrangeSprites;
public Sprite[] BlueSprites;
public Sprite[] YellowSprites;
public Sprite[] GreenSprites;
public int spriteIndex;
public AudioSource m_AudioSource; //The thing to play the audio
    

    public float thrustSpeed = 1f;
    public bool thrusting { get; private set; }
    public bool reversing { get; private set; }

    public float _turnDirection { get; private set; } = 0f;
    public float rotationSpeed = 0.1f;

    public float respawnDelay = 3f;
    public float respawnInvulnerability = 3f;


    public Vector2 velocity = new Vector2(0,3);
    private bool _boost;

    
    public float turnSpeed = 0.1f;

    
    public Bullet2 bullet2Prefab;

    public Bullet3 bullet3Prefab;

    public BlueberryBomb BlueberryPrefab;
    
    public bool bullet2Ready = false;
    public bool bullet3Ready = false;

    public RPB rpbPrefab;
    public bool RPBanana = false;
    public bool BlueBerryBombReady = false;
    
     



    public Vector2 left = new Vector2(-1, 0);
    public Vector2 right = new Vector2(1, 0);
    public Vector2 down = new Vector2(0, -1);
    public Vector2 up = new Vector2(0, 1);
    public Vector2 tr = new Vector2(1,1);
    public Vector2 tl = new Vector2(-1,1);
    public Vector2 br = new Vector2(1,-1);
    public Vector2 bl = new Vector2(-1,-1);
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

                 m_AudioSource = GetComponent<AudioSource>();
 
         if (m_AudioSource == null)
         {
             Debug.LogError("No AudioSource found");
         }
    }

    private void OnEnable()
    {
        // Turn off collisions for a few seconds after spawning to ensure the
        // player has enough time to safely move away from asteroids
        gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Invoke(nameof(TurnOnCollisions), respawnInvulnerability);
        InvokeRepeating(nameof(AnimateSprite), 1f/12f, 1f/12f);
    }

    private void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        reversing = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
     if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
     {
         _turnDirection = 1.0f;
     }
     else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
      {
          _turnDirection = -1.0f;
     }
     else
     {
         _turnDirection = 0.0f;
      }

        if (Input.GetMouseButtonDown(0)) {
            ShootPrimary();
        }
             if((Input.GetKey(KeyCode.Space)) && (bullet2Ready == true))
      {
        ShootSecondary();
        bullet2Ready = false;
        
      }else if ((Input.GetKey(KeyCode.Space) ) && (RPBanana == true)){
        ShootRPB();
    
     }else if((Input.GetKey(KeyCode.Space) ) && (bullet3Ready == true)){

ShootApple();}

         else if ((Input.GetKey(KeyCode.Space) ) && (BlueBerryBombReady == true)){
         
ShootBomb();}}

    private void FixedUpdate()
    {
        if (thrusting) {
            rigidbody.AddForce(this.transform.up * thrustSpeed);
        }
        if (reversing) {
            rigidbody.AddForce(-this.transform.up * thrustSpeed);
        }

        if (_turnDirection != 0f) {
            rigidbody.AddTorque(turnSpeed *_turnDirection);
        }
    }

   private void ShootPrimary()
   {
    PlayShootingSound();
    Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
    bullet.Project(this.transform.up);
   }

    private void ShootSecondary()
   {

    PlayShootingSound();
    Bullet2 bullet2_1 = Instantiate(this.bullet2Prefab, this.transform.position, this.transform.rotation);
    bullet2_1.Project(this.transform.up);

    Bullet2 bullet2_2 = Instantiate(this.bullet2Prefab, this.transform.position, this.transform.rotation);
    bullet2_2.Project(this.transform.up);
    bullet2_2.Project(this.transform.right);

    Bullet2 bullet2_3 = Instantiate(this.bullet2Prefab, this.transform.position, this.transform.rotation);
    bullet2_3.Project(this.transform.up);
    bullet2_3.ProjectNeg(this.transform.right);

    bullet2Ready = false;
    
   }

      private void ShootRPB()
    {
        PlayShootingSound();
       RPB rpb1 = Instantiate(this.rpbPrefab, this.transform.position, this.transform.rotation);
        rpb1.Project(this.left);

        RPB rpb2 = Instantiate(this.rpbPrefab, this.transform.position, this.transform.rotation);
        rpb2.Project(this.right);

        RPB rpb3 = Instantiate(this.rpbPrefab, this.transform.position, this.transform.rotation);
        rpb3.Project(this.down);

        RPB rpb4 = Instantiate(this.rpbPrefab, this.transform.position, this.transform.rotation);
        rpb4.Project(this.up);

        RPB rpb5 = Instantiate(this.rpbPrefab, this.transform.position, this.transform.rotation);
        rpb5.Project(this.tr);

        RPB rpb6 = Instantiate(this.rpbPrefab, this.transform.position, this.transform.rotation);
        rpb6.Project(this.tl);

        RPB rpb7 = Instantiate(this.rpbPrefab, this.transform.position, this.transform.rotation);
        rpb7.Project(this.br);

        RPB rpb8 = Instantiate(this.rpbPrefab, this.transform.position, this.transform.rotation);
        rpb8.Project(this.bl);

        RPBanana = false;
    }

       private void ShootApple()
   {
    PlayShootingSound();
    Bullet3 bullet3_1  = Instantiate(this.bullet3Prefab, this.transform.position, this.transform.rotation);
    bullet3_1.Project(this.transform.up);
    bullet3Ready = false;
   }
       private void ShootBomb()
   {
    BlueberryBomb blue_1  = Instantiate(this.BlueberryPrefab, this.transform.position, this.transform.rotation);
    blue_1.Project(this.transform.up);
    BlueBerryBombReady = false;
   }
    private void TurnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0f;
            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDeath(this);
        }
                if (collision.gameObject.CompareTag("Boundary"))
        {
           _turnDirection = -_turnDirection;
        }
                if (collision.gameObject.tag == "PowerUp")
        {
            int index = Random.Range (0, 3);
            if(index == 1){
                RPBanana = true;
            }else if(index == 2)
            {bullet2Ready = true;} else{
                bullet3Ready = true;
            }
            //Debug.Log("RPBanana is true");
        }
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
private void AnimateChoice(){
if(RPBanana == true){
    runSprites = YellowSprites;
} else if(bullet2Ready == true){
    runSprites = RedSprites;} else if(bullet3Ready == true) {
        runSprites = GreenSprites;
    }else if(BlueBerryBombReady == true) {
        runSprites = BlueSprites;
    }else runSprites = OrangeSprites;}

     private void AnimateSprite(){
        AnimateChoice();
if (spriteIndex >= runSprites.Length) {
                spriteIndex = 0;
            } else {
        spriteRenderer.sprite = runSprites[spriteIndex];
        spriteIndex++;
    }
}
  
 
 
 
 private void PlayShootingSound()
 {
         m_AudioSource.PlayOneShot(m_AudioSource.clip);

}}
   


