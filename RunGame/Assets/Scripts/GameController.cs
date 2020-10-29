﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    /// <summary>
    /// ゲーム全体で使用されるリソースを管理します。
    /// </summary>
    public class GameController : MonoBehaviour
    {
        #region シングルトンインスタンス

        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            // Resourcesからプレハブを読み込む
            var prefab = Resources.Load<GameObject>("GameController");
            Instantiate(prefab);
        }

        /// <summary>
        /// このクラスのインスタンスを取得します。
        /// </summary>
        public static GameController Instance
        {
            get
            {
                return instance;
            }
        }
        private static GameController instance = null;

        /// <summary>
        /// Start()の実行より先行して処理したい内容を記述します。
        /// </summary>
        void Awake()
        {
            // 初回作成時
            if (instance == null)
            {
                instance = this;
                // シーンをまたいで削除されないように設定
                DontDestroyOnLoad(gameObject);
                // セーブデータを読み込む
                Load();
            }
            // 2個目以降の作成時
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion        
        GameObject Player;
        RunGame.Stage.Player script;
        public int Soul = 0;
        public int AllScore = 0;

        /// <summary>
        /// ベストタイムを取得または設定します。
        /// </summary>
        public float BestTime
        {
            get { return bestTime; }
            set
            {
                bestTime = value;
                Save();
            }
        }
        /// <summary>
        /// ベストタイムの基準値を指定します。
        /// </summary>
        [SerializeField]
        private float bestTime = 60;

        public Vector3 GameOverDistance;

        /// <summary>
        /// ステージ名を取得します。
        /// </summary>
        public string[] StageNames
        {
            get { return stageNames; }
        }
        /// <summary>
        /// ステージ名を配列で指定します。
        /// </summary>
        [SerializeField]
        private string[] stageNames = {
            "チュートリアル",
            "旅立ち",
            "激闘",
        };

        /// <summary>
        /// GameControllerが破棄される場合に呼び出されます。
        /// </summary>
        private void OnDestroy()
        {
            Save();
        }

        // セーブデータ用の識別子
        static readonly string bestTimeKey = "BestTimeKey";

        /// <summary>
        /// ゲームデータを読込みます。
        /// </summary>
        private void Load()
        {
            BestTime = PlayerPrefs.GetFloat(bestTimeKey, BestTime);
        }

        /// <summary>
        /// ゲームデータを保存します。
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetFloat(bestTimeKey, BestTime);
            PlayerPrefs.Save();
        }

        void Start()
        {
            Player = GameObject.Find("Player");
            script = Player.GetComponent<Stage.Player>();


        }
        // Update is called once per frame
        void Update()
        {
            Player = GameObject.Find("Player");
            if (Player != null)
            {                
                script = Player.GetComponent<Stage.Player>();
                GameOverDistance[0] = (int)RunGame.Stage.SceneController.Instance.Distance[0];
                if(GameOverDistance[0] == 9999)
                {
                    GameOverDistance[0] = 10000;
                }
                GameOverDistance[0] *= 10;
                if(script.soul == 99)
                {
                    script.soul = 100;
                }
                Soul = script.soul * 100;

                AllScore = (int)GameOverDistance[0] + Soul; 
            }
        }
    }
}