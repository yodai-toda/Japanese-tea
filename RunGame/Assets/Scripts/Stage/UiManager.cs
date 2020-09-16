using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RunGame.Stage
{
    /// <summary>
    /// UIによる情報表示を管理します。
    /// </summary>
    public class UiManager : MonoBehaviour
    {
        #region インスタンスへのstaticなアクセスポイント
        /// <summary>
        /// このクラスのインスタンスを取得します。
        /// </summary>
        public static UiManager Instance {
            get { return instance; }
        }
        static UiManager instance = null;

        /// <summary>
        /// Start()より先に実行されます。
        /// </summary>
        private void Awake()
        {
            instance = this;
        }
        #endregion

        #region 「Message」UI
        /// <summary>
        /// 「Message」UIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject message = null;

        /// <summary>
        /// 「Message」UIにメッセージを表示します。
        /// </summary>
        /// <param name="text">表示させたいテキスト</param>
        public void ShowMessage(string text)
        {
            message.GetComponentInChildren<Text>().text = text;
            message.SetActive(true);
        }

        /// <summary>
        /// 「Message」UIを隠します。
        /// </summary>
        public void HideMessage()
        {
            message.SetActive(false);
        }
        #endregion

        #region 「TIME: 00.00」表示用のUI
        /// <summary>
        /// 「TIME: 00.00」表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private Text timeUI = null;

        /// <summary>
        /// 「TIME: 00.00」UIの表示を更新します。
        /// </summary>
        private void UpdateTimeUI()
        {
            timeUI.text = SceneController.Instance.PlayTime.ToString("00.00");
        }
        #endregion

        #region 「GameOver」UI
        public GameOverUI GameOver {
            get { return GetComponent<GameOverUI>(); }
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            HideMessage();
            UpdateTimeUI();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTimeUI();
        }
    }
}
