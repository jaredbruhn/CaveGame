//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CrawlOnWalls : MonoBehaviour {

//    protected Rigidbody2D rb2d;
//    Transform myTransform;
//    float speed = 5.0f;
//    bool isWalking = true;
//    Vector2 curNormal = Vector2.up;
//    Vector2 hitNormal = Vector2.zero;
//    protected ContactFilter2D contactFilter;
//    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
//    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

//    void Start()
//    {
//        myTransform = transform;
//        contactFilter.useTriggers = false;
//        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
//        contactFilter.useLayerMask = true;
//    }

//    void OnEnable()
//    {
//        rb2d = GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        switch (isWalking)
//        {
//            case true:
//                // check for wall
//                if (Physics2D.Raycast(myTransform.position, myTransform.right, contactFilter, hitBufferList, 0.5f))
//                {
//                    hitNormal = rayHit.normal;
//                    isWalking = false;
//                }
//                Debug.DrawRay(myTransform.position, myTransform.right * 1.0f, Color.red);    // show forward ray    

//                // check for no floor    
//                Vector3 checkRear = myTransform.position + (-myTransform.right * 0.25f);
//                if (Physics.Raycast(checkRear, -myTransform.up, out rayHit, 1.0f))
//                {
//                    // there is a floor!
//                }
//                else
//                {
//                    // find the floor around the corner
//                    Vector3 checkPos = myTransform.position + (myTransform.right * 0.5f) + (-myTransform.up * 0.51f);
//                    Debug.DrawRay(checkPos, -myTransform.right * 1.5f, Color.green);    // show floor check ray
//                    if (Physics.Raycast(checkPos, -myTransform.right, out rayHit, 1.5f))
//                    {
//                        Debug.Log("HitNormal " + rayHit.normal);
//                        hitNormal = rayHit.normal;
//                        isWalking = false;
//                    }
//                }
//                Debug.DrawRay(myTransform.position, -myTransform.up * 1.0f, Color.red);    // show down ray
//                                                                                          // move forward
//                MoveForward();
//                break;

//            case false:
//                curNormal = Vector3.Lerp(curNormal, hitNormal, 4.0f * Time.deltaTime);
//                Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, curNormal);
//                transform.rotation = grndTilt;
//                float check = (curNormal - hitNormal).sqrMagnitude;
//                if (check < 0.001)
//                {
//                    grndTilt = Quaternion.FromToRotation(Vector3.up, hitNormal);
//                    transform.rotation = grndTilt;
//                    isWalking = true;
//                }
//                break;
//        }
//    }

//    void MoveForward()
//    {
//        myTransform.position += transform.right * speed * Time.deltaTime;
//    }
//}
