using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;

    public Vector2 velocity = new Vector2(0,3);
    private bool _boost;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    public Bullet bulletPrefab;
    public Bullet2 bullet2Prefab;
    public float coolDown2 = 3.0f;
    public bool bullet2Ready = false;

    public RPB rpbPrefab;
    public bool RPBanana = false;

        //Directions of projectiles for Banana Bomb
        //tr = top right
        //tl = top left
        //br = bottom right
        //bl = bottom left
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
        _rigidbody = GetComponent<Rigidbody2D>();
    }

   private void Update()
   {
        _thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        //Movement

        //Left, Right, and Sedentary
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

        //Weapon controls

        //Primary Shoot
      if (Input.GetMouseButtonDown(0))
     {
        ShootPrimary();
      }

        //Secondary Shoot
     if ((Input.GetMouseButtonDown(1)) && (bullet2Ready == true))
      {
        ShootSecondary();
        bullet2Ready = false;
        coolDown2 = 3.0f;
      }
        
        //Banana Bomb
     if ((Input.GetKey(KeyCode.Space)) && (RPBanana == true))
     {
        ShootRPB();
     }

        //Cooldown for secondary
     if (coolDown2 > 0.0)
     {
        bullet2Ready = false;
        coolDown2 -= Time.deltaTime;
     }
     else
     {
        Debug.Log("Bullet 2 ready");
        bullet2Ready = true;
     }
   }
        


   private void FixedUpdate()
   {
        //Thrust
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        //Turn
        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
   }



        //Weapons Code

    //Primary Shoot Code
   private void ShootPrimary()
   {
    Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
    bullet.Project(this.transform.up);
   }


    //Secondary Shoot Code
    private void ShootSecondary()
   {
    Bullet2 bullet2_1 = Instantiate(this.bullet2Prefab, this.transform.position, this.transform.rotation);
    bullet2_1.Project(this.transform.up);

    Bullet2 bullet2_2 = Instantiate(this.bullet2Prefab, this.transform.position, this.transform.rotation);
    bullet2_2.Project(this.transform.up);
    bullet2_2.Project(this.transform.right);

    Bullet2 bullet2_3 = Instantiate(this.bullet2Prefab, this.transform.position, this.transform.rotation);
    bullet2_3.Project(this.transform.up);
    bullet2_3.ProjectNeg(this.transform.right);

    bullet2Ready = false;
    coolDown2 = 0.0f;
   }


        //Banana Bomb Code
   private void ShootRPB()
    {
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



        //Collision Code for Asteroid and PowerUp
   private void OnCollisionEnter2D(Collision2D collision)
   {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }

        if (collision.gameObject.tag == "PowerUp")
        {
            RPBanana = true;
            Debug.Log("RPBanana is true");
        }
   }
   
}
