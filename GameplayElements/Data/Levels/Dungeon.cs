using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using ProjectElements.Data;
using GameplayElements.Managers;
using GameplayElements.Data.Entities.Monsters;

namespace GameplayElements.Data
{

    class Dungeon : Level
    {
        public List<Texture2D> tileTextures = new List<Texture2D>();

        private SpriteSheet tileSheet;
        private Goal goal;

        Random rand = new Random();

        public List<Room> rooms = new List<Room>();
        public Goal Goal
        {
            get { return goal; }
        }

        public Dungeon(int widthInTiles, int heightInTiles, int tw, int th) :
            base(widthInTiles, heightInTiles, tw, th)
        {
            this.width = widthInTiles * tw;
            this.height = heightInTiles * th;
            this.tileWidth = tw;
            this.tileHeight = th;

            wallTile = new bool[widthInTiles, heightInTiles];
            mapArr = new Tile[widthInTiles, heightInTiles];

            InitTileSheet();

            GenerateDungeon();
        }

        private void InitTileSheet()
        {

            tileSheet = new SpriteSheet(ProjectData.Content.Load<Texture2D>("Levels\\TileSheets\\fantasy-tileset"),
                32, 32, ProjectData.Graphics.GraphicsDevice);

            int counter = 0;
            int y = 0;
            while (y < tileSheet.Height / tileSheet.SpriteHeight)
            {
                for (int x = 0; x < tileSheet.Width / tileSheet.SpriteWidth; x++)
                {
                    tileTextures.Add(tileSheet.GetSubImage(x, y));
                    counter++;
                }
                y++;
            }
        }

        public void GenerateDungeon()
        {
            for (int x = 0; x < WidthInTiles; x++)
                for (int y = 0; y < HeightInTiles; y++)
                {
                    mapArr[x, y] = new Tile(32, 32, null)
                    {
                        IsWallTile = true,
                        TrueLocation = new Vector2(x * tileWidth, y * tileHeight),
                        Texture = tileTextures[9]
                    };
                }

            int roomsMin = (int)(WidthInTiles * HeightInTiles) / 300;
            int roomsMax = (int)(WidthInTiles * HeightInTiles) / 150;
            int roomCount = 30;

            int widthRoot = (int)Math.Sqrt(width * 2);
            int heightRoot = (int)Math.Sqrt(height * 2);

            int minimumWidth = (int)4 * tileWidth;
            int maximumWidth = (int)8 * tileWidth;
            int minimumHeight = (int)3 * tileHeight;
            int maximumHeight = (int)10 * tileHeight;

            do
            {
                bool ok = false;
                Room room = new Room();

                room.X = (int)Math.Round(rand.Next(0, width) / (double)tileWidth) * tileWidth;
                room.Y = (int)Math.Round(rand.Next(0, height) / (double)tileHeight) * tileHeight;
                room.Width = (int)Math.Round(rand.Next(minimumWidth, maximumWidth) / (double)tileWidth) * tileWidth;
                room.Height = (int)Math.Round(rand.Next(minimumHeight, maximumHeight) / (double)tileHeight) * tileHeight;

                if (room.X < 0 || room.X > width - room.Width ||
                    room.Y < 0 || room.Y > height - room.Height)
                    continue;

                ok = true;

                if (rooms.Count > 0)
                {
                    foreach (Room r in rooms)
                        if (r.Bounds.Intersects(room.Bounds))
                            ok = false;
                }

                if (ok)
                    rooms.Add(room);

            } while (rooms.Count < roomCount);

            rooms.Add(new Room()
            {
                X = 0,
                Y = 0,
                Width = 10 * tileWidth,
                Height = 10 * tileHeight
            });

            List<Room> usableRooms = rooms;
            List<Cell> connectedTiles = new List<Cell>();
            int connections = roomCount;
            int index = 0;

            for (int i = 0; i < connections - 1; i++)
            {

                Room room = rooms[index];
                usableRooms.Remove(room);

                Room connectToRoom = usableRooms[rand.Next(usableRooms.Count)];

                double sideStepChance = 0.4;

                Vector2 pointA = new Vector2(rand.Next(room.Bounds.X, room.Bounds.X + room.WidthInTiles),
                    rand.Next(room.Bounds.Y, room.Bounds.Y + room.HeightInTiles));
                Vector2 pointB = new Vector2(rand.Next(connectToRoom.Bounds.X, connectToRoom.Bounds.X + connectToRoom.WidthInTiles),
                    rand.Next(connectToRoom.Bounds.Y, connectToRoom.Bounds.Y + connectToRoom.HeightInTiles));

                while (pointB != pointA)
                {
                    if (rand.NextDouble() < sideStepChance)
                    {
                        if (pointB.X != pointA.X)
                        {
                            if (pointB.X < pointA.X)
                                pointB.X++;
                            else
                                pointB.X--;
                        }
                    }
                    else if (pointB.Y != pointA.Y)
                    {
                        if (pointB.Y < pointA.Y)
                            pointB.Y++;
                        else
                            pointB.Y--;
                    }

                    if (pointB.X < WidthInTiles && pointB.Y < HeightInTiles)
                    {
                        mapArr[(int)pointB.X, (int)pointB.Y].IsWallTile = false;
                        mapArr[(int)pointB.X, (int)pointB.Y].Texture = tileTextures[10];
                    }
                }
            }

            foreach (Room r in rooms)
            {
                for (int x = (int)r.Position.X; x < r.Width + r.Position.X; x++)
                {
                    for (int y = (int)r.Position.Y; y < r.Height + r.Position.Y; y++)
                    {
                        if (x / 32 == r.Position.X / 32 || x / 32 == (int)(r.WidthInTiles + (r.Position.X / 32) - 1) ||
                            y / 32 == r.Position.Y / 32 || y / 32 == (int)(r.HeightInTiles + (r.Position.Y / 32) - 1))
                        {

                        }
                        else
                        {
                            mapArr[(int)(x / 32), (int)y / 32].Texture = tileTextures[10];
                            mapArr[(int)x / 32, (int)y / 32].IsWallTile = false;
                        }
                    }
                }
            }

            do
            {
                var point = new Point(rand.Next(0, WidthInTiles), rand.Next(0, HeightInTiles));

                if (!mapArr[point.X, point.Y].IsWallTile)
                    goal = new Goal(point);

            } while (goal == null);
        }

