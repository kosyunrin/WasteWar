using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SK.GAMEDATA
{
    public class CSDataManager : MonoBehaviour
    {
        public static CSDataManager sharedInstance = null;
        [Serializable]
        public struct GameData
        {
            public string name;

        }
        public GameData gamedata;
        private const string DataName = "GameData.json";
        private string FullDataPath { get { return Application.persistentDataPath + "/" + DataName; } }

        private void Awake()
        {
            sharedInstance=this;
        }
        private void Start()
        {
        }
        // 游戏开始时载入存档（可能没有）
        public void InitLoadData()
        {
            string dataStr = LoadData(FullDataPath);
            if (dataStr == string.Empty)
            {
                gamedata.name = null;
            }
            else
            {
                gamedata = JsonUtility.FromJson<GameData>(dataStr);
            }
        }
        public GameData InitLoadDatax()
        {
            string dataStr = LoadData(FullDataPath);
            if (dataStr == string.Empty)
            {
                gamedata.name = null;
            }
            else
            {
                return gamedata = JsonUtility.FromJson<GameData>(dataStr);
            }
            return gamedata;
        }

        // 载入存档
        private string LoadData(string path)
        {
            string dataStr = string.Empty;
            if (System.IO.File.Exists(path))
            {
                System.IO.StreamReader file = System.IO.File.OpenText(path);
                dataStr = file.ReadToEnd();
                file.Close();
            }
            return dataStr;
        }

        // 保存
        public void Save(GameData gamedata)
        {
            string content = JsonUtility.ToJson(gamedata, true);

            if (!System.IO.File.Exists(FullDataPath))
            {
                // 创建后要主动释放：Dispose
                System.IO.File.CreateText(FullDataPath).Dispose();
            }

            System.IO.File.WriteAllText(FullDataPath, content, System.Text.Encoding.Unicode);
        }


        // 清除存档
        public void DelData()
        {
            if (System.IO.File.Exists(FullDataPath))
            {
                System.IO.File.Delete(FullDataPath);
            }
        }
    }
}
