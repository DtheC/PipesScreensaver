// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public static class PipeDirectionDecider
// {

//   public static Directions NextDirection(PipePosition currentPos)
//   {
//     if (directionsPossible == null) Init();
//     Reset();
//     directionsPossible[currentPos.lastDirection] = 0;
//     return GetRandomDirection();
//   }

//   public static Directions GetRandomDirection()
//   {
//     float possibilityCount = 0;
//     foreach (KeyValuePair<Directions, Direction> entry in directionsPossible)
//     {
//       possibilityCount += entry.Value.possibility;
//     }
//     if (possibilityCount == 0) return Directions.Unavailable;
//     float randomNumber = Random.RandomRange(0f, possibilityCount);
//     float count = 0;
//     foreach (KeyValuePair<Directions, Direction> entry in directionsPossible)
//     {
//       count += entry.Value.possibility;
//       if (count >= randomNumber)
//       {
//         return entry.Value.direction;
//       }
//     }
//     return Directions.Unavailable;
//   }
// }

// public class PipePosition
// {
//   public int x;
//   public int y;
//   public int z;
//   public Directions lastDirection;
//   public PipePosition(int x, int y, int z, Direction lastDir)
//   {
//     this.x = x;
//     this.y = y;
//     this.z = z;
//   }
// }

// public class Direction
// {
//   public Directions direction;
//   public Directions opposite;
//   public Direction(Directions direction, Directions opposite)
//   {
//     this.direction = direction;
//     this.opposite = opposite;
//   }
// }


