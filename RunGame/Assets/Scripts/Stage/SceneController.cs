using System.Collections;   // コルーチンのため
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RunGame.Stage
{
    /// <summary>
    /// 『ステージ画面』のシーン遷移を制御します。
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        #region インスタンスへのstaticなアクセスポイント
        /// <summary>
        /// このクラスのインスタンスを取得します。
        /// </summary>
        public static SceneController Instance
        {
            get { return instance; }
        }
        static SceneController instance = null;

        /// <summary>
        /// Start()より先に実行されます。
        /// </summary>
        private void Awake()
        {
            instance = this;
        }
        #endregion

        public Vector3 FirstPos;
        public Vector3 Distance;        
        // 起動しているOnPlay()コルーチン
        Coroutine playState = null;
        // 外部のゲームオブジェクトの参照変数
        Player player;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            // 他のゲームオブジェクトを参照
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();           
            // ステージ用のBGMを再生
            AudioClip clip = null;
            // bgmを読み込む
            clip = Resources.Load<AudioClip>("PerituneMaterial_Michikusa_loop");

            var bgmAudio = Camera.main.GetComponent<AudioSource>();
            bgmAudio.clip = clip;
            bgmAudio.Play();

            StartCoroutine(OnStart());
        }

        /// <summary>
        /// コルーチンを使ったカウントダウン演出
        /// </summary>
        IEnumerator OnStart()
        {
            yield return new WaitForSeconds(1); // 1秒待機

            const float showTimeout = 0.6f;

            UiManager.Instance.ShowMessage("3");
            yield return new WaitForSeconds(showTimeout);
            UiManager.Instance.HideMessage();
            yield return new WaitForSeconds(1 - showTimeout);

            UiManager.Instance.ShowMessage("2");
            yield return new WaitForSeconds(showTimeout);
            UiManager.Instance.HideMessage();
            yield return new WaitForSeconds(1 - showTimeout);

            UiManager.Instance.ShowMessage("1");
            yield return new WaitForSeconds(showTimeout);
            UiManager.Instance.HideMessage();
            yield return new WaitForSeconds(1 - showTimeout);
            FirstPos = player.transform.position;

            UiManager.Instance.ShowMessage("GO!");

            // ステージをプレイ開始
            playState = StartCoroutine(OnPlay());

            yield return new WaitForSeconds(1); // 1秒待機
            UiManager.Instance.HideMessage();
        }

        /// <summary>
        /// Playステートの際のフレーム更新処理です。
        /// </summary>
        IEnumerator OnPlay()
        {
            player.IsActive = true;

            while (true)
            {                
                if(Distance[0] < 10000)
                {
                    Distance[0] = player.transform.position[0] - FirstPos[0];
                }
                /*#if UNITY_EDITOR
                                // 「Enter」キーが押された場合『リザルト画面』へ
                                if (Input.GetKeyUp(KeyCode.Return))
                                {
                                    StageClear();
                                    break;
                                }
                                // 'O'キーが押された場合「GameOver」を表示
                                else if (Input.GetKeyUp(KeyCode.O))
                                {
                                    GameOver();
                                    break;
                                }
                #endif*/
                yield return null;
            }
        }

        /// <summary>
        /// ゲームオーバーさせます。
        /// </summary>
        public void GameOver()
        {
            // 現在のコルーチンを停止
            if (playState != null)
            {
                StopCoroutine(playState);
            }

            player.IsActive = false;
            UiManager.Instance.GameOver.Show();
        }
    }
}


