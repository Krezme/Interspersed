using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticChargeGeneratorActivation : MonoBehaviour
{
    public GeneratorTurnOnWithCharge[] generatorTurnOnWithChargeScripts;

    void Start() {
        foreach (GeneratorTurnOnWithCharge generatorTurnOnWithCharge in generatorTurnOnWithChargeScripts) {
            generatorTurnOnWithCharge.EnableGenerator(new Collider());
        }
    }
}
