﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Game
{
    public class NotesController : Yuuki.SingletonMonoBehaviour<NotesController>
    {
        //serialeze param
        [Header("Notes Control Parameter")]
        [SerializeField] float noteSpeed;
        [SerializeField, Tooltip("算出されたノーツのキーを受け付けない時間")] float waitTime = 5.0f;
        [SerializeField] float activeTime = 0.5f;

        //private param
        private Queue<Chart.Note> noteQueue;
        //accessor
        public float NotesSpeed { get { return noteSpeed; } }
        public float WaitTime { get { return waitTime; } }

        public float elapsedTime { get; private set; }

        public List<INote> Notes { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Notes = new List<INote>();
            noteQueue = new Queue<Chart.Note>(ChartManager.Chart.Notes);
        }

        public void SetupNotes()
        {
            Notes.Clear();
            noteQueue.Clear();
            foreach (var it in ChartManager.Chart.Notes)
            {
                noteQueue.Enqueue(it);
            }
            foreach (var it in SingleNotesPool.Instance.PoolList) { it.SetActive(false); }
        }


        /// <summary>
        /// 管理リストの更新
        /// </summary>
        public void Renewal()
        {
            while (noteQueue.Count > 0)
            {
                var ptr = noteQueue.Peek();
                //押す時間 - 経過時間 <= アクティブ時間
                if ((ptr.Time - GameController.Instance.ElapsedTime) <= activeTime)
                {
                    //初期化処理
                    var inst = SingleNotesPool.Instance.GetObject();
                    INote note;
                    if (!inst.TryGetComponent(out note))
                    {
                        Debug.LogError("プールするノーツのプレハブにINoteコンポーネントがアタッチされていない");
                        continue;
                    }
                    // note.Register(ptr.LaneNumber, ptr.Time);
                    note.Register(ptr.LaneNumber, ptr.Time);
                    //登録するノーツキューの更新
                    noteQueue.Dequeue();
                    //更新リストに追加
                    Notes.Add(note);
                    continue;
                }
                break;
            }
        }

        public void Move()
        {
            foreach (var it in Notes)
            {
                it.Move();
            }
        }

        /// <summary>
        /// ノーツリストの廃棄処理
        /// </summary>
        public void Discard()
        {
            //foreach文内ではコレクションの書き換えが出来ないためリストを分ける
            var discardNotes = new Queue<INote>();
            foreach (var it in Notes)
            {
                var time = (it.DownTime - GameController.Instance.ElapsedTime);
                //押されるべき時間 - 実際の時間 > -(Good判定時間)
                if (time < -Common.Define.c_GoodTime)
                {
                    //ミス判定処理
                    Judge.Execute(time);

                    //ノーツの登録解除
                    it.Unregister();

                    //破棄リストに追加
                    discardNotes.Enqueue(it);
                }
                break;
            }
            //破棄リスト内のノーツの登録を解除しているだけ
            foreach (var it in discardNotes) { Notes.Remove(it); }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
        }
    }
}