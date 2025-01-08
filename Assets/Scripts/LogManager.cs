using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public static class LogManager
{
    private static string logFilePath;
    private static string csvFilePath;

    static LogManager()
    {
        // Define the file paths for the log and CSV files
        logFilePath = Path.Combine(Application.persistentDataPath, "GameLogs.txt");
        csvFilePath = Path.Combine(Application.persistentDataPath, "GameLogs.csv");

        // Optionally, clear the log and CSV files when the game starts
        if (File.Exists(logFilePath))
        {
            File.WriteAllText(logFilePath, ""); // Clear previous logs
        }
        if (File.Exists(csvFilePath))
        {
            File.WriteAllText(csvFilePath, ""); // Clear previous CSV data
        }

        // Optionally, add headers to the CSV if it's the first time it's being created
        if (!File.Exists(csvFilePath))
        {
            try
            {
                File.AppendAllText(csvFilePath, "Timestamp,EventType,Message\n");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write CSV header: {e.Message}");
            }
        }

        Debug.Log($"Log file initialized at: {logFilePath}");
        Debug.Log($"CSV file initialized at: {csvFilePath}");
    }

    public static void LogEvent(string eventType, string message)
    {
        // Filter out messages containing "Hit unknown object: Untagged"
        if (message.Contains("Hit unknown object: Untagged"))
        {
            return; // Skip logging this message
        }

        // Create the log entry with timestamp including seconds
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff},{eventType},{message}";

        // Write the log entry to the .txt file
        try
        {
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write log: {e.Message}");
        }

        // Write the log entry to the .csv file
        try
        {
            File.AppendAllText(csvFilePath, logEntry + Environment.NewLine);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write CSV: {e.Message}");
        }

        // Also log the event to the console for debugging
        Debug.Log(logEntry);
    }
}
