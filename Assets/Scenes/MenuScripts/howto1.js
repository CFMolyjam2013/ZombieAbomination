#pragma strict

function Start () {

}

function Update () {
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
         if (hit.collider.name == "zombieCube"){
         	Application.LoadLevel(2);
         }
      }
	}
}