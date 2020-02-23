Shader "Custom/ColourShift"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _RedShiftX("Red Shift X", Range(-10, 10)) = 3.0
        _RedShiftY("Red Shift Y", Range(-10, 10)) = -2.0
        _GreenShiftX("Green Shift X", Range(-10, 10)) = 4.0
        _GreenShiftY("Green Shift Y", Range(-10, 10)) = 0.0
        _BlueShiftX("Blue Shift X", Range(-10, 10)) = 0.0
        _BlueShiftY("Blue Shift Y", Range(-10, 10)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        half _RedShiftX;
        half _RedShiftY;
        half _GreenShiftX;
        half _GreenShiftY;
        half _BlueShiftX;
        half _BlueShiftY;
        half2 _MainTex_TexelSize;

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            half2 redShift = half2(_RedShiftX, _RedShiftY) * _MainTex_TexelSize;
            half2 greenShift = half2(_GreenShiftX, _GreenShiftY) * _MainTex_TexelSize;
            half2 blueShift = half2(_BlueShiftX, _BlueShiftY) * _MainTex_TexelSize;
            half3 c;
            c.r = tex2D(_MainTex, IN.uv_MainTex + redShift).r;
            c.g = tex2D(_MainTex, IN.uv_MainTex + greenShift).g;
            c.b = tex2D(_MainTex, IN.uv_MainTex + blueShift).b;
            o.Albedo = c.rgb;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
