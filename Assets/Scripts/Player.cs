using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int selectedSlotIndex = 0;
    public GameObject hotbarSelector;
    public Inventory inventory;
    public bool inventoryShowing = false;
    public bool hotbarShowing = true;
    public Animator _animator;
    public SpriteRenderer _sprite;
    public Rigidbody2D _body;
    public AudioSource walkAudio;
    public AudioSource shootAudio;
    public Transform firePoint;
    public GameObject bulletPrefab;

    Vector3 pos;
    Vector3 lookDirection;
    float lookAngle;

    public int maxHealth = 100;
    public int health;
    public int damage;
    public BatGFX enemyHealth;

    public bool isgrounded = true;
    bool facingRight = true;
    public bool atLadder = false;
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
        health = maxHealth;
        inventory = GetComponent<Inventory>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyHealth.TakeDamage(damage);
        }
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
        }

        else if(isgrounded){
            _animator.speed=1;

            if(movement != 0){
                if(gunEquipped){
                    _animator.SetTrigger("Run-Shoot");
                }
                else{
                    _animator.SetTrigger("Run");
                }

                walkAudio.enabled = true;
                
            }
            else if(movement == 0){
                if(Input.GetButtonDown("Fire1") && Time.time > nextFire && gunEquipped){
                    _animator.SetTrigger("Shoot");
                }
                else {
                    _animator.SetTrigger("Idle");
                }
                float jump = Input.GetAxis("Jump");

                if(jump > 0 && gunEquipped){
                    _animator.ResetTrigger("Idle");
                    _animator.SetTrigger("Run-Shoot");     
                }
                StartCoroutine(WalkSoundDelay());
            }
        }
        else if(!isgrounded){
            _animator.speed=0;
            StartCoroutine(WalkSoundDelay());
        }  

        if(Input.GetButtonDown("Fire1") && Time.time > nextFire && gunEquipped){
            Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.E)){
            inventoryShowing = !inventoryShowing;
            hotbarShowing = !hotbarShowing;
        }

        inventory.inventoryUI.SetActive(inventoryShowing);
        inventory.hotbarUI.SetActive(hotbarShowing);

        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            if (selectedSlotIndex<inventory.inventoryWidth-1)
                selectedSlotIndex+=1;
        } else if (Input.GetAxis("Mouse ScrollWheel") > 0){
            if (selectedSlotIndex>0)
                selectedSlotIndex-=1;
        }

        if (selectedSlotIndex == 0)
            gunEquipped = true;
        else gunEquipped = false;

        hotbarSelector.transform.position = inventory.hotbarUISlots[selectedSlotIndex].transform.position;
    }

    void getInput(){

    }

    void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal");

        if(atLadder && Input.GetAxis("Vertical") > 0){
            _body.velocity = new Vector3(_body.velocity.x, 10f, 0);
        }

        if(movement != 0){
            _body.velocity = new Vector3(movement * speed, _body.velocity.y, 0);
        }
        if(isgrounded){
            float jump = Input.GetAxis("Jump");

            if(jump > 0){
                _body.velocity = new Vector3(_body.velocity.x, jump * jumpHeight, 0); 
            }
        }
        
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shootAudio.enabled = true;
        nextFire = Time.time + fireRate; 
        StartCoroutine(GunSoundDelay()); 
    }

    IEnumerator GunSoundDelay(){
        yield return new WaitForSeconds(fireRate - 0.025f);
        shootAudio.enabled = false;
    }

    IEnumerator WalkSoundDelay(){
        yield return new WaitForSeconds(0.04f);
        walkAudio.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.CompareTag("Ladder"))
        {
            atLadder = true;
        }
    }
    
    
    void OnTriggerExit2D(Collider2D theCollision)
    {
        if (theCollision.CompareTag("Ladder"))
        {
            atLadder = false;
        }
    }
}
