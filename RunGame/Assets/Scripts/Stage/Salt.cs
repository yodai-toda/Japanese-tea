using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class Salt : MonoBehaviour
    {
        [SerializeField]
        private float speed = 6;
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
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player" || collider.tag == "Monster" || collider.tag == "Nurikabe" || collider.tag == "SoulAttack")
            {
                Destroy(gameObject);
            }
            
        }
    }
}