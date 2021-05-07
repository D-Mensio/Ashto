// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BackgroundShader"
{
	Properties{
	[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_FirstColor("First Color", Color) = (1,1,1,1)
	_SecondColor("Bot Color", Color) = (1,1,1,1)
	}
	SubShader{
		Tags {"Queue" = "Background"  "IgnoreProjector" = "True"}
		LOD 100

		ZWrite On

		Pass {
		CGPROGRAM
		#pragma vertex vert  
		#pragma fragment frag
		#include "UnityCG.cginc"

		fixed4 _FirstColor;
		fixed4 _SecondColor;

		struct v2f {
			float4 pos : SV_POSITION;
			float4 texcoord : TEXCOORD0;
		};

		v2f vert(appdata_full v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.texcoord = v.texcoord;
			return o;
		}

		fixed4 frag(v2f i) : COLOR {
			fixed4 c = lerp(_FirstColor, _SecondColor, i.texcoord.y);
			c.a = 1;
			return c;
		}
		ENDCG
		}
	}
}