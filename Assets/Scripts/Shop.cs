using UnityEngine;


public class Shop : MonoBehaviour
{


    public TurretBluePrint gunMachine;
    public TurretBluePrint sniperTower;
    public TurretBluePrint laserBeamer;
    public TurretBluePrint rocketLouncher;

    
    BuildManager buildManager;


    private void Start()
    {

        buildManager = BuildManager.instance;
        GunPriceInfo();
    }


    private void GunPriceInfo()
    {

        gunMachine.GunCostInfo();
        sniperTower.GunCostInfo();
        laserBeamer.GunCostInfo();
        rocketLouncher.GunCostInfo();

    }

    public void SelectGunMachine()
    {

        buildManager.SelectTurretToBuild(gunMachine); //x = -0.17 , y = 0.6 , z=0
        buildManager.SetGunTransform(new Vector3(-.17f, .6f, 0f));
        

    }

    public void SelectSniperTower()
    {

        buildManager.SelectTurretToBuild(sniperTower); //x=0.3 , y=0.1 , z= 2.29
        buildManager.SetGunTransform(new Vector3(.3f, .1f, 2.29f));
        
    }

    public void SelectLaserBeamer()
    {

        buildManager.SelectTurretToBuild(laserBeamer); //x=0 , y=0.19 , z= 0
        buildManager.SetGunTransform(new Vector3(0f, .19f, 0f));
        
    }

    public void SelectRocketLouncher()
    {

        buildManager.SelectTurretToBuild(rocketLouncher); //x=0 , y=0.46 , z= 0
        buildManager.SetGunTransform(new Vector3(0f, .46f, 0f));
        
    }

}
