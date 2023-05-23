using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour{
    [SerializeField]
    private GameObject mobZombieType1;
    [SerializeField]
    private GameObject mobZomdev;

    [SerializeField]
    private float spawnInterval;
    private float intervalDecrement;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Player playerLevel;

    private float bossSpawnInterval = 60f;

    private float rangex, rangey, coefx, coefy;

    void Awake(){
        rangey = camera.orthographicSize*2f;
        rangex = camera.aspect * rangey;

        StartCoroutine(spawnEnemy(mobZombieType1, "npc"));
        StartCoroutine(spawnEnemy(mobZomdev, "boss"));
    }

    void Update(){
        if(playerLevel.GetLevel()<5){intervalDecrement = playerLevel.GetLevel()*0.6f;}
        else{intervalDecrement = 3;}
    }

    private IEnumerator spawnEnemy(GameObject enemy, string type){
        if(type == "npc"){ yield return new WaitForSeconds(spawnInterval-intervalDecrement);}
        else {yield return new WaitForSeconds(bossSpawnInterval);}
        coefx = Random.Range(0,2)*2-1;
        coefy = Random.Range(0,2)*2-1;

        if(player.position.x > 7f){
            if(player.position.y > 7f){
                ThirdK(enemy, type);
            }
            else if(player.position.y < -7f){
                SecondK(enemy, type);
            }
            else{
                int var = Random.Range(0, 2);
                if(var == 0){SecondK(enemy, type);}
                else{ThirdK(enemy, type);}
            }
        }
        else if(player.position.x <-7f){
            if(player.position.y > 7f){
                FourthK(enemy, type);
            }
            else if(player.position.y < -7f){
                FirstK(enemy, type);
            }
            else{
                int var = Random.Range(0, 2);
                if(var == 0){FirstK(enemy, type);}
                else{FourthK(enemy, type);}
            }
        }
        else{
            if(player.position.y > 7f){
                int var = Random.Range(0, 2);
                if(var == 0){ThirdK(enemy, type);}
                else{FourthK(enemy, type);}
            }
            else if(player.position.y < -7f){
                int var = Random.Range(0, 2);
                if(var == 0){SecondK(enemy, type);}
                else{FirstK(enemy, type);}
            }
            else{
                int var = Random.Range(0, 4);
                if(var == 0){FirstK(enemy, type);}
                else if(var == 1){SecondK(enemy, type);}
                else if(var == 2){ThirdK(enemy, type);}
                else{FourthK(enemy, type);}
            }
        }
    }

    private void FirstK(GameObject enemy, string type){
        int max = Random.Range(0, 2);
        float rand;
        if(max == 0){ // x max; y rand
            rand = Random.Range(0f, rangey);
            GameObject newEnemy = Instantiate(enemy, new Vector3(player.position.x + rangex,  player.position.y + rand, 0), Quaternion.identity);
        }
        else{ //x rand; y max
            rand = Random.Range(0f, rangex);
            GameObject newEnemy = Instantiate(enemy, new Vector3(player.position.x + rand,  player.position.y + rangey, 0), Quaternion.identity);
        }
        
        StartCoroutine(spawnEnemy(enemy, type));
    }
    
    private void SecondK(GameObject enemy, string type){
        int max = Random.Range(0, 2);
        float rand;
        if(max == 0){ // x max; y rand
            rand = Random.Range(0f, rangey);
            GameObject newEnemy = Instantiate(enemy, new Vector3(-(player.position.x + rangex),  player.position.y + rand, 0), Quaternion.identity);
        }
        else{ // x rand; y max
            rand = Random.Range(0f, rangex);
            GameObject newEnemy = Instantiate(enemy, new Vector3(-(player.position.x + rand),  player.position.y + rangey, 0), Quaternion.identity);
        }
        StartCoroutine(spawnEnemy(enemy, type));
    }

    private void ThirdK(GameObject enemy, string type){
        int max = Random.Range(0, 2);
        float rand;
        if(max == 0){ // x max; y rand
            rand = Random.Range(0f, rangey);
            GameObject newEnemy = Instantiate(enemy, new Vector3(-(player.position.x + rangex),  -(player.position.y + rand), 0), Quaternion.identity);
        }
        else{ // x rand; y max
            rand = Random.Range(0f, rangex);
            GameObject newEnemy = Instantiate(enemy, new Vector3(-(player.position.x + rand),  -(player.position.y + rangey), 0), Quaternion.identity);
        }
        StartCoroutine(spawnEnemy(enemy, type));
    }

    private void FourthK(GameObject enemy, string type){
        int max = Random.Range(0, 2);
        float rand;
        if(max == 0){ // x max; y rand
            rand = Random.Range(0f, rangey);
            GameObject newEnemy = Instantiate(enemy, new Vector3(player.position.x + rangex,  -(player.position.y + rand), 0), Quaternion.identity);
        }
        else{
            rand = Random.Range(0f, rangex);
            GameObject newEnemy = Instantiate(enemy, new Vector3(player.position.x + rand,  -(player.position.y + rangey), 0), Quaternion.identity);
        }
        StartCoroutine(spawnEnemy(enemy, type));
    }
}
