using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame.Load
{
    public class Load : MonoBehaviour
    {
        float timer = 0.0f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            //if (timer == 3.0f)
            if (Input.GetKeyUp(KeyCode.Return))
            {
                // 『ステージ選択画面』へシーン遷移
                SceneManager.LoadScene("Stage");
            }
        }
    }
}
