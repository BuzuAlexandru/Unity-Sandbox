using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    public int selectedSlotIndex = 0;
    public GameObject hotbarSelector;
    public Inventory inventory;
    public Crafting crafting;
    public bool inventoryShowing = false;
    public bool craftingShowing = false;
    public bool hotbarShowing = true;
    public Animator _animator;
    public SpriteRenderer _sprite;
    public Rigidbody2D _body;
    public AudioSource walkAudio;
    public AudioSource shootAudio;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int maxHealth = 100;
    public int currentHealth;

    Vector3 pos;
    Vector3 lookDirection;
    float lookAngle;

    public int health;

    public bool isgrounded = true;
    bool facingRight = true;
    public bool atLadder = false;
    bool prevOrientation = true;
    public bool gunEquipped = false;
    public bool telekinesis = false;
    float fireRate = 0.3f;
    float nextFire;
    public Transform feetPos;
    public float checkRadius;
    public Transform spawnpoint;
    public LayerMask whatIsGround;

    public float speed = 20f;
    float jumpHeight = 20f;

    void Start()
    {
        health = maxHealth;
        inventory = GetComponent<Inventory>();
        crafting = GetComponent<Crafting>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.transform.position = spawnpoint.position;
            // Destroy(gameObject);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        
        if (Input.GetKeyDown(KeyCode.E) && !craftingShowing){
            inventoryShowing = !inventoryShowing;
            hotbarShowing = !hotbarShowing;
        }

        if (Input.GetKeyDown(KeyCode.Q) && !inventoryShowing){
            craftingShowing = !craftingShowing;
            hotbarShowing = !hotbarShowing;
        }

        crafting.craftingUI.SetActive(craftingShowing);
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
        if (selectedSlotIndex == 1)
            telekinesis = true;
        else telekinesis = false;

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

    public void updateButton1(){
        if (inventory.inventorySlots[5,3].quantity >= 1 && inventory.inventorySlots[6,3].quantity >= 1){
            inventory.inventorySlots[5,3].quantity-=1;
            inventory.inventorySlots[6,3].quantity-=1;
            inventory.inventorySlots[0,2].quantity+=1;
            inventory.UpdateInventoryUI();
        }
    }

    public void updateButton2(){
        if (inventory.inventorySlots[7,3].quantity >= 3){
            inventory.inventorySlots[7,3].quantity-=3;
            inventory.inventorySlots[1,2].quantity+=1;
            inventory.UpdateInventoryUI();
        }
    }

    public void updateButton3(){
        if (inventory.inventorySlots[4,3].quantity >= 3 && inventory.inventorySlots[3,3].quantity >= 3){
            inventory.inventorySlots[4,3].quantity-=3;
            inventory.inventorySlots[3,3].quantity-=3;
            inventory.inventorySlots[2,2].quantity+=1;
            inventory.UpdateInventoryUI();
        }
    }

    public void updateButton4(){
        if (inventory.inventorySlots[8,3].quantity >= 2 && inventory.inventorySlots[2,3].quantity >= 2){
            inventory.inventorySlots[8,3].quantity-=2;
            inventory.inventorySlots[2,3].quantity-=2;
            inventory.inventorySlots[3,2].quantity+=1;
            inventory.UpdateInventoryUI();
        }
    }

    public void updateButton5(){
        if (inventory.inventorySlots[7,3].quantity >= 2 && inventory.inventorySlots[4,3].quantity >= 2){
            inventory.inventorySlots[7,3].quantity-=2;
            inventory.inventorySlots[4,3].quantity-=2;
            inventory.inventorySlots[4,2].quantity+=1;
            inventory.UpdateInventoryUI();
        }
    }

    public void updateButton6(){
        if (inventory.inventorySlots[0,2].quantity >= 1 && inventory.inventorySlots[1,2].quantity >= 1 && inventory.inventorySlots[2,2].quantity >= 1 && inventory.inventorySlots[3,2].quantity >= 1 && inventory.inventorySlots[4,2].quantity >= 1){
            inventory.inventorySlots[0,2].quantity -=1;
            inventory.inventorySlots[1,2].quantity -=1;
            inventory.inventorySlots[2,2].quantity -=1;
            inventory.inventorySlots[3,2].quantity -=1;
            inventory.inventorySlots[4,2].quantity -=1;
            inventory.inventorySlots[5,2].quantity +=1;
            inventory.UpdateInventoryUI();
        }
    }

    
}
