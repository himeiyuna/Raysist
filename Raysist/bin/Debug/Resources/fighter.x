xof 0302txt 0064
template Header {
 <3D82AB43-62DA-11cf-AB39-0020AF71E433>
 WORD major;
 WORD minor;
 DWORD flags;
}

template Vector {
 <3D82AB5E-62DA-11cf-AB39-0020AF71E433>
 FLOAT x;
 FLOAT y;
 FLOAT z;
}

template Coords2d {
 <F6F23F44-7686-11cf-8F52-0040333594A3>
 FLOAT u;
 FLOAT v;
}

template Matrix4x4 {
 <F6F23F45-7686-11cf-8F52-0040333594A3>
 array FLOAT matrix[16];
}

template ColorRGBA {
 <35FF44E0-6C7C-11cf-8F52-0040333594A3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
 FLOAT alpha;
}

template ColorRGB {
 <D3E16E81-7835-11cf-8F52-0040333594A3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
}

template IndexedColor {
 <1630B820-7842-11cf-8F52-0040333594A3>
 DWORD index;
 ColorRGBA indexColor;
}

template Boolean {
 <4885AE61-78E8-11cf-8F52-0040333594A3>
 WORD truefalse;
}

template Boolean2d {
 <4885AE63-78E8-11cf-8F52-0040333594A3>
 Boolean u;
 Boolean v;
}

template MaterialWrap {
 <4885AE60-78E8-11cf-8F52-0040333594A3>
 Boolean u;
 Boolean v;
}

template TextureFilename {
 <A42790E1-7810-11cf-8F52-0040333594A3>
 STRING filename;
}

template Material {
 <3D82AB4D-62DA-11cf-AB39-0020AF71E433>
 ColorRGBA faceColor;
 FLOAT power;
 ColorRGB specularColor;
 ColorRGB emissiveColor;
 [...]
}

template MeshFace {
 <3D82AB5F-62DA-11cf-AB39-0020AF71E433>
 DWORD nFaceVertexIndices;
 array DWORD faceVertexIndices[nFaceVertexIndices];
}

template MeshFaceWraps {
 <4885AE62-78E8-11cf-8F52-0040333594A3>
 DWORD nFaceWrapValues;
 Boolean2d faceWrapValues;
}

template MeshTextureCoords {
 <F6F23F40-7686-11cf-8F52-0040333594A3>
 DWORD nTextureCoords;
 array Coords2d textureCoords[nTextureCoords];
}

template MeshMaterialList {
 <F6F23F42-7686-11cf-8F52-0040333594A3>
 DWORD nMaterials;
 DWORD nFaceIndexes;
 array DWORD faceIndexes[nFaceIndexes];
 [Material]
}

template MeshNormals {
 <F6F23F43-7686-11cf-8F52-0040333594A3>
 DWORD nNormals;
 array Vector normals[nNormals];
 DWORD nFaceNormals;
 array MeshFace faceNormals[nFaceNormals];
}

template MeshVertexColors {
 <1630B821-7842-11cf-8F52-0040333594A3>
 DWORD nVertexColors;
 array IndexedColor vertexColors[nVertexColors];
}

template Mesh {
 <3D82AB44-62DA-11cf-AB39-0020AF71E433>
 DWORD nVertices;
 array Vector vertices[nVertices];
 DWORD nFaces;
 array MeshFace faces[nFaces];
 [...]
}

Header{
1;
0;
1;
}

