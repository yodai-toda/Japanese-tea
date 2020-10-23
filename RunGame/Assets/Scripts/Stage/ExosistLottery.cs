using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    public class ExosistLottery : MonoBehaviour
    {
        public GameObject Exosist1;
        public GameObject Exosist2;
        public GameObject Exosist3;

        int Count = 0;
        float CreateType = 0.0f;

        Transform target;

        // Start is called before the first frame update
        void Start()
        {
            CreateType = Random.Range(0.0f, 3.0f);
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {            
            if (Count == 0)
            {
                Count++;
                // ハゲ
                if (CreateType < 1.0f)
                {
                    var position = transform.position;
                    Instantiate(Exosist1, position, Quaternion.identity);
                }
                // メガネ
                else if (CreateType < 2.0f)
                {                    
                    var position = transform.position;
                    Instantiate(Exosist2, position, Quaternion.identity);
                    
                }
                // 爺
                else if (CreateType < 3.0f)
                {                    
                    var position = transform.position;
                    Instantiate(Exosist3, position, Quaternion.identity);                    
                }
            }
        }
    }
}
