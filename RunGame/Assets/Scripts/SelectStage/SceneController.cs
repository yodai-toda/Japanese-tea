using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RunGame.SelectStage
{
    /// <summary>
    /// 『ステージ選択画面』のシーン遷移を制御します。
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// 「StageButton」の親オブジェクトを指定します。
        /// </summary>
        [SerializeField]
        private Transform buttons = null;
        /// <summary>
        /// ボタン選択時の表示スケールを指定します。
        /// </summary>
        [SerializeField]
        private Vector3 selectedScale = new Vector3(1.1f, 1.1f, 1);

        // 現在選択されているボタンを示すインデックス
        int selectedIndex = 0;

        // Start is called before the first frame update
        void Start() {
            // GameControllerからステージ名一覧を取得
            var stageNames = GameController.Instance.StageNames;
            // buttons配列から各ボタンをループ処理
            for (int index = 0; index < buttons.childCount; index++) {
                var button = buttons.GetChild(index);
                // ボタンのテキストを修正
                button.GetComponentInChildren<Text>().text =
                    string.Format("STAGE {0}\n{1}", index, stageNames[index]);
            }
        }

        // Update is called once per frame
        void Update() {
            // 「Enter」キーが押された場合
            if (Input.GetKeyUp(KeyCode.Return)) {
                // 『ステージ画面』へシーン遷移
                Stage.SceneController.StageNo = selectedIndex;
                SceneManager.LoadScene("Stage");
                return;
            }
            // 左カーソルキーが押された場合
            else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                selectedIndex--;
                if (selectedIndex < 0) {
                    selectedIndex = 0;
                }
            }
            // 右カーソルキーが押された場合
            else if (Input.GetKeyUp(KeyCode.RightArrow)) {
                selectedIndex++;
                if (selectedIndex > buttons.childCount - 1) {
                    selectedIndex = buttons.childCount - 1;
                }
            }

            // buttons配列から各ボタンをループ処理
            for (int index = 0; index < buttons.childCount; index++) {
                var button = buttons.GetChild(index);
                // 選択中のボタンは拡大表示
                if (index == selectedIndex) {
                    button.localScale = selectedScale;
                }
                // 非選択中のボタンは通常表示
                else {
                    button.localScale = Vector3.one;
                }
            }
        }
    }
}