using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Yuuki.MethodExpansions;
using System.Linq;
public class CenterLane : Lane
{
    IEnumerator routine;
    const float y = -0.5f;
    const float wait = 0.5f;
    bool isAction {
        get
        {
            var lPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            var rPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Debug.LogError("比較");
            Debug.LogError("L:" + lPos + "/R:" + rPos);
            return (lPos.y <= y) || (rPos.y <= y);
        }
    }

    public override void Execute()
    {
        if (isAction) {
        }
            if (routine != null) { return; }
            routine = MainRoutine();
            this.StartCoroutine(routine,()=> { routine = null; });
    }
    IEnumerator MainRoutine()
    {
        OnTapLane();
        yield break;
    }

    private void OnTriggerStay(Collider other)
    {
        var handLayer = other.gameObject.layer;
        //手のコライダーが接触かつノーツのコライダーが接触

        Debug.LogError("Enter of Stay");

        if(handLayer == LayerMask.NameToLayer("Hand"))
        {
            Debug.LogError("Enter of IF");

            var notes = NotesController.Instance.Notes;
            if (notes.Where(it=>it.LaneNumber == 1).Count() == 0) { return; }
            var note = notes.
                Where(it => it.LaneNumber == 1).
                OrderBy(it => it.DownTime).First();
            //ノーツが"n"秒経過していたら処理しない
            //TODO:処理用確認
            var tapTime = note.DownTime - NotesController.Instance.elapsedTime;
            if (Mathf.Abs(tapTime) > NotesController.Instance.WaitTime) { return; }
            //判定処理
            Judge.Execute(tapTime);
            note.Unregister();
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
