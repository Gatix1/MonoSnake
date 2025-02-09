using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Nito.Collections;

namespace MonoSnake
{
    public class Globals
    {
        public static Color darkColor = new Color(63, 41, 30);
        public static Color lightColor = new Color(253, 202, 85);

        public const int cellSize = 30;
        public const int cellsWidth = 25;
        public const int cellsHeight = 30;
        public const int offset = 75;

        public const int width = cellSize * cellsWidth + offset * 2;
        public const int height = cellSize * cellsHeight + offset * 2;
        public static bool ElementInDeque(Deque<Vector2> deque, Vector2 element)
        {
            for (int i = 0; i < deque.Count; i++)
            {
                if (deque[i] == element)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
