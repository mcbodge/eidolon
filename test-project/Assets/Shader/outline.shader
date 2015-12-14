Shader "Hidden/Outline" {
  Properties {
    _MainTex ("Base (RGB)", 2d) = "white" {}
    _bwBlend ("Black & White blend", Range (0, 1)) = 0
  }
  SubShader {
    Pass {
      CGPROGRAM
      #pragma vertex vert_img
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      uniform sampler2D _MainTex;
      uniform float _bwBlend;
      
      float4 frag(v2f_img i) : COLOR {
        float4 c = tex2D(_MainTex, i.uv); // l'immagine renderizzata
        
        float lum = c.r*.3 + c.g*.59 + c.b*.11;
        float3 bw = float3( lum, lum, lum );
        
        // fare una lerp?
        //float derivate = smoothstep (0., 1., length(ddx(c*c*float4(100.)))+length(ddy(c*c*float4(100.))) );

                 
		float4 derivata = ddx(c) + ddy(c);
        
        float4 result = step(derivata,0.5);
      //  result.rgb = lerp(c.rgb, bw, _bwBlend);
      
        // seleziono gli oggetti da lasciare in Colore
        float4 color = c* step(c, 0.99);
        if(color.r!=0) return color;
        return result;
      }
      ENDCG
    }
  }
}
