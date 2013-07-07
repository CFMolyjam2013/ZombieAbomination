var isPaused : boolean;

var restartScreen:Vector3;
var resumeScreen:Vector3;
var mainScreen:Vector3;
var defaultscreen:Vector3;
var pauseSkin : GUISkin;


function Start ()
{
	

	isPaused = false;
	
	
}

function Update ()
{
	if(Input.GetKeyDown(KeyCode.P)){
		isPaused = true;
		Time.timeScale = 0;
		}
		

    if(isPaused)
    {
		if(Input.GetKeyDown(KeyCode.R)){
		Time.timeScale = 1;
		isPaused = false;
		}
		if(Input.GetKeyDown(KeyCode.M)){
		Time.timeScale = 1;
		Application.LoadLevel(0);
		}
	}
	
	
}


function OnGUI(){
	if(isPaused){
		GUI.skin = pauseSkin;
		
		GUI.Label(Rect(70, 240, 800, 50), "You have puased the game.  M = restarts R = resume.");
	
	}
}