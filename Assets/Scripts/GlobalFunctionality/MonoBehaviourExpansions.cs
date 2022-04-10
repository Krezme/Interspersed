using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimplifiedMonoBehaviour { // ! This needs to be fixed (Currently it shows a NullRefferenceExeption coming from the StartCoroutine())
    public class QualityOfLife : MonoBehaviour
    {

        public static void CallWithDelay(Action Method, float delay) {
            QualityOfLife qualityOfLife = new QualityOfLife();
            qualityOfLife.InitiateCoroutine(Method, delay);
        }

        public void InitiateCoroutine(Action Method, float delay) {
            StartCoroutine(CallWithDelayRoutine(Method, delay));
        }

        public IEnumerator CallWithDelayRoutine(Action Method, float delay) {
            yield return new WaitForSeconds(delay);
            Method();
        }
    }
}
