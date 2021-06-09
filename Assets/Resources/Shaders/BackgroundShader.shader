Shader "Custom/BackgroundShader"
{
	Properties{
	[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_FirstColor("Light Color", Color) = (1,1,1,1)
	_SecondColor("Dark Color", Color) = (1,1,1,1)
	_NoiseTex("Noise Texture", 2D) = "white" {}
	_NoiseStrength("Noise Strength", Float) = 0.7
	_HorizontalOffset("Horizontal Offset", Float) = 0
	_VerticalOffset("Vertical Offset", Float) = 0
	_DimensionMultiplier("Dimension Multiplier", Float) = 1
	_DarkMultiplier("Dark Multiplier", Float) = 0
	}
	SubShader{
		Tags {"Queue" = "Background"  "IgnoreProjector" = "True"}

		Pass {
		CGPROGRAM
		#pragma vertex vert  
		#pragma fragment frag
		#include "UnityCG.cginc"

		fixed4 _FirstColor;
		fixed4 _SecondColor;
		float _HorizontalOffset;
		float _VerticalOffset;
		sampler2D _NoiseTex;
		float _NoiseStrength;
		float _DimensionMultiplier;
		float _DarkMultiplier;

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
			float noiseSample = tex2Dlod(_NoiseTex, float4((i.texcoord.x + _HorizontalOffset) * _DimensionMultiplier, (i.texcoord.y + _VerticalOffset) * _DimensionMultiplier, 0, 0));
			float f = i.texcoord.y;
			//float f = sqrt((0.5 - i.texcoord.x) * (0.5 - i.texcoord.x) + (0 - i.texcoord.y) * (0 - i.texcoord.y));
			fixed4 c = lerp(_FirstColor, _SecondColor, clamp(f + _NoiseStrength * (0.5 - noiseSample),0,1));
			//c= lerp(c, c * _NoiseStrength, noiseSample);
			//c.a = 1;
			c -= _DarkMultiplier * fixed4(1,1,1,1);
			return c;
		}
		ENDCG
		}
	}
}