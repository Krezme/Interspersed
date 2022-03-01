#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED


struct CustomLightingData {

	//position and orienatation
	float3 normalWS;

	//surface attributes
	float3 albedo;
};

#ifndef SHADERGRAPH_PREVIEW
float3 CustomLightingHandling(CustomLightingData d, Light light) {
	
	float3 radiance = light.color;

	float diffuse = saturate(dot(d.normalWS, light.direction));

	float3 color = d.albedo * radiance * diffuse;

	return color;
}
#endif

float3 CalculateCustomLighting(CustomLightingData d) {
#ifdef SHADERGRAPH_PREVIEW //in preview estimate diffuse + spec
	float3 lightDir = float3(0.5, 0.5, 0);
	float intensity = saturate(dot(d.normalWS, lightDir));
	return d.albedo * intensity;
#else

	//get Main light 
	Light mainLight = GetMainLight();

	float3 color = 0;
	//shade the main light
	color += CustomLightingHandling(d, mainLight);

	return color;
#endif
}

void CalculateCustomLighting_float(float3 Normal, float3 Albedo,
	out float3 Color) {

	CustomLightingData d;
	d.normalWS = Normal;
	d.albedo = Albedo;

	Color = CalculateCustomLighting(d);
}

#endif