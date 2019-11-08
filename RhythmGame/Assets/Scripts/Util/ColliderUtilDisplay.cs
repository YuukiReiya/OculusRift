using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUtilDisplay : MonoBehaviour
{
    //#if UNITY_EDITOR
    [SerializeField] private Collider collider;
    [SerializeField] private Color cr;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        //TODO:コライダーの処理に応じた描画分岐

        if (collider == null) { collider = GetComponent<Collider>(); }

        //めんどいからボックスのみで
        var bCol = collider as BoxCollider;
        if (bCol == null) {
            Debug.LogError("cast as failed.");
            return;
        }
        Debug.Log("display collider");

        //色指定
        Gizmos.color = cr;
        
        //ギズモ描画
        //Gizmos.DrawLine

        Gizmos.DrawWireCube(this.transform.position + bCol.center, bCol.size);
    }

    private void Reset()
    {
        cr = Color.red;
        collider = GetComponent<Collider>();
        Debug.Log(collider ? "true" : "false");
    }
//#endif
}
