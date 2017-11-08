using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048Core
{
    // AI Player class
    public class GamePlayer
    {
        GameManager _game;
        
        public GamePlayer(GameManager game)
        {
            _game = game.CopyGame();
        }

        public Direction NextMovement(int depth)
        {
            return  IterativeDeeping(_game, Direction.Down, _game.Score, depth);
        }

        Dictionary<Direction, GameManager> Successors(GameManager game)
        {
            Dictionary<Direction, GameManager> dictSuccessors = new Dictionary<Direction, GameManager>();

            foreach (Direction direction in game.AvailableMovements())
            {
                GameManager temp = game.CopyGame();
                temp.Move(direction);
                dictSuccessors[direction] = temp;    
            }
            
            return dictSuccessors;
        }

        Direction IterativeDeeping(GameManager game, Direction direction, int score = 0, int depth = 0)
        {
            Dictionary<Direction, GameManager> successors = Successors(game);
            int bestScore = int.MinValue;
            List<Direction> lstBestDirection = new List<Direction>();
           
            foreach (KeyValuePair<Direction, GameManager> item in successors)
            {
                int currentScore = HighScoreWithDepth(item.Value, item.Value.Score, depth);
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    lstBestDirection.Clear();
                    lstBestDirection.Add(item.Key);
                }
                else if (currentScore == bestScore) 
                {
                    lstBestDirection.Add(item.Key);
                }
            }

            switch (lstBestDirection.Count())
            {
                case 0://game over
                    return GetRandomMovement(_game);

                case 1 : //high score movement
                    return lstBestDirection[0];
                
                default://select best scenario 
                    return SelectBestMove(_game, lstBestDirection);
            }
        }

        int HighScoreWithDepth(GameManager game, int score = 0, int depth = 0)
        {
            int bestScore = 0;// int.MinValue;
            if (depth == 0)
            {
                return score;
            }

            Dictionary<Direction, GameManager> successors = Successors(game);
        
            foreach (KeyValuePair<Direction, GameManager> item in successors)
            {
                int currentScore = HighScoreWithDepth(item.Value, item.Value.Score, depth -1);
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                }
            }

            return bestScore;        
        }

        Direction SelectBestMove(GameManager game, List<Direction> lstDirection)
        {
            int topEdgeValue = game.AllTiles[0].Value + game.AllTiles[1].Value + game.AllTiles[2].Value + game.AllTiles[3].Value;
            int bottonEdgeValue = game.AllTiles[12].Value + game.AllTiles[13].Value + game.AllTiles[14].Value + game.AllTiles[15].Value;
            int rightEdgeValue = game.AllTiles[3].Value + game.AllTiles[7].Value + game.AllTiles[11].Value + game.AllTiles[15].Value;
            int leftEdgeValue = game.AllTiles[0].Value + game.AllTiles[4].Value + game.AllTiles[8].Value + game.AllTiles[12].Value;

            List<int> edges = new List<int>();
            edges.Add(topEdgeValue);
            edges.Add(bottonEdgeValue);
            edges.Add(rightEdgeValue);
            edges.Add(leftEdgeValue);
            edges.Sort();
            
            
                // check edges
                if (edges.Max() == topEdgeValue)//top row
                {
                    if (lstDirection.Contains(Direction.Up))
                    {
                        return Direction.Up;
                    }
                    else if (lstDirection.Contains(Direction.Left))
                    {
                        return Direction.Left;
                    }
                    else if (lstDirection.Contains(Direction.Right))
                    {
                        return Direction.Right;
                    }
                    else if (lstDirection.Contains(Direction.Down))
                    {
                        return Direction.Down;
                    }
                }
                else if (edges.Max() == bottonEdgeValue)//bottom row
                {
                    if (lstDirection.Contains(Direction.Down))
                    {
                        return Direction.Down;
                    }
                    else if (lstDirection.Contains(Direction.Right))
                    {
                        return Direction.Right;
                    }
                    else if (lstDirection.Contains(Direction.Left))
                    {
                        return Direction.Left;
                    }
                    else if (lstDirection.Contains(Direction.Up))
                    {
                        return Direction.Up;
                    }
                }
                else if (edges.Max() == leftEdgeValue)
                {
                    if (lstDirection.Contains(Direction.Left))
                    {
                        return Direction.Left;
                    }
                    else if (lstDirection.Contains(Direction.Down))
                    {
                        return Direction.Down;
                    }
                    else if (lstDirection.Contains(Direction.Up))
                    {
                        return Direction.Up;
                    }
                    else if (lstDirection.Contains(Direction.Right))
                    {
                        return Direction.Right;
                    }
                }
                else if (edges.Max() == rightEdgeValue)//right
                {
                    if (lstDirection.Contains(Direction.Right))
                    {
                        return Direction.Right;
                    }
                    else if (lstDirection.Contains(Direction.Down))
                    {
                        return Direction.Down;
                    }
                    else if (lstDirection.Contains(Direction.Up))
                    {
                        return Direction.Up;
                    }
                    else if (lstDirection.Contains(Direction.Left))
                    {
                        return Direction.Left;
                    }
                }
                return lstDirection[0];//default
        }

        Direction GetRandomMovement(GameManager game)
        {
            if (game.CanMoveDown())
                return Direction.Down;
            
            if (game.CanMoveUp())
                return Direction.Up;
            
            if (game.CanMoveLeft())
                return Direction.Left;
            
            return Direction.Right;
                
        }
    }



}
