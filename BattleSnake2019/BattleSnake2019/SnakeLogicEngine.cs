using System;
using System.Collections.Generic;

namespace BattleSnake2019
{
    public class SnakeLogicEngine
    {

        private Board _board;

        // Defines the directions that the snake can take.
        private enum SnakeDirections
        {
            Up = -1,
            Down = 1,
            Left = -1,
            Right = 1  
        }
        
        public string decideMoveDirection(Board currBoard, Snake ourSnake)
        {

            _board = currBoard;

            // Find the closet food item to the head of the snake.
            Position closestFood = getClosestFood( ourSnake.Body[0], currBoard.Food);
                   
            return MoveToLocation( ourSnake.Body[0], closestFood).ToString().ToLower();
        }


        
        // Find the position of the closest food based on our current location.
        private Position getClosestFood(Position ourLoc, List<Position> foodLocation)
        {
            
            if (ourLoc == null) throw new ArgumentNullException(nameof(ourLoc));
            if (foodLocation == null) throw new ArgumentNullException(nameof(foodLocation));
            
            
            var closestDistance = double.MaxValue;

            var closest = new Position{ X = 0, Y = 0};

            // Go through each food location in the list to find the closest one.
            foreach (var food in foodLocation)
            {
                // Get the x and y distance from the next food to us.
                var deltaX = food.X - ourLoc.X;
                var deltaY = food.Y - ourLoc.Y;

                // Calculate the distance between the two points.
                var distance = Math.Sqrt( deltaX^2 + deltaY^2 );

                // If this food is closer, then save it.
                if (distance < closestDistance)
                {
                    closest = food;
                }
            }

            return closest;
        }


        // Calculates the direction the snake needs to move to get to the desired location.
        private SnakeDirections MoveToLocation(Position ourPosition, Position movePosition)
        {
            
            // Get the x and y distance from the location and us.
            var deltaX = movePosition.X - ourPosition.X ;
            var deltaY = movePosition.Y - ourPosition.Y;

            SnakeDirections direction;
            
            // Prioritize moving along the X-axis first, then check the Y-axis.
            if (deltaX > 0 && !CheckForCollision(SnakeDirections.Right) )
            {
                direction = SnakeDirections.Right;
            }
            else if( deltaX < 0 && !CheckForCollision(SnakeDirections.Left) )
            {
                direction = SnakeDirections.Left;
            } 
            else if (deltaY > 0 && !CheckForCollision(SnakeDirections.Down) )
            {
                direction = SnakeDirections.Down;
            } 
            else if (deltaY < 0 && !CheckForCollision(SnakeDirections.Up) )
            {
                direction = SnakeDirections.Up;
            }
            else
            {
               // If we are here, then we are screwed, meaning we are blocked in.
                direction = SnakeDirections.Right;
            }
            return direction;
        }


        // Checks if the desired direction will cause a collision with self, wall, or other snake.
        private bool CheckForCollision(SnakeDirections desiredDirection)
        {
            
            // Get the starting position as our snake head.
            Position desiredPosition = _board.Snakes.Find(snake => (snake.Name == "you") ).Body[0];

            // Adjust the position based on the desired direction we want to go.
            if (desiredDirection == SnakeDirections.Down || desiredDirection == SnakeDirections.Up)
            {
                desiredPosition.Y += (long) desiredDirection;
            }
            else
            {
                desiredPosition.X += (long) desiredDirection;
            }

            // If the direction collides us with the wall, return true for collision
            if (desiredPosition.X < 0 || desiredPosition.X >= _board.Width) return true;
            if (desiredPosition.Y < 0 || desiredPosition.X >= _board.Height) return true;
            
            
            // Look through all the snakes to see if we'll collide with it.
            /*foreach (var snake in _board.Snakes )
            {

                foreach (var snakePart in snake.Body)
                {
                    
                }
            }*/

            // If we made it this far, then there will be no collision.
            return false;
        }
             
    }
}