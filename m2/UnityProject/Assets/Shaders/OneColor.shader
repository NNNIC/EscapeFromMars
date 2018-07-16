// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OneColor" {
Properties {
    _Color   ("Color",Color) = (1,1,1,1)
}
SubShader {
    Pass {
			Fog { Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting Off 
			Ztest Off
			AlphaTest Greater 0
 CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

fixed4 _Color; 

struct v2f {
    float4  pos : SV_POSITION;
};

v2f vert (appdata_base v)
{
    v2f o;
    o.pos = UnityObjectToClipPos (v.vertex);
    return o;
}

half4 frag (v2f i) : COLOR
{
    return _Color;
}
ENDCG

    }
}
Fallback "VertexLit"
} 