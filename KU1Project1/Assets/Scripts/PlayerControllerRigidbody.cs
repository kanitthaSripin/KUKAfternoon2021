using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerRigidbody : MonoBehaviour
{   public float speed = 2f;
    Rigidbody rb; 
    float newRotY = 0;
    public float rotSpeed = 20f;
    public GameObject prefabBullet;
    public Transform gunPosition;
    public float gunPower = 15f;
    public float gunCooldown = 2f;
    public float  gunCooldownCount = 0 ;
    public bool hasGun = false;
    public int bulletCount = 0 ;
   
    public int coinCount = 0;
    PlaygroundSceneManager manager;
    public AudioSource audioCoin;
    public AudioSource audioFire;
    public AudioSource audioJump;
    public float jumpPower = 0.5f;

    private void Start()
    {
         rb = GetComponent<Rigidbody>();
         
         if(manager == null)
         {
           manager = FindObjectOfType<PlaygroundSceneManager>();
         }
         if(PlayerPrefs.HasKey("CoinCount"))
         { 
            coinCount = PlayerPrefs.GetInt("CoinCount");
         }
         manager.SetTextCoin(coinCount);
        
    }

    void FixedUpdate()
    {   
        float horizontal = Input.GetAxis("Horizontal")*speed;
        float vertical = Input.GetAxis("Vertical")*speed;
        if(horizontal > 0)
        {   
            newRotY = 90;
        }
        else if(horizontal < 0)
        {
            newRotY = -90;
        }
        if(vertical > 0)
        {
            newRotY = 0;
        }
        else if(vertical < 0)
        {
            newRotY = 180;
        }
       rb.AddForce(horizontal,0,vertical,ForceMode.VelocityChange);
       transform.rotation = Quaternion.Lerp( Quaternion.Euler(0,newRotY,0),
                                             transform.rotation,
                                             rotSpeed * Time.deltaTime); 
    }
    //ÂÔ§»×¹
   private void Update()
   {  
        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(0,jumpPower,0,ForceMode.Impulse);
            audioJump.Play();
        }

       gunCooldownCount += Time.deltaTime;
       if(Input.GetButtonDown("Fire1")&& (bulletCount > 0) && (gunCooldownCount >= gunCooldown))
       {   
          gunCooldownCount = 0;
          GameObject bullet = Instantiate(prefabBullet,gunPosition.position,gunPosition.rotation);
          //  bullet.getComponent<Rigidbody>().AddForce(transform.forward * 15f,ForceMode.Impulse);
          Rigidbody bRb = bullet.GetComponent<Rigidbody>();
          bRb.AddForce(transform.forward*gunPower, ForceMode.Impulse);
          Destroy(bullet,2f);

          bulletCount--;
          manager.SetTextBullet(bulletCount);
          audioFire.Play();
       }
   }
   private void OnCollisionEnter(Collision collision)
   {
            print(collision.gameObject.name);
            if(collision.gameObject.tag == "collectables")
            {
                Destroy(collision.gameObject);
            }
   }

  private void OnTriggerEnter(Collider other)
   {
          
            if(other.gameObject.tag == "collectable")
            {
                Destroy(other.gameObject);
                coinCount++;
                manager.SetTextCoin(coinCount);
                audioCoin.Play();
                PlayerPrefs.SetInt("CoinCount",coinCount);
            }

            if(other.gameObject.name == "Gun")    
            {    
                print("Yeal I have a gun!");
                Destroy(other.gameObject);
                hasGun = true;
                bulletCount += 10;
                manager.SetTextBullet(bulletCount);
            }  
   }
}       

