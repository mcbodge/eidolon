  Shader "Colored Objects" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}

    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Ramp vertex:vert

      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 customColor;
      };

    half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
            half NdotL = dot (s.Normal, lightDir);
            half diff = NdotL * 0.5 + 0.5;
           // half3 ramp = tex2D (_Ramp, float2(diff)).rgb;
            half4 c;            
            c.rgb = s.Albedo * _LightColor0.rgb  * atten;
            c.a = s.Alpha;
            return c;
        }

      void vert (inout appdata_full v, out Input o) {
          UNITY_INITIALIZE_OUTPUT(Input,o);
          float3 worldNormal = mul( _Object2World, float4( v.normal, 0.0 ) ).xyz;
          o.customColor = abs(v.normal);
      }

      sampler2D _MainTex;
      sampler2D _BumpMap;

      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb/5.;
          //o.Albedo *= IN.customColor;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
      }

      ENDCG
    } 
    Fallback "Diffuse"
  }
