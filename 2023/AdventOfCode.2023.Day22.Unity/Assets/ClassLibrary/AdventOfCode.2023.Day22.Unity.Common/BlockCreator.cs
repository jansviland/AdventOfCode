using System;
using System.IO;
using UnityEngine;

namespace AdventOfCode._2023.Day22.Unity.Common
{
    public class BlockCreator : MonoBehaviour
    {
        public GameObject blockPrefab;

        private int currentLetter = 65; // A = 65 in ASCII, B = 66, etc.
        private const string filePath = "Assets/input.txt";

        void Start()
        {
            CreateBlocksFromFile();
        }

        void CreateBlocksFromFile()
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                CreateBlock(line);
            }
        }

        void CreateBlock(string line)
        {
            Debug.Log(line);

            // Parse line and create blocks
            string[] points = line.Split('~');
            string[] startPoint = points[0].Split(',');
            string[] endPoint = points[1].Split(',');

            // Calculate mid point of the block
            // change z and y, because the input uses z as up, but unity uses y as up
            Vector3 start = new Vector3(int.Parse(startPoint[0]), int.Parse(startPoint[2]), int.Parse(startPoint[1]));
            Vector3 end = new Vector3(int.Parse(endPoint[0]), int.Parse(endPoint[2]), int.Parse(endPoint[1]));
            Vector3 midPoint = (start + end) / 2;

            // Create block
            GameObject block = Instantiate(blockPrefab, midPoint, Quaternion.identity);
            block.name = "Block " + (char)currentLetter;
            currentLetter++;

            // Calculate scale of the block
            Vector3 scale = new Vector3(
                Mathf.Max(1, Mathf.Abs(end.x - start.x)),
                Mathf.Max(1, Mathf.Abs(end.y - start.y)),
                Mathf.Max(1, Mathf.Abs(end.z - start.z))
            );

            block.transform.localScale = scale;
        }
    }
}