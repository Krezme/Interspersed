using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class Thirdpersonmovement : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    //[SerializeField] private Transform ChargedpfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private ThirdPersonPlayerController thirdPersonPlayerController;
    private Animator animator;
    public int currentEnergy;
    public int maxEnergy = 5;
    public int maxHealth = 100;
    public int currentHealth;
    public Healthbar healthbar;
    public Energybar energybar;
    public AudioClip shootSound;
    private AudioSource source;
    public float power;
    float maxPower = 5;
    float chargeSpeed = 3;
    bool shootHeldDown;
    
    void Start()
    {
        thirdPersonPlayerController = GetComponent<ThirdPersonPlayerController>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        source = GetComponent<AudioSource>();
    }
   
    public void HoldShoot()
    {
        shootHeldDown = true;
    }
    public void ReleaseButton()
    {
        shootHeldDown = false;
        power = 0;
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
        else {
            debugTransform.position = ray.direction * 999f;
            mouseWorldPosition = ray.direction * 999f;
        }

        if (OnPlayerInput.instance.onFire2)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            OnPlayerInput.instance.mouseSensitivityCurrent = OnPlayerInput.instance.mouseSensitivityAim;
            thirdPersonPlayerController.SetRotateOnMove(true);
            for (int i = 0; thirdPersonPlayerController.rigBuilder.layers.Count > i; i++) { //Setting the weight of the rigs to 1 (Animation Rigging)
                thirdPersonPlayerController.rigBuilder.layers[i].rig.weight = Mathf.Lerp(thirdPersonPlayerController.rigBuilder.layers[i].rig.weight, 1f, Time.deltaTime * 10f);
            }
            
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            OnPlayerInput.instance.mouseSensitivityCurrent = OnPlayerInput.instance.mouseSensitivity;
            thirdPersonPlayerController.SetRotateOnMove(false);
            for (int i = 0; thirdPersonPlayerController.rigBuilder.layers.Count > i; i++) { //Setting the weight of the rigs to 0 (Animation Rigging)
                thirdPersonPlayerController.rigBuilder.layers[i].rig.weight = Mathf.Lerp(thirdPersonPlayerController.rigBuilder.layers[i].rig.weight, 0f, Time.deltaTime * 10f);
            }
        }

        if (OnPlayerInput.instance.onFire2)
        {
            if (OnPlayerInput.instance.onFire1)
            {
                
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                OnPlayerInput.instance.onFire1 = false;
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
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "HealthPickup")
        {
            Heal(50);
        }
    }
}
