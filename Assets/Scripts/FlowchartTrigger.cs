using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Fungus;

public class FlowchartTrigger : MonoBehaviour
{
    public Flowchart flowchart;
    private bool hasleft = true;
    private bool frozen = false;
    public string triggerName;
    public string blockName;//block to go to
    public GameObject player;
    //private GameObject triggervisible;
    private Vector3 pos;
    private Rigidbody rb;


    void Start()
    {
        //this hides the mesh renderer component of the trigger so that it can't be seen
        this.GetComponent<MeshRenderer>().enabled = false;
    }


    void Update()
    {
        //this forces the player character to be fixed - its a kludge as i couldn't freeze the player otherwise
        if (frozen)
        {
            player.transform.position = pos;
        }
    }



    void OnTriggerEnter(UnityEngine.Collider collider)
    {
        //hasleft is used to check that the player jhas left the trigger box before triggering it again
        if (hasleft == true)
        {
            hasleft = false;
            if (collider.gameObject.tag == "Player")
            {
                Debug.Log(blockName);
                flowchart.ExecuteBlock(blockName);
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    void OnTriggerExit(UnityEngine.Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            hasleft = true;
        }
    }

    public void DisableMovement()
    {
        //need to disable the normal character movement here until the dialogue box is close
        player.GetComponent<ThirdPersonPlayerController>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        Vector3 zeromovement = new Vector3(0, 0, 0);
        frozen = true;
        player.GetComponent<Rigidbody>().velocity = zeromovement;
        pos = player.transform.position;

    }

    public void EnableMovement()
    {
        //need to enable the normal character movement here after the dialogue box is closed.
        player.GetComponent<ThirdPersonPlayerController>().enabled = true;

        player.GetComponent<Animator>().enabled = true;
        frozen = false;
        Debug.Log("reached end and close");
    }

}
