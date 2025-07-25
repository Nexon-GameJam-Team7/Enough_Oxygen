﻿// System
using System.IO;

// Unity
using UnityEngine;

public class DataManager
{
    // --- 게임 데이터 파일이름 설정 ("원하는 이름.json) --- //
    string gameDataFileName = "GameData.json";

    // --- 저장용 클래스 변수 --- //
    public Data data = new Data();

    // --- 데이터 로드 --- //
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + gameDataFileName;

        // 저장된 게임이 이미 있을 경우
        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(FromJsonData);
        }
    }

    // --- 데이터 저장 --- //
    public void SaveGameData()
    {
        // 클래스 -> Json (true = 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + gameDataFileName;

        //이미 저장된 파일 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);
    }

}