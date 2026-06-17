Texture2D SceneColorTex : register(t0);
SamplerState SceneColorSampler : register(s0);

void edge_from_depth(float2 uv, float depth, out float edge)
{
    SceneColorTex.Sample(SceneColorSampler, uv);
}