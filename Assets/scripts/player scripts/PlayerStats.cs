using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    public float maxHealth = 100f;
    public float regenSpeed = 10f;
    [Tooltip("Time until regen starts")]
    public float waitTimeToHeal = 10.0f;

    [Header("Load Management")]
    [Tooltip("Scene to load on player death")]
    public int sceneIndex = 0;


    [Header("Player Body")]
    [SerializeField]
    private Transform playerCenter;
    public GameObject spawnAmmoZone;

    [Header("Player Interaction")]
    [SerializeField]
    private XRRayInteractor uiInteraction;

    [HideInInspector]
    public float health;

    float healTimer = 0;
    bool decreaseHealth;
    float waitTime = 30.0f;

    IOCcam iOCcam;

    public GameObject returnAmmoZone()
    {
        return spawnAmmoZone;
    }    

    void Start()
    {
        decreaseHealth = false;
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if(health < maxHealth && healTimer <= 0)
        {
            health += Time.fixedDeltaTime * regenSpeed;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }
        else if( healTimer > 0)
        {
            healTimer -= Time.fixedDeltaTime;
        }

        if(uiInteraction != null)
        {
            if (uiInteraction.hasHover)
            {
                uiInteraction.gameObject.GetComponent<XRInteractorLineVisual>().gameObject.SetActive(true);
                uiInteraction.gameObject.GetComponent<LineRenderer>().gameObject.SetActive(true);
            }
            else if (!uiInteraction.hasHover)
            {
                uiInteraction.gameObject.GetComponent<XRInteractorLineVisual>().gameObject.SetActive(false);
                uiInteraction.gameObject.GetComponent<LineRenderer>().gameObject.SetActive(false);
            }
        }

        

    }

    void Update()
    {
        //These are controls for testing helath increase and decrease
        if (decreaseHealth == true)
        {
            health -= 1.0f / waitTime * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            health += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            health -= 0.1f;
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public float CurrentHealth()
    {
        //sets up health when called to calculate the current exposure weight of the blocks in the tubes 
        return (1f - (health / maxHealth));
    }
    public float CurrentHealthCanvas()
    {
        //calculates the fill of an image based on current player health
        return ((health / maxHealth));
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healTimer = waitTimeToHeal + Time.fixedDeltaTime;

        if(health <= 0)
        {
            if (iOCcam != null)
                iOCcam.DisposeNow();
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene(sceneIndex);
        }

    }

    public Transform playerTarget()
    {
        if (playerCenter == null)
            return this.gameObject.transform;
        else
            return playerCenter;
    }

}
