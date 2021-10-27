using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class Thirdpersonmovement : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform ChargedpfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    
    
    

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    private Animator animator;
    private AudioSource source;
    public int currentEnergy;
    public int maxEnergy = 5;
    public int maxHealth = 100;
    public int currentHealth;
    public Healthbar healthbar;
    public Energybar energybar;
    public AudioClip shootSound;
    public GameObject gameover;
    public float power;
    float maxPower = 5;
    float chargeSpeed = 3;
    
    

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();

      
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        source = GetComponent<AudioSource>();
        
    }
   
    
    


    void AddEnergy(int gain)
    {
        currentEnergy += gain;

        if(currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

        energybar.SetEnergy(currentEnergy);

    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
    }
    void Heal(int heal)
    {
        currentHealth += heal;
       
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthbar.SetHealth(currentHealth);
       
    }
    

    public void Update()
    {
        

        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        else
        {
            debugTransform.position = ray.direction * 999f;
            mouseWorldPosition = ray.direction * 999f;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            
        }

        if (starterAssetsInputs.aim)
        {
            if (starterAssetsInputs.shoot)
            {
                
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                starterAssetsInputs.shoot = false;
                source.PlayOneShot(shootSound);
                
            }
            
        }
        
    }
   
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Energy")
        {
            AddEnergy(1);
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(20);

            if(currentHealth <= 0)
            {
                gameover.SetActive(true);
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "HealthPickup")
        {
            Heal(50);
        }
    }

   

}
