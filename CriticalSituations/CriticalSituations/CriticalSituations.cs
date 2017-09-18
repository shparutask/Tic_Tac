using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalSituations
{
    //Должен реализовывать интерфейс поля
    public class Field //:Field 
    {
        public int get(int row, int col)
        {
            return 0;
        }
    }

    public class CriticalSituations
    {
        //структура для удобства просмотра поля
        public struct Move
        {
            public int x;
            public int y;

            public Move(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        protected int[][] m_field;
        int rowCount = 5;
        int colCount = 5;
        protected int winLength;
        int player;
        //public static int PLAYER_X = 1;
        //public static int PLAYER_O = -1;

        //занято ли поле соперником?
        private bool IsEnemy(int col, int row)
        {
            return m_field[col][row] != 0 && m_field[col][row] != player;
        }

        //есть ли вокруг клетки метка соперника?
        private Move isMoved(int col, int row)
        {
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    if (IsEnemy(col + i, row + j))
                    {
                        return new Move(i, j);
                    }
                }
            return new Move(0, 0);
        }

        private Move is_critical(int col, int row)
        {
            bool flag = false;
            int winCount = 0;
            Move news = isMoved(col, row);
            Move needToMove = new Move(0, 0);
            if (news.x == 0 && news.y == 0) return new Move(0, 0);
            for (int i = 1; i < winLength; i++)
            {
                if (!IsEnemy(col + news.x, row + news.y))
                {
                    if (flag) return needToMove;
                    flag = true;
                    needToMove = new Move(col + news.x * i, row + news.y * i);
                }
                winCount++;
            }
            if (winCount == winLength - 1)
                return new Move(col - news.x * winCount, row - news.y * winCount);
            return needToMove;
        }

        public Move IsCritical(Field field)
        {
            /*
             rowCount = field.getRowCount();
             colCount = field.getColCount();
             player = field.player;
             winLength = field.winLength;
             */
            Move critical_coord = new Move(0, 0);
            for (int i = 0; i < rowCount; i++)
            {
                m_field[i] = new int[5];
                for (int j = 0; j < colCount; j++)
                {
                    m_field[i][j] = field.get(i, j);
                }
            }

            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                {
                    if (IsEnemy(i, j))
                    {
                        critical_coord = is_critical(i, j);
                    }
                }
            return critical_coord;
        }
    }
}
