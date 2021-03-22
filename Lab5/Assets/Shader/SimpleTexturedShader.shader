// NOTE: For anything unclear and uncommented, see 'SimpleExampleShader.shader' for detailed notes.

Shader "Example/Simple Textured Shader" {
	Properties {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader {
		Pass {
			CGPROGRAM
				// Unity preference is to name the vertex shader `vert` and the fragment shader
				// `frag`. This is obviously a preference, but 
				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"

				float4 _Tint;
				
				// We need to use a sampler to access a 2D element.
				sampler2D _MainTex;

				// This variable will allow us to access tiling and offset information for our
				// texture. Use the convention `_Property_ST` to access this information: _ST
				// stands for 'Scaling and Translation'.
				//
				// We need this variable if we're going to enable scaling and offset, even if
				// we're using the TRANSFORM_TEX() macro below.
				float4 _MainTex_ST;

				// Unity preference is to call the data passed into the vertex shader `appdata`.
				struct appdata {
					float4 position : POSITION;
					float2 uv : TEXCOORD0;
				};

				// Unity preference is to call the data passed from the vertex shader into the
				// fragment shader `v2f` (for 'Vertex 2 Fragment').
				struct v2f {
					float4 position : SV_POSITION;
					// For textures, we'll be interpolating our UV coordinates. Remember UV from
					// CPS511: 3D objects can be represented as parameterized equations in UV space.
					// For more information, check out https://en.wikipedia.org/wiki/UV_mapping .
					float2 uv : TEXCOORD0;
				};

				// Notice we can bundle inputs into structs as well.
				//
				// Unity preference is to call the vertex program input `v` and call the vertex shader
				// `vert()`.
				v2f vert(appdata v) {
					// Unity preference is to label the output `o`
					v2f o;
					
					// Here we're multiplying the UV coordinates by the texture's tiling parameters
					// (x and y are the scale for U and V) so that they are applied. If we did not
					// multiply the UV coordinates by _MainTex_ST, the tiling would be the default
					// size (1, 1). We're also applying the tiling offset (by ` + _MainText_ST.zw`).
					//
					// We could write this as `o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;`, but
					// Unity has a macro for that (a function that the preprocessor will replace with
					// the above statement), as we see below.
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);

					o.position = UnityObjectToClipPos(v.position);

					return o;
				}
				
				// Unity preference is to label the fragment shader input as `i` and call the fragment
				// shader `frag()`.
				float4 frag(v2f i) : SV_TARGET {
					/* Try this as an output! */
					// return float4(i.uv, 1, 1);

					// `tex2D` returns the point in the texture determined by the UV coordinate.
					/* Try this as an output! */
					return tex2D(_MainTex, i.uv) * _Tint;
				}
			ENDCG
		}
	}
}