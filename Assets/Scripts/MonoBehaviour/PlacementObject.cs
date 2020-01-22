using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementObject : MonoBehaviour
{
    public enum PlacementObjectState
    {
        NOT_SELECTED,
        SELECTED,
        ROTATING
    }

    private GameObject PlacementTile = null;

    private PlacementObjectState State;
    [SerializeField] private SpriteRenderer ObjectSpriteRenderer = null;
    [SerializeField] private SpriteRenderer SquareSpriteRenderer = null;

    [SerializeField] private ParticleSystem SeasonalSparkles = null;

    [SerializeField] private Canon Canon = null;
    [SerializeField] private Slapper Slapper = null;
    [SerializeField] private Bomb Bomb = null;

    private List<GameObject> PlacedTowers = new List<GameObject>();
    private List<ParticleSystem> Sparkles = new List<ParticleSystem>();

    private Color Red = new Color(1.0f, 0.0f, 0.0f, 0.5f);
    private Color Green = new Color(0.0f, 1.0f, 0.0f, 0.5f);

    private FruitData FruitData;

    void Start()
    {
        UnSelectObject();
    }

    void Update()
    {
        switch(State)
        {
            case PlacementObjectState.NOT_SELECTED:
                break;
            case PlacementObjectState.SELECTED:
                PlacementTile = MouseOverUnoccupiedTile();
                if(PlacementTile != null
                    && FruitData != null
                    && FruitData.Type != TowerType.BOMB)
                {
                    SnapToTile(PlacementTile.transform.position);
                }
                else
                {
                    MoveToMouse();
                }
                break;
            case PlacementObjectState.ROTATING:
                RotateTowardsMouse();
                break;
        }
    }

    public FruitData GetFruitData()
    {
        return FruitData;
    }

    public PlacementObjectState GetState()
    {
        return State;
    }

    public void SelectObject(FruitData p_FruitData)
    {
        if(p_FruitData != null)
        {
            FruitData = p_FruitData;
            ObjectSpriteRenderer.sprite = p_FruitData.Sprite;
            ObjectSpriteRenderer.enabled = true;
            if (p_FruitData.Type != TowerType.BOMB)
            {
                SquareSpriteRenderer.enabled = true;
            }
            else
            {
                SquareSpriteRenderer.enabled = false;
            }
            State = PlacementObjectState.SELECTED;
        }
    }

    public void UnSelectObject()
    {
        FruitData = null;
        ObjectSpriteRenderer.sprite = null;
        ObjectSpriteRenderer.enabled = false;
        SquareSpriteRenderer.enabled = false;
    }

    private void RotateTowardsMouse()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ObjectSpriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void MoveToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;
        transform.position = mousePos;
    }

    public void LockPlacement()
    {
        if (PlacementTile == null)
            return;
        State = PlacementObjectState.ROTATING;
    }

    public void UnlockPlacement()
    {
        State = PlacementObjectState.SELECTED;
    }

    public void ClearTable()
    {
        for(int i = 0; i < PlacedTowers.Count; i++)
        {
            Destroy(PlacedTowers[i]);
        }
        for(int i = 0; i < Sparkles.Count; i++)
        {
            
            Destroy(Sparkles[i]);
        }
    }

    public void PlaceSelectedObject()
    {
        if(FruitData.Type == TowerType.SHOOTER)
        {
            if (PlacementTile == null)
                return;
            Canon newCanon = Instantiate(Canon, transform.position, ObjectSpriteRenderer.transform.rotation);
            newCanon.SetFruitData(FruitData);
            Inventory.Instance.RemoveItem(FruitData);
            PlacedTowers.Add(newCanon.gameObject);
        }
        else if(FruitData.Type == TowerType.SLAPPER)
        {
            if (PlacementTile == null)
                return;
            Slapper newSlapper = Instantiate(Slapper, transform.position, ObjectSpriteRenderer.transform.rotation);
            newSlapper.SetFruitData(FruitData);
            Inventory.Instance.RemoveItem(FruitData);
            PlacedTowers.Add(newSlapper.gameObject);
        }
        else if(FruitData.Type == TowerType.BOMB)
        {
            Bomb newBomb = Instantiate(Bomb, transform.position, ObjectSpriteRenderer.transform.rotation);
            newBomb.SetFruitData(FruitData);
            Inventory.Instance.RemoveItem(FruitData);
        }

        if(FruitData.SeasonStrength[SeasonController.Instance.SeasonIterator])
        {
            ParticleSystem newSparkles = Instantiate(SeasonalSparkles, transform.position, transform.rotation);
            Sparkles.Add(newSparkles);
        }

        UnlockPlacement();
        ObjectSpriteRenderer.transform.right = Vector3.right;
        if(PlacementTile != null)
        {
            PlacementTile.tag = "OccupiedTile";
        }
        if (!Inventory.Instance.ItemRemaining(FruitData))
        {
            UnSelectObject();
        }
    }

    private GameObject MouseOverUnoccupiedTile()
    {
        Ray MouseCameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, MouseCameraRay.direction);


        if (Physics.Raycast(MouseCameraRay, out RaycastHit MouseGridRayHit)
            && MouseGridRayHit.collider.CompareTag("PlaceableTile"))
        {
            SquareSpriteRenderer.color = Green;
            return MouseGridRayHit.collider.transform.gameObject;
        }
        SquareSpriteRenderer.color = Red;
        return null;
    }
    private void SnapToTile(Vector3 p_TilePosition)
    {
         transform.position = p_TilePosition;
    }
}
