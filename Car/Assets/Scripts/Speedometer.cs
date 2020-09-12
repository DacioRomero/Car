using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Speedometer : MonoBehaviour
{
  public Rigidbody target;
  Text text;

  void Awake()
  {
    text = GetComponent<Text>();
  }

  void Update()
  {
    text.text = Mathf.CeilToInt(target.velocity.magnitude * 2.23694f) + " MPH";
  }
}
