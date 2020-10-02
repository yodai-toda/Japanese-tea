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
        // Update is called once per frame
        void Update() {
            // 「Enter」キーが押された場合
            if (Input.GetKeyUp(KeyCode.Space)) {
                // 『ステージ画面』へシーン遷移
                SceneManager.LoadScene("Stage");
                return;
            }
            
        }
    }
}