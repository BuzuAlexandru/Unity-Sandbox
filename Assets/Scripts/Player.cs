using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Animator _animator;
    SpriteRenderer _sprite;
    Rigidbody2D _body;
    AudioSource _audio;
    public Transform firePoint;
    public GameObject bulletPrefab;

    Vector3 pos;

    bool isgrounded = true;
    bool facingRight = true;
    bool prevOrientation = true;
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
        _audio = GetComponent<AudioSource>();
        pos = transform.position;
    }

    void Update(){
        isgrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        getInput();
         
        float movement = Input.GetAxis("Horizontal");

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

        if(Input.GetAxis("Vertical") > 0){
            _animator.Play("Base Layer.ladder-Animation");
            _animator.speed=1;
        }else

        if(isgrounded){
            _animator.speed=1;

            if(movement != 0){
                if(Input.GetButtonDown("Fire1")){
                    _animator.Play("Base Layer.Run_shoot_Animation");
                    Shoot();
                }
                else if(Input.GetAxis("Fire1") == 0){
                    _animator.Play("Base Layer.Run_Animation");
                }

                if(!_audio.isPlaying){
                    _audio.enabled = true;
                }
            }
            else if(movement == 0){
                if(Input.GetButtonDown("Fire1")){
                    _animator.Play("Base Layer.Shoot_Animation");
                    Shoot();
                }
                else if(Input.GetAxis("Fire1") == 0){
                    _animator.Play("Base Layer.idle-Animation");
                }
                
                _audio.enabled = false;
            }
        }
        else if(!isgrounded){
            _animator.speed=0;
            _audio.enabled = false;
        }    
    }

    void getInput(){

    }

    void FixedUpdate()
    {
         float movement = Input.GetAxis("Horizontal");

        if(Input.GetAxis("Vertical") > 0){
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
