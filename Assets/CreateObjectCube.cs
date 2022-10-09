using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectCube : MonoBehaviour
{
    public float high;
    //オブジェクト間の幅
    public float width;
    //上から見て縦、Z軸のオブジェクトの量
    public int vertical;
    //上から見て横、X軸のオブジェクトの量
    public int horizontal;
    //Prefabを入れる欄を作る
    public GameObject cube;
    //位置を入れる変数
    Vector3 pos;
    private GameObject[,] copy = new GameObject[100,100];


    public RsDevice2 rsDevice2;
    void Start()
    {

        //このスクリプトを入れたオブジェクトの位置
        pos = transform.position;


        //Z軸にverticalの数だけ並べる
        for (int vi = 0; vi < vertical; vi++)
        {
            //X軸にhorizontalの数だけ並べる
            for (int hi = 0; hi < horizontal; hi++)
            {
                //PrefabのCubeを生成する
                copy[vi,hi] = Instantiate(cube,
                    //生成したものを配置する位置
                    new Vector3(
                        //X軸
                        pos.x + horizontal * width / 2 - hi * width - width / 2,
                        //Y軸
                        high,
                        //Z軸
                        pos.z + vertical * width / 2 - vi * width - width / 2
                    //Quaternion.identityは無回転を指定する
                    ), Quaternion.identity);
            }
        }
    }
    void Update(){
        for(int i = 0; i < vertical; i++){
            for(int j = 0; j < horizontal; j++){
                float nowPos = rsDevice2.facePosition[i,j] * 10;
                copy[i,j].transform.position = new Vector3(copy[i,j].transform.position.x,nowPos,copy[i,j].transform.position.z);
            }
        } 
    }
}
