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
    public Text KennelText;

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
            KennelText.enabled = false;
            CheckForInteractables();
            CheckForAttachables();

            if (carryingKennel)
            {
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
                    KennelText.enabled = true;
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

                if (Input.GetButtonDown("Interact"))
                {
                    PickupAttachable(item);
                }
            }
        }
    }

    private void PickupItem(GameObject item)
    {
        Instantiate(item.GetComponent<WorldItemScript>().UIAlternative, kennelUI.transform.position, Quaternion.identity, kennelUI.transform);
        Destroy(item);
        Interactables.Remove(item);
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
        KennelText.enabled = false;
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
        if (Physics.Raycast(this.transform.position, Vector3.right, out hit, 2f))
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
        }
                                                                                                                           
        Attachables.Remove(item);
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
