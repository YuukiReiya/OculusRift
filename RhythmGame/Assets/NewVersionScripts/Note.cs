//https://jp.gamesindustry.biz/article/1805/18050901/
//Unite講演を参考にランダムな座標から手前までに誘導運動するノーツを作ってみる
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR
{
    public class Note : MonoBehaviour
    {
        Vector3 velocity;
        Vector3 pos;
        [SerializeField]Transform target;
        [SerializeField]float period;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            MulAccel();
        }

        //加速度計算
        void MulAccel()
        {
            var accel = Vector3.zero;
            var diff = target.position - pos;
            accel += (diff - velocity * period) * 2f / (period * period);
            #region 足し算
            //accel+=(diff-velocity<<)
            #endregion
            period -= Time.deltaTime;
            if (period < 0) { return; }
            velocity += accel * Time.deltaTime;
            pos += velocity * Time.deltaTime;
            transform.position += pos;
        }
    }
}