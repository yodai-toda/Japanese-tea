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
        /// 起動するシーン番号を取得または設定します。
        /// </summary>
        public static int StageNo {
            get { return stageNo; }
            set { stageNo = value; }
        }
        private static int stageNo = 0;

        /// <summary>
        /// プレハブからステージを生成する場合はtrueを指定します。
        /// </summary>
        /// <remarks>ステージ開発用のシーンではfalseに設定します。</remarks>
        [SerializeField]
        private bool instantiateStage = true;

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

            // ステージプレハブを読み込む
            if (instantiateStage)
            {
                var stageName = string.Format("Stage {0}", stageNo);
                var stage = Resources.Load<GameObject>(stageName);
                Instantiate(stage);
            }

            // それぞれのステージ用のBGMを再生
            AudioClip clip = null;
            // bgmを読み込む
            if (stageNo == GameController.Instance.StageNames.Length - 1)
            {
                // 最終ステージの場合
                clip = Resources.Load<AudioClip>("bgm_02");
            }
            else
            {
                // 通常ステージの場合
                clip = Resources.Load<AudioClip>("bgm_01");
            }
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

#if UNITY_EDITOR
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
#endif
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

            // ステージクリアー演出のコルーチンを開始
            StartCoroutine(OnStageClear());
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

        /// <summary>
        /// StageClearステートの際のフレーム更新処理です。
        /// </summary>
        IEnumerator OnStageClear()
        {
            // ベストタイムを更新
            if (PlayTime < GameController.Instance.BestTime)
            {
                GameController.Instance.BestTime = PlayTime;
            }
            UiManager.Instance.ShowMessage("CLEAR!");
            yield return new WaitForSeconds(1);
            // 入力を待ち受けるために無限ループ
            while (true)
            {
                // 「Enter」キーが押された場合
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    // ステージ番号を伝えてから「Result」を読み込む
                    Result.SceneController.StageNo = StageNo;
                    Result.SceneController.ClearTime = PlayTime;
                    SceneManager.LoadScene("Result");
                    break;
                }
                yield return null;  // 次のフレームまで待機
            }
        }
    }
}