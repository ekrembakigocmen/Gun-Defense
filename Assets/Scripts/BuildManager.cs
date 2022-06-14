using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;


    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager in scene!");
            return;
        }
        instance = this;

    }
    [HideInInspector]
    public NodeUI nodeUI;
    [HideInInspector]
    public Shop shop;
    [HideInInspector]
    public Turret turretBullet;
    [HideInInspector]
    public string nodeTag = "Node";

    private Vector3 gunTransform;

    private bool isUpgrade;
    private TurretBluePrint turretToBuild;
    private TurretBluePrint turretToUpgrade;
    private Node selectedNode;
    private Node tempdNode;
    private GameObject selectedTurret;

    public SelectionFilter selectionFilter;


    [Header("Upgrade Color")]
    public Color startColor;
    public Color levelTwo;
    public Color levelThree;
    public Color levelFour;


    [Header("MachineGun Upgrade Parameters")]
    public int upgradeRangeMG;
    public int upgradeFireRateMG;
    public int upgradeDamageMG;


    [Header("SniperTower Upgrade Parameters")]
    public float upgradeRangeSP;
    public float upgradeFireRateSP;
    public int upgradeDamageSP;

    [Header("LaserBeamer Upgrade Parameters")]
    public float upgradeRangeLB;
    public float upgradeSlowAmount;
    public int upgradeDamageOverTime;

    [Header("RocketLouncher Upgrade Parameters")]
    public float upgradeRangeRL;
    public float upgradeFireRateRL;
    public float upgradeExplosionRadius;
    public int upgradeDamageRL;


    [Header("Upgrade Information")]
    public GameObject sniperMachine;
    public GameObject laser;
    public GameObject rocket;

   





    public Vector3 GetGunTransform()
    {
        return gunTransform;

    }

    public void SetGunTransform(Vector3 gun)
    {
        gunTransform = gun;

    }


    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node) // For Pc Format
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build");
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.preFab, node.GetBuildPosition() + gunTransform, Quaternion.identity);
        node.turret = turret;
        

    }

   /*  public void BuildTurretOnPhone(GameObject _turret) // For touchscreen format
     {
         Touch finger = Input.GetTouch(0);
         if (finger.phase == TouchPhase.Ended)
         {
             Ray ray = Camera.main.ScreenPointToRay(finger.position);
             RaycastHit hitinfo;
             if (Physics.Raycast(ray, out hitinfo))
             {

                 if (hitinfo.transform.GetComponent<Node>().name == "builded")
                 {
                     SelectNode(hitinfo.transform.GetComponent<Node>());
                     return;
                 }
                 if (PlayerStats.Money < turretToBuild.cost)
                 {
                     Debug.Log("Not enough money to build");
                     return;
                 }
                 if (!CanBuild)
                     return;
                if (hitinfo.transform.GetComponent<Node>().enabled == false)
                    return;  
                 PlayerStats.Money -= turretToBuild.cost;
                 GameObject turret = (GameObject)Instantiate(turretToBuild.preFab, hitinfo.transform.position + gunTransform, Quaternion.identity);
                 _turret = turret;
                 hitinfo.transform.GetComponent<Node>().name = "builded";
             }
         }
     }*/

    public void SelectNode(Node node)
    {
        
        NodeColliderOn();
        isUpgrade = false;
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedTurret = (GameObject)node.transform.GetComponent<Node>().turret;
        selectedNode = node;
        NodeColliderOff();
        nodeUI.SetTarget(node);
        TurretUpdateInfo(selectedTurret);


    }
    public void DeselectNode()
    {

        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        NodeColliderOn();
        DeselectNode();
    }

    public void Upgrade()
    {
        isUpgrade = true;

        TurretUpdateInfo(selectedTurret);
    }


    public void Sell()
    {

        SellItem(selectedTurret);
        NodeColliderOn();
        DeselectNode();

    }

    void TurretUpdateInfo(GameObject turret_)
    {
        selectionFilter.HideFilter();
        selectedTurret = turret_;
        GunMachine _turret = selectedTurret.transform.GetComponent<GunMachine>();
        Turret turret = selectedTurret.transform.GetComponent<Turret>();

        if (selectedTurret.name == "GunMachine(Clone)")
        {
            turretToUpgrade = shop.gunMachine;
            TurretInfo();
            if (isUpgrade)
            {
                if (PlayerStats.Money < _turret.upgradePrice)
                {
                    Debug.Log("Not enough money to build");
                    return;
                }
                UpGradeGunMachine();

                return;
            }
            return;
        }
        if (selectedTurret.name == "SniperTower(Clone)")
        {
            turretToUpgrade = shop.sniperTower;

            TurretInfo();

            if (isUpgrade)
            {
                if (PlayerStats.Money < turret.upgradePrice)
                {
                    Debug.Log("Not enough money to build");
                    return;
                }

                UpgradeSniperTower();


                return;
            }
            return;
        }
        if (selectedTurret.name == "LaserBeamer(Clone)")
        {
            turretToUpgrade = shop.laserBeamer;
            TurretInfo();
            if (isUpgrade)
            {
                if (PlayerStats.Money < turret.upgradePrice)
                {
                    Debug.Log("Not enough money to build");
                    return;
                }
                UpgradeLaserBeamer();

                return;
            }
            return;
        }
        if (selectedTurret.name == "RocketLouncher(Clone)")
        {
            turretToUpgrade = shop.rocketLouncher;
            TurretInfo();
            if (isUpgrade)
            {
                if (PlayerStats.Money < turret.upgradePrice)
                {
                    Debug.Log("Not enough money to build");
                    return;
                }
                UpgradeRocketLouncher();

                return;
            }
            return;
        }

    }

    void TurretInfo()
    {
        selectionFilter.HideFilter();
        GunMachine _turret = selectedTurret.transform.GetComponent<GunMachine>();
        Turret turret = selectedTurret.transform.GetComponent<Turret>();

        turretToBuild = null;
        if (_turret)
        {
            if (_turret.upgradePrice == shop.gunMachine.upgradePrice)
            {

                turretToUpgrade.upgradePriceTxt.text = "$" + shop.gunMachine.upgradePrice.ToString();
                turretToUpgrade.sellPriceTxt.text = "$" + shop.gunMachine.sellPrice.ToString();
                return;
            }
            if (_turret.upgradePrice != shop.gunMachine.upgradePrice)
            {
                if (_turret.gunLevel >= 3)
                {
                    turretToUpgrade.upgradePriceTxt.text = "MAX";
                    return;
                }

                turretToUpgrade.upgradePriceTxt.text = "$" + _turret.upgradePrice.ToString();
                turretToUpgrade.sellPriceTxt.text = "$" + _turret.sellPrice.ToString();
                return;
            }
        }
        else
        {
            if (turret.upgradePrice == shop.gunMachine.upgradePrice)
            {

                turretToUpgrade.upgradePriceTxt.text = "$" + shop.gunMachine.upgradePrice.ToString();
                turretToUpgrade.sellPriceTxt.text = "$" + shop.gunMachine.sellPrice.ToString();
                return;
            }
            if (turret.upgradePrice != shop.gunMachine.upgradePrice)
            {
                if (turret.gunLevel >= 3)
                {
                    turretToUpgrade.upgradePriceTxt.text = "MAX";
                    return;
                }


                turretToUpgrade.upgradePriceTxt.text = "$" + turret.upgradePrice.ToString();
                turretToUpgrade.sellPriceTxt.text = "$" + turret.sellPrice.ToString();
                return;
            }
        }



    }


    void UpGradeGunMachine()
    {
        GunMachine _turret = selectedTurret.transform.GetComponent<GunMachine>();
        MeshRenderer nodeMesh = selectedNode.transform.GetComponent<MeshRenderer>();

        if (_turret.gunLevel == 0)
        {
            _turret.range += upgradeRangeMG;
            _turret.gunLevel++;
            UpgradeCalculate();
            isUpgrade = false;
            nodeMesh.material.color = levelTwo;
            return;
        }
        if (_turret.gunLevel == 1)
        {

            _turret.fireRate += upgradeFireRateMG;
            _turret.gunLevel++;

            UpgradeCalculate();
            isUpgrade = false;
            nodeMesh.material.color = levelThree;
            return;
        }
        if (_turret.gunLevel == 2)
        {
            _turret.damage += upgradeDamageMG;
            _turret.gunLevel++;
            UpgradeCalculate();
            turretToUpgrade.upgradePriceTxt.text = "MAX";
            isUpgrade = false;
            nodeMesh.material.color = levelFour;
            return;
        }

    }

    void UpgradeSniperTower()
    {
        Turret turret = selectedTurret.transform.GetComponent<Turret>();
        MeshRenderer nodeMesh = selectedNode.transform.GetComponent<MeshRenderer>();
        if (turret.gunLevel == 0)
        {
            turret.range += upgradeRangeSP;
            turret.gunLevel++;
            UpgradeCalculate();
            isUpgrade = false;
            nodeMesh.material.color = levelTwo;
            return;
        }
        if (turret.gunLevel == 1)
        {
            turret.fireRate += upgradeFireRateSP;
            turret.gunLevel++;
            UpgradeCalculate();
            isUpgrade = false;
            nodeMesh.material.color = levelThree;
            return;
        }
        if (turret.gunLevel == 2)
        {
            turret.damage += upgradeDamageSP;
            turret.gunLevel++;
            UpgradeCalculate();
            turretToUpgrade.upgradePriceTxt.text = "MAX";
            isUpgrade = false;
            nodeMesh.material.color = levelFour;
            return;
        }

    }

    void UpgradeLaserBeamer()
    {
        Turret turret = selectedTurret.transform.GetComponent<Turret>();
        MeshRenderer nodeMesh = selectedNode.transform.GetComponent<MeshRenderer>();

        if (turret.gunLevel == 0)
        {
            turret.damageOverTime += upgradeDamageOverTime;
            turret.gunLevel++;
            UpgradeCalculate();
            isUpgrade = false;
            nodeMesh.material.color = levelTwo;
            return;
        }
        if (turret.gunLevel == 1)
        {
            turret.slowAmount += upgradeSlowAmount;
            turret.gunLevel++;
            UpgradeCalculate();
            isUpgrade = false;
            nodeMesh.material.color = levelThree;
            return;
        }
        if (turret.gunLevel == 2)
        {
            turret.range += upgradeRangeLB;
            turret.gunLevel++;
            UpgradeCalculate();
            turretToUpgrade.upgradePriceTxt.text = "MAX";
            isUpgrade = false;
            nodeMesh.material.color = levelFour;
            return;
        }
    }

    void UpgradeRocketLouncher()
    {
        Turret turret = selectedTurret.transform.GetComponent<Turret>();
        MeshRenderer nodeMesh = selectedNode.transform.GetComponent<MeshRenderer>();
        if (turret.gunLevel == 0)
        {
            turret.range += upgradeRangeRL;
            turret.gunLevel++;
            UpgradeCalculate();
            isUpgrade = false;
            nodeMesh.material.color = levelTwo;
            return;
        }
        if (turret.gunLevel == 1)
        {
            turret.fireRate += upgradeFireRateRL;
            turret.gunLevel++;
            UpgradeCalculate();
            isUpgrade = false;
            nodeMesh.material.color = levelThree;
            return;

        }
        if (turret.gunLevel == 2)
        {
            turret.radius += upgradeExplosionRadius;
            turret.gunLevel++;
            UpgradeCalculate();
            turretToUpgrade.upgradePriceTxt.text = "MAX";
            isUpgrade = false;
            nodeMesh.material.color = levelFour;
            return;
        }
    }

    void SellItem(GameObject turret_)
    {
        GunMachine _turret = selectedTurret.transform.GetComponent<GunMachine>();
        Turret turret = selectedTurret.transform.GetComponent<Turret>();

        if (_turret)
        {
            PlayerStats.Money += _turret.sellPrice;
            selectedNode.transform.GetComponent<MeshRenderer>().material.color = selectedNode.transform.GetComponent<Node>().startColor;
            Destroy(turret_);
            return;
        }
        else
        {
            PlayerStats.Money += turret.sellPrice;
            selectedNode.transform.GetComponent<MeshRenderer>().material.color = selectedNode.transform.GetComponent<Node>().startColor;
            Destroy(turret_);
            return;
        }

    }

    void UpgradeCalculate()
    {
        GunMachine _turret = selectedTurret.transform.GetComponent<GunMachine>();
        Turret turret = selectedTurret.transform.GetComponent<Turret>();
        if (_turret)
        {                       // 0(-100) 1(-200) 2(-300)
            PlayerStats.Money -= _turret.upgradePrice;
            _turret.priceMultiplier += 2;
            _turret.upgradePrice = _turret.priceMultiplier * turretToUpgrade.upgradePrice; //0(3*100) , 1(3*100)
            _turret.sellPrice = _turret.priceMultiplier * turretToUpgrade.sellPrice;
            turretToUpgrade.upgradePriceTxt.text = "$" + _turret.upgradePrice.ToString();
            turretToUpgrade.sellPriceTxt.text = "$" + _turret.sellPrice.ToString();
            return;
        }

        if (turret)
        {
            PlayerStats.Money -= turret.upgradePrice;
            turret.priceMultiplier += 2;
            turret.upgradePrice = turret.priceMultiplier * turretToUpgrade.upgradePrice;
            turret.sellPrice = turret.priceMultiplier * turretToUpgrade.sellPrice;
            turretToUpgrade.upgradePriceTxt.text = "$" + turret.upgradePrice.ToString();
            turretToUpgrade.sellPriceTxt.text = "$" + turret.sellPrice.ToString();
            return;
        }
    }


    private void NodeColliderOff()// Upgrade veya sell butonlerina tiklandiginda arkada kalan taret varsa etkilesimini keser.
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag(nodeTag);
        foreach (var node in nodes)
        {
            float dif2 = Vector3.Distance(node.transform.position, selectedNode.transform.position + selectedNode.nodeRangeLocation);
            if (dif2 < 5.5)
            {
                node.transform.GetComponent<BoxCollider>().enabled = false;
                selectedNode.transform.GetComponent<BoxCollider>().enabled = true;
            }

        }
    }
    private void NodeColliderOn()// Tekrar etkilesime gecmesini aktif hale getirir.
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag(nodeTag);

        foreach (var node in nodes)
        {
            node.transform.GetComponent<BoxCollider>().enabled = true;

        }
    }


    public void SniperMachine()
    {
        sniperMachine.SetActive(true);
        laser.SetActive(false);
        rocket.SetActive(false);

    }
    public void Laser()
    {
        sniperMachine.SetActive(false);
        laser.SetActive(true);
        rocket.SetActive(false);
    }
    public void Rocket()
    {
        sniperMachine.SetActive(false);
        laser.SetActive(false);
        rocket.SetActive(true);

    }
}

