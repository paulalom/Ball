using System;
using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {

    public int numSidesInBasketPoly = 32;
    const float nbaNetDiameter = 0.4572f;
    float netRadius = nbaNetDiameter / 2;

	// Use this for initialization
	void Start () {
        GenerateBasket();
    }


    void GenerateBasket()
    {
        int numSidesToUseInMaths = numSidesInBasketPoly * 2; // Not sure why this is needed
        double rad = 180 / (double)numSidesToUseInMaths * Math.PI / 180;
        float sideLength = (float)(netRadius* 2 * Math.Sin(rad));
        float interiorAngle = (numSidesToUseInMaths - 2) * 180 / numSidesToUseInMaths;
        float currentAngle = 0;
        Vector3 centerPoint = transform.localPosition;
        for (int i = 0; i < numSidesInBasketPoly; i++)
        {
            GameObject side = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            side.transform.parent = transform;

            Vector3 scale = side.transform.localScale * sideLength;
            scale.y += .002f;
            scale.z /= 4;
            side.transform.localScale = scale;

            float currentAngleInRad = (float)(currentAngle * Math.PI / 180);
            float x = (float)(centerPoint.x + netRadius * Math.Cos(currentAngleInRad));
            float z = (float)(centerPoint.z + netRadius * Math.Sin(currentAngleInRad));
            side.transform.localPosition = new Vector3(x, 0, z);

            side.transform.Rotate(new Vector3(0, 90-currentAngle, 90));

            CapsuleCollider capsuleCollider1 = side.GetComponent<CapsuleCollider>();
            CapsuleCollider capsuleCollider2 = side.AddComponent<CapsuleCollider>();
            BoxCollider boxCollider = side.AddComponent<BoxCollider>();
            capsuleCollider1.radius /= 2;
            capsuleCollider2.radius /= 2;
            Vector3 temp = boxCollider.size;
            boxCollider.size = new Vector3(temp.x / 2, temp.y, temp.z);
            temp = capsuleCollider1.center;
            capsuleCollider1.center = new Vector3(temp.x + capsuleCollider1.radius, temp.y, temp.z);
            temp = capsuleCollider2.center;
            capsuleCollider2.center = new Vector3(temp.x - capsuleCollider2.radius, temp.y, temp.z);

            currentAngle += interiorAngle;
        }
    }
}
