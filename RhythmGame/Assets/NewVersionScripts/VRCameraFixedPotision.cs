using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace MyVR
{
    /// <summary>
    /// カメラの位置を固定し、回転だけを適用させたい。。。
    /// (位置トラッキング無効)
    /// </summary>
    public class VRCameraFixedPotision : MonoBehaviour
    {
        //serialize param
        //[SerializeField] Vector3 position;
        //private param
        //public param

        private void Start()
        {
            InputTracking.disablePositionalTracking = true;
        }
    }
}