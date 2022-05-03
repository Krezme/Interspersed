using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


    [RequireComponent(typeof(Collider))]
    public class FireSizzleOnTrigger : MonoBehaviour
    {
        [Header(" ")]
        [Header("                                     ---===== From Rhys' Collection =====---")]

        //public LayerMask layers;
        public UnityEvent OnEnter, OnExit;
        new Collider collider;

        public string tagToWatch;

        public GameObject objectToInstantiate;

        public Transform spawnPoint;

        void Reset()
        {
            //layers = LayerMask.NameToLayer("Everything");
            collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == tagToWatch)
            {
                Destroy(other.transform.root.gameObject); //! This is the reason why enemies cannot be inside a parent gameObject
                Instantiate(objectToInstantiate, spawnPoint.transform.position, spawnPoint.transform.rotation);
                ExecuteOnEnter(other);
            }
        }

        protected virtual void ExecuteOnEnter(Collider other)
        {
            OnEnter.Invoke();
        }

        /* void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == tagToWatch)
            {
                ExecuteOnExit(other);
            }
        } */

        /* protected virtual void ExecuteOnExit(Collider other)
        {
            OnExit.Invoke();
        } */

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
        }

        void OnDrawGizmosSelected()
        {
            //need to inspect events and draw arrows to relevant gameObjects.
        }

    } 

