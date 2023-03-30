using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public struct Map
{
    public int lenghtX;
    public int lenghtY;
    public GameObject mapObject;
}

public class MapManager : Singleton<MapManager>
{
    public Map testMap;

    Vector2 mainMapPos;
    List<GameObject> maps;

    Map activeMap;

    public override void Awake()
    {
        base.Awake();
        activeMap = testMap;

        maps = new List<GameObject>();

        mainMapPos = new Vector2();

        EventManager.AddEventAction("Start Run", SetUpMap);

        EventManager.AddEventAction("Start Run", () => enabled = true);
        EventManager.AddEventAction("End Run", () => enabled = false);
    }


    void SetUpMap()
    {
        {
            var mapObject = Instantiate(testMap.mapObject, this.transform);
            maps.Add(mapObject);
        }

        Vector2 dir = Vector2.up;

        for (int i = 0; i < 4; i++)
        {
            var mapObject = Instantiate(testMap.mapObject, this.transform);
            Vector2 pos = new Vector2(dir.x * activeMap.lenghtX * 2, dir.y * activeMap.lenghtY * 2);
            mapObject.transform.position = GameManager.Translate3D(pos);
            maps.Add(mapObject);
            dir.Rotate(90);
        }

        dir = new Vector2(1, 1);

        for (int i = 0; i < 4; i++)
        {
            var mapObject = Instantiate(testMap.mapObject, this.transform);
            Vector2 pos = new Vector2(dir.x * activeMap.lenghtX * 2, dir.y * activeMap.lenghtY * 2);
            mapObject.transform.position = GameManager.Translate3D(pos);
            maps.Add(mapObject);
            dir.Rotate(90);
        }
    }

    void UpdateMap(in Vector2 pos)
    {
        mainMapPos = pos;

        maps[0].transform.position = GameManager.Translate3D(pos);

        Vector2 dir = Vector2.up;

        for (int i = 1; i < 5; i++)
        {
            var mapObject = maps[i];
            Vector2 tPos = new Vector2(dir.x * activeMap.lenghtX * 2 + pos.x, dir.y * activeMap.lenghtY * 2 + pos.y);
            mapObject.transform.position = GameManager.Translate3D(tPos);
            dir.Rotate(90);
        }

        dir = new Vector2(1, 1);

        for (int i = 5; i < 9; i++)
        {
            var mapObject = maps[i];
            Vector2 tPos = new Vector2(dir.x * activeMap.lenghtX * 2 + pos.x, dir.y * activeMap.lenghtY * 2 + pos.y);
            mapObject.transform.position = GameManager.Translate3D(tPos);
            dir.Rotate(90);
        }
    }

    private void Update()
    {
        int minLenght = activeMap.lenghtY > activeMap.lenghtX ? activeMap.lenghtX : activeMap.lenghtY;

        foreach (var map in maps)
        {
            if (Vector2.Distance(Player.s_Instance.transform.position, map.transform.position) <= minLenght)
            {
                if (mainMapPos == (Vector2)map.transform.position) return;
                UpdateMap(map.transform.position);
            }
        }

    }
}