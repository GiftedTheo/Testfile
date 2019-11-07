
namespace ChenSiYuan
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;
    using Common;
    using cn.bmob.api;
    using cn.bmob.io;
    using cn.bmob.exception;
    using cn.bmob.tools;
    using cn.bmob.json;
    using cn.bmob.response;
    using cn.bmob.http;

    public class ZhuangchaiScriptweb : MonoBehaviour
    {
        public GameObject[] gameobject = new GameObject[25];
        public float[] x = new float[25];
        public float[] y = new float[25];
        public float[] z = new float[25];
        public int num1 = 0;
        public int num2 = 0;
        private int k = 0, all = 0;
        private string score;
        //private string updateScore;             //上传分数参数，该参数应用于机械课题组大的实验平台，也适用于该项目内部小的数据库
        private Button chongzhi;
        private Button liucheng;
        public GameObject chaizhuangtishi;
        public int a = 0;
        private int[] m = new int[11];
        public UserManager UserManager;
        //private Vector3 target;
        //private int jilushu;
        private static BmobUnity Bmob;
        
        
        void Start()
        {
            for (int i = 0; i < 25; i++)//利用tag将所有定义的游戏对象赋值
            {
                gameobject[i] = GameObject.FindWithTag(i.ToString());
            }
            for (int i = 0; i < 25; i++)//利用for循环获取每一个零件的原始x,y,z轴坐标位置，用于装配时直接归位
            {
                x[i] = gameobject[i].transform.position.x;//获取x轴坐标位置
                y[i] = gameobject[i].transform.position.y;
                z[i] = gameobject[i].transform.position.z;
            }
            gameobject[24].SetActive(false);
            liucheng = GameObject.Find("流程").GetComponent<Button>();
            chongzhi = GameObject.Find("Button").GetComponent<Button>();
            chongzhi.onClick.AddListener(() => OnClick(1));
            liucheng.onClick.AddListener(() => OnClick(2));
            chaizhuangtishi = GameObject.Find("拆装提示");
            
            BmobDebug.Register (print);
            BmobDebug.level = BmobDebug.Level.TRACE;
            Bmob = gameObject.GetComponent<BmobUnity> ();
            
        }

        void Update()
        {
            if (Dialog.ExistDialog)
            {
                return;
            }
            
            if (Input.GetButtonDown("Fire1"))//判断左键是否被点击，左键代表拆卸
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//利用射线检测碰撞器状态
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo) )
                {
                    GameObject gameObj = hitInfo.transform.gameObject;//将被点击的零件作为临时游戏对象
                    if (gameObj != gameobject[num1])
                        k++;
                    if (gameObj == chaizhuangtishi && num1 <= 23)
                    {
                        gameObj = gameobject[num1];
                        k = 5;
                    }
               
                    if (gameObj == gameobject[num1])
                    {
                        gameobject[24].SetActive(false);//隐藏错误提示
                        num2 = num1;
                        num1++;
                        switch (gameObj.transform.tag)//利用switch-case和零件的tag来判断被点击的零件，执行对应的操作
                        {                            //tag即标签是在unity中设定好的
                            case "0"://0是装配体21-六角螺栓5、6、7、8的tag
                                for (int k = 0; k < 4; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], 250, z[k]);
                                num1 = 4;
                                m[0] = Jifen(); a = 1;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>1.盖板螺钉<color=#00000000>空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                            case "4"://4是装配体平垫圈9/10/11/12的tag
                                for (int k = 4; k < 8; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], 220, z[k]);
                                num1 = 8;
                                m[1] = Jifen(); a = 2;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>2.平垫圈<color=#00000000>空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                            case "8"://8是装配体底盖的tag
                                /*
                                jilushu = 8;
                                break;
                                */
                                gameobject[8].transform.position = new Vector3(x[8], -200, z[8]);
                                
                                m[2] = Jifen(); a = 3;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>3.底盖<color=#00000000>空空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                                
                            case "9"://9是密封圈的tag
                                /*
                                jilushu = 9;
                                break;
                                */
                                gameobject[9].transform.position = new Vector3(x[9], -100, z[9]);
                                m[3] = Jifen(); a = 4;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>4.密封圈<color=#00000000>空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                                
                            case "10"://10是上盖的tag
                                gameobject[10].transform.position = new Vector3(x[10], 190, z[10]);
                                m[4] = Jifen(); a = 5;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>5.上盖<color=#00000000>空空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                            case "11"://11、12是滤芯的tag
                                for (int k = 11; k < 13; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], 150, z[k]);
                                num1 = 13;
                                m[5] = Jifen(); a = 6;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>6.滤芯<color=#00000000>空空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                score = string.Format(
                              "<size=20>1.盖板螺钉<color=#00000000>空1</color> {0,-1}/共10分</size>\n" +
                              "<size=20>2.平垫圈<color=#00000000>空空1</color> {1,-1}/共9分</size>\n" +
                              "<size=20>3.底盖<color=#00000000>空空空1</color> {2,-1}/共9分</size>\n" +
                              "<size=20>4.密封圈<color=#00000000>空空1</color> {3,-1}/共9分</size>\n" +
                              "<size=20>5.上盖<color=#00000000>空空空1</color> {4,-1}/共9分</size>\n" +
                              "<size=20>6.滤芯<color=#00000000>空空空1</color> {5,-1}/共9分</size>\n" +
                              "<size=20>总分：{6,-1}分/共100分</size>",
                                         m[0], m[1], m[2], m[3], m[4],m[5], all);
                                Dialog.Create("", "网式滤油器拆装完毕");
                                Dialog.Create("得分", score);
                                all = 0;
                                break;
                            case "13"://0是装配体21-六角螺栓5、6、7、8的tag
                                for (int k = 13; k < 17; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], 520, z[k]);
                                num1 = 17;
                                m[6] = Jifen(); a = 7;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>1.盖板螺钉<color=#00000000>空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                            case "17"://4是装配体平垫圈9/10/11/12的tag
                                for (int k = 17; k < 21; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], 470, z[k]);
                                num1 = 21;
                                m[7] = Jifen(); a = 8;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>2.平垫圈<color=#00000000>空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                            case "21"://8是装配体底盖的tag
                                gameobject[21].transform.position = new Vector3(x[21], 400, z[21]);
                                
                                m[8] = Jifen(); a = 9;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>3.上盖<color=#00000000>空空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                                
                            case "22"://9是密封圈的tag
                                gameobject[22].transform.position = new Vector3(x[22], 350, z[22]);
                                m[9] = Jifen(); a = 10;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>4.密封圈<color=#00000000>空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                break;
                            case "23"://23是滤芯的tag
                                gameobject[23].transform.position = new Vector3(x[23], 300, z[23]);
                                num1 = 24;
                                m[10] = Jifen(); a = 11;
                                all = 0;
                                for (int i = 0; i < a; i++)
                                {
                                    all = all + m[i];
                                }
                                Dialog.Create("得分", string.Format("<size=20>5.滤芯<color=#00000000>空空空1</color> {0,-1}分</size>\n" +
                                                                    "<size=20>目前总分:<color=#00000000>空11</color> {1,-1}分</size>", m[a - 1], all));
                                score = string.Format(
                                    "<size=20>网式滤油器拆装</size>\n" +
                                    "<size=20>1.盖板螺钉<color=#00000000>空1</color> {0,-1}/共10分</size>\n" +
                                    "<size=20>2.平垫圈<color=#00000000>空空1</color> {1,-1}/共9分</size>\n" +
                                    "<size=20>3.底盖<color=#00000000>空空空1</color> {2,-1}/共9分</size>\n" +
                                    "<size=20>4.密封圈<color=#00000000>空空1</color> {3,-1}/共9分</size>\n" +
                                    "<size=20>5.上盖<color=#00000000>空空空1</color> {4,-1}/共9分</size>\n" +
                                    "<size=20>6.滤芯<color=#00000000>空空空1</color> {5,-1}/共9分</size>\n" +
                                    "<size=20>线隙式滤油器拆装</size>\n" +
                                    "<size=20>1.盖板螺钉<color=#00000000>空1</color> {6,-1}/共9分</size>\n" +
                                    "<size=20>2.平垫圈<color=#00000000>空空1</color> {7,-1}/共9分</size>\n" +
                                    "<size=20>3.上盖<color=#00000000>空空空1</color> {8,-1}/共9分</size>\n" +
                                    "<size=20>4.密封圈<color=#00000000>空空1</color> {9,-1}/共9分</size>\n" +
                                    "<size=20>5.滤芯<color=#00000000>空空空1</color> {10,-1}/共9分</size>\n" +
                                    "<size=20>总分：{11,-1}分/共100分</size>",
                                    m[0], m[1], m[2], m[3], m[4], m[5], m[6], m[7], m[8], m[9], m[10], all);
                                Dialog.Create("", "线隙式滤油器拆装完毕");
                                Dialog.Create("得分", score);
                                Dialog.Create("", "拆卸已完成");
                                
                                
                                //update_data(all);
                                
                                add_data(all);
                                //UpLoadScore(updateScore);
                                all = 0;
                                break;
                        }
                        k = 0;
                    }
                    else
                        if (gameObj != chaizhuangtishi)
                        gameobject[24].SetActive(true);

                }       //当拆装顺序错误时跳出错误提示
                
            }
            
            if (Input.GetButtonDown("Fire2"))//判断右键是否被点击，右键代表装配操作，其他代码与拆卸部分方法相同
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo) )
                {
                    GameObject gameObj = hitInfo.transform.gameObject;
                    if (gameObj == chaizhuangtishi&&num2>=0)
                        gameObj = gameobject[num2];
                    if (gameObj == gameobject[num2])
                    {
                        gameobject[24].SetActive(false);
                        num1 = num2;
                        switch (gameObj.transform.tag)
                        {
                            case "0":
                                for (int k = 0; k < 4; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], y[k], z[k]);
                                num2 = -1;
                                Dialog.Create("", "网式滤油器装配已完成");
                                break;
                            case "4":
                                for (int k = 4; k < 8; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], y[k], z[k]);
                                num2 = 0;
                                break;
                            case "8":
                                gameobject[8].transform.position = new Vector3(x[8], y[8], z[8]);
                                num2 = 4;
                                break;
                            case "9":
                                gameobject[9].transform.position = new Vector3(x[9], y[9], z[9]);
                                num2 = 8;
                                break;
                            case "10":
                                gameobject[10].transform.position = new Vector3(x[10], y[10], z[10]);
                                num2 = 9;
                                break;
                            case "11":
                                for (int k = 11; k < 13; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], y[k], z[k]);
                                num2 = 10;
                                break;
                            case "13"://0是装配体21-六角螺栓5、6、7、8的tag
                                for (int k = 13; k < 17; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], y[k], z[k]);
                                num2 = 11;
                                Dialog.Create("", "线隙式滤油器装配已完成");
                                break;
                            case "17"://4是装配体平垫圈9/10/11/12的tag
                                for (int k = 17; k < 21; k++)
                                    gameobject[k].transform.position = new Vector3(x[k], y[k], z[k]);
                                num2 = 13;
                                
                                break;
                            case "21"://8是装配体底盖的tag
                                gameobject[21].transform.position = new Vector3(x[21], y[21], z[21]);
                                num2 = 17;
                                break;
                                
                            case "22"://9是密封圈的tag
                                gameobject[22].transform.position = new Vector3(x[22], y[22], z[22]);
                                num2 = 21;
                                break;
                            case "23"://11、12是滤芯的tag
                                gameobject[23].transform.position = new Vector3(x[23], y[23], z[23]);
                                num2 = 22;
                                break;
                        }
                    }
                    else
                        if (gameObj != chaizhuangtishi)
                        gameobject[24].SetActive(true);
                }
            }
        }
        private void OnClick(int n)
        {
            string chaixieliucheng;
            int all1 = 0;
            string[] b = new string[11];//20-6
            if (a < 11)
            {
                for (int i = 0; i < a; i++)
                {
                    b[i] = m[i] + "分";
                    all1 = all1 + m[i];
                }
                for (int i = a + 1; i < 11; i++)
                    b[i] = "未拆卸";
                b[a] = "等待拆卸";
            }
            if (a == 11)
            {
                for (int i = 0; i < a; i++)
                {
                    b[i] = m[i] + "分";
                    all1 = all1 + m[i];
                }
            }
            chaixieliucheng =
                string.Format("<size=20>网式滤油器拆装</size>\n" +
                              "<size=20>1.盖板螺钉<color=#00000000>空1</color> {0,-1}/共10分</size>\n" +
                              "<size=20>2.平垫圈<color=#00000000>空空1</color> {1,-1}/共9分</size>\n" +
                              "<size=20>3.底盖<color=#00000000>空空空1</color> {2,-1}/共9分</size>\n" +
                              "<size=20>4.密封圈<color=#00000000>空空1</color> {3,-1}/共9分</size>\n" +
                              "<size=20>5.上盖<color=#00000000>空空空1</color> {4,-1}/共9分</size>\n" +
                              "<size=20>6.滤芯<color=#00000000>空空空1</color> {5,-1}/共9分</size>\n" +
                              "<size=20>线隙式滤油器拆装</size>\n" +
                              "<size=20>1.盖板螺钉<color=#00000000>空1</color> {6,-1}/共9分</size>\n" +
                              "<size=20>2.平垫圈<color=#00000000>空空1</color> {7,-1}/共9分</size>\n" +
                              "<size=20>3.上盖<color=#00000000>空空空1</color> {8,-1}/共9分</size>\n" +
                              "<size=20>4.密封圈<color=#00000000>空空1</color> {9,-1}/共9分</size>\n" +
                              "<size=20>5.滤芯<color=#00000000>空空空1</color> {10,-1}/共9分</size>\n" +
                              "<size=20>总分：{11,-1}分/共100分</size>",
                              b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], b[10], all1);
            if (n == 1)
            {
                Camera.main.orthographicSize = 400f;
                Camera.main.transform.position = new Vector3(-600, 220, 500);
                Camera.main.transform.localEulerAngles = new Vector3(0, 110, 0);
            }
            if (n == 2)
                Dialog.Create("拆卸流程", chaixieliucheng);
           
        }
        private int Jifen()
        {
            int j = 0;
            if (k == 0 || k == 1)
                j = 9;
            if (k == 2)
                j = 6;
            if (k == 3)
                j = 3;
            if (k > 3)
                j = 0;
            if (num1 == 4)
                j = j + 1;
            return j;
        }
        
        
        public const string TABLENAME = "FilterStudy";

        public class BmobGameObject : BmobTable
        {
            public string userId { get; set; }
            //public string password { get; set; }
            public BmobInt myscore { get; set; }

            public override void readFields(BmobInput input)  //读取数据库里数据
            {
                base.readFields(input);
            }

            public override void write(BmobOutput output, bool all)  //写入数据至数据库
            {
                base.write(output, all);
                output.Put("username",this.userId);
                //output.Put("password",this.password);
                output.Put("cxscore",this.myscore);
            }
        }

        //private string usernameid;
        
        void add_data(int thisscore)      //在数据库中增加数据方法
        {
            var new_data = new BmobGameObject();
            new_data.userId = PlayerPrefs.GetString(typeof(name_pass) + "Id");
            new_data.myscore = thisscore;
            
            
            Bmob.Create(TABLENAME, new_data, (resp, exception) =>
                {
                    if (exception != null)
                    {
                        print("fail" + exception.Message);
                        return;
                    }
                    
                    Debug.Log("创建成功");
                
                }
            );
        }
        
        /*
        void update_data(int thisscore)
        {
            BmobGameObject game = new BmobGameObject();
            game.myscore = thisscore;
            
            usernameid = PlayerPrefs.GetString("usernameid");
            
            Bmob.Update(TABLENAME, usernameid, game, (resp, exception) =>
            {
                if (exception != null)
                {
                    print("update failed");
                    print(usernameid);
                    return;
                }
                print("success");
            });
            //PlayerPrefs.DeleteAll();
        }
        */
        
        //方法UpLoadScore是基于科明公司编写的上传成绩的库所写的方法，在上面的程序中会调用一次，但是在自己学校的项目中不会使用该方法
        //而是使用自己编写的方法
        /*
        private void UpLoadScore(string thescore)
        {
            StartCoroutine(UserManager.UploadScore(thescore));
        }
        */
    }
}
