
namespace ChenSiYuan
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using Common;

    public class Fanhui : MonoBehaviour
    {
        private Button fanhui;

        // Use this for initialization
        void Start()
        {
            fanhui = GameObject.Find("back").GetComponent<Button>();

            fanhui.onClick.AddListener(() => OnClick());


        }

        // Update is called once per frame
        private void OnClick()
        {

            MySceneManager.RestorePreviousScene();

        }
    }
}