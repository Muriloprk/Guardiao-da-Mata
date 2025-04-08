using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Player; //Declaração de variavel que se refere ao player
    public float minX, maxX; //variaveis que definem o minimo e maximo que a camera pode se mover no eixo X
    public float timeLerp;//declarando variavel que define a suavidade da camera;

    private void FixedUpdate()
    {
        if(Player != null)
        {
            Vector3 newPosition = Player.position + new Vector3(0f,0f,-10); //dizendo que a posição da camera é no player porem adicionando o eixo Z que no caso é -10 para que possamos ver o cenario
            newPosition.y = 0.1f; //modificando o y da camera para que não apareça o fundo azul na parte de baixo
            newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);//Lerp é responsavel pela suavidade da transição da camera.
            transform.position = newPosition; //setando y da camera para 0,1

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX , maxX), transform.position.y, transform.position.z); //aplicando o minX e maxX ao eixo x da camera
        }
    }
}