        public override void Draw(SpriteBatch batch, Rectangle region)
        {
            for (int x = 0; x < WidthInTiles; x++)
            {
                for (int y = 0; y < HeightInTiles; y++)
                {
                    if (Camera.IsOnCamera(new Rectangle(x * 32, y * 32, tileWidth, tileHeight)))
                        batch.Draw(mapArr[x, y].Texture, new Vector2(x * tileWidth,
                            y * tileHeight), Color.White);
                }
            }

            goal.Draw(batch);
        }
    }


    public struct Room
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int WidthInTiles
        {
            get { return Width / 32; }
        }
        public int HeightInTiles
        {
            get { return Height / 32; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(X / 32, Y / 32, WidthInTiles, HeightInTiles); }
        }
        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set { X = (int)value.X; Y = (int)value.Y; }
        }

        public void Draw(SpriteBatch batch, Texture2D texture)
        {
            batch.Draw(texture, new Rectangle((int)Position.X,
                (int)Position.Y, Width, Height), Color.White);
        }
    }

    public struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class MazeCell
    {
        public float x, y;
        public bool Wall = true;
        public bool Visited = false;
        public bool hasOpening = false;

        public Texture2D Texture { get; set; }

        public MazeCell(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Checks if the cell is bordered by an open cell
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="neighbor"></param>
        /// <param name="cells"></param>
        /// <returns></returns>
        public bool HasOpening(Vector2 cell, Vector2 neighbor, MazeCell[,] cells)
        {
            // Test each adjacent block minus the neighbor
            foreach (Vector2 dir in adjacent(cell))
                if (dir != neighbor)
                    if (!cells[(int)dir.X, (int)dir.Y].Wall)
                        return true;

            // If none of the cells are adjacent, the block doesn't border a wall
            return false;
        }

        /// <summary>
        /// Gets all adjacent cells (N, S, E, and W)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public List<Vector2> adjacent(Vector2 cell)
        {
            List<Vector2> adjacents = new List<Vector2>();

            Vector2 N = new Vector2(cell.X, cell.Y - 1);
            Vector2 S = new Vector2(cell.X, cell.Y + 1);
            Vector2 W = new Vector2(cell.X - 1, cell.Y);
            Vector2 E = new Vector2(cell.X + 1, cell.Y);

            adjacents.Add(N);
            adjacents.Add(S);
            adjacents.Add(W);
            adjacents.Add(E);

            adjacents.ForEach(delegate(Vector2 dir)
            {
                if (dir.X <= 0 || dir.Y <= 0 ||
                    dir.X >= LevelManager.GetCurrentLevel().Width / LevelManager.GetCurrentLevel().TileWidth ||
                    dir.Y >= LevelManager.GetCurrentLevel().Height / LevelManager.GetCurrentLevel().TileHeight)
                    adjacents.Remove(dir);
            });

            return adjacents;
        }
    }

    class Goal
    {
        private Texture2D tex;
        private Point pos;
        private bool taken = false;

        public Rectangle Bounds
        {
            get { return new Rectangle(pos.X * 32, pos.Y * 32, 32, 32); }
        }

        public bool Taken
        {
            get { return taken; }
            set { taken = value; }
        }

        public Goal(Point pos)
        {
            this.pos = pos;
            this.tex = ProjectData.Content.Load<Texture2D>("Sprites\\medal");
        }

        public void Draw(SpriteBatch batch)
        {
            if (!taken)
                batch.Draw(tex, Bounds, Color.White);
        }
    }
}