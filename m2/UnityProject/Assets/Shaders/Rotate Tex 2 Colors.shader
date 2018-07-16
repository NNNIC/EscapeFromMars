Shader "Custom/Rotate Tex 2 Colors" {
	Properties{
		_Color("Color",Color) = (1,1,1,1)
		_Color2("Color2",Color) = (0,0,0,1)
		_MainTex("Texture", 2D) = "white" { }
		_Degree("Degree",Range(0,360)) = 0
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
			fixed4 _Color2;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float  _Degree;

			struct v2f {
				float4  pos : SV_POSITION;
				float2  uv  : TEXCOORD0;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			//half4 frag(v2f i) : COLOR
			//{
			//	half4  texcol = tex2D(_MainTex, i.uv);
			//	return texcol * _Color;
			//}

			fixed4 frag(v2f_img i) :COLOR{

				fixed4 tmpcol = fixed4(1,1,1,1);

				float angle = _Degree * 3.14 / 180;
				float pivot = 0.5;
				float x = (i.uv.x - pivot) * cos(angle) - (i.uv.y - pivot) * sin(angle) + pivot;
				float y = (i.uv.x - pivot) * sin(angle) + (i.uv.y - pivot) * cos(angle) + pivot;

				//この位置に必要なピクセル座標
				float2 uv = float2(x,y);

				//描画範囲内:x
				if (0 <= x && x <= 1.0) {
					//描画範囲内:y
					if (0 <= y  && y <= 1.0) {
						tmpcol = tex2D(_MainTex, uv);
						return tmpcol.r * _Color2 + tmpcol.g * _Color;
					}
				}
				return _Color;
			}

			ENDCG
		}
	}
	Fallback "VertexLit"
}