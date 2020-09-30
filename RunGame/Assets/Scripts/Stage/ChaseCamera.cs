using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    /// <summary>
    /// 追尾カメラを表します。
    /// </summary>
    public class ChaseCamera : MonoBehaviour
    {
        // 追尾対象(プレイヤー)
        Transform target;
        float time = 0;

        // Start is called before the first frame update
        void Start() 
        {
            // 他のゲームオブジェクトを参照
            target = GameObject.FindGameObjectWithTag("Player").transform;

            // 追尾対象が未指定の場合は"Player"タグのオブジェクトで設定
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        // Update is called once per frame
        void Update() {
            time += Time.deltaTime;
            if (target != null && time >= 1.93f) 
            {
                var position = Camera.main.transform.position;
                position.x = target.position.x;
                Camera.main.transform.position = position;
            }
        }
    }
}