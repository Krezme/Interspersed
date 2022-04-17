using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToObjectSize : MonoBehaviour
{
    public GameObject objectScaleTo; //object to scale in size to
    [HideInInspector]
    public GameObject parent;
    public float scaleOffset = 1;
    public float scaleToSpeed; //speed at which it scales to the targeted Scale
    private Vector3 oldScale; //the previouse size it was scaled to
    public Vector3 parentSize; //the size the object should scale to currently
    public RagdollController ragdollControllerFound;

    [SerializeField]
    private ShieldProjectilesAbsorption shieldProjectilesAbsorption;



    
    void Start()
    {
        parent = transform.parent.gameObject;
        ragdollControllerFound = FindRagdollController(parent);
        shieldProjectilesAbsorption.InstantiateStart();
        if (ragdollControllerFound != null){
            parentSize = ragdollControllerFound.meshRenderer.bounds.size;
            //gameObject.transform.position = ragdollControllerFound.meshRenderer.bounds.center;
            transform.parent = ragdollControllerFound.rigCentre.transform;
            transform.localScale = (parentSize + (new Vector3(scaleOffset, scaleOffset, scaleOffset) * ragdollControllerFound.slimeSphereSizeMultyplier));
            
        }
        else {
            if (parent.TryGetComponent<Renderer>(out Renderer renderer)) {
                parentSize = renderer.bounds.size;
                gameObject.transform.position = renderer.bounds.center;
                transform.localScale = (parentSize + new Vector3(scaleOffset, scaleOffset, scaleOffset));
            }
            else {
                Vector3 newScale = new Vector3(scaleOffset, scaleOffset, scaleOffset);
                transform.parent = objectScaleTo.transform;
                transform.localScale = newScale;
                transform.parent = parent.transform;
            }
        }
    }

    RagdollController FindRagdollController(GameObject newGameObject) {
        if (newGameObject.tag == "Enemy") {
            if (newGameObject.TryGetComponent<RagdollController>(out RagdollController ragdollController)) {
                return ragdollController;
            }else{
                try {
                    return FindRagdollController(newGameObject.transform.parent.gameObject);
                }
                catch (System.Exception) {
                    return null;
                }
            }
        }
        return null;
    } 
    
    /* void Update()
    {
        if (parent.TryGetComponent<Renderer>(out Renderer renderer)) {
            transform.localScale = (parentSize + new Vector3(scaleOffset, scaleOffset, scaleOffset));
        }
        
    } */
}
