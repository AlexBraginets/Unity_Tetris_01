﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CommonNonUnityUtils;
public static class SweepLine
{
    private static int[] RowsToCheckForSweep(Tetramino tetramino)
    {
        Vector2Int[] absPoses = tetramino.AbsPoses;
        int[] rowsToCheckForSweepNotUnique = LoopUtil.LoopFunc<int>((i) => absPoses[i].y, 4);
        int[] rowsToCheckForSweepUnique = rowsToCheckForSweepNotUnique.Distinct().OrderBy(i=>i).ToArray();
        return rowsToCheckForSweepUnique;
    }
    private static List<int> RowsToSweep(Tetramino tetramino, out bool sweep)
    {

        List<int> rowsToSweep = new List<int>();
        int[] rowsToCheckForSweep = RowsToCheckForSweep(tetramino);
        foreach (int row in rowsToCheckForSweep)
        {
            if (Grid.FullRow(row))
            {
                rowsToSweep.Add(row);
            }
        }
        sweep = rowsToSweep.Count != 0;
        return rowsToSweep;
    }
    private static List<int> RowsToSweepExtended(Tetramino tetramino, out bool sweep)
    {
        List<int> rowsToSweep = RowsToSweep(tetramino, out sweep);
        List<int> rowsToSweepExtended = rowsToSweep.ToList();// copy list
       
        int rowCount = Grid.gridSize.y;
        rowsToSweepExtended.Add( rowCount + 1 );
        return rowsToSweepExtended;
    }
    // (a, b)
    private static List<int> InRange(int a, int b)
    {
        List<int> result = new List<int>();
        for(int i = a + 1; i < b; i++)
        {
            result.Add(i);
        }
        return result;
    }
    private static List<List<int>> SplitList(List<int> list)
    {
        List<List<int>> splitList = new List<List<int>>();
        for(int i = 0; i < list.Count - 1; i++)
        {
            int a = list[i];
            int b = list[i + 1];
            List<int> inRange = InRange(a, b);
            
            splitList.Add(inRange);
        }
        return splitList;
    }
    
    public static void Sweep(Tetramino tetramino)
    {
        bool sweep;
        List<int> rowsToSweepExtended = RowsToSweepExtended(tetramino, out sweep);
        
        if (!sweep)
            return;
        List<List<int>> splitList = SplitList(rowsToSweepExtended);
        for(int dropCount = 1; dropCount <= splitList.Count; dropCount++)
        {
            int sweepRowIndex = rowsToSweepExtended[dropCount - 1];
            Grid.EmptyAndDestroyBricksInRow(sweepRowIndex);
            foreach(int rowIndex in splitList[dropCount - 1])
            {
                Grid.DropBricksInRow(rowIndex, dropCount);
            }
        }
    }
}
