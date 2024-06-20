
using System.Collections.Generic;
using UnityEngine;


namespace Source.Scripts
{
    public class BrickFactory
    {
        public List<Brick> GenerateBrickList(params KeyValuePair<Brick, int>[] bricks)
        {
            List<Brick> brickList = new List<Brick>();
            foreach (var brickPair in bricks)
            {
                for (int i = 0; i < brickPair.Value; i++)
                {
                    brickList.Add(brickPair.Key);
                }
            }

            return brickList;
        }

        public List<Brick> ShuffleBricks(List<Brick> bricks)
        {
            List<Brick> shuffledBricks = new List<Brick>();

            while (bricks.Count > 0)
            {
                int index = Random.Range(0, bricks.Count);
                shuffledBricks.Add(bricks[index]);
                bricks.RemoveAt(index);
            }

            return shuffledBricks;
        }
    }
}