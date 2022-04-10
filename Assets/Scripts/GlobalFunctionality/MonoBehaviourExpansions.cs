using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimplifiedMonoBehaviour {
    public class QualityOfLife
    {
        public static void CallWithDelay(Action Method, float delay){}/*  => StartCoroutine(CallWithDelayRoutine(Method, delay)) */

        IEnumerator CallWithDelayRoutine(Action Method, float delay) {
            yield return new WaitForSeconds(delay);
            Method();
        }
    }
}

