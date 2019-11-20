using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SingleNote : MonoBehaviour, INote
    {
        public bool isReset { get; }

        uint laneNumber;
        public uint LaneNumber
        {
            get { return 1; }
        }

        float dt;
        public float DownTime { get { return this.dt; } }

        public void Move()
        {
            var x = this.transform.position.x;
            float timing = DownTime - NotesController.Instance.elapsedTime;

            //TODO:汚い
            //Vector3 ndir = (this.transform.position - NotesController.Instance.JustTimingPosition).normalized;
            Vector3 ndir = (-this.transform.forward.normalized);

            //TODO:n秒後に目標地点到達:目標地点を定義→Zeroは中央のみ
            var pos = Vector3.zero - ndir * timing * NotesController.Instance.NotesSpeed;
            pos.x = x;
            this.transform.position = pos;
        }

        /// <summary>
        /// ノーツの登録
        /// </summary>
        public void Register(uint laneNumber, float downTime)
        {
            this.laneNumber = laneNumber;
            dt = downTime;

            //レーンの位置に合わせた初期化
            //TODO:汚い<マジックナンバー>
            var pos = this.transform.position;
            switch (LaneNumber)
            {
                //左
                case 0: pos.x = -4; break;
                //中
                case 1: pos.x = 0; break;
                //右
                case 2: pos.x = 4; break;
            }
            this.transform.position = pos;
        }
        public void Unregister()
        {
            this.gameObject.SetActive(false);
            //このまま場所を変えないと再利用した際に一瞬視界に入ってしまうので視覚外に移動させる
            this.gameObject.transform.position = new Vector3(-100, -100, -100);
            NotesController.Instance.Notes.Remove(this);
        }
    }
}