using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class PlayAudioFromBank : ActionNode
{
    public int soundBankIndex;
    protected override void OnStart() {
        context.enemyAnimationEventAudio.soundBank.PlayRandomClip(defaultBankIndex: soundBankIndex);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
