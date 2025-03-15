Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        SPIN_ROTATION("SPIN_ROTATION", float) = -2.0
        SPIN_SPEED("SPIN_SPEED", float) = 7.0
        THEOFFSET("THEOFFSET", float) = 0
        COLOUR_1("COLOUR_1", Color) = (0.871, 0.267, 0.231, 1.0)
        COLOUR_2("COLOUR_2", Color) = (0.0, 0.42, 0.706, 1.0)
        COLOUR_3("COLOUR_3", Color) = (0.086, 0.137, 0.145, 1.0)
        CONTRAST("CONTRAST", float) = 3.5
        LIGTHING("LIGTHING", float) = 0.4
        SPIN_AMOUNT("SPIN_AMOUNT", float) = 0.25
        PIXEL_FILTER("PIXEL_FILTER", float) = 745.0
        SPIN_EASE("SPIN_EASE", float) = 1.0
        PI("PI", float) = 3.14159265359
        IS_ROTATE("IS_ROTATE", float) = 0
        TIMESTAMP("TIMESTAMP", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float SPIN_ROTATION;
            float SPIN_SPEED;
            float OFFSET;
            float4 COLOUR_1;
            float4 COLOUR_2;
            float4 COLOUR_3;
            float CONTRAST;
            float LIGTHING;
            float SPIN_AMOUNT;
            float PIXEL_FILTER;
            float SPIN_EASE;
            float PI;
            float IS_ROTATE;
            float TIMESTAMP;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 effect(float2 screenSize, float2 uv) {
                float uv_len = length(uv);
    
                float speed = (SPIN_ROTATION*SPIN_EASE*0.2);
                if(IS_ROTATE){
                   speed = 0.5 * speed;
                }
                speed += 302.2;
                float new_pixel_angle = atan2(uv.y, uv.x) + speed - SPIN_EASE*20.*(1.*SPIN_AMOUNT*uv_len + (1. - 1.*SPIN_AMOUNT));
                float2 mid = (screenSize.xy/length(screenSize.xy))/2.;
                uv = (float2((uv_len * cos(new_pixel_angle) + mid.x), (uv_len * sin(new_pixel_angle) + mid.y)) - mid);
    
                uv *= 30.;
                speed = 0.5*(SPIN_SPEED);
                float2 uv2 = float2(uv.x, uv.y);
    
                for(int i=0; i < 5; i++) {
                    uv2 += sin(max(uv.x, uv.y)) + uv;
                    uv  += 0.5*float2(cos(5.1123314 + 0.353*uv2.y + speed*0.131121),sin(uv2.x - 0.113*speed));
                    uv  -= 1.0*cos(uv.x + uv.y) - 1.0*sin(uv.x*0.711 - uv.y);
                }
    
                float contrast_mod = (0.25*CONTRAST + 0.5*SPIN_AMOUNT + 1.2);
                float paint_res = min(2., max(0.,length(uv)*(0.035)*contrast_mod));
                float c1p = max(0.,1. - contrast_mod*abs(1.-paint_res));
                float c2p = max(0.,1. - contrast_mod*abs(paint_res));
                float c3p = 1. - min(1., c1p + c2p);
                float light = (LIGTHING - 0.2)*max(c1p*5. - 4., 0.) + LIGTHING*max(c2p*5. - 4., 0.);
                return (0.3/CONTRAST)*COLOUR_1 + (1. - 0.3/CONTRAST)*(COLOUR_1*c1p + COLOUR_2*c2p + float4(c3p*COLOUR_3.rgb, c3p*COLOUR_1.a)) + light;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float2 screen = float2(_ScreenParams.x/4, _ScreenParams.y/4);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return effect(screen, i.uv);
            }

            float4 effectscreen(float2 screenSize, float2 screen_coords) {
                float pixel_size = length(screenSize.xy) / PIXEL_FILTER;
                float2 uv = (floor(screen_coords.xy*(1./pixel_size))*pixel_size - 0.5*screenSize.xy)/length(screenSize.xy) - OFFSET;
                float uv_len = length(uv);
    
                float speed = (SPIN_ROTATION*SPIN_EASE*0.2);
                if(IS_ROTATE){
                   speed = TIMESTAMP * speed;
                }
                speed += 302.2;
                float new_pixel_angle = atan2(uv.y, uv.x) + speed - SPIN_EASE*20.*(1.*SPIN_AMOUNT*uv_len + (1. - 1.*SPIN_AMOUNT));
                float2 mid = (screenSize.xy/length(screenSize.xy))/2.;
                uv = (float2((uv_len * cos(new_pixel_angle) + mid.x), (uv_len * sin(new_pixel_angle) + mid.y)) - mid);
    
                uv *= 30.;
                speed = TIMESTAMP*(SPIN_SPEED);
                float2 uv2 = float2(uv.x, uv.y);
    
                for(int i=0; i < 5; i++) {
                    uv2 += sin(max(uv.x, uv.y)) + uv;
                    uv  += 0.5*float2(cos(5.1123314 + 0.353*uv2.y + speed*0.131121),sin(uv2.x - 0.113*speed));
                    uv  -= 1.0*cos(uv.x + uv.y) - 1.0*sin(uv.x*0.711 - uv.y);
                }
    
                float contrast_mod = (0.25*CONTRAST + 0.5*SPIN_AMOUNT + 1.2);
                float paint_res = min(2., max(0.,length(uv)*(0.035)*contrast_mod));
                float c1p = max(0.,1. - contrast_mod*abs(1.-paint_res));
                float c2p = max(0.,1. - contrast_mod*abs(paint_res));
                float c3p = 1. - min(1., c1p + c2p);
                float light = (LIGTHING - 0.2)*max(c1p*5. - 4., 0.) + LIGTHING*max(c2p*5. - 4., 0.);
                return (0.3/CONTRAST)*COLOUR_1 + (1. - 0.3/CONTRAST)*(COLOUR_1*c1p + COLOUR_2*c2p + float4(c3p*COLOUR_3.rgb, c3p*COLOUR_1.a)) + light;
            }
            ENDCG
        }
    }
}
