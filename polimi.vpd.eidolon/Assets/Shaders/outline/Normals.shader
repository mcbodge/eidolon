Shader "Normals" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}

	}
	SubShader {
		Pass {
			Name "Threshold"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;


      struct vertexInput {
        float4 vertex : POSITION;
        float3 normal : NORMAL;	
		    float3 color : COLOR;		    
	    };
	    struct vertexOutput {
	      float4 pos: POSITION;
        float4 posWorld : TEXCOORD0;
        float3 normalDir : TEXCOORD1;
	      float3 color: COLOR;
		  };

      vertexOutput vert(vertexInput input) {
      
        vertexOutput output;
        float4x4 modelMatrix = _Object2World;
        float4x4 modelMatrixInverse = _World2Object;
        output.posWorld = mul(modelMatrix, input.vertex);
        output.normalDir = normalize(mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
        output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
        // pos è necessaria ai vertici, posWorld serve poi nel fragment
        
        //!--- codice di obra dinn
	      float3 origin = mul(_Object2World, float4(0.0,0.0,0.0,1.0)).xyz;
   	    float3 area = float3(100,100,100);
   		  float3 cameraDir = mul((float3x3)UNITY_MATRIX_V,float3(0,0,1));
        float3 norm = mul(modelMatrix, float4(input.normal, 0.0));
        norm *= input.color.r;
        float light = saturate((dot(norm, cameraDir)+1.0)*0.5);
        output.color = ((origin + area) * 0.5) / area;
        output.color.x *= light;
        output.color.y /= light;
        output.color *= input.color.g;
        output.color = frac(output.color * 100);
        //!--- fine codice di obra dinn
        
        return output;
      }

			float4 frag(vertexOutput fragInput) : COLOR {

        float4 posWorld = fragInput.posWorld;
        posWorld = abs(normalize(posWorld));
        float4 normalDir = float4(fragInput.normalDir, 0);
        normalDir = abs(normalize(normalDir));
        normalDir = step(normalDir, 0.3);
        
				//float4 c = tex2D(_MainTex, posWorld.xy);
				return normalize(posWorld*posWorld);
			}
			ENDCG
		}
	}
}
