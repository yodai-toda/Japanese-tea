using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class Exosist3 : MonoBehaviour
    {
        public GameObject Prefabs;
        float time = 0.0f;
        float Starttime = 0.0f;
        float SoulTime = 5.0f;

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
        void Update()
        {
            time += Time.deltaTime;
            Starttime += Time.deltaTime;
            SoulTime += Time.deltaTime;
            var velocity = rigidbody.velocity;
            velocity.x = speed;
            rigidbody.velocity = velocity;
            if (target != null)
            {
                if (time >= 5.0f && time <= 5.1f)
                {
                    Animator.SetFloat("isKekkai", time);
                }
                if (time >= 5.8f)
                {
                    time = 0.0f;
                    Animator.SetFloat("isKekkai", time);
                    var position = transform.position;
                    position.x = transform.position.x + 17;
                    position.y = transform.position.y;
                    Instantiate(Prefabs, position, Quaternion.identity);
                }
            }
            
            if (Starttime < 3.0f)
            {
                speed = 6;
            }
            else if(SoulTime < 3.0f)
            {
                speed = 3;
            }
            else if(speed != 0)
            {
                speed = 4;
            }
            
            if(target == null)
            {
                speed = 0;
                Animator.SetBool("Jorei", true);
            }
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            // 人魂と接触
            if (collider.tag == "SoulAttack")
            {
                if (SoulTime > 3.0f)
                {
                    speed = 3;
                    SoulTime = 0.0f;
                }
            }
        }
    }
}