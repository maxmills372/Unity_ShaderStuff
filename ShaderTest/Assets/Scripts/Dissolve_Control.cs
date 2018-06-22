using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dissolve_Control : MonoBehaviour {

	// Finds in Components or you can pass in
	public bool Find_in_this_object = true;
	public bool Turn_off_shadows = true;
	public Material Dissolve_Shader;

	public bool Play = false;
	public bool dissolve_out = true;
	public bool custom = false;

	[Range(-1.0f,1.0f)]
	public float dissolve_control = -1.0f;
	public float dissolve_speed = 0.5f;


	float t_;

	// Use this for initialization
	void Start () 
	{
		// Object is visible to start
		dissolve_control = -1.0f;

		if(Find_in_this_object)
			Dissolve_Shader = GetComponent<Renderer>().material;
		else {
			print("Pass in Dissolve Shader material to " + gameObject.name);
		}

		if(Turn_off_shadows)
		{
			GetComponent<Renderer>().receiveShadows = false;
			GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(Dissolve_Shader)
		{
			// This is because my shader doesnt pass through shadows when transparant, 
			// and the transparent render queue isnt perfect too

			// Set render queue to from shader
			if(t_ < -0.3f)
				Dissolve_Shader.renderQueue = 2000; // FromShader  
			// Else set render queue to transparent
			else
				Dissolve_Shader.renderQueue = 3000; // Transparent  
			
			// Button Press
			if(Play)
			{
				// Dissolve out
				if(dissolve_out)
				{			
					t_ += Time.deltaTime * dissolve_speed;			
				}
				// Dissolve In
				else
				{		
					t_ -= Time.deltaTime * dissolve_speed;
				}

			}
			//Reset button
			else
			{
				// Dissolve out
				if(dissolve_out)
					t_ = -1.0f;
				// Dissolve In
				else
					t_ = 1.0f;
			}

			// Set Time property
			Dissolve_Shader.SetFloat("Vector1_D261F3D3",t_);

			// Custom amount
			if (custom)
			{
				Play = false;
				Dissolve_Shader.SetFloat("Vector1_D261F3D3",dissolve_control);
			}

			// Clamp time value
			t_ = Mathf.Clamp(t_,-1.0f,1.0f);
		}

	}		
}
