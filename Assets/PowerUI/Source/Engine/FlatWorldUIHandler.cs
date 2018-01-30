//--------------------------------------//               PowerUI////        For documentation or //    if you have any issues, visit//        powerUI.kulestar.com////    Copyright � 2013 Kulestar Ltd//          www.kulestar.com//--------------------------------------using System;using UnityEngine;namespace PowerUI{		/// <summary>	/// This class manages rendering flat WorldUI's for Unity Indie (free) users.	/// You don't need to use it directly - PowerUI will set this up for you when you create a FlatWorldUI.	/// </summary>		internal class FlatWorldUIHandler:MonoBehaviour{				/// <summary>Maximum rate is 8fps.</summary>		public const float MaximumRate=1f/8f;				/// <summary>The flat world UI camera.</summary>		public Camera Camera;		/// <summary>The target screen location.</summary>		public Rect Location;		/// <summary>The target image.</summary>		public Texture2D Output;		/// <summary>The current frame rate manager.</summary>		public float CurrentRate;		/// <summary>Set true when the flat world UI will redraw.</summary>		public bool Redraw=false;		/// <summary>The camera aspect.</summary>		public float Aspect=1f;						public void Update(){						// Resize camera:			Camera.aspect=Aspect;						if(Output==null){				return;			}						CurrentRate+=Time.unscaledDeltaTime;						if(CurrentRate<MaximumRate){								// Apply limiter.				return;							}						// Clear:			CurrentRate=0f;						Redraw=true;					}				public void OnPostRender(){						if(Redraw){								Redraw=false;								// Read into the output image:				Output.ReadPixels(Location,0,0,false);								// Flush out:				Output.Apply();							}					}			}	}