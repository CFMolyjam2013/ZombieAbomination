#pragma strict
/////////////////////////////////////
// ControllerGuideScript created by patrick rasmussen 
// Script handles the back button for player to learn possible controls.
/////////////////////////////////////

//intro variables for gui for scroll and blink

///public
var scrollblock: Transform;
//private
private var titleSpeed: float = .10f;

function Awake(){

}


function Update () {	
	
	// checks if scroll has left screen and moves up if not 
	if(scrollblock.position.y <= 1.52){
	scrollblock.transform.Translate(Vector3.up * titleSpeed * Time.deltaTime);
	}
	// once the scroll is off screen reposition at bottom.
	else{
		scrollblock.position.y = -.32f;
	}
	
	//check if the left mouse has been pressed down this frame
    if (Input.GetMouseButtonDown(0)){
        //empty RaycastHit object which raycast puts the hit details into
        var hit : RaycastHit;
        //ray shooting out of the camera from where the mouse is
        var ray : Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
 		// checks the hit if so tells us the name in debug and then checks what to do with that name.
        if (Physics.Raycast(ray, hit)){
        
        Debug.Log(hit.collider.name);
        // goes back to main menu
        if (hit.collider.name == "backCube"){
         	Application.LoadLevel(0);
         }
      }
   }
}
