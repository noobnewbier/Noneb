using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using Tiles;
using UnityEngine;

namespace Maps
{
    //https://en.wikipedia.org/wiki/A*_search_algorithm
    public class Pathfinder
    {
        public bool TryFindPath(Tile start, Tile goal, Map map, out IList<Tile> path)
        {
            var tileToDiscover = new SimplePriorityQueue<Tile, float>();
            var cameFrom = new Dictionary<Tile, Tile>();
            var distanceToTile = new Dictionary<Tile, float> {[start] = 0f};

            tileToDiscover.Enqueue(start, Heuristic(start, goal));
            path = null;

            while (tileToDiscover.Any())
            {
                var current = tileToDiscover.Dequeue();
                if (current == goal)
                {
                    path = new List<Tile>();
                    while (current != start)
                    {
                        path.Add(current);
                        current = cameFrom[current];
                    }

                    path = path.Reverse().ToList();
                    return true;
                }

                foreach (var neighbour in map.GetNeighbours(current.Coordinate).Values)
                {
                    var currentDistanceToNeighbour = distanceToTile[current] + neighbour.TileData.Weight;
                    if (!distanceToTile.TryGetValue(neighbour, out var previousDistanceToNeighbour))
                    {
                        previousDistanceToNeighbour = float.PositiveInfinity;
                    }

                    if (currentDistanceToNeighbour > previousDistanceToNeighbour)
                    {
                        continue;
                    }

                    var newScoreForNeighbour = currentDistanceToNeighbour + Heuristic(neighbour, goal);
                    cameFrom[neighbour] = current;
                    distanceToTile[neighbour] = currentDistanceToNeighbour;
                    if (!tileToDiscover.TryUpdatePriority(neighbour, newScoreForNeighbour))
                    {
                        tileToDiscover.Enqueue(neighbour, newScoreForNeighbour);
                    }
                }
            }

            return false;
        }

        private float Heuristic(Tile tile, Tile goal)
        {
            var tileCoordinate = tile.Coordinate;
            var goalCoordinate = goal.Coordinate;

            return
                Mathf.Abs(goalCoordinate.X - tileCoordinate.X) +
                Mathf.Abs(goalCoordinate.Y - tileCoordinate.Y) +
                Mathf.Abs(goalCoordinate.Z - tileCoordinate.Z);
        }
    }
}