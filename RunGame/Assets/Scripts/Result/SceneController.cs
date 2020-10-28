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
        public static float Distance {
            get { return distance; }
            set { distance = value; }
        }
        private static float distance = 9999.99f;

        /// <summary>
        /// クリアータイム表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject DistanceUI = null;

        /// <summary>
        /// ベストタイム表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject SoulUI = null;

        // Start is called before the first frame update
        void Start() {
            // UIを更新
            DistanceUI.GetComponentInChildren<Text>().text =
                GameController.Instance.GameOverDistance[0].ToString("0");
            SoulUI.GetComponentInChildren<Text>().text =
                GameController.Instance.Soul.ToString("0");
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