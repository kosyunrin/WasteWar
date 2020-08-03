using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    [SerializeField]
    Text texxxt;
    SK.GAMEDATA.CSDataManager.GameData ddd;
    // Start is called before the first frame update
    private void Awake()
    {
        //SK.GAMEDATA.CSDataManager.sharedInstance.InitLoadData();
    }
    void Start()
    {
        print(Application.persistentDataPath);
        //texxxt.text =
        //SK.GAMEDATA.CSDataManager.sharedInstance.gamedata.name;
        ddd = SK.GAMEDATA.CSDataManager.sharedInstance.InitLoadDatax();
        texxxt.text =
      ddd.name;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.G))
        {
            SK.GAMEDATA.CSDataManager.GameData asd;
            asd.name = myName;
            SK.GAMEDATA.CSDataManager.sharedInstance.Save(asd);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SK.GAMEDATA.CSDataManager.sharedInstance.DelData();
        }
    }

    string text = "";
    string myName = "";
    void OnGUI()
    {
        //用标签显示文本
        GUILayout.Label("请输入你的名字：");
        //用文本区域输入名字
        text = GUILayout.TextField(text);
        //
        if (GUILayout.Button("提交"))
        {
            myName = text;
            texxxt.text = myName;

            SK.GAMEDATA.CSDataManager.GameData asd;
            asd.name = myName;
            SK.GAMEDATA.CSDataManager.sharedInstance.Save(asd);
        }
        //当myName不为空的时候，说明我们已经提交了名字，则显示名字
        if (!string.IsNullOrEmpty(myName))
        {
            GUILayout.Label("提交成功，名字：" + myName);
        }
    }
}
