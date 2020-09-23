using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class Exosist : MonoBehaviour
    {
        float time = 0.0f;

        [SerializeField]
        private float speed = 4;
        Transform target;

        Animator Animator;
       

        new Rigidbody2D rigidbody;
        void Start()
        {
            Animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            // 事前にコンポーネントを参照
            rigidbody = GetComponent<Rigidbody2D>();

            // Box Collider 2Dの判定エリアを取得
            var collider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            time += Time.deltaTime;
            var velocity = rigidbody.velocity;
            velocity.x = speed;
            rigidbody.velocity = velocity;
            if (target == null)
            {
                speed = 0;
                Animator.SetBool("Jorei", true);
            }
            if(time >=  5.0f)
            {
                Animator.SetFloat("Time", time);
                
            }
            if (time >= 5.1f)
            {
                time = 0.0f;
                Animator.SetFloat("Time", time);
            }
        }
    }
}