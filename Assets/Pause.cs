using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private movement player; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                pause();
            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SaveByXML()
    {
        Save save = createSaveGameObject();
        XmlDocument xmlDocument = new XmlDocument();
        #region CreateXML elements

        XmlElement root = xmlDocument.CreateElement("Save");
        XmlElement healthNumber = xmlDocument.CreateElement("healthNum");
        healthNumber.InnerText = save.healthNum.ToString();
        root.AppendChild(healthNumber);

        XmlElement playerPosXElement = xmlDocument.CreateElement("PlayerPositionX");
        playerPosXElement.InnerText = save.playerPositionX.ToString();
        root.AppendChild(playerPosXElement);

        XmlElement playerPosYElement = xmlDocument.CreateElement("PlayerPositionY");
        playerPosYElement.InnerText = save.playerPositionY.ToString();
        root.AppendChild(playerPosYElement);


        #endregion
        xmlDocument.AppendChild(root);
        xmlDocument.Save(Application.dataPath + "/DataXML.text");
        if (File.Exists(Application.dataPath + "/DataXML.text"))
        {
            Debug.Log("XML file saved");
        }
    }

    private void LoadByXML()
    {
        if (File.Exists(Application.dataPath + "/DataXML.text"))
        {
            Save save = new Save();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/DataXML.text");

            XmlNodeList healthNum = xmlDocument.GetElementsByTagName("healthNum");

            int healthNumCount = int.Parse(healthNum[0].InnerText);
            save.healthNum = healthNumCount;

            XmlNodeList playerPosX = xmlDocument.GetElementsByTagName("PlayerPositionX");
            float playerPosXNum = float.Parse(playerPosX[0].InnerText);
            save.playerPositionX = playerPosXNum;

            XmlNodeList playerPosY = xmlDocument.GetElementsByTagName("PlayerPositionY");
            float playerPosYNum = float.Parse(playerPosY[0].InnerText);
            save.playerPositionY = playerPosYNum;

            GameManager.instance.health = save.healthNum;
            player.transform.position = new Vector3(save.playerPositionX, save.playerPositionY);
        }
        else
        {
            Debug.Log("No file can be found");
        }
    
    }

    public void SaveButton()
    {
        SaveByXML();
    }
    public void LoadButton()
    {
        LoadByXML();
        Resume();
    }

    private Save createSaveGameObject()
    {
        Save save = new Save();
        save.healthNum = GameManager.instance.health;
        save.playerPositionX = player.transform.position.x;
        save.playerPositionY = player.transform.position.y;

        return save;
    }
}
