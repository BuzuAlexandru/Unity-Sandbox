using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Animator _animator;
    SpriteRenderer _sprite;
    Rigidbody2D _body;
    public AudioSource walkAudio;
    public AudioSource shootAudio;
    public Transform firePoint;
    public GameObject bulletPrefab;

    Vector3 pos;
    Vector3 lookDirection;
    float lookAngle;

    bool isgrounded = true;
    bool facingRight = true;
    bool atLadder = false;
    bool prevOrientation = true;
    public bool gunEquipped = false;
    float fireRate = 0.4f;
    float nextFire;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float speed = 20f;
    float jumpHeight = 20f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    void Update(){

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = lookDirection - transform.position;
        lookAngle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        

        isgrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        getInput();
         
        float movement = Input.GetAxis("Horizontal");

        if(gunEquipped){
            var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (delta.x >= 0 && !facingRight) { 
                transform.Rotate(0, 180, 0); 
                facingRight = true;
            } else if (delta.x < 0 && facingRight) { 
                transform.Rotate(0, 180, 0); 
                facingRight = false;
            }
            prevOrientation = facingRight;
        }
        else{
            if(movement < 0){
                facingRight = false;
            }
            else if(movement > 0){
                facingRight = true;
            }

            if(facingRight != prevOrientation){
                transform.Rotate(0, 180, 0);
                prevOrientation = facingRight;
            }
        }
        
         
        

        if(atLadder && Input.GetAxis("Vertical") > 0){
            _animator.SetTrigger("Climb");
            _animator.speed=1;
            walkAudio.enabled = true;
        }else

        if(isgrounded){
            _animator.speed=1;

            if(movement != 0){
                if(gunEquipped){
                    if(Input.GetButtonDown("Fire1") && Time.time > nextFire){
                        Shoot();
                    }
                    _animator.SetTrigger("Run-Shoot");
                }
                else{
                    _animator.SetTrigger("Run");
                }

                if(!walkAudio.isPlaying){
                    walkAudio.enabled = true;
                }
            }
            else if(movement == 0){
                if(Input.GetButtonDown("Fire1") && Time.time > nextFire && gunEquipped){
                    _animator.SetTrigger("Shoot");
                    Shoot();
                }
                else {
                    _animator.SetTrigger("Idle");
                }
                
                walkAudio.enabled = false;
            }
        }
        else if(!isgrounded){
            _animator.speed=0;
            walkAudio.enabled = false;
        }    
    }

    void getInput(){

    }

    void FixedUpdate()
    {
         float movement = Input.GetAxis("Horizontal");

        if(atLadder && Input.GetAxis("Vertical") > 0){
            _body.velocity = new Vector3(0, 5f, 0);
        }else

        if(isgrounded){
            if(movement != 0){
                _body.velocity = new Vector3(movement * speed, 0, 0);
            }

            float jump = Input.GetAxis("Jump");

            if(jump > 0){
                if(movement != 0){
                    _body.velocity = new Vector3(movement * speed, jump * jumpHeight, 0);
                }
                else _body.velocity = new Vector3(0, jump * jumpHeight, 0);
            }
        }
        
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shootAudio.enabled = true;
        nextFire = Time.time + fireRate;  
    }
    // void OnCollisionStay2D(Collision2D theCollision)
    // {
    //     if (!isgrounded && theCollision.gameObject.name == "floor")
    //     {
    //         isgrounded = true;
    //     }
    // }
    
    
    // void OnCollisionExit2D(Collision2D theCollision)
    // {
    //     if (theCollision.gameObject.name == "floor")
    //     {
    //         isgrounded = false;
    //     }
    // }
}
