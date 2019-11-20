using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Yuuki.MethodExpansions;
using System.Linq;
public class CenterLane : Lane
{
    IEnumerator routine;

    public override void Execute()
    {
    }
    IEnumerator MainRoutine()
    {
        OnTapLane();
        yield break;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.LogError("akfhalh");
        if (other.tag != "HandSphere") { return; }

        var notes = NotesController.Instance.Notes;
        if (notes.Where(it => it.LaneNumber == 1).Count() == 0) { return; }
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

    private void OnDrawGizmos()
    {
        
    }
}
