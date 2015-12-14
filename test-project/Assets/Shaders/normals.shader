Shader "Normals" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_bwBlend ("Black & White blend", Range (0, 1)) = 0
	}
	SubShader {
		Pass {
			Name "Threshold"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _bwBlend;

			 
// aggiungere dithering!			 
			 
			 struct vertexInput {
			    float4 vertex : POSITION;
            float3 normal : NORMAL;			    
			 };
			 struct vertexOutput {
			  float4 pos: SV_POSITION;
            float4 posWorld : TEXCOORD0;
            float3 normalDir : TEXCOORD1;			  
			 };
			 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            float4x4 modelMatrix = _Object2World;
            float4x4 modelMatrixInverse = _World2Object; 
 
            output.posWorld = mul(modelMatrix, input.vertex);
            output.normalDir = normalize(
               mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }			 

			float4 frag(vertexOutput i) : COLOR {
				//float4 texture = tex2D(_MainTex, i.posWorld);
				
				float4 c = float4(step(abs(i.normalDir),.5), 1.);
				float lum = c.r*.6 + c.g*.3 + c.b*.11;
				float3 bw = float3( lum, lum, lum ); 
				
			//	c = lerp(1.,0., step(c,float3(0.5)));
				float4 result = c;
			//	result.rgb = lerp(c.rgb, bw, _bwBlend);
				return result;
			}
			ENDCG
		}
	}
}
