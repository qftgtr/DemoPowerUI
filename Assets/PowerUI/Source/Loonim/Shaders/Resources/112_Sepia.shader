﻿Shader "Loonim/112" { // Sepia on _Src0

Properties {
	_Src0("Source 0",2D) = "white" {}
}

SubShader {
	Pass {
		
		
		Blend Off
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
		ZWrite Off
		
		CGPROGRAM
		
		
		#define OneInput
		#define NoDataInput
		#include "StdLoonimDraw.cginc"
		#include "StdLoonimColours.cginc"
		#pragma vertex vert
		#pragma fragment frag
		
		float4 frag(v2f i) : COLOR
		{
			float2 pt=i.uv;
			
			float4 _0=tex2D(_Src0,pt);
			
			return float4(
				(_0.r * .393) + (_0.g *.769) + (_0.b * .189),
				(_0.r * .349) + (_0.g *.686) + (_0.b * .168),
				(_0.r * .272) + (_0.g *.534) + (_0.b * .131),
				_0.a
			);
			
		}
		
		ENDCG
	}
}

}