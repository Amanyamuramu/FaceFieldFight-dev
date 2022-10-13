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
    private GameObject[,] copy; 
    private float[,] distPosition; 
    private float[,] pastPosition;
    public int objectNum = 100;
    public float alpha = 10f;
    public float threshold = 10f;
    public float distancetoCamera  = 20f;

    public RsDevice2 rsDevice2;
    void Start()
    {
        copy = new GameObject[objectNum,objectNum];
        distPosition = new float[objectNum,objectNum];
        pastPosition = new float[objectNum,objectNum];


        //このスクリプトを入れたオブジェクトの位置
        pos = transform.position;


        //Z軸にverticalの数だけ並べる
        for (int vi = 0; vi < objectNum; vi++)
        {
            //X軸にhorizontalの数だけ並べる
            for (int hi = 0; hi < objectNum; hi++)
            {
                pastPosition[vi,hi] = 0f;
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
        for(int i = 0; i < objectNum; i++){
            for(int j = 0; j < objectNum; j++){
                float nowPos = rsDevice2.facePosition[i,j] * alpha;
                float distance = Mathf.Abs(pastPosition[i,j] - nowPos);
                if(distance > threshold && nowPos < distancetoCamera){
                    copy[i,j].transform.position = new Vector3(copy[i,j].transform.position.x,nowPos,copy[i,j].transform.position.z);
                }

                else{
                    //変化がなく、一定時間経過したらにする
                    // copy[i,j].transform.position = new Vector3(copy[i,j].transform.position.x,0f,copy[i,j].transform.position.z);
                }
                pastPosition[i,j] = nowPos;
            }
        } 
    }
}
