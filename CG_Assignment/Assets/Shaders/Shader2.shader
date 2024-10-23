Shader "Custom/Shader2"
{
    Properties
    {
        _myColor ("Sample Color", Color) = (1,1,1,1)
        _myVector ("Sample Vector", Vector) = (0.5,1,1,1)
        _myFloat ("Sample Float", Float) = 0.5
        _myCube ("Sample Cube", CUBE) = "" {}
        _myTex ("Sample Texture", 2D) = "white" {}
        _myRange1 ("Sample Range", Range(0,5)) = 1
        _myRange2 ("Sample Range", Range(0,5)) = 1
    }
    
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert
        
        fixed4 _myColor;
        half _myRange1;
        half _myRange2;
        sampler2D _myTex;
        samplerCUBE _myCube;
        float _myFloat;
        float4 _myVector;

        struct Input
        {
            float2 uv_myTex;
            float3 worldRefl;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = (tex2D(_myTex, IN.uv_myTex) * _myRange1).rgb;
            o.Emission = (texCUBE(_myCube, IN.worldRefl) * _myRange2).rgb;
        }
        ENDCG
    }
    Fallback "Diffuse"
}
