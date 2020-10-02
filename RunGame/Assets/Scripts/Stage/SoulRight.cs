using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class SoulRight : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10;

        new Rigidbody2D rigidbody;
        void Start()
        {
            // 事前にコンポーネントを参照
            rigidbody = GetComponent<Rigidbody2D>();

            // Capsule Collider 2Dの判定エリアを取得
            var collider = GetComponent<CapsuleCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            var velocity = rigidbody.velocity;
            velocity.x = speed;
            rigidbody.velocity = velocity;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Exosist" || collider.tag == "Monster" || collider.tag == "Nurikabe" || collider.tag == "Salt" || collider.tag == "Kekkai")
            {
                Destroy(gameObject);
            }
        }
    }
}
