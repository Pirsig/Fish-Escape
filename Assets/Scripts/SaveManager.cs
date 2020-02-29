﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveManager : MonoBehaviour
{
    public static void SaveHighScores(HighScore[] highScores)
    {
        Debug.Log("Saving...");

        //create a file if it doesn't exist or open save file if it exists
        FileStream file = new FileStream(Application.persistentDataPath + "/highscores.dat", FileMode.OpenOrCreate);
        //attempt to serialize the data to our save file
        try
        {
            //binary formatter for writing data to save file
            BinaryFormatter formatter = new BinaryFormatter();

            //serialization method to write to file
            formatter.Serialize(file, highScores);
        }
        catch(SerializationException exception)
        {
            Debug.LogError("There was an error attempting to serialize high score data in saving. " + exception);
        }
        finally
        {
            //Close the file so it's not open forever
            file.Close();
        }
        Debug.Log("Saving finished.");
    }

    public static HighScore[] LoadHighScores()
    {
        Debug.Log("Loading...");

        HighScore[] loadedHighScores;
        //open the file to load from
        FileStream file = new FileStream(Application.persistentDataPath + "/highscores.dat", FileMode.Open);

        try
        {
            //binary formatter for writing data to save file
            BinaryFormatter formatter = new BinaryFormatter();

            //Deserialization method
            loadedHighScores = (HighScore[])formatter.Deserialize(file);
        }
        catch(SerializationException exception)
        {
            Debug.LogError("There was an error attempting to serialize high score data in loading. " + exception);
            loadedHighScores = null;
        }
        finally
        {
            file.Close();
        }

        Debug.Log("Loading finished.");
        return loadedHighScores;
    }
}