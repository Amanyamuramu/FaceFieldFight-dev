using UnityEngine;

public class RsDevice2 : RsDevice
{
    public CreateObjectCube createObjectcube;
    public float[,] facePosition;
    void Start(){
        facePosition = new float[createObjectcube.objectNum,createObjectcube.objectNum];
    }
    void Update()
    {
        if (!Streaming)
            return;

        if (processMode != ProcessMode.UnityThread)
            return;

        if (m_pipeline.PollForFrames(out var frames))
        {
            //ここで、特定の範囲 ex. w:100~200,h:100~200程度の範囲でスパースを持たせて取得して、それを配列として格納する
            for(int i = 0; i < createObjectcube.objectNum; i++){
                for(int j = 0; j < createObjectcube.objectNum; j++){
                    facePosition[i,j] = frames.DepthFrame.GetDistance(100+i*2,0+j*2);
                    // Debug.Log(i + j + " : " + facePosition[i,j]);
                }
            }
        }
    }
}