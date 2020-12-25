Shader "Custom/Additive Color" {
	Properties{
		_Color("Color",Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" { }
	}
	SubShader{
		Pass{
			Fog{ Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting Off
			Ztest Off
			AlphaTest Greater 0
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct v2f {
				float4  pos : SV_POSITION;
				float2  uv  : TEXCOORD0;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv  = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				half4  texcol = tex2D(_MainTex, i.uv);
				return texcol * _Color;
			}
			ENDCG
		}
	}
	Fallback "VertexLit"
}