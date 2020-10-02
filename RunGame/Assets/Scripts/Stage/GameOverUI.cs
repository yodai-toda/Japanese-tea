using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame.Stage
{
    /// <summary>
    /// ゲームオーバーの表示と入力を処理します。
    /// </summary>
    public class GameOverUI : MonoBehaviour
    {
        /// <summary>
        /// 「GameOver」UIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject gameOver = null;

        /// <summary>
        /// 「RESULT」ボタンを指定します。
        /// </summary>
        /// <remarks>
        /// buttons[0]   resultButton
        /// </remarks>
        [SerializeField]
        private Transform[] buttons = null;

        // 0の場合はresultButton
        int selectedIndex = 0;

        /// <summary>
        /// 「GameOver」ダイアログを表示します。
        /// </summary>
        public void Show()
        {
            // ゲームオーバー演出のコルーチンを開始
            StartCoroutine(OnGameOver());
        }

        /// <summary>
        /// GameOverステートの際のフレーム更新処理です。
        /// </summary>
        IEnumerator OnGameOver()
        {
            gameOver.SetActive(true);
            yield return new WaitForSeconds(1);

            while (true)
            {
                // 「Enter」キーが押された場合
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    // 「RESULT」選択中
                    if (selectedIndex == 0)
                    {
                        SceneManager.LoadScene("Result");
                        break;
                    }
                }

                // 0～ボタン配列の最大数から飛び出ないように
                selectedIndex = Mathf.Clamp(selectedIndex, 0, buttons.Length - 1);
                // ボタン配列のすべての要素を繰り返し処理
                for (int index = 0; index < buttons.Length; index++)
                {
                    // 選択中のボタンのみ拡大
                    if (index == selectedIndex)
                    {
                        buttons[index].localScale = new Vector3(1.3f, 1.3f, 1);
                    }
                    else
                    {
                        buttons[index].localScale = Vector3.one;
                    }
                }

                yield return null;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            gameOver.SetActive(false);
        }
    }
}
