using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Helper
{
    
    public static byte[] EncodeToWAV(AudioClip audioClip)
    {
        // Chuyển đổi âm thanh thành mảng float
        float[] audioData = new float[audioClip.samples * audioClip.channels];
        audioClip.GetData(audioData, 0);

        // Chuyển đổi mảng float thành mảng byte
        byte[] audioBytes = new byte[audioData.Length * 2];
        for (int i = 0; i < audioData.Length; i++)
        {
            short sampleValue = (short)(audioData[i] * 32767);
            byte[] sampleBytes = BitConverter.GetBytes(sampleValue);
            audioBytes[i * 2] = sampleBytes[0];
            audioBytes[i * 2 + 1] = sampleBytes[1];
        }

        // Tạo header của file WAV
        byte[] header = CreateWAVHeader(audioClip);

        // Kết hợp header và dữ liệu âm thanh
        byte[] wavData = new byte[header.Length + audioBytes.Length];
        header.CopyTo(wavData, 0);
        audioBytes.CopyTo(wavData, header.Length);

        return wavData;
    }

    public static byte[] CreateWAVHeader(AudioClip audioClip)
    {
        int channelCount = audioClip.channels;
        int sampleRate = audioClip.frequency;
        int sampleCount = audioClip.samples;

        int byteRate = sampleRate * channelCount * 2;
        short blockAlign = (short)(channelCount * 2);
        short bitsPerSample = 16;

        int dataSize = sampleCount * channelCount * 2;

        byte[] header = new byte[44];
        header[0] = (byte)'R';
        header[1] = (byte)'I';
        header[2] = (byte)'F';
        header[3] = (byte)'F';

        BitConverter.GetBytes(dataSize + 36).CopyTo(header, 4);

        header[8] = (byte)'W';
        header[9] = (byte)'A';
        header[10] = (byte)'V';
        header[11] = (byte)'E';
        header[12] = (byte)'f';
        header[13] = (byte)'m';
        header[14] = (byte)'t';
        header[15] = (byte)' ';

        BitConverter.GetBytes(16).CopyTo(header, 16); // Chunk size (16 for PCM)
        BitConverter.GetBytes((short)1).CopyTo(header, 20); // Audio format (1 for PCM)
        BitConverter.GetBytes((short)channelCount).CopyTo(header, 22); // Number of channels
        BitConverter.GetBytes(sampleRate).CopyTo(header, 24); // Sample rate
        BitConverter.GetBytes(byteRate).CopyTo(header, 28); // Byte rate
        BitConverter.GetBytes(blockAlign).CopyTo(header, 32); // Block align
        BitConverter.GetBytes(bitsPerSample).CopyTo(header, 34); // Bits per sample

        header[36] = (byte)'d';
        header[37] = (byte)'a';
        header[38] = (byte)'t';
        header[39] = (byte)'a';

        BitConverter.GetBytes(dataSize).CopyTo(header, 40); // Data size

        return header;
    }
    public static string ConvertToMinuteSecond(int seconds)
    {
        string minu_sec = "";
        int minutes = seconds / 60;
        seconds = seconds - minutes * 60;
        if (minutes < 10) minu_sec = "0" + minutes;
        else minu_sec = minutes + "";

        minu_sec += ": ";

        if (seconds < 10) minu_sec += ("0" + seconds);
        else minu_sec += (seconds + "");
        return minu_sec;
    }

    public static string GetRecordDay()
    {
        DateTime now = DateTime.Now;
        return now.Day + "/" + now.Month + "/" + now.Year;
    }
}
