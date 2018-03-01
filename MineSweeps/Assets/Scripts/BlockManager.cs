using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

    GameObject[][] m_Blocks;

    public int m_GridSize;
    
    public bool m_UsingPercenage;
    public float m_MinePercentage;

    public GameObject m_BlockPrefab;

	// Use this for initialization
	void Start ()
    {
        GameObject[][] m_Blocks = new GameObject[m_GridSize][];

        for(int i = 0; i < m_GridSize; i++)
        {
            m_Blocks[i] = new GameObject[m_GridSize];           
        }


        for (int i = 0; i < m_GridSize; i++)
        {
            for (int n = 0; n < m_GridSize; n++)
            {
                m_Blocks[i][n] = Instantiate(m_BlockPrefab);

                //figure out what is bigger, width or length
                if(Screen.width > Screen.height)
                {
                    //width is bigger, scale of off height

                }
                //size of block

            }
        }
        //determine Mine location
        CreateMines();
        //determine mine proxcimity score

	}

    /// <summary>
    /// determines where mines are placed
    /// </summary>
    void CreateMines()
    {
        //Determine how many mines we should place
        float percentage = m_MinePercentage * 0.01f;

        //safety check to make sure we don't have more mines than blocks.
        if (percentage > 1)
        {
            percentage = 1;
        }

        int numberOfMines = Mathf.RoundToInt((m_GridSize * m_GridSize) * percentage);
        int numberOfBlocks = m_GridSize * m_GridSize;

        //loop through all mines and determine randomly if block is a mine.
        for (int i = 0; i < m_GridSize; i++)
        {
            for (int n = 0; n < m_GridSize; n++)
            {
                //if we have already placed all mines, no reason to contine.
                if (numberOfMines <= 0)
                {
                    i = m_GridSize;
                    return;
                }

                int randomMine = Random.Range(0, numberOfBlocks);

                if (randomMine < numberOfMines)
                {
                    //we have a mine
                    numberOfMines--;

                    //turn mine varible to true.
                    m_Blocks[i][n].GetComponent<MineScore>().m_IsMine = true;
                }

                numberOfBlocks--;
            }
        }
    }

    void DetermineMineScore()
    {
        //loop through all mines and determine randomly if block is a mine.
        for (int i = 0; i < m_GridSize; i++)
        {
            for (int n = 0; n < m_GridSize; n++)
            {
                //check and see if block is mine, if so, return.

                int DangerScore = 0;
                //check all squares surounding current square, if mine, add one to square.

                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        int gridPosX = i - 1 + x;
                        int gridPosY = n - 1 + y;

                        //check if grid Pos is out of bounds, 
                        if (gridPosX < 0 || gridPosX > m_GridSize || gridPosY < 0 || gridPosY > m_GridSize)
                        {
                            //no block, so no mine, so skip
                            break;
                        }

                        if(m_Blocks[gridPosX][gridPosY].GetComponent<MineScore>().m_IsMine)
                        {
                            DangerScore++;
                        }
                    }
                }
            }
        }
    }

    void ClearAllBlocks()
    {
    }
}
