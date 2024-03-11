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
            Vector3Int playerCellPosition = tilemap.WorldToCell(player.position); // �÷��̾��� ���� ��ġ�� �� ��ǥ�� ��ȯ
            Vector3Int mouseCellPosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // ���콺 ��ġ�� �� ��ǥ�� ��ȯ

            // �÷��̾�κ��� 1ĭ �Ÿ� ���� �ִ��� Ȯ��
            if (Mathf.Abs(playerCellPosition.x - mouseCellPosition.x) <= 1 && Mathf.Abs(playerCellPosition.y - mouseCellPosition.y) <= 1)
            {
                ChangeTile(mouseCellPosition); // Ÿ�� ���� �Լ� ȣ��
            }
    }


    private void ChangeTile(Vector3Int position)
    {
        // ������ Ÿ�� �ֺ��� 3x3 �׸��带 ��ȸ
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // ���� ��ȸ ���� ��ġ�� ����մϴ�.
                Vector3Int tilePosition = new Vector3Int(position.x + x, position.y + y, position.z);
                // ���⼭ �߰����� ������ �˻��Ͽ� Ư�� ���ǿ� �´� Ÿ�ϸ� ������ �� �ֽ��ϴ�.
                // ��: �� �� �ִ� ������ Ȯ���ϴ� ����
                // Ÿ���� �����մϴ�. ȣ������ ȿ���� �����մϴ�.
                tilemap.SetTile(tilePosition, changeTile );
            }
        }
    }

}
