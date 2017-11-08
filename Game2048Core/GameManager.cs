using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Core
{
    public enum Direction : int { Up = 1, Down, Left, Right}

    public class GameManager
    {
        //public Tile[,] AllTiles = new Tile[4, 4];
        public Tile[] AllTiles = new Tile[4 * 4]; //one dimension array is better solution than two dimension regarding to the performance
        List<Tile> EmptyTiles = new List<Tile>();
        
        public int Score { get; private set; }
        
        public int FreeTilesCount
        {
            get
            {
                return  EmptyTiles.Count();
            }
        }
        
        public bool IsGameover
        {
            get
            {
                return AvailableMovements().Count() == 0;
            }

        }

        public GameManager()
        {
            Tile t;
            for (int i = 0; i < AllTiles.Length; i++)
            {
                t = new Tile(0);
                AllTiles[i] = t;
                EmptyTiles.Add(t);
            }

            Score = 0;

            //generate 2 tiles at the start of the game
            GenerateTile();
            GenerateTile();
        }

        //handle 1 dimension array as 2 dimensions array
        Tile GetTile(int row, int column) 
        {
            int index = row * 4 + column;
            return AllTiles[index];
        }

        //generate new tile
        void GenerateTile()
        {
            if (EmptyTiles.Count <= 0) return ;//game over
            Random r = new Random();
            int newTileValue = r.Next(0, 10) == 10 ? 4 : 2;
            Tile _tile = EmptyTiles[r.Next(0, EmptyTiles.Count - 1)];
            _tile.Value = (short)newTileValue;
            EmptyTiles.Remove(_tile);
        }

        void ResetMergedTiles() 
        {
            for (int i = 0; i < AllTiles.Length; i++)
            {
                AllTiles[i].IsMerged = false;
            }
        }

        public void Move(Direction direction)
        {
            bool IsMovement = false;
            
            switch (direction)
            {
                case Direction.Up:
                    while (MoveUp())
                    {
                        IsMovement = true;
                    }
                    break;
                case Direction.Down:
                    while (MoveDown())
                    {
                        IsMovement = true;
                    }
                    break;
                case Direction.Right:
                    while (MoveRight())
                    {
                        IsMovement = true;
                    }
                    break;
                case Direction.Left:
                    while (MoveLeft())
                    {
                        IsMovement = true;
                    }
                    break;
            }

            if (IsMovement)
            {
                GenerateTile();
                ResetMergedTiles();
            }

        }

        private bool MoveUp()
        {
            Tile t1, t2;

            //handling movement
            for (int j = 0; j < 4; j++)//rows
            {
                for (int i = 0; i < 3; i++)//columns
                {
                    t1 = GetTile(i, j);
                    t2 = GetTile(i + 1, j);

                    if (t1.Value == 0 && t2.Value != 0)
                    {
                        t1.Value = t2.Value;
                        t2.Value = 0;
                        EmptyTiles.Remove(t1);
                        EmptyTiles.Add(t2);
                        return true;
                    }
                }
            }

            //handling merge
            for (int j = 0; j < 4; j++)//rows
            {
                for (int i = 0; i < 3; i++)//columns
                {
                    t1 = GetTile(i, j);
                    t2 = GetTile(i + 1, j);

                    if (t1.Value != 0 && t1.Value == t2.Value && t1.IsMerged == false && t2.IsMerged == false)
                    {
                        t1.Value *= 2;
                        t2.Value = 0;
                        Score += t1.Value;
                        EmptyTiles.Add(t2);
                        t1.IsMerged = true;
                        return true;
                    }
                }
            }

            return false;

        }

        private bool MoveLeft()
        {
            Tile t1, t2;

            //handling movement
            for (int i = 0; i < 4; i++)//rows
            {
                for (int j = 0; j < 3; j++)//columns
                {
                    t1 = GetTile(i, j);
                    t2 = GetTile(i, j + 1);

                    if (t1.Value == 0 && t2.Value != 0)
                    {
                        t1.Value = t2.Value;
                        t2.Value = 0;
                        EmptyTiles.Remove(t1);
                        EmptyTiles.Add(t2);
                        return true;
                    }
                }
            }

            //handling merge
            for (int i = 0; i < 4; i++)//rows
            {
                for (int j = 0; j < 3; j++)//columns
                {
                    t1 = GetTile(i, j);
                    t2 = GetTile(i, j + 1);

                    if (t1.Value != 0 && t1.Value == t2.Value && t1.IsMerged == false && t2.IsMerged == false)
                    {
                        t1.Value *= 2;
                        t2.Value = 0;
                        Score += t1.Value;
                        EmptyTiles.Add(t2);
                        t1.IsMerged = true;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool MoveDown()
        {
            Tile t1, t2;

            //handling movement
            for (int j = 3; j >= 0 ; j--)//rows
            {
                for (int i = 3; i > 0; i--)//columns
                {
                    t1 = GetTile(i, j);
                    t2 = GetTile(i - 1, j);

                    if (t1.Value == 0 && t2.Value != 0)
                    {
                        t1.Value = t2.Value;
                        t2.Value = 0;
                        EmptyTiles.Remove(t1);
                        EmptyTiles.Add(t2);
                        return true;
                    }
                }
            }

            //handling merge
            for (int j = 3; j >= 0; j--)//rows
            {
                for (int i = 3; i > 0; i--)//columns
                {
                    t1 = GetTile(i, j);
                    t2 = GetTile(i - 1, j);

                    if (t1.Value != 0 && t1.Value == t2.Value
                            && t1.IsMerged == false && t2.IsMerged == false)
                    {
                        t1.Value *= 2;
                        t2.Value = 0;
                        Score += t1.Value;
                        EmptyTiles.Add(t2);
                        t1.IsMerged = true;
                        return true;
                    }
                }
            }

            return false;

        }

        private bool MoveRight()
        {
            Tile t1, t2;

            //handling movement
            for (int i = 3; i >= 0; i--)//rows
            {
                for (int j = 3; j > 0; j--)//columns
                {
                    t1 = GetTile(i, j);
                    t2 = GetTile(i, j - 1);

                    if (t1.Value == 0 && t2.Value != 0)
                    {
                        t1.Value = t2.Value;
                        t2.Value = 0;
                        EmptyTiles.Remove(t1);
                        EmptyTiles.Add(t2);
                        return true;
                    }
                }
            }

            //handling merge
            for (int i = 3; i >= 0; i--)//rows
            {
                for (int j = 3; j > 0; j--)//columns
                {
                    t1 = GetTile(i, j);
                    t2 = GetTile(i, j - 1);

                    if (t1.Value != 0 && t1.Value == t2.Value
                            && t1.IsMerged == false && t2.IsMerged == false)
                    {
                        t1.Value *= 2;
                        t2.Value = 0;
                        Score += t1.Value;
                        EmptyTiles.Add(t2);
                        t1.IsMerged = true;
                        return true;
                    }
                }
            }

            return false;
        }

        public List<Direction> AvailableMovements() 
        {
            List<Direction> availableMovements = new List<Direction>();
            
            if (CanMoveUp())
                availableMovements.Add(Direction.Up);
            
            if (CanMoveDown())
                availableMovements.Add(Direction.Down);

            if (CanMoveRight())
                availableMovements.Add(Direction.Right);
            
            if (CanMoveLeft())
                availableMovements.Add(Direction.Left);

            return availableMovements;
        }

        public bool CanMoveUp()
        {
            int t1, t2;
            for (int j = 0; j < 4; j++)//rows
                for (int i = 0; i < 3; i++)//columns
                {
                    t1 = GetTile(i, j).Value;
                    t2 = GetTile(i + 1, j).Value;
                    if ((t1 == 0 && t2!= 0) || (t1 == t2) && t1 != 0)
                        return true;
                }
            return false;
        }

        public bool CanMoveDown()
        {
            int t1, t2;
            for (int j = 3; j >= 0; j--)//rows
                for (int i = 3; i > 0; i--)//columns
                {
                    t1 = GetTile(i, j).Value;
                    t2 = GetTile(i - 1, j).Value;
                    if ((t1 == 0 && t2 != 0) || (t1 == t2) && t1 != 0)
                        return true;
                }
            return false;
        }

        public bool CanMoveRight()
        {
            int t1, t2;
            for (int i = 3; i >= 0; i--)//rows
                for (int j = 3; j > 0; j--)//columns
                {
                    t1 = GetTile(i, j).Value;
                    t2 = GetTile(i, j - 1).Value;
                    if ((t1 == 0 && t2 != 0) || t1 != 0 &&( t1 == t2))
                        return true;
                }
            return false;
        }

        public bool CanMoveLeft()
        {
            int t1, t2;
            for (int i = 0; i < 4; i++)//rows
                for (int j = 0; j < 3; j++)//columns
                {
                    t1 = GetTile(i, j).Value;
                    t2 = GetTile(i, j + 1).Value;
                    if ((t1 == 0 && t2 != 0) || (t1!= 0 && t1 == t2))
                        return true;
                }
             return false;
        }

        public GameManager CopyGame()
        {
            GameManager newGame = new GameManager();
            newGame.EmptyTiles.Clear();

            for (int i = 0; i < newGame.AllTiles.Length; i++ )
            {
                short value = this.AllTiles[i].Value;
                newGame.AllTiles[i].Value = value;
                if (value == 0)
                {
                    newGame.EmptyTiles.Add(newGame.AllTiles[i]);
                }
            }
            newGame.Score = this.Score;
            return newGame;
        }

    }
}
