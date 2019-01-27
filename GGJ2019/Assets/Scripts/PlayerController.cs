using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject Kennel;
    public GameObject KennelHolder;

    public bool carryingKennel = true;

    public MonoBehaviour[] ItemsToDisable;

    public GameObject kennelUI;

    bool usingKennelUI;

    public GameObject MainCamera;

    public List<GameObject> Interactables;
    public List<GameObject> Attachables;

    public Text InteractText;
    public Text EnterKennelText;
    public Text PickupKennelText;

    public bool hasParachute;

    public Sprite holdingKennelSprite;
    public Sprite droppedKennelSprite;

    public GameObject Parachute;
    public Sprite ParachuteOpen;
    public Sprite ParachuteClosed;

    bool LampFound = false;

    Animator anim;


    private void Awake()
    {
        instance = this;
        anim = this.GetComponent<Animator>();
    }

    void Start()
    {
        PickupKennel();
    }

    void Update()
    {

        if (usingKennelUI)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (Input.GetButtonDown("EnterKennel"))
            {
                ExitKennel();
            }
        }
        else
        {
            InteractText.enabled = false;
            EnterKennelText.enabled = false;
            PickupKennelText.enabled = false;
            CheckForInteractables();
            CheckForAttachables();

            if (carryingKennel)
            {
                if (!InteractText.enabled)
                {
                    EnterKennelText.enabled = true;
                    EnterKennelText.text = "Enter Kennel: Q";
                }

                if (!PickupKennelText.enabled)
                {
                    PickupKennelText.enabled = true;
                    PickupKennelText.text = "Drop Kennel: F";
                }

                CarryKennel();
                if (Input.GetButtonDown("EnterKennel"))
                {
                    EnterKennel();
                }

                if (Input.GetButtonDown("PickupKennel"))
                {
                    DropKennel();
                }
            }
            else
            {
                if (KennelIsClose())
                {
                    if (!EnterKennelText.enabled)
                    {
                        EnterKennelText.enabled = true;
                        EnterKennelText.text = "Enter Kennel: Q";
                    }

                    if (!PickupKennelText.enabled)
                    {
                        PickupKennelText.enabled = true;
                        PickupKennelText.text = "Pickup Kennel: F";
                    }

                    if (Input.GetButtonDown("EnterKennel"))
                    {
                        EnterKennel();
                    }

                    if (Input.GetButtonDown("PickupKennel"))
                    {
                        PickupKennel();
                    }
                }
            }
        }
    }

    void CheckForInteractables()
    {
        foreach (var item in Interactables)
        {
            if (Vector3.Distance(this.transform.position, item.transform.position) < 2)
            {
                InteractText.enabled = true;
                InteractText.text = "Pickup " + item.name + ": E";

                if (Input.GetButtonDown("Interact"))
                {
                    PickupItem(item);
                }
            }
        }
    }

    void CheckForAttachables()
    {
        foreach (var item in Attachables)
        {
            if (Vector3.Distance(this.transform.position, item.transform.position) < 2)
            {
                InteractText.enabled = true;
                InteractText.text = "Pickup " + item.name + ": E";

                if (Input.GetButtonDown("Interact"))
                {
                    PickupAttachable(item);
                }
            }
        }
    }

    private void PickupItem(GameObject item)
    {
        GameObject newItem = Instantiate(item.GetComponent<WorldItemScript>().UIAlternative, kennelUI.transform.position, Quaternion.identity, kennelUI.transform);
        kennelUI.GetComponent<CollectionControl>().selectableObjects.Add(newItem);

        if (kennelUI.GetComponent<CollectionControl>().lastSpawnPos == Vector3.zero)
        {
            newItem.transform.localPosition = new Vector3(-7.5F, -2.5F, 0);
            kennelUI.GetComponent<CollectionControl>().lastSpawnPos = kennelUI.GetComponent<CollectionControl>().lastSpawnPos + new Vector3(-7.5F, -2.5F, 0);
        }
        else
        {
            newItem.transform.localPosition = kennelUI.GetComponent<CollectionControl>().lastSpawnPos + Vector3.right * 1.65f;
            kennelUI.GetComponent<CollectionControl>().lastSpawnPos = kennelUI.GetComponent<CollectionControl>().lastSpawnPos + Vector3.right * 1.65f;
        }

        GameObject reference = item;
        Interactables.Remove(item);
        Destroy(reference);
    }

    void CarryKennel()
    {
        Kennel.transform.position = KennelHolder.transform.position;
        Kennel.transform.eulerAngles = KennelHolder.transform.eulerAngles;
    }

    bool KennelIsClose()
    {
        if(Vector3.Distance(this.transform.position, Kennel.transform.position) < 2)
        {
            return true;
        }

        return false;
    }

    void EnterKennel()
    {
        Debug.Log("Entering Kennel");
        
        foreach (MonoBehaviour item in ItemsToDisable)
        {
            item.enabled = false;
        }
        MainCamera.SetActive(false);
        usingKennelUI = true;
        kennelUI.SetActive(true);
        PickupKennelText.enabled = false;
        EnterKennelText.enabled = true;
        EnterKennelText.text = "Leave Kennel: Q";
    }

    void ExitKennel()
    {
        Debug.Log("Exiting Kennel");

        foreach (MonoBehaviour item in ItemsToDisable)
        {
            item.enabled = true;
        }
        MainCamera.SetActive(true);
        usingKennelUI = false;
        kennelUI.SetActive(false);
        
    }

    void DropKennel()
    {
        carryingKennel = false;
        Kennel.GetComponent<BoxCollider>().enabled = true;
        Kennel.GetComponentInChildren<SpriteRenderer>().sortingOrder = this.GetComponentInChildren<SpriteRenderer>().sortingOrder;

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, Vector3.right * transform.localScale.x, out hit, 2f))
        {
            Kennel.transform.position += Vector3.right * 1.1f * -transform.localScale.x;
        }
        else
        {
            this.transform.position += Vector3.left * 1.1f * -transform.localScale.x;
        }


        Kennel.GetComponentInChildren<SpriteRenderer>().sprite = droppedKennelSprite;
        Kennel.transform.eulerAngles = new Vector3(Kennel.transform.eulerAngles.x, Kennel.transform.eulerAngles.y, 0);
    }

    void PickupKennel()
    {
        carryingKennel = true;
        Kennel.GetComponent<BoxCollider>().enabled = false;
        Kennel.GetComponentInChildren<SpriteRenderer>().sortingOrder = this.GetComponentInChildren<SpriteRenderer>().sortingOrder + 1;
        Kennel.GetComponentInChildren<SpriteRenderer>().sprite = holdingKennelSprite;
    }

    void PickupAttachable(GameObject item)
    {
        item.transform.parent = Kennel.transform;
        item.GetComponent<AttachableScript>().GoToSpot = true;

        if (item.tag == "Parachute")
        {
            hasParachute = true;
        }

        if (item.tag == "Lamp")
        {
            LampFound = true;
            GameObject[] darkzones = GameObject.FindGameObjectsWithTag("DarkZone");
            foreach (var darkzone in darkzones)
            {
                darkzone.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
                                                                                                                           
        Attachables.Remove(item);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "DarkZone")
        {
            anim.SetBool("Scared", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "DarkZone")
        {
            anim.SetBool("Scared", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {                                                                                                                      
        if(other.tag == "DarkZone")                                                                                        
        {
            if (!LampFound)                                                                                                
            {                                                                     
                anim.SetBool("Scared", true);
            }
        }
    }
}
