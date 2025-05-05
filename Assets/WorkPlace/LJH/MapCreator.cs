using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] public GameObject[] groundPrefabs;
    public GameObject wallPrefab;
    public GameObject lavaPrefab;  
    public int width = 100;
    public int height = 100;
    public int mapStartXPos = -101;
    public int mapStartZPos = -101;
    public int FloorPos = -1;
    [SerializeField] public bool finalStage;
    


    void Awake()
    {
        for (int x = mapStartXPos; x <= width + 1; x++)
        {
            for (int z = mapStartZPos; z <= height + 1; z++)
            {
                
                //마지막 스테이지가 아니라면
                if (finalStage == false)
                {
                    if (x != mapStartXPos && x != width + 1 && z != mapStartZPos && z != height + 1)
                    {
                        //배열에 있는 큐브를 랜덤으로 불러옵니다.
                        GameObject selecteGround = groundPrefabs[Random.Range(0, groundPrefabs.Length)];

                        Vector3 groundpos = new Vector3(x, FloorPos, z);
                        //맵을 구성합니다.
                        Instantiate(selecteGround, groundpos, Quaternion.identity, this.transform);
                    }

                    //바닥-1부터 높이 4의 (벽을 세웁니다.)
                    for (int y = -1; y < 4; y++)
                    {
                        //맵 테두리에
                        if (x == mapStartXPos || x == width + 1 || z == mapStartZPos || z == height + 1)
                        {
                            //위치를 정하고
                            Vector3 wallpos = new Vector3(x, y, z);
                            //벽을 세웁니다.
                            Instantiate(wallPrefab, wallpos, Quaternion.identity, this.transform);
                        }
                    }
                }
                else
                {
                    if (x <= mapStartXPos + 20 || x >= width - 20 || z <= mapStartZPos + 20 || z >= height - 20)
                    {
                        //(벽은 없고 땅 옆으로) 위치를 정하고
                        Vector3 lavapos = new Vector3(x, FloorPos, z);
                        //용암을 셋팅합니다.
                        Instantiate(lavaPrefab, lavapos, Quaternion.identity, this.transform);
                        
                    }
                    else
                    {
                        //배열에 있는 큐브를 랜덤으로 불러옵니다.
                        GameObject selecteGround = groundPrefabs[Random.Range(0, groundPrefabs.Length)];

                        Vector3 groundpos = new Vector3(x, FloorPos, z);
                        //맵을 구성합니다.
                        Instantiate(selecteGround, groundpos, Quaternion.identity, this.transform);
                    }
                }

                
            }
        }
    }

}
