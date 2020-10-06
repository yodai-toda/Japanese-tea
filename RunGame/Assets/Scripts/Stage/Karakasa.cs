using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class Karakasa : MonoBehaviour
    {
        [SerializeField]
        private float speed = -2;
        Transform target;
        Animator animator;
        bool isJump = false;
        float time = 0.0f;

        new Rigidbody2D rigidbody;
        void Start()
        {
            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            // 事前にコンポーネントを参照
            rigidbody = GetComponent<Rigidbody2D>();

            // Box Collider 2Dの判定エリアを取得
            var collider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime; 
            var velocity = rigidbody.velocity;
            if(isJump == false) 
            {
                velocity.x = 0;
                if (time > 1.0f && time < 1.2f)
                {
                    animator.SetBool("isJump", true);
                }
                else if(time > 1.2f)
                {
                    isJump = true;
                }
            }
            else if(isJump == true)
            {
                velocity.x = speed;
                if (time > 2.0f)
                {
                    time = 0.0f;
                    isJump = false;
                    animator.SetBool("isJump", false);
                }
            }
            rigidbody.velocity = velocity;


            if (target == null)
            {
                speed = 0;
                animator.SetBool("isJump", false);
            }
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                speed = 0;
            }
            if (collider.tag == "Exosist" || collider.tag == "SoulAttack")
            {
                Destroy(gameObject);
            }
        }
    }
}
