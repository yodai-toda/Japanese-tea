using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class SoulFactory : MonoBehaviour
    {
        public GameObject Prefabs;

        float CreateTime = 5.0f;
        float CreateTimer = -3.0f;
        
        Transform target;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Exosist").transform;
        }

        // Update is called once per frame
        void Update()
        {
            CreateTimer += Time.deltaTime;
            if (CreateTime < CreateTimer)
            {
                CreateTimer = 0.0f;
                var position = target.position;
                position.x = target.position.x + 20;
                position.y = transform.position.y;
                Instantiate(Prefabs, position, Quaternion.identity);
            }         
        }
    }
}
