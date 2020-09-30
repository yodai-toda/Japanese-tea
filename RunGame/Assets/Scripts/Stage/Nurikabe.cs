using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class Nurikabe : MonoBehaviour
    {
        Transform target;

        new Rigidbody2D rigidbody;
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            // 事前にコンポーネントを参照
            rigidbody = GetComponent<Rigidbody2D>();

            // Box Collider 2Dの判定エリアを取得
            var collider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Exosist")
            {
                Destroy(gameObject);
            }
        }
    }
}