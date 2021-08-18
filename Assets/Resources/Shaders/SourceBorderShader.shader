Shader "Custom/SourceBorderShader"
{
	Properties{
	[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Color", Color) = (1,1,1,1)
	_CenterX("Center X", Float) = 0
	_CenterY("Center Y", Float) = 0
	_Speed("Speed", Float) = 1
	_DimensionMultiplier("Dimension Multiplier", Float) = 1
	_DarkMultiplier("Dark Multiplier", Float) = 0
	_Thickness("Thickness", Float) = 0.1
	_NumberOfLines("Lines Multiplier", Float) = 1
	_Target("Target", Float) = 0
	}
	SubShader{
		Tags {"Queue" = "Overlay"  "IgnoreProjector" = "True"}

		Pass {
		CGPROGRAM

		#pragma vertex vert  
		#pragma fragment frag
		#include "UnityCG.cginc"

		sampler2D _ArrowTexture;
		float _Speed;
		float _DimensionMultiplier;
		float _DarkMultiplier;
		fixed4 _Color;
		float _CenterX;
		float _CenterY;
		float _Thickness;
		float _NumberOfLines;
		float _Target;

		struct v2f {
			float4 pos : SV_POSITION;
			float4 wPos : TEXCOORD1;
			float4 texcoord : TEXCOORD0;
		};

		v2f vert(appdata_full v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.wPos = mul(unity_ObjectToWorld, v.vertex);
			o.texcoord = v.texcoord;
			return o;
		}

		fixed4 frag(v2f i) : COLOR{

			float timeValue = abs(_DimensionMultiplier * _Target - fmod(_Time[1] * _Speed * (_DimensionMultiplier), _DimensionMultiplier));
			//float timeValue = _DimensionMultiplier;
		//float dist = sqrt((_CenterX - i.wPos.x) * (_CenterX - i.wPos.x) + (_CenterY - i.wPos.y) * (_CenterY - i.wPos.y));
		//float dist = sqrt((i.texcoord.x - 0.5) * (i.texcoord.x - 0.5) + (i.texcoord.y - 0.5) * (i.texcoord.y - 0.5));
		//float dist = abs(_CenterX - i.wPos.x) + abs(_CenterY - i.wPos.y);
			float xValue = abs(_CenterX - i.wPos.x);
			float yValue = abs(_CenterY - i.wPos.y);
			float dist = xValue + yValue;
			//float dist = fmod(xValue + yValue, _DimensionMultiplier/_NumberOfLines);
			//float v = fmod(abs(dist - timeValue), 1);

			fixed4 c = _Color;
			//if ((abs(xValue - timeValue) < _Thickness && yValue - timeValue < _Thickness) || (xValue - timeValue < _Thickness && abs(yValue - timeValue) < _Thickness)) {
			if (abs(dist -  timeValue) <= _Thickness) {
				c = lerp(_Color, fixed4(1, 1, 1, 1), _DarkMultiplier);
			}
			return c;
		}
		ENDCG
		}
	}
}