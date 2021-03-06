//--------------------------------------
//         Nitro Script Engine
//          Dom Framework
//
//        For documentation or 
//    if you have any issues, visit
//         nitro.kulestar.com
//
//    Copyright � 2013 Kulestar Ltd
//          www.kulestar.com
//--------------------------------------

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace Nitro{
	
	/// <summary>
	/// Represents the division (A/B) operation.
	/// </summary>
	
	public class DivideOperation:Operation{
	
		public DivideOperation(CompiledMethod method,CompiledFragment input0,CompiledFragment input1):base(method){
			Input0=input0;
			Input1=input1;
		}
	
		public override Type OutputType(out CompiledFragment v){
			v=this;
			Type typeA=Input1.OutputType(out Input1);
			Type typeB=Input0.OutputType(out Input0);
			return Numerical(typeA,typeB,"Division",ref v);
		}
		
		public override void OutputIL(NitroIL into){
			Input0.OutputIL(into);
			Input1.OutputIL(into);
			into.Emit(OpCodes.Div);
		}
		
	}
	
}