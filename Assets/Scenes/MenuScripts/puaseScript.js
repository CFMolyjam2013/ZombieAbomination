var isPaused : boolean;
var restartBox: Transform;
var resumeBox: Transform;
var mainBox: Transform;
var restartScreen:Vector3;
var resumeScreen:Vector3;
var mainScreen:Vector3;
var defaultscreen:Vector3;
var pauseSkin : GUISkin;


function Start ()
{
	
    restartScreen = Vector3(.7,3.3,-40);
	resumeScreen = Vector3(0,-1.2,-40);
	mainScreen = Vector3(1,-6,-40);
	defaultPosition = Vector3.zero;
	isPaused = false;
		restartBox.position = defaultscreen;
		resumeBox.position = defaultscreen;
		mainBox.position = defaultPosition;
	
}

function Update ()
{
	if(Input.GetKeyDown(KeyCode.P)){
		isPaused = true;
		Time.timeScale = 0;
		}
		

    if(isPaused)
    {
    	restartBox.position = restartScreen;
		resumeBox.position = resumeScreen;
		mainBox.position = mainScreen;
		
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
				 Debug.Log(hit.collider.name);
				// restarts game.
				if (hit.collider.name == "restartBox")
		 		{
		     		Time.timeScale = 1;
		     		Application.LoadLevel(1);
    		
				}
				// exits game 
		 		if (hit.collider.name == "resumeBox")
				{
		    	 	isPaused = false;
			     	Time.timeScale = 1;
		     	}
		     	if(hit.collider.name == "mainBox")
		     	{
		     		Application.LoadLevel(0);
					
		     	}
			 }  
		}
	}
	
	if(!isPaused) 
	{
		restartBox.position = defaultscreen;
		resumeBox.position = defaultscreen;
		mainBox.position = defaultscreen;
	}
}


function OnGUI(){
	if(isPaused){
		GUI.skin = pauseSkin;
		
		GUI.Label(Rect(	440, 240, 800, 40), "Restart");
		GUI.Label(Rect(	443, 280, 800, 40), "Resume");
		GUI.Label(Rect( 435, 320, 800, 40), "Main Menu");
	}
}