using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FileElements.Data;

namespace GameplayElements.Data.Entities.PathFinding
{
    static class PathFinder
    {
        // Enumeration for readability; it could easily be 0s and 1s
        private enum NodeStatus { Open, Closed };

        // Dictionary to hold node statuses.
        // Dictionary because it'll be easier to look up
        private static Dictionary<Vector2, NodeStatus> nodeStatus = new Dictionary<Vector2, NodeStatus>();

        // This is for my use, to calculate which path is shortest
        private const int CostStraight = 10;
        private const int CostDiagonal = 15;

        private static List<PathNode> openList = new List<PathNode>();

        // Dictionary for holding the costs of each node
        private static Dictionary<Vector2, float> nodeCosts = new Dictionary<Vector2, float>();

        /// <summary>
        /// Marks a node as open
        /// </summary>
        /// <param name="node"></param>
        static private void AddNodeToOpenList(PathNode node)
        {
            int index = 0;
            float cost = node.TotalCost;

            // Keeps incrementing index until it can be inserted at the top
            while ((openList.Count > index) && (cost < openList[index].TotalCost))
            {
                index++;
            }

            openList.Insert(index, node);
            nodeCosts[node.GridLocation] = node.TotalCost;
            nodeStatus[node.GridLocation] = NodeStatus.Open;
        }

        /// <summary>
        /// Finds the path
        /// </summary>
        /// <param name="startTile"></param>
        /// <param name="endTile"></param>
        /// <returns>The list of points that lead to the destination</returns>
        public static List<Vector2> FindPath(Vector2 startTile, Vector2 endTile)
        {
            // If any of the locations are walls, cancel the search
            //if (MazeComponents.Maze.IsWallTile(startTile) ||
            //    MazeComponents.Maze.IsWallTile(endTile))
            //{
            //    return null;
            //}

            openList.Clear();
            nodeCosts.Clear();
            nodeStatus.Clear();

            PathNode startNode;
            PathNode endNode;

            endNode = new PathNode(null, null, endTile, 0);
            startNode = new PathNode(null, endNode, startTile, 0);

            AddNodeToOpenList(startNode);

            while (openList.Count > 0)
            {
                PathNode currentNode = openList[openList.Count - 1];

                if (currentNode.IsEqualToNode(endNode))
                {
                    List<Vector2> bestPath = new List<Vector2>();
                    while (currentNode != null)
                    {
                        bestPath.Insert(0, currentNode.GridLocation);
                        currentNode = currentNode.ParentNode;
                    }
                    return bestPath;
                }

                openList.Remove(currentNode);
                nodeCosts.Remove(currentNode.GridLocation);

                foreach (PathNode possibleNode in FindAdjacentNodes(currentNode, endNode))
                {
                    if (nodeStatus.ContainsKey(possibleNode.GridLocation))
                    {
                        if (nodeStatus[possibleNode.GridLocation] == NodeStatus.Closed)
                        {
                            continue;
                        }

                        if (nodeStatus[possibleNode.GridLocation] == NodeStatus.Open)
                        {
                            if (possibleNode.TotalCost >= nodeCosts[possibleNode.GridLocation])
                            {
                                continue;
                            }
                        }
                    }

                    AddNodeToOpenList(possibleNode);
                }

                nodeStatus[currentNode.GridLocation] = NodeStatus.Closed;
            }
            return null;
        }

        // Gets adjacent nodes including diagonals
        // I don't know how to explain this really...
        // Just test each node to get the cost and choose the best node (cheapest cost)
        static private List<PathNode> FindAdjacentNodes(PathNode currentNode, PathNode endNode)
        {
            List<PathNode> adjcacentNodes = new List<PathNode>();

            int X = currentNode.GridX;
            int Y = currentNode.GridY;

            bool upLeft = true;
            bool upRight = true;
            bool downLeft = true;
            bool downRight = true;

            Vector2 N = new Vector2(X, Y - 1);
            Vector2 S = new Vector2(X, Y + 1);
            Vector2 E = new Vector2(X + 1, Y);
            Vector2 W = new Vector2(X - 1, Y);
            Vector2 NW = new Vector2(X - 1, Y - 1);
            Vector2 NE = new Vector2(X + 1, Y - 1);
            Vector2 SE = new Vector2(X + 1, Y + 1);
            Vector2 SW = new Vector2(X - 1, Y + 1);

            if ((X > 0) && X < ProjectData.GameWidth)
            {
                adjcacentNodes.Add(new PathNode(currentNode, endNode, W, CostStraight + currentNode.DirectCost));
            }
            else
            {
                upLeft = false;
                downLeft = false;
            }

            //if ((X < Maze.MAZEWIDTH) && (!Maze.IsWallTile(E)))
            //{
            //    adjcacentNodes.Add(new PathNode(currentNode, endNode, E, CostStraight + currentNode.DirectCost));
            //}
            //else
            //{
            //    upRight = false;
            //    downRight = false;
            //}

            //if ((Y > 0) && (!Maze.IsWallTile(N)))
            //{
            //    adjcacentNodes.Add(new PathNode(currentNode, endNode, N, CostStraight + currentNode.DirectCost));
            //}
            //else
            //{
            //    upLeft = false;
            //    upRight = false;
            //}

            //if ((Y < Maze.MAZEHEIGHT) && (!Maze.IsWallTile(S)))
            //{
            //    adjcacentNodes.Add(new PathNode(currentNode, endNode, S, CostStraight + currentNode.DirectCost));
            //}
            //else
            //{
            //    downLeft = false;
            //    downRight = false;
            //}

            //if ((upLeft) && (!Maze.IsWallTile(NW)))
            //{
            //    adjcacentNodes.Add(new PathNode(currentNode, endNode, NW, CostDiagonal + currentNode.DirectCost));
            //}

            //if ((upRight) && (!Maze.IsWallTile(NE)))
            //{
            //    adjcacentNodes.Add(new PathNode(currentNode, endNode, NE, CostDiagonal + currentNode.DirectCost));
            //}

            //if ((downLeft) && (!Maze.IsWallTile(SW)))
            //{
            //    adjcacentNodes.Add(new PathNode(currentNode, endNode, SW, CostDiagonal + currentNode.DirectCost));
            //}

            //if ((downRight) && (!Maze.IsWallTile(SE)))
            //{
            //    adjcacentNodes.Add(new PathNode(currentNode, endNode, SE, CostDiagonal + currentNode.DirectCost));
            //}

            return adjcacentNodes;
        }
    }
}

