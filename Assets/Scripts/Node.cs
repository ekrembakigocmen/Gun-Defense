using UnityEngine;


public class Node : MonoBehaviour
{

    public GameObject turret;
    // public Vector3 positionOffset;
    [HideInInspector]
    public Vector3 nodeRangeSize;
    [HideInInspector]
    public Vector3 nodeRangeLocation;
    public Color hoverColor;
    public Color startColor;
    public Color notEnoughMoneyColor;
    private Renderer rend; // mousenin veya dokunulan obje ile islem yapmamiz icin.

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    /*  void FixedUpdate()
      {

          if (Input.touchCount > 0)
          {
              buildManager.BuildTurretOnPhone(turret);
          }
      }*/



    public Vector3 GetBuildPosition()
    {

        return transform.position;
    }



    // *** Build for PC ***
  /*  void OnMouseEnter()
    {
        
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            //rend.material.color = hoverColor;
        }
        // else
        // rend.material.color = notEnoughMoneyColor;

    
    }*/
    void OnMouseDown()
    {
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (this.enabled == false)
            return;
        if (!buildManager.CanBuild)
            return;
        
        buildManager.BuildTurretOn(this);
    }
    // ******** End ***********



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + nodeRangeLocation, nodeRangeSize);
        
        
    }

}
