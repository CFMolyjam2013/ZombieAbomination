#pragma strict
/////////////////////////////////////
// menuScript created by patrick rasmussen 
// Main menu script handles the selections through the games menu system.
/////////////////////////////////////

// updates to see if raycast returns a hit and checks that hit name for advancement or exit.
function Update()
{

    //check if the left mouse has been pressed down this frame
    if (Input.GetMouseButtonDown(0))
    {
        //empty RaycastHit object which raycast puts the hit details into
        var hit : RaycastHit;
        //ray shooting out of the camera from where the mouse is
        var ray : Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
 		// checks the hit if so tells us the name in debug and then checks what to do with that name.
        if (Physics.Raycast(ray, hit))
        {
        // starts game.
        if (hit.collider.name == "startCube")
         {
         	Application.LoadLevel(4);
         }
         // loads controller guide
         if (hit.collider.name == "howtoCube")
         {
         	Application.LoadLevel(1);
         } 
         // loads how to pla
         if (hit.collider.name == "creditsCube")
         {
         	Application.LoadLevel(3);
         } 
       
        }
    }
}