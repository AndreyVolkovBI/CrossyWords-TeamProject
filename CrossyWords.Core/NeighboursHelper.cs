﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyWords.Core
{
    public class NeighboursHelper
    {
        public List<int> GetIds(int id) // возвращает список id ячеек
        {
            List<int> ids = new List<int>();

            if ((id == 10 || id == 15 || id == 20) ||
               (id == 6 || id == 11 || id == 16) ||
               (id > 1 && id < 5) ||
               (id > 21 && id < 25))
            {
                if (id > 1 && id < 5)
                {
                    ids.Add(id + 1);
                    for (int i = 6; i > 3; i--)
                        ids.Add(id + i);
                    ids.Add(id - 1);
                }
                else if (id > 21 && id < 25)
                {
                    ids.Add(id - 1);
                    for (int i = 6; i > 3; i--)
                        ids.Add(id - i);
                    ids.Add(id + 1);
                }
                else if (id == 10 || id == 15 || id == 20)
                {
                    ids.Add(id - 5);
                    for (int i = -6; i < 5; i += 5)
                        ids.Add(id + i);
                    ids.Add(id + 5);
                }
                else if (id == 6 || id == 11 || id == 16)
                {
                    ids.Add(id - 5);
                    for (int i = -4; i < 11; i += 5)
                        ids.Add(id + i);
                    ids.Add(id + 5);
                }

            }
            else if (id == 1 || id == 5 || id == 21 || id == 25)
            {
                if (id == 1)
                {
                    ids.Add(2); ids.Add(7); ids.Add(6);
                }
                else if (id == 5)
                {
                    ids.Add(4); ids.Add(9); ids.Add(10);
                }
                else if (id == 21)
                {
                    ids.Add(16); ids.Add(17); ids.Add(22);
                }
                else if (id == 25)
                {
                    ids.Add(20); ids.Add(19); ids.Add(24);
                }
            }
            else
            {
                for (int i = 6; i > 3; i--) // сверху
                    ids.Add(id - i);
                ids.Add(id + 1); // спарва
                for (int j = 6; j > 3; j--) // снизу
                    ids.Add(id + j);
                ids.Add(id - 1); // слева

            }
            return ids;
        }

        //public int NeighbourCount(int id) // количество соседей у ячейки
        //{
        //    if (IsCell3(id))
        //        return 3;
        //    else if (IsCell5(id))
        //        return 5;
        //    else
        //        return 9;
        //}

        //public bool IsCell3(int id)
        //{
        //    if (id == 1 || id == 5 || id == 21 || id == 25)
        //        return true;
        //    return false;
        //}

        //public bool IsCell5(int id)
        //{
        //    // (10, 15, 20) || (6, 11, 16) || (2, 3, 4) || (22, 23, 24)
        //    if ((id == 10 || id == 15 || id == 20) ||
        //       (id == 6 || id == 11 || id == 16) ||
        //       (id > 1 && id < 5) ||
        //       (id > 21 && id < 25))
        //        return true;
        //    return false;
        //}
    }
}
