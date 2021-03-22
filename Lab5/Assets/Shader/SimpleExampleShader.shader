// Whatever we name the shader here is how it will appear in the shader inspector list. This shader
// will appear in the 'Example' folder.
//
// This shader is written in HLSL. Unity compiles it into lower-level languages for graphics cards.
// To see this, click on the shader in the Inspector tab and click 'Compile and Show Code'. The
// dropdown allows you to select which platform you'd like the code to be compiled for.
Shader "Example/Simple Example Shader" {

	// These are properties defined by the system that you pass into your shader. Notice how
	// in the Material view, you can modify the values of these properties.
	Properties {
		// Convention is to use underscore followed by UpperCamelCase, e.g. `_MyProperty`.
		//
		// Here, we the Tint property. The definition reads as
		// `_PropVariable ("Name as it will appear in the material inspector", PropType) = defaultValue`
		//
		// Note that this property has the type `Color`, which uses an RGBA value.
		_Tint ("Tint", Color) = (1, 1, 1, 1)

		_ExampleProp ("Example in the Editor", float) = 1.0
	}

	// This is the actual shader program, where the work is done. SubShaders are written in order
	// of priority for graphics cards: if this SubShader is too complicated for a given graphics
	// card, the card will fall back to the next one, and the next one, and the next...
	SubShader {

		// This is a single run of the shader. Shaders can have multiple passes for layering effects.
		Pass {

			// Here, we're declaring that we're writing this code in CG.
			//
			// CG is a programming language of its own ('C for Graphics': check out
			// https://en.wikipedia.org/wiki/Cg_(programming_language) ). In reality, Unity
			// doesn't use CG anymore: `CGPROGRAM` and `HLSLPROGRAM` (HLSL = High Level
			// Shader Language) are equivalent, as CG is interpreted as HLSL.
			CGPROGRAM

				// A `pragma` is an action that needs to be done. (i.e. the word 'pragmatic')
				//
				// In programming languages, it tells compilers how to process its input. In
				// this case, we're alerting the compiler that 'MyVertexProgram' is a vertex
				// shader (and should be compiled as such), while 'MyFragmentProgram' is a
				// fragment shader.
				#pragma vertex ExampleVertexProgram
				#pragma fragment ExampleFragmentProgram

				// A sample `include` statement. This file includes other Unity standard shader
				// files, such as 'UnityShaderVariables.cginc' (for transformation, light, and
				// camera data) and 'UnityShaderInstancing.cginc' (for instancing support, used
				// for fewer draw calls).
				//
				// Like in C, `include`ing files effectively pastes their contents into this
				// program.
				#include "UnityCG.cginc"

				// Here we re-declare the tint so that we can use it inside our HLSL program.
				// Doing this allows us to access it as a global variable.
				float4 _Tint;

				// These are the values we will output from our vertex shader that will be
				// interpolated when used by the fragment shader. We bundle our values
				// together in a C-like struct for cleaner code.
				//
				// The struct elements are `float4` and `float3` respectively. `float4` is for 
				// homogenous coordinates (XYZW/XYZH, think CPS511), while `float3` is for
				// inhomogenous coordinates (XYZ). One can imagine we use `float4` to refer to
				// points that will be in screen-space (i.e. will be moved after perspective
				// division) while `float3` is for points in world space.
				//
				// Text like `variable : SV_POSITION` tells the compiler what to interpret
				// the given variable as. Consider how different values could be used:
				// some will be screen coordinates, others will be interpolated values. These
				// are called shader semantics. We will see what these 
				//
				// We use `TEXCOORD0` here: `TEXCOORD` channels are used for interpolated data.
				// There are 8 `TEXCOORD`s (TEXCOORD0 - TEXCOORD7), with each one being a
				// separate channel. The fragment shader can access these interpolated values
				// by having an input attached to the appropriate channel: see the `localPosition`
				// input to the fragment shader below.
				//
				// `SV_POSITION` tells the compiler to interpret `position` as a screenspace
				// position, rather than a local or interpolated position. `SV` stands for
				// 'System Value', and 'POSITION' means that we're returning a position.
				struct InterpolatedValues {
					float4 position : SV_POSITION;
					float3 localPosition : TEXCOORD0;
				};

				// Returns the final coordinates for a vertex. The `float4` refers to homogenous
				// coordinates: think x, y, z, w (a.k.a. h), just like in CPS511.
				//
				// `POSITION` tells the compiler that the input is the vertex position in
				// homogenous space.
				//
				// Instead of returning a struct, we can have `out` values as parameters in
				// addition to a return value. However, returning a struct is much cleaner and more
				// readable, so we tend to use that instead.
				InterpolatedValues ExampleVertexProgram(float4 position : POSITION) {
					InterpolatedValues iv;

					// Notice that `localPosition` was declared as a `float3`, and here we're
					// returning the inhomogenous coordinates (we've removed the `w` component).
					// Accessing these components in this manner is called a 'swizzle', and we can
					// mix and match components using swizzles: `position.xy`, `position.xx`,
					// `position.wzyx`, etc.
					iv.localPosition = position.xyz;

					// We need to multiply the current position of the vertex by the Model-View
					// matrix so that it does not appear distorted.
					//
					// `UnityObjectToClipPos(position)` is equivalent to writing
					// `mul(UNITY_MATRIX_MVP, position)`, where `UNITY_MATRIX_MVP` is the built-in
					// Model-View Matrix.
					iv.position = UnityObjectToClipPos(position);

					return iv;
				}

				// The `float4` return value here is because we're returning an RGBA colour for
				// each screen fragment.
				//
				// Our inputs are the outputs of the vertex shader. All of our input values
				// must come from the vertex shader: if we try to use something that is not
				// given by the vertex shader (as output or an `out` value) we'll get an error.
				//
				// `SV_TARGET` tells the shader to write to the frame buffer.
				float4 ExampleFragmentProgram(InterpolatedValues iv) : SV_TARGET {
					// You can quickly try the outputs below by uncommenting them
					// (in editors like VSCode or Sublime text, you can toggle comments
					// using Ctrl+/ (or Cmd+/ on Mac))

					/* Try this as an output! */
					//return 0;

					/* Try this as an output! */
					//return float4(1, 1, 0, 1);

					/* Try this as an output! */
					//return _Tint;

					/* Try this as an output! */
					//return float4(iv.localPosition, 1);

					// Using 0.5 below clamps the value returned by localPosition between 0
					// and 1.
					//
					/* Try this as an output! */
					//return float4(i.localPosition + 0.5, 1) * _Tint;

					// `sin(_Time.y)` accesses the _Time parameter that's automatically
					// included with Unity HLSL programs. Doing `sin(_Time.y)` allows
					// us to create a 'pulsing' effect over time.
					//
					/* Try this as an output! */
					return float4(
						(iv.localPosition + 0.5 + (sin(_Time.y) * 0.5)),
					 	1
					) * _Tint;
				}
			ENDCG
		}
	}

	// You could write a second SubShader down here as a fallback for older graphics
	// cards.
}