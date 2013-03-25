using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ProjectElements.Data;
using GameplayElements.Managers;

namespace GameplayElements.Data.Entities.PathFinding
{

    /**
     * I'm not sure how well I can explain this so bear
     * with me.
     * */

    // The path node is each cell on the way to the destination
    class PathNode
    {
        // The beginning of the search
        public PathNode ParentNode;
        // The end of the search
        public PathNode EndNode;

        // The grid location of the current node
        private Vector2 gridLocation;

        // The total cost to the destination
        public float TotalCost;
        // The straight line cost to the destination
        public float DirectCost;

        // Gets and sets the grid location...
        // The set is different because the grid location needs to be in bounds of the maze
        public Vector2 GridLocation
        {
            get { return gridLocation; }
            set 
            {
                gridLocation = new Vector2((float)MathHelper.Clamp(value.X, 0f, (float)LevelManager.GetCurrentLevel().widthInTiles),
                    (float)MathHelper.Clamp(value.Y, 0f, (float) LevelManager.GetCurrentLevel().heightInTiles));
            }
        }

        public int GridX { get { return (int)gridLocation.X; } }
        public int GridY { get { return (int)gridLocation.Y; } }

        public PathNode(PathNode parentNode, PathNode endNode, Vector2 gridLocation, float cost)
        {
            ParentNode = parentNode;
            GridLocation = gridLocation;
            EndNode = endNode;
            DirectCost = cost;
            if (!(endNode == null))
                TotalCost = DirectCost + LinearCost();
        }

        public float LinearCost()
        {
            return Vector2.Distance(EndNode.GridLocation, this.GridLocation);
        }

        // Checks if the node is itself
        public bool IsEqualToNode(PathNode node)
        {
            return GridLocation == node.GridLocation;
        }

    }
}
