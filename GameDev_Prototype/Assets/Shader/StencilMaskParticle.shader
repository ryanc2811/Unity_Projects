// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Custom/StencilMaskParticle"{
Properties {
    _MainTex ("Particle Texture", 2D) = "white" {}
}

Category {
    Tags {"RenderType"="Opaque" "Queue"="Geometry-100" "IgnoreProjector"="True"}
	ColorMask 0
		ZWrite off
        LOD 200

		Stencil{
			Ref 1
			Pass replace
		}
    Blend SrcAlpha One
    Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
    
    BindChannels {
        Bind "Color", color
        Bind "Vertex", vertex
        Bind "TexCoord", texcoord
    }
    
    SubShader {
        Pass {
            SetTexture [_MainTex] {
                combine texture * primary
            }
        }
    }
}
}