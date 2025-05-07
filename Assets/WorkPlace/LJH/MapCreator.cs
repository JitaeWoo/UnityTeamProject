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
                
                //������ ���������� �ƴ϶��
                if (finalStage == false)
                {
                    if (x != mapStartXPos && x != width + 1 && z != mapStartZPos && z != height + 1)
                    {
                        //�迭�� �ִ� ť�긦 �������� �ҷ��ɴϴ�.
                        GameObject selecteGround = groundPrefabs[Random.Range(0, groundPrefabs.Length)];

                        Vector3 groundpos = new Vector3(x, FloorPos, z);
                        //���� �����մϴ�.
                        Instantiate(selecteGround, groundpos, Quaternion.identity, this.transform);
                    }

                    //�ٴ�-1���� ���� 4�� (���� ����ϴ�.)
                    for (int y = -1; y < 4; y++)
                    {
                        //�� �׵θ���
                        if (x == mapStartXPos || x == width + 1 || z == mapStartZPos || z == height + 1)
                        {
                            //��ġ�� ���ϰ�
                            Vector3 wallpos = new Vector3(x, y, z);
                            //���� ����ϴ�.
                            Instantiate(wallPrefab, wallpos, Quaternion.identity, this.transform);
                        }
                    }
                }
                else
                {
                    if (x <= mapStartXPos + 20 || x >= width - 20 || z <= mapStartZPos + 20 || z >= height - 20)
                    {
                        //(���� ���� �� ������) ��ġ�� ���ϰ�
                        Vector3 lavapos = new Vector3(x, FloorPos, z);
                        //����� �����մϴ�.
                        Instantiate(lavaPrefab, lavapos, Quaternion.identity, this.transform);
                        
                    }
                    else
                    {
                        //�迭�� �ִ� ť�긦 �������� �ҷ��ɴϴ�.
                        GameObject selecteGround = groundPrefabs[Random.Range(0, groundPrefabs.Length)];

                        Vector3 groundpos = new Vector3(x, FloorPos, z);
                        //���� �����մϴ�.
                        Instantiate(selecteGround, groundpos, Quaternion.identity, this.transform);
                    }
                }

                
            }
        }
    }

}