Mesh {
 259;
 -2.04190;8.07270;20.32580;,
 -0.20670;5.02800;16.82230;,
 -0.20670;5.57020;13.86100;,
 -2.16700;8.57320;18.82430;,
 3.63040;8.07270;20.32580;,
 1.79530;5.02800;16.82230;,
 1.79530;5.57020;13.86100;,
 3.75550;8.57320;18.82430;,
 1.79530;3.10950;-32.81010;,
 1.79530;1.60800;-34.77040;,
 -0.20670;1.60800;-34.77040;,
 -0.20670;3.10950;-32.81010;,
 1.79530;6.57120;-20.88160;,
 -0.20670;6.57120;-20.88160;,
 1.79530;7.57220;-10.95510;,
 -0.20670;7.57220;-10.95510;,
 -1.70820;1.60800;-32.81010;,
 -2.16700;4.56930;-20.88160;,
 -0.20670;0.10650;-32.81010;,
 -0.20670;-3.35530;-20.88160;,
 -2.16700;-1.35330;-20.88160;,
 1.79530;0.10650;-32.81010;,
 1.79530;-3.35530;-20.88160;,
 -1.70820;3.44310;13.36060;,
 -0.20670;5.57020;7.39630;,
 -1.70820;2.90090;16.82230;,
 -0.20670;3.61000;18.82430;,
 1.79530;5.57020;7.39630;,
 1.79530;3.61000;18.82430;,
 3.75550;4.56930;-20.88160;,
 3.29670;1.60800;-32.81010;,
 3.75550;-1.35330;-20.88160;,
 3.29670;3.44310;13.36060;,
 3.29670;2.90090;16.82230;,
 -6.67150;1.60800;-17.92030;,
 -8.13120;1.60800;-6.99290;,
 -2.16700;5.57020;-10.95510;,
 -2.16700;5.57020;-3.03070;,
 -4.16900;1.60800;13.86100;,
 1.79530;2.60900;40.67930;,
 -0.20670;2.60900;40.67930;,
 -3.33480;1.60800;17.32280;,
 -0.70720;1.60800;43.64060;,
 2.29580;1.60800;43.64060;,
 -3.96050;2.40050;20.82630;,
 -2.16700;2.40050;39.67830;,
 -1.33280;2.60900;19.82530;,
 -3.96050;0.81550;20.82630;,
 -2.16700;0.81550;39.67830;,
 9.71980;1.60800;-6.99290;,
 8.26000;1.60800;-17.92030;,
 3.75550;5.57020;-10.95510;,
 3.75550;5.57020;-3.03070;,
 5.75750;1.60800;13.86100;,
 4.92340;1.60800;17.32280;,
 3.75550;2.40050;39.67830;,
 5.54900;2.40050;20.82630;,
 2.92140;2.60900;19.82530;,
 5.54900;0.81550;20.82630;,
 3.75550;0.81550;39.67830;,
 -0.20670;7.57220;-3.03070;,
 1.79530;7.57220;-3.03070;,
 -0.20670;-2.14580;-12.95710;,
 1.79530;-2.14580;-12.95710;,
 1.79530;-5.35730;38.67730;,
 1.79530;0.60700;40.67930;,
 -0.20670;0.60700;40.67930;,
 -0.20670;-5.35730;38.67730;,
 -1.33280;0.60700;19.82530;,
 -0.33180;-5.35730;35.71600;,
 -0.99920;-2.35430;24.78850;,
 -2.12530;-0.89450;7.39630;,
 2.58770;-2.35430;24.78850;,
 3.71380;-0.89450;7.39630;,
 1.92040;-5.35730;35.71600;,
 2.92140;0.60700;19.82530;,
 -2.16700;-2.35430;-10.95510;,
 3.75550;-2.35430;-10.95510;,
 -2.16700;-2.35430;-3.03070;,
 3.75550;-2.35430;-3.03070;,
 3.75550;-0.89450;7.39630;,
 -22.43700;-12.03050;13.73590;,
 -20.22660;-5.48240;13.73590;,
 -20.68530;-6.77530;15.48770;,
 -22.43700;-12.03050;15.48770;,
 -22.43700;-12.03050;11.98420;,
 -20.22660;-5.48240;3.22550;,
 -21.81150;-0.01860;1.84920;,
 -22.68730;-0.85280;1.84920;,
 -21.10240;-0.01860;-2.90560;,
 -22.68730;-1.52010;-2.90560;,
 -21.10240;-0.64430;-7.66020;,
 -21.97830;-1.52010;-7.66020;,
 -21.81150;-4.85680;1.84920;,
 -22.68730;-3.98090;1.84920;,
 -21.10240;-4.85680;-2.90560;,
 -22.68730;-3.27180;-2.90560;,
 -21.10240;-4.14770;-7.66020;,
 -21.97830;-3.27180;-7.66020;,
 -17.84920;-0.85280;1.84920;,
 -17.84920;-1.52010;-2.90560;,
 -18.72500;-0.01860;1.84920;,
 -19.35070;-0.01860;-2.90560;,
 -18.47480;-1.52010;-7.66020;,
 -19.35070;-0.64430;-7.66020;,
 -17.84920;-3.98090;1.84920;,
 -17.84920;-3.27180;-2.90560;,
 -18.72500;-4.85680;1.84920;,
 -19.35070;-4.85680;-2.90560;,
 -18.47480;-3.27180;-7.66020;,
 -19.35070;-4.14770;-7.66020;,
 -34.24040;-2.39600;7.18780;,
 -23.31290;-2.39600;13.73590;,
 -24.60590;-2.39600;4.97720;,
 -34.24040;-2.39600;5.85310;,
 -33.36450;-2.39600;3.68430;,
 -25.48170;-2.39600;2.76670;,
 24.02560;-12.03050;13.73590;,
 21.81510;-5.48240;13.73590;,
 22.27390;-6.77530;15.48770;,
 24.02560;-12.03050;15.48770;,
 24.02560;-12.03050;11.98420;,
 21.81510;-5.48240;3.22550;,
 24.27590;-0.85280;1.84920;,
 24.27590;-1.52010;-2.90560;,
 23.40000;-0.01860;1.84920;,
 22.69090;-0.01860;-2.90560;,
 23.56680;-1.52010;-7.66020;,
 22.69090;-0.64430;-7.66020;,
 24.27590;-3.98090;1.84920;,
 24.27590;-3.27180;-2.90560;,
 23.40000;-4.85680;1.84920;,
 22.69090;-4.85680;-2.90560;,
 23.56680;-3.27180;-7.66020;,
 22.69090;-4.14770;-7.66020;,
 20.31350;-0.01860;1.84920;,
 19.43770;-0.85280;1.84920;,
 20.93920;-0.01860;-2.90560;,
 19.43770;-1.52010;-2.90560;,
 20.93920;-0.64430;-7.66020;,
 20.06330;-1.52010;-7.66020;,
 20.31350;-4.85680;1.84920;,
 19.43770;-3.98090;1.84920;,
 20.93920;-4.85680;-2.90560;,
 19.43770;-3.27180;-2.90560;,
 20.93920;-4.14770;-7.66020;,
 20.06330;-3.27180;-7.66020;,
 35.82890;-2.39600;7.18780;,
 24.90150;-2.39600;13.73590;,
 26.19440;-2.39600;4.97720;,
 35.82890;-2.39600;5.85310;,
 34.95300;-2.39600;3.68430;,
 27.07030;-2.39600;2.76670;,
 23.40000;-0.64430;-7.66020;,
 23.56680;-0.85280;-7.66020;,
 23.56680;-3.98090;-7.66020;,
 23.40000;-4.14770;-7.66020;,
 -18.34970;-1.35330;9.81540;,
 -5.04480;3.23460;-2.02970;,
 -4.46090;1.10750;-12.54000;,
 -17.97430;-2.52110;3.68430;,
 -17.97430;-2.52110;13.11030;,
 -4.46090;1.10750;3.68430;,
 -3.91870;-1.01970;-2.02970;,
 -17.68230;-3.77230;9.81540;,
 -20.22660;0.64870;13.73590;,
 -20.22660;1.98340;4.97720;,
 -22.43700;-2.39600;13.73590;,
 -21.10240;-2.39600;22.07750;,
 -20.22660;-1.52010;22.07750;,
 -20.22660;-0.22720;13.73590;,
 -20.22660;-0.64430;-4.65730;,
 -20.22660;-1.52010;-14.29180;,
 -21.10240;-2.39600;-14.29180;,
 -21.97830;-2.39600;-4.65730;,
 -17.18180;-2.39600;13.73590;,
 -20.22660;0.64870;-4.65730;,
 -17.18180;-2.39600;-4.65730;,
 -20.22660;-5.48240;-4.65730;,
 -23.31290;-2.39600;-4.65730;,
 -19.35070;-2.39600;-14.29180;,
 -20.22660;-3.27180;-14.29180;,
 -19.35070;-2.39600;22.07750;,
 -20.22660;-3.27180;22.07750;,
 -23.56320;-2.39600;-2.90560;,
 -20.22660;0.85720;-2.90560;,
 -21.81150;-0.01860;-2.90560;,
 -22.68730;-0.85280;-2.90560;,
 -21.97830;-0.85280;-7.66020;,
 -21.81150;-0.64430;-7.66020;,
 -20.22660;-6.77530;4.97720;,
 -20.22660;-4.60650;13.73590;,
 -20.22660;-4.14770;-4.65730;,
 -20.22660;-5.73260;-2.90560;,
 -21.81150;-4.85680;-2.90560;,
 -22.68730;-3.98090;-2.90560;,
 -21.97830;-3.98090;-7.66020;,
 -21.81150;-4.14770;-7.66020;,
 -15.84720;-2.39600;4.97720;,
 -18.05770;-2.39600;13.73590;,
 -18.47480;-2.39600;-4.65730;,
 -16.97330;-2.39600;-2.90560;,
 -18.72500;-0.01860;-2.90560;,
 -17.84920;-0.85280;-2.90560;,
 -18.47480;-0.85280;-7.66020;,
 -18.72500;-0.64430;-7.66020;,
 -18.72500;-4.85680;-2.90560;,
 -17.84920;-3.98090;-2.90560;,
 -18.47480;-3.98090;-7.66020;,
 -18.72500;-4.14770;-7.66020;,
 6.63340;3.23460;-2.02970;,
 19.93820;-1.35330;9.81540;,
 19.56280;-2.52110;3.68430;,
 6.04950;1.10750;-12.54000;,
 19.56280;-2.52110;13.11030;,
 6.04950;1.10750;3.68430;,
 19.27090;-3.77230;9.81540;,
 5.50730;-1.01970;-2.02970;,
 21.81510;0.64870;13.73590;,
 21.81510;1.98340;4.97720;,
 22.69090;-2.39600;22.07750;,
 24.02560;-2.39600;13.73590;,
 21.81510;-0.22720;13.73590;,
 21.81510;-1.52010;22.07750;,
 21.81510;-1.52010;-14.29180;,
 21.81510;-0.64430;-4.65730;,
 23.56680;-2.39600;-4.65730;,
 22.69090;-2.39600;-14.29180;,
 18.77040;-2.39600;13.73590;,
 18.77040;-2.39600;-4.65730;,
 21.81510;0.64870;-4.65730;,
 24.90150;-2.39600;-4.65730;,
 21.81510;-5.48240;-4.65730;,
 20.93920;-2.39600;-14.29180;,
 21.81510;-3.27180;-14.29180;,
 20.93920;-2.39600;22.07750;,
 21.81510;-3.27180;22.07750;,
 21.81510;0.85720;-2.90560;,
 25.15170;-2.39600;-2.90560;,
 23.40000;-0.01860;-2.90560;,
 24.27590;-0.85280;-2.90560;,
 21.81510;-6.77530;4.97720;,
 21.81510;-4.60650;13.73590;,
 21.81510;-4.14770;-4.65730;,
 21.81510;-5.73260;-2.90560;,
 23.40000;-4.85680;-2.90560;,
 24.27590;-3.98090;-2.90560;,
 17.43570;-2.39600;4.97720;,
 19.64620;-2.39600;13.73590;,
 20.06330;-2.39600;-4.65730;,
 18.56180;-2.39600;-2.90560;,
 20.31350;-0.01860;-2.90560;,
 19.43770;-0.85280;-2.90560;,
 20.06330;-0.85280;-7.66020;,
 20.31350;-0.64430;-7.66020;,
 20.31350;-4.85680;-2.90560;,
 19.43770;-3.98090;-2.90560;,
 20.06330;-3.98090;-7.66020;,
 20.31350;-4.14770;-7.66020;;
 
 301;
 4;0,1,2,3;,
 4;4,5,6,7;,
 4;8,9,10,11;,
 4;12,8,11,13;,
 4;14,12,13,15;,
 4;16,17,13,11;,
 3;16,11,10;,
 3;18,16,10;,
 4;16,18,19,20;,
 4;21,18,10,9;,
 4;21,22,19,18;,
 3;23,2,24;,
 4;1,2,23,25;,
 3;26,1,25;,
 4;24,2,6,27;,
 4;2,1,5,6;,
 4;1,26,28,5;,
 3;15,13,17;,
 4;29,30,8,12;,
 3;8,30,9;,
 3;30,21,9;,
 4;21,30,31,22;,
 3;6,32,27;,
 4;6,5,33,32;,
 3;5,28,33;,
 3;12,14,29;,
 4;34,35,36,17;,
 3;34,17,16;,
 3;35,37,36;,
 4;35,38,24,37;,
 4;39,28,26,40;,
 4;38,41,25,23;,
 3;38,23,24;,
 4;42,43,39,40;,
 4;1,0,3,2;,
 4;44,45,40,46;,
 3;42,40,45;,
 3;44,41,47;,
 3;46,41,44;,
 4;47,48,45,44;,
 4;49,50,29,51;,
 3;29,50,30;,
 3;52,49,51;,
 4;53,49,52,27;,
 4;54,53,32,33;,
 3;32,53,27;,
 4;5,4,7,6;,
 4;55,56,57,39;,
 3;39,43,55;,
 3;54,56,58;,
 3;54,57,56;,
 4;59,58,56,55;,
 4;36,37,60,15;,
 3;37,24,60;,
 4;24,27,61,60;,
 4;60,61,14,15;,
 3;15,17,36;,
 4;62,19,22,63;,
 3;19,62,20;,
 4;64,65,66,67;,
 4;52,51,14,61;,
 3;27,52,61;,
 3;29,14,51;,
 3;63,22,31;,
 3;46,40,26;,
 4;41,46,26,25;,
 3;66,68,67;,
 3;67,68,69;,
 3;69,68,70;,
 3;70,68,71;,
 4;72,70,71,73;,
 4;72,74,69,70;,
 4;74,64,67,69;,
 3;39,57,28;,
 4;57,54,33,28;,
 3;65,64,75;,
 3;64,74,75;,
 3;75,74,72;,
 3;75,72,73;,
 4;65,43,42,66;,
 3;20,34,16;,
 4;76,35,34,20;,
 4;62,63,77,76;,
 3;62,76,20;,
 3;76,78,35;,
 4;71,38,35,78;,
 4;79,78,76,77;,
 4;79,80,71,78;,
 4;68,41,38,71;,
 4;48,47,68,66;,
 3;48,42,45;,
 3;66,42,48;,
 3;68,47,41;,
 3;50,31,30;,
 4;49,77,31,50;,
 3;77,63,31;,
 3;79,77,49;,
 4;53,73,79,49;,
 4;54,75,73,53;,
 4;58,59,65,75;,
 3;43,59,55;,
 3;43,65,59;,
 3;58,75,54;,
 4;81,82,83,84;,
 4;85,81,82,86;,
 3;87,88,89;,
 3;88,90,89;,
 3;89,90,91;,
 3;91,90,92;,
 3;93,94,95;,
 3;94,96,95;,
 3;95,96,97;,
 3;97,96,98;,
 3;99,100,101;,
 3;101,100,102;,
 3;100,103,102;,
 3;102,103,104;,
 3;105,106,107;,
 3;107,106,108;,
 3;106,109,108;,
 3;108,109,110;,
 4;111,112,113,114;,
 4;115,114,113,116;,
 4;117,118,119,120;,
 4;121,117,118,122;,
 3;123,124,125;,
 3;125,124,126;,
 3;124,127,126;,
 3;126,127,128;,
 3;129,130,131;,
 3;131,130,132;,
 3;130,133,132;,
 3;132,133,134;,
 3;135,136,137;,
 3;136,138,137;,
 3;137,138,139;,
 3;139,138,140;,
 3;141,142,143;,
 3;142,144,143;,
 3;143,144,145;,
 3;145,144,146;,
 4;147,148,149,150;,
 4;151,150,149,152;,
 4;127,128,153,154;,
 4;134,133,155,156;,
 4;157,158,159,160;,
 4;157,161,162,158;,
 4;163,164,160,159;,
 4;161,164,163,162;,
 4;112,165,166,113;,
 4;167,168,169,170;,
 4;171,172,173,174;,
 4;175,165,112,82;,
 4;176,177,178,179;,
 4;172,180,181,173;,
 4;182,169,168,183;,
 4;82,81,84,83;,
 4;81,85,86,82;,
 4;176,179,184,185;,
 4;185,184,113,166;,
 3;88,87,90;,
 3;87,89,90;,
 3;90,89,92;,
 3;92,89,91;,
 4;87,186,187,88;,
 3;89,186,87;,
 3;187,90,88;,
 4;188,92,90,187;,
 4;91,189,186,89;,
 4;186,189,188,187;,
 4;82,112,113,190;,
 4;168,167,191,183;,
 4;181,192,174,173;,
 4;179,178,193,184;,
 4;184,193,190,113;,
 3;94,93,96;,
 3;93,95,96;,
 3;96,95,98;,
 3;98,95,97;,
 4;194,93,94,195;,
 3;194,95,93;,
 3;96,195,94;,
 4;98,196,195,96;,
 4;197,97,95,194;,
 4;197,194,195,196;,
 4;165,175,198,166;,
 4;182,199,170,169;,
 4;172,171,200,180;,
 4;177,176,185,201;,
 4;201,185,166,198;,
 3;101,102,99;,
 3;99,102,100;,
 3;102,104,100;,
 3;100,104,103;,
 4;202,101,99,203;,
 3;202,102,101;,
 3;100,203,99;,
 4;103,204,203,100;,
 4;205,104,102,202;,
 4;205,202,203,204;,
 4;175,82,190,198;,
 4;199,182,183,191;,
 4;192,181,180,200;,
 4;178,177,201,193;,
 4;193,201,198,190;,
 3;107,108,105;,
 3;105,108,106;,
 3;108,110,106;,
 3;106,110,109;,
 4;107,206,207,105;,
 3;108,206,107;,
 3;207,106,105;,
 4;208,109,106,207;,
 4;110,209,206,108;,
 4;206,209,208,207;,
 4;112,111,114,113;,
 4;114,115,116,113;,
 4;91,92,188,189;,
 4;98,97,197,196;,
 4;110,109,208,209;,
 4;103,104,205,204;,
 4;210,211,212,213;,
 4;214,211,210,215;,
 4;216,217,213,212;,
 4;216,214,215,217;,
 4;218,148,149,219;,
 4;220,221,222,223;,
 4;224,225,226,227;,
 4;218,228,118,148;,
 4;229,230,231,232;,
 4;233,224,227,234;,
 4;223,235,236,220;,
 4;118,117,120,119;,
 4;117,121,122,118;,
 4;231,230,237,238;,
 4;238,237,219,149;,
 3;125,126,123;,
 3;123,126,124;,
 3;126,128,124;,
 3;124,128,127;,
 4;239,125,123,240;,
 3;239,126,125;,
 3;124,240,123;,
 4;127,154,240,124;,
 4;153,128,126,239;,
 4;153,239,240,154;,
 4;148,118,241,149;,
 4;221,220,236,242;,
 4;243,234,227,226;,
 4;232,231,238,244;,
 4;244,238,149,241;,
 3;131,132,129;,
 3;129,132,130;,
 3;132,134,130;,
 3;130,134,133;,
 4;131,245,246,129;,
 3;132,245,131;,
 3;246,130,129;,
 4;155,133,130,246;,
 4;134,156,245,132;,
 4;245,156,155,246;,
 4;228,218,219,247;,
 4;248,235,223,222;,
 4;225,224,233,249;,
 4;230,229,250,237;,
 4;237,250,247,219;,
 3;136,135,138;,
 3;135,137,138;,
 3;138,137,140;,
 3;140,137,139;,
 4;135,251,252,136;,
 3;137,251,135;,
 3;252,138,136;,
 4;253,140,138,252;,
 4;139,254,251,137;,
 4;251,254,253,252;,
 4;118,228,247,241;,
 4;235,248,242,236;,
 4;234,243,249,233;,
 4;229,232,244,250;,
 4;250,244,241,247;,
 3;142,141,144;,
 3;141,143,144;,
 3;144,143,146;,
 3;146,143,145;,
 4;255,141,142,256;,
 3;255,143,141;,
 3;144,256,142;,
 4;146,257,256,144;,
 4;258,145,143,255;,
 4;258,255,256,257;,
 4;148,147,150,149;,
 4;150,151,152,149;,
 4;128,127,154,153;,
 4;133,134,156,155;,
 4;146,145,258,257;,
 4;139,140,253,254;,
 4;213,217,163,159;,
 4;215,210,158,162;,
 4;210,213,159,158;,
 4;162,163,217,215;;
 
 MeshMaterialList {
  6;
  301;
  0,
  0,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  2,
  2,
  2,
  2,
  2,
  2,
  2,
  3,
  2,
  2,
  2,
  2,
  0,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  0,
  3,
  3,
  3,
  3,
  4,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  2,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  2,
  3,
  3,
  0,
  2,
  0,
  2,
  0,
  0,
  0,
  5,
  5,
  0,
  0,
  2,
  0,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  0,
  0,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  0,
  0,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  0,
  0,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  2,
  3,
  3,
  3,
  3,
  0,
  2,
  0,
  2,
  0,
  0,
  0,
  5,
  5,
  0,
  0,
  2,
  0,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  0,
  0,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  0,
  0,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  0,
  0,
  2,
  0,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  3,
  0,
  2,
  3,
  3,
  3,
  3,
  0,
  2,
  0,
  2;;
  Material {
   0.800000;0.800000;0.800000;1.000000;;
   100.000000;
   0.700000;0.700000;0.700000;;
   0.200000;0.200000;0.200000;;
  }
  Material {
   0.170000;0.382500;0.680000;1.000000;;
   15.000000;
   0.900000;0.900000;0.900000;;
   0.030000;0.067500;0.120000;;
  }
  Material {
   0.280000;0.280000;0.280000;1.000000;;
   100.000000;
   0.700000;0.700000;0.700000;;
   0.070000;0.070000;0.070000;;
  }
  Material {
   0.520000;0.520000;0.520000;1.000000;;
   100.000000;
   0.700000;0.700000;0.700000;;
   0.130000;0.130000;0.130000;;
  }
  Material {
   0.000000;0.000000;0.000000;1.000000;;
   5.000000;
   0.000000;0.000000;0.000000;;
   0.850000;0.150000;0.150000;;
  }
  Material {
   0.000000;0.000000;0.000000;1.000000;;
   5.000000;
   0.000000;0.000000;0.000000;;
   0.250000;0.450000;0.900000;;
  }
 }
 MeshNormals {
  331;
  0.890568;0.447989;0.078707;,
  0.890577;-0.447971;-0.078702;,
  0.357853;0.832004;-0.423923;,
  -0.357839;0.832009;-0.423926;,
  -0.329240;0.749618;-0.574173;,
  0.329256;0.749613;-0.574169;,
  0.382463;0.907070;-0.175916;,
  -0.382451;0.907075;-0.175917;,
  0.462946;0.884851;-0.052151;,
  -0.462932;0.884858;-0.052152;,
  -0.656030;0.693401;-0.298026;,
  -0.670442;0.731109;-0.126436;,
  -0.357841;-0.832006;-0.423928;,
  -0.551292;-0.828100;-0.101625;,
  -0.388120;-0.919600;-0.060819;,
  0.357856;-0.832001;-0.423926;,
  0.388132;-0.919595;-0.060819;,
  -0.708935;0.698850;0.094967;,
  -0.483738;0.869039;0.103771;,
  -0.698844;0.688600;0.193513;,
  -0.437030;0.860641;0.261348;,
  0.483746;0.869035;0.103772;,
  0.437035;0.860640;0.261341;,
  0.670454;0.731099;-0.126435;,
  0.656045;0.693389;-0.298020;,
  0.551301;-0.828094;-0.101625;,
  0.708944;0.698842;0.094966;,
  0.698853;0.688594;0.193501;,
  -0.609449;0.779393;-0.145321;,
  -0.644993;0.763254;-0.037774;,
  -0.582363;0.812851;0.011308;,
  -0.648407;0.758757;0.062097;,
  -0.585792;0.799877;0.130554;,
  0.204371;0.968472;0.142458;,
  -0.204368;0.968473;0.142458;,
  -0.671625;0.739824;-0.039762;,
  -0.120494;0.946439;0.299557;,
  0.120500;0.946439;0.299556;,
  -0.132203;0.983826;-0.120868;,
  -0.390870;0.920336;-0.014206;,
  -0.167925;0.975485;0.142231;,
  -0.999162;0.000000;-0.040928;,
  -0.975034;0.000000;0.222054;,
  0.582359;0.812853;0.011307;,
  0.645001;0.763248;-0.037774;,
  0.609448;0.779394;-0.145322;,
  0.648414;0.758751;0.062096;,
  0.585788;0.799881;0.130553;,
  0.671623;0.739826;-0.039762;,
  0.167932;0.975484;0.142230;,
  0.390871;0.920336;-0.014210;,
  0.132208;0.983826;-0.120866;,
  0.999163;0.000000;-0.040914;,
  0.975037;0.000000;0.222044;,
  -0.386931;0.917948;0.087493;,
  0.386943;0.917944;0.087492;,
  -0.188986;-0.981877;0.014203;,
  0.188992;-0.981876;0.014202;,
  0.000000;-0.318215;0.948018;,
  -0.093076;-0.895767;0.434670;,
  0.093080;-0.895767;0.434670;,
  -0.998112;-0.037101;0.048949;,
  -0.998129;-0.011923;0.059975;,
  -0.977953;-0.194772;0.075309;,
  -0.798253;-0.591177;0.115329;,
  0.000000;-0.984558;-0.175058;,
  0.410230;-0.910137;0.057980;,
  0.000000;-0.991023;-0.133694;,
  0.977949;-0.194798;0.075299;,
  -0.237880;-0.969474;-0.059449;,
  0.237877;-0.969474;-0.059449;,
  -0.303923;-0.950378;0.066430;,
  0.303919;-0.950379;0.066430;,
  0.000000;-0.990342;0.138650;,
  0.947774;-0.318929;0.003010;,
  0.948064;-0.318022;0.006021;,
  0.888515;-0.457274;0.037965;,
  -0.947474;0.319833;0.000000;,
  0.693971;-0.713027;0.099989;,
  0.703169;-0.703249;0.104856;,
  0.690730;-0.716103;0.100447;,
  0.685145;-0.721253;0.101837;,
  0.686081;-0.720294;0.102318;,
  0.684977;-0.721725;0.099592;,
  -0.502175;-0.855307;-0.127555;,
  -0.914813;-0.380142;-0.136416;,
  -0.884708;-0.455351;-0.099742;,
  -0.826119;-0.560740;-0.055651;,
  -0.504697;-0.862480;-0.037530;,
  -0.455384;-0.884689;-0.099751;,
  0.380673;0.916805;-0.120651;,
  0.856955;0.502904;-0.112765;,
  0.883259;0.460144;-0.090114;,
  0.853906;0.519158;-0.036336;,
  0.550466;0.833281;-0.051277;,
  0.455856;0.885678;-0.088148;,
  -0.703207;0.703207;0.104876;,
  -0.713017;0.693980;0.099993;,
  -0.716268;0.690858;0.098361;,
  -0.703248;0.703168;0.104866;,
  -0.713037;0.693960;0.099993;,
  -0.716255;0.690872;0.098364;,
  0.000000;1.000000;0.000000;,
  -0.379466;0.925202;0.002411;,
  -0.379443;0.923463;0.056921;,
  0.947770;0.318939;-0.003021;,
  0.948062;0.318029;-0.006042;,
  -0.947470;-0.319846;0.000000;,
  0.380674;0.916805;-0.120650;,
  0.851041;0.510608;-0.122507;,
  0.877340;0.469710;-0.098220;,
  0.853885;0.519191;-0.036339;,
  0.544488;0.837048;-0.053693;,
  0.445724;0.890442;-0.091884;,
  -0.703208;0.703207;0.104876;,
  -0.703208;0.703207;0.104876;,
  -0.703208;0.703207;0.104873;,
  -0.703208;0.703208;0.104872;,
  -0.703208;0.703208;0.104872;,
  -0.703208;0.703207;0.104875;,
  0.704046;-0.704086;0.092644;,
  0.704026;-0.704107;0.092634;,
  0.703917;-0.703944;0.094680;,
  0.695321;-0.712449;0.094578;,
  0.686878;-0.721130;0.090389;,
  0.698248;-0.709666;0.093938;,
  -0.510592;-0.851052;-0.122499;,
  -0.916796;-0.380700;-0.120630;,
  -0.890438;-0.445734;-0.091875;,
  -0.830175;-0.555111;-0.051580;,
  -0.504870;-0.862558;-0.033147;,
  -0.465435;-0.879835;-0.096235;,
  0.000000;-1.000000;0.000000;,
  0.381747;-0.924262;0.002899;,
  0.381042;-0.922845;0.056247;,
  0.000000;0.000000;-1.000000;,
  -0.259373;0.962597;0.078314;,
  -0.469917;0.860569;-0.196467;,
  -0.242184;0.948652;-0.203485;,
  -0.128967;0.988500;0.078966;,
  -0.007524;0.938243;0.345896;,
  -0.015047;0.939253;0.342896;,
  0.129371;-0.988390;0.079672;,
  0.260498;-0.962177;0.079722;,
  -0.703657;0.710526;0.004470;,
  -0.700802;0.705567;0.105128;,
  -0.699479;0.706114;0.110149;,
  -0.705671;0.705630;-0.064147;,
  0.000000;0.000000;1.000000;,
  -0.698304;0.709597;-0.094041;,
  -0.923311;0.377899;-0.068470;,
  -0.386353;0.920121;-0.064092;,
  -0.922924;0.360309;-0.135604;,
  -0.399747;0.907541;-0.128727;,
  -0.707100;-0.707094;0.005372;,
  -0.702633;-0.702616;0.112418;,
  -0.705650;-0.705650;-0.064153;,
  -0.703788;-0.703782;-0.096817;,
  -0.382327;-0.921491;-0.068414;,
  -0.921508;-0.382287;-0.068407;,
  -0.914818;-0.380129;-0.136417;,
  -0.380155;-0.914805;-0.136430;,
  0.707097;0.707108;0.003329;,
  0.702889;0.702906;0.108951;,
  0.705640;0.705661;-0.064150;,
  0.704087;0.704104;-0.092191;,
  0.363395;0.929653;-0.060748;,
  0.913957;0.401083;-0.061776;,
  0.905131;0.406742;-0.123689;,
  0.354077;0.927377;-0.120832;,
  0.912240;-0.404391;-0.065461;,
  0.359284;-0.930692;-0.068749;,
  0.895749;-0.424531;-0.131936;,
  0.334464;-0.932574;-0.135791;,
  0.128968;0.988500;0.078966;,
  0.242186;0.948652;-0.203485;,
  0.469920;0.860567;-0.196467;,
  0.259374;0.962596;0.078314;,
  0.015047;0.939253;0.342896;,
  0.007524;0.938243;0.345896;,
  -0.260497;-0.962178;0.079718;,
  -0.129371;-0.988391;0.079671;,
  0.700797;0.705573;0.105123;,
  0.703654;0.710528;0.004468;,
  0.699470;0.706122;0.110152;,
  0.705670;0.705631;-0.064147;,
  0.698301;0.709601;-0.094031;,
  0.386333;0.920130;-0.064094;,
  0.923303;0.377919;-0.068479;,
  0.707097;-0.707097;0.005370;,
  0.702624;-0.702624;0.112424;,
  0.705650;-0.705650;-0.064153;,
  0.703786;-0.703786;-0.096807;,
  0.921499;-0.382307;-0.068416;,
  0.382307;-0.921499;-0.068416;,
  -0.707100;0.707106;0.003331;,
  -0.702890;0.702906;0.108948;,
  -0.705640;0.705661;-0.064150;,
  -0.704089;0.704100;-0.092201;,
  -0.913957;0.401082;-0.061776;,
  -0.363395;0.929653;-0.060748;,
  -0.905131;0.406742;-0.123689;,
  -0.354078;0.927377;-0.120832;,
  -0.359285;-0.930692;-0.068749;,
  -0.912241;-0.404391;-0.065461;,
  -0.895749;-0.424531;-0.131935;,
  -0.334464;-0.932574;-0.135791;,
  -0.656033;-0.693398;-0.298026;,
  -0.329240;-0.749618;-0.574173;,
  0.329256;-0.749613;-0.574169;,
  -0.462189;0.883343;0.078020;,
  -0.447180;0.823189;0.349842;,
  0.462202;0.883336;0.078020;,
  0.447195;0.823182;0.349840;,
  0.656049;-0.693386;-0.298020;,
  -0.890568;-0.447989;-0.078707;,
  -0.890577;0.447971;0.078702;,
  -0.998382;-0.018096;0.053912;,
  -0.998174;-0.036628;0.048037;,
  0.000000;-0.999617;0.027676;,
  0.998382;-0.018096;0.053912;,
  0.998174;-0.036628;0.048037;,
  0.998111;-0.037146;0.048932;,
  0.998129;-0.011988;0.059956;,
  0.997925;-0.004841;0.064207;,
  0.120500;-0.946439;0.299556;,
  -0.120494;-0.946439;0.299557;,
  -0.609452;-0.779390;-0.145323;,
  -0.582513;-0.812742;0.011370;,
  -0.585641;-0.800936;0.124605;,
  -0.385133;-0.921046;-0.057858;,
  -0.167925;-0.975485;0.142231;,
  -0.132203;-0.983826;-0.120868;,
  -0.938343;0.000000;0.345706;,
  0.609451;-0.779391;-0.145324;,
  0.582509;-0.812745;0.011369;,
  0.585643;-0.800935;0.124602;,
  0.385140;-0.921043;-0.057860;,
  0.132208;-0.983826;-0.120866;,
  0.167932;-0.975484;0.142230;,
  0.938350;0.000000;0.345686;,
  -0.947774;0.318929;-0.003010;,
  -0.947770;-0.318939;0.003021;,
  -0.888513;-0.457278;0.037973;,
  0.023233;-0.979476;-0.200215;,
  0.011617;-0.979868;-0.199306;,
  0.462606;-0.815025;0.348899;,
  0.238220;-0.902295;0.359327;,
  -0.948064;0.318022;-0.006021;,
  0.947474;-0.319833;-0.000000;,
  -0.697242;0.710927;-0.091848;,
  -0.853906;0.519157;-0.036336;,
  -0.544520;0.837027;-0.053693;,
  -0.877352;0.469689;-0.098212;,
  -0.445736;0.890437;-0.091882;,
  -0.851048;0.510599;-0.122495;,
  -0.380673;0.916805;-0.120651;,
  -0.703292;-0.703280;0.103817;,
  -0.381046;-0.922843;0.056249;,
  -0.381748;-0.924262;0.002900;,
  -0.703532;-0.703521;-0.100506;,
  0.703249;0.703168;0.104866;,
  0.703239;0.703176;0.104871;,
  0.703236;0.703180;0.104869;,
  0.703222;0.703193;0.104872;,
  0.703219;0.703197;0.104871;,
  0.703207;0.703207;0.104876;,
  0.703032;0.703043;0.107128;,
  0.704604;0.704627;-0.083868;,
  -0.686877;-0.721131;0.090388;,
  -0.698247;-0.709666;0.093938;,
  -0.695321;-0.712449;0.094578;,
  -0.703917;-0.703944;0.094681;,
  -0.704045;-0.704086;0.092644;,
  -0.704026;-0.704107;0.092634;,
  0.705567;-0.700803;0.105120;,
  0.710523;-0.703660;0.004468;,
  0.706113;-0.699478;0.110157;,
  0.705620;-0.705681;-0.064152;,
  0.710923;-0.697249;-0.091829;,
  0.709593;-0.698310;-0.094030;,
  0.504870;-0.862559;-0.033147;,
  0.465435;-0.879835;-0.096235;,
  0.830175;-0.555112;-0.051580;,
  0.890438;-0.445735;-0.091875;,
  0.510592;-0.851052;-0.122499;,
  0.916796;-0.380700;-0.120630;,
  -0.011618;-0.979868;-0.199306;,
  -0.023233;-0.979477;-0.200215;,
  -0.462604;-0.815028;0.348893;,
  -0.238219;-0.902296;0.359323;,
  0.379439;0.923464;0.056918;,
  0.379464;0.925203;0.002409;,
  -0.948062;-0.318029;0.006042;,
  0.947470;0.319846;0.000000;,
  0.697237;0.710934;-0.091829;,
  -0.686040;-0.720334;0.102311;,
  -0.684948;-0.721753;0.099592;,
  -0.685113;-0.721284;0.101836;,
  -0.690715;-0.716116;0.100454;,
  -0.693959;-0.713036;0.099997;,
  -0.703168;-0.703247;0.104870;,
  0.922916;0.360322;-0.135622;,
  0.399734;0.907547;-0.128733;,
  0.703286;-0.703286;0.103814;,
  0.703528;-0.703528;-0.100486;,
  0.504663;-0.862500;-0.037527;,
  0.455362;-0.884701;-0.099750;,
  0.826098;-0.560772;-0.055650;,
  0.884701;-0.455362;-0.099751;,
  0.502165;-0.855312;-0.127561;,
  0.914811;-0.380141;-0.136435;,
  0.914810;-0.380141;-0.136435;,
  0.380142;-0.914810;-0.136435;,
  -0.703037;0.703037;0.107133;,
  -0.704609;0.704620;-0.083889;,
  -0.550467;0.833281;-0.051277;,
  -0.883260;0.460143;-0.090113;,
  -0.455856;0.885678;-0.088148;,
  -0.856955;0.502903;-0.112765;,
  -0.380673;0.916805;-0.120651;,
  -0.705573;-0.700797;0.105123;,
  -0.710526;-0.703657;0.004470;,
  -0.706113;-0.699478;0.110157;,
  -0.705620;-0.705681;-0.064152;,
  -0.710927;-0.697242;-0.091850;,
  -0.709595;-0.698306;-0.094040;,
  0.713037;0.693959;0.099993;,
  0.716269;0.690858;0.098360;,
  0.716255;0.690871;0.098364;,
  0.713017;0.693980;0.099993;;
  301;
  4;0,0,0,0;,
  4;1,1,1,1;,
  4;2,5,4,3;,
  4;6,2,3,7;,
  4;8,6,7,9;,
  4;10,11,7,3;,
  3;10,3,4;,
  3;12,207,208;,
  4;207,12,14,13;,
  4;15,12,208,209;,
  4;15,16,14,12;,
  3;17,210,18;,
  4;211,210,17,19;,
  3;20,211,19;,
  4;18,210,212,21;,
  4;210,211,213,212;,
  4;211,20,22,213;,
  3;9,7,11;,
  4;23,24,2,6;,
  3;2,24,5;,
  3;214,15,209;,
  4;15,214,25,16;,
  3;212,26,21;,
  4;212,213,27,26;,
  3;213,22,27;,
  3;6,8,23;,
  4;28,30,29,11;,
  3;28,11,10;,
  3;30,31,29;,
  4;30,32,18,31;,
  4;33,22,20,34;,
  4;32,35,19,17;,
  3;32,17,18;,
  4;36,37,33,34;,
  4;215,215,215,215;,
  4;38,40,34,39;,
  3;36,34,40;,
  3;41,35,41;,
  3;39,35,38;,
  4;41,42,42,41;,
  4;43,45,23,44;,
  3;23,45,24;,
  3;46,43,44;,
  4;47,43,46,21;,
  4;48,47,26,27;,
  3;26,47,21;,
  4;216,216,216,216;,
  4;49,51,50,33;,
  3;33,37,49;,
  3;48,52,52;,
  3;48,50,51;,
  4;53,52,52,53;,
  4;29,31,54,9;,
  3;31,18,54;,
  4;18,21,55,54;,
  4;54,55,8,9;,
  3;9,11,29;,
  4;56,14,16,57;,
  3;14,56,13;,
  4;58,60,59,58;,
  4;46,44,8,55;,
  3;21,46,55;,
  3;23,8,44;,
  3;57,16,25;,
  3;39,34,20;,
  4;35,39,20,19;,
  3;217,63,218;,
  3;218,63,61;,
  3;61,63,62;,
  3;62,63,64;,
  4;65,65,219,66;,
  4;65,67,67,65;,
  4;67,132,132,67;,
  3;33,50,22;,
  4;50,48,27,22;,
  3;220,221,68;,
  3;221,222,68;,
  3;68,222,223;,
  3;68,223,224;,
  4;60,225,226,59;,
  3;13,227,207;,
  4;69,228,227,13;,
  4;56,57,70,69;,
  3;56,69,13;,
  3;69,71,228;,
  4;64,229,228,71;,
  4;72,71,69,70;,
  4;72,73,219,71;,
  4;63,230,229,64;,
  4;231,232,232,59;,
  3;42,233,42;,
  3;59,226,231;,
  3;232,232,230;,
  3;234,25,214;,
  4;235,70,25,234;,
  3;70,57,25;,
  3;72,70,235;,
  4;236,66,72,235;,
  4;237,68,66,236;,
  4;238,239,60,238;,
  3;240,53,53;,
  3;225,60,239;,
  3;238,238,237;,
  4;74,76,75,75;,
  4;77,241,241,77;,
  3;82,81,83;,
  3;81,80,83;,
  3;83,80,78;,
  3;78,80,79;,
  3;88,87,89;,
  3;87,86,89;,
  3;89,86,84;,
  3;84,86,85;,
  3;93,92,94;,
  3;94,92,95;,
  3;92,91,95;,
  3;95,91,90;,
  3;99,98,100;,
  3;100,98,101;,
  3;98,97,101;,
  3;101,97,96;,
  4;102,104,103,102;,
  4;102,102,103,102;,
  4;105,105,106,106;,
  4;107,242,243,107;,
  3;111,110,112;,
  3;112,110,113;,
  3;110,109,113;,
  3;113,109,108;,
  3;117,116,118;,
  3;118,116,119;,
  3;116,115,119;,
  3;119,115,114;,
  3;124,123,125;,
  3;123,122,125;,
  3;125,122,120;,
  3;120,122,121;,
  3;130,129,131;,
  3;129,128,131;,
  3;131,128,126;,
  3;126,128,127;,
  4;132,134,133,132;,
  4;132,132,133,132;,
  4;135,135,135,135;,
  4;135,135,135,135;,
  4;136,139,138,137;,
  4;136,141,140,139;,
  4;142,143,244,245;,
  4;246,143,142,247;,
  4;104,145,144,103;,
  4;146,146,146,146;,
  4;147,147,147,147;,
  4;148,148,148,148;,
  4;135,135,135,135;,
  4;135,135,135,135;,
  4;148,148,148,148;,
  4;241,241,248,248;,
  4;74,249,249,76;,
  4;250,250,149,149;,
  4;149,149,103,144;,
  3;251,252,253;,
  3;252,254,253;,
  3;253,254,255;,
  3;255,254,256;,
  4;252,151,150,251;,
  3;254,151,252;,
  3;150,253,251;,
  4;152,255,253,150;,
  4;256,153,151,254;,
  4;151,153,152,150;,
  4;257,258,259,154;,
  4;155,155,155,155;,
  4;156,156,156,156;,
  4;260,260,157,157;,
  4;157,157,154,259;,
  3;261,262,263;,
  3;262,264,263;,
  3;263,264,265;,
  3;265,264,266;,
  4;158,88,87,159;,
  3;158,89,88;,
  3;86,159,87;,
  4;85,160,159,86;,
  4;161,84,89,158;,
  4;161,158,159,160;,
  4;267,267,162,162;,
  4;163,163,163,163;,
  4;164,164,164,164;,
  4;268,268,165,165;,
  4;165,165,162,162;,
  3;269,270,271;,
  3;271,270,272;,
  3;270,273,272;,
  3;272,273,274;,
  4;166,94,93,167;,
  3;166,95,94;,
  3;92,167,93;,
  4;91,168,167,92;,
  4;169,90,95,166;,
  4;169,166,167,168;,
  4;275,76,276,276;,
  4;277,277,277,277;,
  4;278,278,278,278;,
  4;279,279,280,280;,
  4;280,280,276,276;,
  3;281,282,283;,
  3;283,282,284;,
  3;282,285,284;,
  3;284,285,286;,
  4;281,171,170,283;,
  3;282,171,281;,
  3;170,284,283;,
  4;172,286,284,170;,
  4;285,173,171,282;,
  4;171,173,172,170;,
  4;258,132,132,259;,
  4;132,132,132,259;,
  4;135,135,135,135;,
  4;135,135,135,135;,
  4;135,135,135,135;,
  4;135,135,135,135;,
  4;174,177,176,175;,
  4;178,177,174,179;,
  4;180,181,287,288;,
  4;180,289,290,181;,
  4;182,291,292,183;,
  4;184,184,184,184;,
  4;185,185,185,185;,
  4;148,148,148,148;,
  4;135,135,135,135;,
  4;135,135,135,135;,
  4;148,148,148,148;,
  4;243,242,293,293;,
  4;105,294,294,105;,
  4;295,295,186,186;,
  4;186,186,183,292;,
  3;296,297,298;,
  3;298,297,299;,
  3;297,300,299;,
  3;299,300,301;,
  4;187,112,111,188;,
  3;187,113,112;,
  3;110,188,111;,
  4;109,302,188,110;,
  4;303,108,113,187;,
  4;303,187,188,302;,
  4;134,304,189,133;,
  4;190,190,190,190;,
  4;191,191,191,191;,
  4;305,305,192,192;,
  4;192,192,133,189;,
  3;306,307,308;,
  3;308,307,309;,
  3;307,310,309;,
  3;309,310,311;,
  4;306,194,193,308;,
  3;307,194,306;,
  3;193,309,308;,
  4;312,311,309,193;,
  4;310,313,194,307;,
  4;194,313,312,193;,
  4;314,314,195,195;,
  4;196,196,196,196;,
  4;197,197,197,197;,
  4;315,315,198,198;,
  4;198,198,195,195;,
  3;251,316,317;,
  3;316,318,317;,
  3;317,318,319;,
  3;319,318,320;,
  4;316,200,199,251;,
  3;318,200,316;,
  3;199,317,251;,
  4;201,319,317,199;,
  4;320,202,200,318;,
  4;200,202,201,199;,
  4;243,321,322,322;,
  4;323,323,323,323;,
  4;324,324,324,324;,
  4;325,325,326,326;,
  4;326,326,322,322;,
  3;261,327,328;,
  3;327,329,328;,
  3;328,329,330;,
  3;330,329,266;,
  4;203,130,129,204;,
  3;203,131,130;,
  3;128,204,129;,
  4;127,205,204,128;,
  4;206,126,131,203;,
  4;206,203,204,205;,
  4;291,102,102,292;,
  4;102,102,102,292;,
  4;148,148,148,148;,
  4;148,148,148,148;,
  4;135,135,135,135;,
  4;135,135,135,135;,
  4;287,181,142,245;,
  4;179,174,139,140;,
  4;174,175,138,139;,
  4;247,142,181,290;;
 }
}