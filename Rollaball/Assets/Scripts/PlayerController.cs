using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
   // Rigidbody of the player.
   private Rigidbody rb; 

   // Movement along X and Y axes.
   private float movementX;
   private float movementY;
   private int count;

   // Speed at which the player moves.
   public float speed = 0;
   public float timeRemaining = 60;
   public bool timerIsRunning = false;
   public TextMeshProUGUI countText;
   public TextMeshProUGUI timerText;
   public TextMeshProUGUI endText;
   public GameObject winTextObject;

   // Start is called before the first frame update.
   void Start()
   {
      // Get and store the Rigidbody component attached to the player.
      rb = GetComponent<Rigidbody>();
      count = 0;
      SetCountText();
      SetTimerText();
      timerIsRunning = true;
      winTextObject.SetActive(false);
   }
 
   // This function is called when a move input is detected.
   void OnMove(InputValue movementValue)
   {
      // Convert the input value into a Vector2 for movement.
      Vector2 movementVector = movementValue.Get<Vector2>();

      // Store the X and Y components of the movement.
      movementX = movementVector.x;
      movementY = movementVector.y;
   }

   void SetCountText() 
   {
      countText.text =  "Count: " + count.ToString();
      if (count >= 12)
      {
         SetEndText(true);
         timerIsRunning = false;
      }
   }

   void SetTimerText()
   {
      timerText.text = "Time: " + timeRemaining.ToString("F2");
   }

   void SetEndText(bool win)
   {
      if (winTextObject != null)
      {
         winTextObject.SetActive(true);
         if (win)
         {
            endText.text = "You Win!";
         }
         else
         {
            endText.text = "You Lose!";
         }
      }
   }

   // FixedUpdate is called once per fixed frame-rate frame.
   private void FixedUpdate() 
   {
      // Create a 3D movement vector using the X and Y inputs.
      Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

      // Apply force to the Rigidbody to move the player.
      rb.AddForce(movement * speed); 

      // Update the timer.
      if (timerIsRunning)
      {
         if (timeRemaining > 0)
         {
            timeRemaining -= Time.deltaTime;
            SetTimerText();
         }
         // if time is over and the count is less than 12, the player loses.
         else
         {
            timeRemaining = 0;
            timerIsRunning = false;
            if (count < 12)
            {
               SetEndText(false);
            }
            if (count >= 12)
            {
               SetEndText(true);
            }
         }
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      // Check if the object the player collided with has the "PickUp" tag.
      if (other.gameObject.CompareTag("PickUp"))
      {
         // Deactivate the collided object (making it disappear).
         other.gameObject.SetActive(false);
         count++;
         SetCountText();
      }
   }
}