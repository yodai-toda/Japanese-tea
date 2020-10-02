using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RunGame.Result
{
    /// <summary>
    /// 『リザルト画面』のシーン遷移を制御します。
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// クリアータイムを取得または設定します。
        /// </summary>
        public static float ClearTime {
            get { return clearTime; }
            set { clearTime = value; }
        }
        private static float clearTime = 99.99f;

        /// <summary>
        /// クリアータイム表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject clearTimeUI = null;

        /// <summary>
        /// ベストタイム表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject bestTimeUI = null;

        // Start is called before the first frame update
        void Start() {
            // UIを更新
            clearTimeUI.GetComponentInChildren<Text>().text =
                ClearTime.ToString("00.00");
            bestTimeUI.GetComponentInChildren<Text>().text =
                GameController.Instance.BestTime.ToString("00.00");
        }

        // Update is called once per frame
        void Update() {
            // 「Enter」キーが押された場合
            if (Input.GetKeyUp(KeyCode.Space)) 
            {
                
                // 『タイトル画面』へシーン遷移
                SceneManager.LoadScene("Title");
                
            }
        }
    }
}