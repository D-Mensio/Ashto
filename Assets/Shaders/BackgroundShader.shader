Shader "Custom/BackgroundShader"
{
	Properties{
	[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_FirstColor("Light Color", Color) = (1,1,1,1)
	_SecondColor("Dark Color", Color) = (1,1,1,1)
	_NoiseTex("Noise Texture", 2D) = "white" {}
	_HorizontalSpeed("Horizontal speed", Float) = 0.1
	_VerticalSpeed("Vertical speed", Float) = 0.05
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
		float _HorizontalSpeed;
		float _VerticalSpeed;
		sampler2D _NoiseTex;

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

		fixed4 frag(v2f i) : COLOR{
			float noiseSample = tex2Dlod(_NoiseTex, float4(i.texcoord.x + 0.01 * _HorizontalSpeed * _Time[1], i.texcoord.y + 0.01 * _VerticalSpeed * _Time[1], 0, 0));
			//float f = (i.texcoord.x + i.texcoord.y) / 2;
			float f = sqrt((0.5 - i.texcoord.x) * (0.5 - i.texcoord.x) + (0 - i.texcoord.y) * (0 - i.texcoord.y));
			fixed4 c = lerp(_FirstColor, _SecondColor, f);
			c= lerp(c, c * 0.8, noiseSample);
			//c.a = 1;
			return c;
		}
		ENDCG
		}
	}
}