using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    Camera Screen;

    float _bulletCount = 10;
    float _shootRange = 10;

    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            Shoot();
    }

    public void Shoot()
    {
        if (_bulletCount <= 0)
            return;

        Ray ray = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawLine(transform.position, transform.forward * 100f, Color.red, 1.0f);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.CompareTag("Enemy"))
            {
                print("적 검출");
            }
        }

        _bulletCount -= 1;
        print("총알 " + _bulletCount + "발 남음");
    }


}
