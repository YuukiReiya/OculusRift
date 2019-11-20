using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using System.Linq;
namespace Game
{
    public abstract class Lane : MonoBehaviour
    {
        [SerializeField, Tooltip("ノーツの流れるレーン番号")]protected uint laneNumber;
        #region VR
        //[Header("VR")]
        //[SerializeField] LaneTapEffect effect;
        #endregion

        protected void OnTapLane()
        {
            var notes = NotesController.Instance.Notes;
            if (notes.Where(it => it.LaneNumber == laneNumber).Count() == 0) { return; }
            INote note = notes.
                //対象のレーン
                Where(it => it.LaneNumber == laneNumber).
                //判定ラインに最も近いノーツ = (判定時間 - 再生時間 の差分が最も小さい)
                OrderBy(it => Mathf.Abs(it.DownTime - NotesController.Instance.elapsedTime)).
                First();

            //ノーツが"n"秒経過していたら処理しない
            //TODO:処理用確認
            var tapTime = note.DownTime - NotesController.Instance.elapsedTime;
            if (Mathf.Abs(tapTime) > NotesController.Instance.WaitTime) { return; }

            //判定
            string judge;

            //TODO:汚い
            if (Mathf.Abs(tapTime) <= Common.Define.c_PerfectTime)
            {
                judge = "perfect";
                ScoreController.Instance.StartScoreEffect(ScoreController.Score.PERFECT);
            }
            else if (Mathf.Abs(tapTime) <= Common.Define.c_GreatTime)
            {
                judge = "great";
                ScoreController.Instance.StartScoreEffect(ScoreController.Score.GREAT);
            }
            else if (Mathf.Abs(tapTime) <= Common.Define.c_GoodTime)
            {
                judge = "good";
                ScoreController.Instance.StartScoreEffect(ScoreController.Score.GOOD);
            }
            else
            {
                judge = "miss";
                ScoreController.Instance.StartScoreEffect(ScoreController.Score.MISS);
            }
            Debug.Log(judge);

            note.Unregister();
        }

        public abstract void Execute();
    }
}