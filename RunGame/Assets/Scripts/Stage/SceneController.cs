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
        public static SceneController Instance {
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
      
        /// <summary>
        /// ステージ開始からの経過時間(秒)を取得します。
        /// </summary>
        public float PlayTime { get; private set; }
        //public float PlayTime {
        //    get { return playTime; }
        //    private set { playTime = value; }
        //}
        //float playTime = 0;

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
                PlayTime += Time.deltaTime;

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
        /// ステージをクリアーさせます。
        /// </summary>
        public void StageClear()
        {
            // 現在のコルーチンを停止
            if (playState != null)
            {
                StopCoroutine(playState);
            }

            player.IsActive = false;           
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