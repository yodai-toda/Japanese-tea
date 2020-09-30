using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RunGame.Stage
{
    public class IttanMomen : MonoBehaviour
    {
        [SerializeField]
        private float speed = -4;
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
            var velocity = rigidbody.velocity;
            velocity.x = speed;
            rigidbody.velocity = velocity;
            if (target == null)
            {
                speed = 0;
            }
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                speed = 0;
            }
            if (collider.tag == "Exosist")
            {
                Destroy(gameObject);
            }
        }
    }
}
