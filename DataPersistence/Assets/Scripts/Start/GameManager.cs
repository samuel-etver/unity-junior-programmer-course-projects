using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartScene
{
    public class GameManager : MonoBehaviour
    {
        public TMP_InputField PlayerNameInput;

        public void OnStartButtonClick()
        {
            var playerName = PlayerNameInput.text;
            Debug.Log(playerName);
            var globalStorage = GlobalStorage.Instance;
            globalStorage.PlayerName = playerName;

            SceneManager.LoadScene("main");
        }
    }
}
