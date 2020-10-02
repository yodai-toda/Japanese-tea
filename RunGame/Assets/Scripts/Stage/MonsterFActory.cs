using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class MonsterFActory : MonoBehaviour
    {
        public GameObject IttanMomenPrefabs;
        public GameObject NurikabePrefabs;
        public GameObject CatPrefabs;

        float CreateTime = 7.0f;
        float CreateTimer = -3.0f;
        float CreateType = 0.0f;

        Transform target;
        Transform Target;
        
        // Start is called before the first frame update
        void Start()
        {
            CreateType = Random.Range(0.0f, 2.0f);
            target = GameObject.FindGameObjectWithTag("Player").transform;
            Target = GameObject.FindGameObjectWithTag("Respawn").transform;
        }

        // Update is called once per frame
        void Update()
        {
            CreateTimer += Time.deltaTime;
            // 一反木綿
            if(CreateType < 1.0f)
            {
                if(CreateTime < CreateTimer)
                {
                    CreateTimer = 0.0f;
                    CreateType = Random.Range(0.0f, 2.0f);
                    var position = target.position;
                    position.x = target.position.x + 20;
                    position.y = transform.position.y -1;
                    Instantiate(IttanMomenPrefabs, position, Quaternion.identity);
                }

            }
            // 塗り壁
            else if(CreateType >= 1.0f)
            {
                if (CreateTime < CreateTimer)
                {
                    CreateTimer = 0.0f;
                    CreateType = Random.Range(0.0f, 2.0f);
                    var position = target.position;
                    position.x = target.position.x + 20;
                    position.y = transform.position.y ;
                    Instantiate(NurikabePrefabs, position, Quaternion.identity);
                    var Position = target.position;
                    Position.x = target.position.x + 10;
                    Position.y = Target.position.y;
                    Instantiate(CatPrefabs, Position, Quaternion.identity);
                }
            }
        }
    }
}