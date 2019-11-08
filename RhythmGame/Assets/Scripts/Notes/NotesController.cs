using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.Networking;

namespace Game
{
    public class NotesController : Yuuki.SingletonMonoBehaviour<NotesController>
    {
        //serialeze param
        [Header("Notes Control Parameter")]
        [SerializeField] float noteSpeed;
        [SerializeField, Tooltip("算出されたノーツのキーを受け付けない時間")] float waitTime = 5.0f;
        [SerializeField] AudioSource audioSource;

        //private param

        //accessor
        public float NotesSpeed { get { return noteSpeed; } }
        public float WaitTime { get { return waitTime; } }

        public float elapsedTime { get; private set; }

        public List<INote> Notes { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Notes = new List<INote>();
        }

        // Start is called before the first frame update
        void Start() {
            StartCoroutine(MainRoutine());
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            //elapsedTime = audioSource.time;
            elapsedTime = Time.timeSinceLevelLoad;
        }

        /// <summary>
        /// 管理リストの更新
        /// </summary>
        void Renewal()
        {
            Notes.RemoveAll(it => it.isReset);
        }

        void Move()
        {
            foreach (var it in Notes)
            {
                it.Move();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
        }

        public void Setup(Chart chart)
        {
            float prevSec = 0.0f;
            foreach (var it in chart.timing)
            {
                if (it > prevSec + chart.NotesInterval)
                {
                    var note = SingleNotesPool.Instance.GetObject().GetComponent<SingleNote>();
                    note.Setup(1, it);
                    prevSec = it;
                }
            }
        }


        IEnumerator MainRoutine()
        {
            //譜面データのロード
            using (var req = UnityWebRequest.Get(Application.streamingAssetsPath + "\\" + "Charts\\" + "chart_1.json"))
            {
                yield return req.SendWebRequest();
                if (req.isNetworkError) { yield break; }

                var chart = JsonUtility.FromJson<Chart>(req.downloadHandler.text);
                foreach (var it in chart.timing)
                {
                    SingleNote note = SingleNotesPool.Instance.GetObject().GetComponent<SingleNote>();
                    note.Setup(1, it);
                    Notes.Add(note);
                }
            }
            audioSource.Play();
            yield break;
        }
    }
}