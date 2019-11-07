using System.Xml.Schema;

namespace ChenSiYuan
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Video;

    public class Bofang : MonoBehaviour
    {
        private VideoPlayer wplayer;
        private GameObject plane;
        private GameObject tupian;
        private Button bofang;
        int i;
        int k;

        private float x, y, z;
        // Use this for initialization
        private void Start()
        {
            plane = GameObject.FindWithTag("shipin");
            tupian = GameObject.Find("picture");
            x = tupian.transform.position.x;
            y = tupian.transform.position.y;
            z = tupian.transform.position.z;
            wplayer = plane.GetComponent<VideoPlayer>();
            bofang = GameObject.Find("Button").GetComponent<Button>();
            bofang.onClick.AddListener(() => OnClick());//button被点击，就触发OnClick函数。
            plane.transform.position = new Vector3(890, 200, 100);//展示图片
        }

        // Update is called once per frame
        private void OnClick()
        {
            i++;
            k = i % 2;
            if (k == 1)
            {
                plane.transform.position = new Vector3(890, 200, -100);
                tupian.transform.position = new Vector3(1000,2000,0);
                wplayer.Play();//视频播放
            }
            if (k == 0)
            {
                plane.transform.position = new Vector3(890, 200, 100);
                tupian.transform.position = new Vector3(x,y,z);
                wplayer.Stop();//视频关闭

            }


        }
    }
}
