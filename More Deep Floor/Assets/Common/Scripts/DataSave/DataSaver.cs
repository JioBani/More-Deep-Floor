using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LNK.MoreDeepFloor.Common.DataSave
{
    [Serializable]
    public class Data
    {
        
    }
    
    public class DataSaver
    {

        public static bool SaveData<T>(T data, string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            
            try
            {
                if (!data.GetType().IsSerializable)
                {
                    Debug.LogError("[DataSaver.SaveData()] 직렬화 가능한 객체가 아닙니다.");
                    return false;
                }
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream,data);
                fileStream.Close();
                Debug.Log($"[DataSaver.SaveData()] 저장 완료 : {path}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[DataSaver.SaveData()] 파일을 저장하는데 실패했습니다. {path}");
                Debug.LogError(e.Message);
                fileStream.Close();
                return false;
            }
        }

        public static bool LoadData<T>(string path , out T data) where T : class
        {
            try
            {
                if (!File.Exists(path))
                {
                    Debug.LogError($"[DataSaver.LoadData()] 파일이 존재하지 않습니다. : {path}");
                    data = null;
                    return false;
                }
                else
                {
                    FileStream fileStream = new FileStream(path, FileMode.Open);
                    BinaryFormatter formatter = new BinaryFormatter();
                    data = formatter.Deserialize(fileStream) as T;
                    fileStream.Close();
                    if (data == null) return false;
                    else return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[DataSaver.LoadData()] 파일을 불러오는데 실패했습니다. {path}");
                Debug.LogError(e.Message);
                data = null;
                return false;
            }
        }

        public static bool CheckData(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


