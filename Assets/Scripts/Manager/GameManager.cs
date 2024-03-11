using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile changeTile;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            Vector3Int playerCellPosition = tilemap.WorldToCell(player.position); // 플레이어의 현재 위치를 셀 좌표로 변환
            Vector3Int mouseCellPosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // 마우스 위치를 셀 좌표로 변환

            // 플레이어로부터 1칸 거리 내에 있는지 확인
            if (Mathf.Abs(playerCellPosition.x - mouseCellPosition.x) <= 1 && Mathf.Abs(playerCellPosition.y - mouseCellPosition.y) <= 1)
            {
                ChangeTile(mouseCellPosition); // 타일 변경 함수 호출
            }
    }


    private void ChangeTile(Vector3Int position)
    {
        // 선택한 타일 주변의 3x3 그리드를 순회
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // 현재 순회 중인 위치를 계산합니다.
                Vector3Int tilePosition = new Vector3Int(position.x + x, position.y + y, position.z);
                // 여기서 추가적인 조건을 검사하여 특정 조건에 맞는 타일만 변경할 수 있습니다.
                // 예: 갈 수 있는 땅인지 확인하는 로직
                // 타일을 변경합니다. 호미질한 효과를 적용합니다.
                tilemap.SetTile(tilePosition, changeTile );
            }
        }
    }

}
