//--------------------------------------
//               PowerUI
//
//        For documentation or 
//    if you have any issues, visit
//        powerUI.kulestar.com
//
//    Copyright � 2013 Kulestar Ltd
//          www.kulestar.com
//--------------------------------------

using System;


namespace Css.Functions{
	
	/// <summary>
	/// Represents the invert() css function.
	/// </summary>
	
	public class InvertFunction:FilterFunction{
		
		public InvertFunction(){
			
			Name="invert";
			
		}
		
		public override string[] GetNames(){
			return new string[]{"invert"};
		}
		
		protected override Css.Value Clone(){
			InvertFunction result=new InvertFunction();
			result.Values=CopyInnerValues();
			return result;
		}
		
		public override void OnValueReady(CssLexer lexer){
			
		}
		
	}
	
}



