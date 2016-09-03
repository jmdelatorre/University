#include "primes.h"
#include <stdlib.h>

/* GOTTA GO FAST */
static uint8_t primes2[1] = {2};
static uint8_t primes3[1] = {3};
static uint8_t primes4[2] = {2, 2};
static uint8_t primes5[1] = {5};
static uint8_t primes6[2] = {2, 3};
static uint8_t primes7[1] = {7};
static uint8_t primes8[3] = {2, 2, 2};
static uint8_t primes9[2] = {3, 3};
static uint8_t primes10[2] = {2, 5};
static uint8_t primes11[1] = {11};
static uint8_t primes12[3] = {2, 2, 3};
static uint8_t primes13[1] = {13};
static uint8_t primes14[2] = {2, 7};
static uint8_t primes15[2] = {3, 5};
static uint8_t primes16[4] = {2, 2, 2, 2};
static uint8_t primes17[1] = {17};
static uint8_t primes18[3] = {2, 3, 3};
static uint8_t primes19[1] = {19};
static uint8_t primes20[3] = {2, 2, 5};
static uint8_t primes21[2] = {3, 7};
static uint8_t primes22[2] = {2, 11};
static uint8_t primes23[1] = {23};
static uint8_t primes24[4] = {2, 2, 2, 3};
static uint8_t primes25[2] = {5, 5};
static uint8_t primes26[2] = {2, 13};
static uint8_t primes27[3] = {3, 3, 3};
static uint8_t primes28[3] = {2, 2, 7};
static uint8_t primes29[1] = {29};
static uint8_t primes30[3] = {2, 3, 5};
static uint8_t primes31[1] = {31};
static uint8_t primes32[5] = {2, 2, 2, 2, 2};
static uint8_t primes33[2] = {3, 11};
static uint8_t primes34[2] = {2, 17};
static uint8_t primes35[2] = {5, 7};
static uint8_t primes36[4] = {2, 2, 3, 3};
static uint8_t primes37[1] = {37};
static uint8_t primes38[2] = {2, 19};
static uint8_t primes39[2] = {3, 13};
static uint8_t primes40[4] = {2, 2, 2, 5};
static uint8_t primes41[1] = {41};
static uint8_t primes42[3] = {2, 3, 7};
static uint8_t primes43[1] = {43};
static uint8_t primes44[3] = {2, 2, 11};
static uint8_t primes45[3] = {3, 3, 5};
static uint8_t primes46[2] = {2, 23};
static uint8_t primes47[1] = {47};
static uint8_t primes48[5] = {2, 2, 2, 2, 3};
static uint8_t primes49[2] = {7, 7};
static uint8_t primes50[3] = {2, 5, 5};
static uint8_t primes51[2] = {3, 17};
static uint8_t primes52[3] = {2, 2, 13};
static uint8_t primes53[1] = {53};
static uint8_t primes54[4] = {2, 3, 3, 3};
static uint8_t primes55[2] = {5, 11};
static uint8_t primes56[4] = {2, 2, 2, 7};
static uint8_t primes57[2] = {3, 19};
static uint8_t primes58[2] = {2, 29};
static uint8_t primes59[1] = {59};
static uint8_t primes60[4] = {2, 2, 3, 5};
static uint8_t primes61[1] = {61};
static uint8_t primes62[2] = {2, 31};
static uint8_t primes63[3] = {3, 3, 7};
static uint8_t primes64[6] = {2, 2, 2, 2, 2, 2};
static uint8_t primes65[2] = {5, 13};
static uint8_t primes66[3] = {2, 3, 11};
static uint8_t primes67[1] = {67};
static uint8_t primes68[3] = {2, 2, 17};
static uint8_t primes69[2] = {3, 23};
static uint8_t primes70[3] = {2, 5, 7};
static uint8_t primes71[1] = {71};
static uint8_t primes72[5] = {2, 2, 2, 3, 3};
static uint8_t primes73[1] = {73};
static uint8_t primes74[2] = {2, 37};
static uint8_t primes75[3] = {3, 5, 5};
static uint8_t primes76[3] = {2, 2, 19};
static uint8_t primes77[2] = {7, 11};
static uint8_t primes78[3] = {2, 3, 13};
static uint8_t primes79[1] = {79};
static uint8_t primes80[5] = {2, 2, 2, 2, 5};
static uint8_t primes81[4] = {3, 3, 3, 3};
static uint8_t primes82[2] = {2, 41};
static uint8_t primes83[1] = {83};
static uint8_t primes84[4] = {2, 2, 3, 7};
static uint8_t primes85[2] = {5, 17};
static uint8_t primes86[2] = {2, 43};
static uint8_t primes87[2] = {3, 29};
static uint8_t primes88[4] = {2, 2, 2, 11};
static uint8_t primes89[1] = {89};
static uint8_t primes90[4] = {2, 3, 3, 5};
static uint8_t primes91[2] = {7, 13};
static uint8_t primes92[3] = {2, 2, 23};
static uint8_t primes93[2] = {3, 31};
static uint8_t primes94[2] = {2, 47};
static uint8_t primes95[2] = {5, 19};
static uint8_t primes96[6] = {2, 2, 2, 2, 2, 3};
static uint8_t primes97[1] = {97};
static uint8_t primes98[3] = {2, 7, 7};
static uint8_t primes99[3] = {3, 3, 11};
static uint8_t primes100[4] = {2, 2, 5, 5};
static uint8_t primes101[1] = {101};
static uint8_t primes102[3] = {2, 3, 17};
static uint8_t primes103[1] = {103};
static uint8_t primes104[4] = {2, 2, 2, 13};
static uint8_t primes105[3] = {3, 5, 7};
static uint8_t primes106[2] = {2, 53};
static uint8_t primes107[1] = {107};
static uint8_t primes108[5] = {2, 2, 3, 3, 3};
static uint8_t primes109[1] = {109};
static uint8_t primes110[3] = {2, 5, 11};
static uint8_t primes111[2] = {3, 37};
static uint8_t primes112[5] = {2, 2, 2, 2, 7};
static uint8_t primes113[1] = {113};
static uint8_t primes114[3] = {2, 3, 19};
static uint8_t primes115[2] = {5, 23};
static uint8_t primes116[3] = {2, 2, 29};
static uint8_t primes117[3] = {3, 3, 13};
static uint8_t primes118[2] = {2, 59};
static uint8_t primes119[2] = {7, 17};
static uint8_t primes120[5] = {2, 2, 2, 3, 5};
static uint8_t primes121[2] = {11, 11};
static uint8_t primes122[2] = {2, 61};
static uint8_t primes123[2] = {3, 41};
static uint8_t primes124[3] = {2, 2, 31};
static uint8_t primes125[3] = {5, 5, 5};
static uint8_t primes126[4] = {2, 3, 3, 7};
static uint8_t primes127[1] = {127};
static uint8_t primes128[7] = {2, 2, 2, 2, 2, 2, 2};
static uint8_t primes129[2] = {3, 43};
static uint8_t primes130[3] = {2, 5, 13};
static uint8_t primes131[1] = {131};
static uint8_t primes132[4] = {2, 2, 3, 11};
static uint8_t primes133[2] = {7, 19};
static uint8_t primes134[2] = {2, 67};
static uint8_t primes135[4] = {3, 3, 3, 5};
static uint8_t primes136[4] = {2, 2, 2, 17};
static uint8_t primes137[1] = {137};
static uint8_t primes138[3] = {2, 3, 23};
static uint8_t primes139[1] = {139};
static uint8_t primes140[4] = {2, 2, 5, 7};
static uint8_t primes141[2] = {3, 47};
static uint8_t primes142[2] = {2, 71};
static uint8_t primes143[2] = {11, 13};
static uint8_t primes144[6] = {2, 2, 2, 2, 3, 3};
static uint8_t primes145[2] = {5, 29};
static uint8_t primes146[2] = {2, 73};
static uint8_t primes147[3] = {3, 7, 7};
static uint8_t primes148[3] = {2, 2, 37};
static uint8_t primes149[1] = {149};
static uint8_t primes150[4] = {2, 3, 5, 5};
static uint8_t primes151[1] = {151};
static uint8_t primes152[4] = {2, 2, 2, 19};
static uint8_t primes153[3] = {3, 3, 17};
static uint8_t primes154[3] = {2, 7, 11};
static uint8_t primes155[2] = {5, 31};
static uint8_t primes156[4] = {2, 2, 3, 13};
static uint8_t primes157[1] = {157};
static uint8_t primes158[2] = {2, 79};
static uint8_t primes159[2] = {3, 53};
static uint8_t primes160[6] = {2, 2, 2, 2, 2, 5};
static uint8_t primes161[2] = {7, 23};
static uint8_t primes162[5] = {2, 3, 3, 3, 3};
static uint8_t primes163[1] = {163};
static uint8_t primes164[3] = {2, 2, 41};
static uint8_t primes165[3] = {3, 5, 11};
static uint8_t primes166[2] = {2, 83};
static uint8_t primes167[1] = {167};
static uint8_t primes168[5] = {2, 2, 2, 3, 7};
static uint8_t primes169[2] = {13, 13};
static uint8_t primes170[3] = {2, 5, 17};
static uint8_t primes171[3] = {3, 3, 19};
static uint8_t primes172[3] = {2, 2, 43};
static uint8_t primes173[1] = {173};
static uint8_t primes174[3] = {2, 3, 29};
static uint8_t primes175[3] = {5, 5, 7};
static uint8_t primes176[5] = {2, 2, 2, 2, 11};
static uint8_t primes177[2] = {3, 59};
static uint8_t primes178[2] = {2, 89};
static uint8_t primes179[1] = {179};
static uint8_t primes180[5] = {2, 2, 3, 3, 5};
static uint8_t primes181[1] = {181};
static uint8_t primes182[3] = {2, 7, 13};
static uint8_t primes183[2] = {3, 61};
static uint8_t primes184[4] = {2, 2, 2, 23};
static uint8_t primes185[2] = {5, 37};
static uint8_t primes186[3] = {2, 3, 31};
static uint8_t primes187[2] = {11, 17};
static uint8_t primes188[3] = {2, 2, 47};
static uint8_t primes189[4] = {3, 3, 3, 7};
static uint8_t primes190[3] = {2, 5, 19};
static uint8_t primes191[1] = {191};
static uint8_t primes192[7] = {2, 2, 2, 2, 2, 2, 3};
static uint8_t primes193[1] = {193};
static uint8_t primes194[2] = {2, 97};
static uint8_t primes195[3] = {3, 5, 13};
static uint8_t primes196[4] = {2, 2, 7, 7};
static uint8_t primes197[1] = {197};
static uint8_t primes198[4] = {2, 3, 3, 11};
static uint8_t primes199[1] = {199};
static uint8_t primes200[5] = {2, 2, 2, 5, 5};
static uint8_t primes201[2] = {3, 67};
static uint8_t primes202[2] = {2, 101};
static uint8_t primes203[2] = {7, 29};
static uint8_t primes204[4] = {2, 2, 3, 17};
static uint8_t primes205[2] = {5, 41};
static uint8_t primes206[2] = {2, 103};
static uint8_t primes207[3] = {3, 3, 23};
static uint8_t primes208[5] = {2, 2, 2, 2, 13};
static uint8_t primes209[2] = {11, 19};
static uint8_t primes210[4] = {2, 3, 5, 7};
static uint8_t primes211[1] = {211};
static uint8_t primes212[3] = {2, 2, 53};
static uint8_t primes213[2] = {3, 71};
static uint8_t primes214[2] = {2, 107};
static uint8_t primes215[2] = {5, 43};
static uint8_t primes216[6] = {2, 2, 2, 3, 3, 3};
static uint8_t primes217[2] = {7, 31};
static uint8_t primes218[2] = {2, 109};
static uint8_t primes219[2] = {3, 73};
static uint8_t primes220[4] = {2, 2, 5, 11};
static uint8_t primes221[2] = {13, 17};
static uint8_t primes222[3] = {2, 3, 37};
static uint8_t primes223[1] = {223};
static uint8_t primes224[6] = {2, 2, 2, 2, 2, 7};
static uint8_t primes225[4] = {3, 3, 5, 5};
static uint8_t primes226[2] = {2, 113};
static uint8_t primes227[1] = {227};
static uint8_t primes228[4] = {2, 2, 3, 19};
static uint8_t primes229[1] = {229};
static uint8_t primes230[3] = {2, 5, 23};
static uint8_t primes231[3] = {3, 7, 11};
static uint8_t primes232[4] = {2, 2, 2, 29};
static uint8_t primes233[1] = {233};
static uint8_t primes234[4] = {2, 3, 3, 13};
static uint8_t primes235[2] = {5, 47};
static uint8_t primes236[3] = {2, 2, 59};
static uint8_t primes237[2] = {3, 79};
static uint8_t primes238[3] = {2, 7, 17};
static uint8_t primes239[1] = {239};
static uint8_t primes240[6] = {2, 2, 2, 2, 3, 5};
static uint8_t primes241[1] = {241};
static uint8_t primes242[3] = {2, 11, 11};
static uint8_t primes243[5] = {3, 3, 3, 3, 3};
static uint8_t primes244[3] = {2, 2, 61};
static uint8_t primes245[3] = {5, 7, 7};
static uint8_t primes246[3] = {2, 3, 41};
static uint8_t primes247[2] = {13, 19};
static uint8_t primes248[4] = {2, 2, 2, 31};
static uint8_t primes249[2] = {3, 83};
static uint8_t primes250[4] = {2, 5, 5, 5};
static uint8_t primes251[1] = {251};
static uint8_t primes252[5] = {2, 2, 3, 3, 7};
static uint8_t primes253[2] = {11, 23};
static uint8_t primes254[2] = {2, 127};
static uint8_t primes255[3] = {3, 5, 17};
static uint8_t primes256[8] = {2, 2, 2, 2, 2, 2, 2, 2};

static uint8_t length[256] =
{
  0, 1, 1, 2, 1, 2, 1, 3, 2, 2, 1, 3, 1, 2, 2, 4,
  1, 3, 1, 3, 2, 2, 1, 4, 2, 2, 3, 3, 1, 3, 1, 5,
  2, 2, 2, 4, 1, 2, 2, 4, 1, 3, 1, 3, 3, 2, 1, 5,
  2, 3, 2, 3, 1, 4, 2, 4, 2, 2, 1, 4, 1, 2, 3, 6,
  2, 3, 1, 3, 2, 3, 1, 5, 1, 2, 3, 3, 2, 3, 1, 5,
  4, 2, 1, 4, 2, 2, 2, 4, 1, 4, 2, 3, 2, 2, 2, 6,
  1, 3, 3, 4, 1, 3, 1, 4, 3, 2, 1, 5, 1, 3, 2, 5,
  1, 3, 2, 3, 3, 2, 2, 5, 2, 2, 2, 3, 3, 4, 1, 7,
  2, 3, 1, 4, 2, 2, 4, 4, 1, 3, 1, 4, 2, 2, 2, 6,
  2, 2, 3, 3, 1, 4, 1, 4, 3, 3, 2, 4, 1, 2, 2, 6,
  2, 5, 1, 3, 3, 2, 1, 5, 2, 3, 3, 3, 1, 3, 3, 5,
  2, 2, 1, 5, 1, 3, 2, 4, 2, 3, 2, 3, 4, 3, 1, 7,
  1, 2, 3, 4, 1, 4, 1, 5, 2, 2, 2, 4, 2, 2, 3, 5,
  2, 4, 1, 3, 2, 2, 2, 6, 2, 2, 2, 4, 2, 3, 1, 6,
  4, 2, 1, 4, 1, 3, 3, 4, 1, 4, 2, 3, 2, 3, 1, 6,
  1, 3, 5, 3, 3, 3, 2, 4, 2, 4, 1, 5, 2, 2, 3, 8
};

static uint8_t *prime_factors[256] =
{
	NULL,
	primes2,
	primes3,
	primes4,
	primes5,
	primes6,
	primes7,
	primes8,
	primes9,
	primes10,
	primes11,
	primes12,
	primes13,
	primes14,
	primes15,
	primes16,
	primes17,
	primes18,
	primes19,
	primes20,
	primes21,
	primes22,
	primes23,
	primes24,
	primes25,
	primes26,
	primes27,
	primes28,
	primes29,
	primes30,
	primes31,
	primes32,
	primes33,
	primes34,
	primes35,
	primes36,
	primes37,
	primes38,
	primes39,
	primes40,
	primes41,
	primes42,
	primes43,
	primes44,
	primes45,
	primes46,
	primes47,
	primes48,
	primes49,
	primes50,
	primes51,
	primes52,
	primes53,
	primes54,
	primes55,
	primes56,
	primes57,
	primes58,
	primes59,
	primes60,
	primes61,
	primes62,
	primes63,
	primes64,
	primes65,
	primes66,
	primes67,
	primes68,
	primes69,
	primes70,
	primes71,
	primes72,
	primes73,
	primes74,
	primes75,
	primes76,
	primes77,
	primes78,
	primes79,
	primes80,
	primes81,
	primes82,
	primes83,
	primes84,
	primes85,
	primes86,
	primes87,
	primes88,
	primes89,
	primes90,
	primes91,
	primes92,
	primes93,
	primes94,
	primes95,
	primes96,
	primes97,
	primes98,
	primes99,
	primes100,
	primes101,
	primes102,
	primes103,
	primes104,
	primes105,
	primes106,
	primes107,
	primes108,
	primes109,
	primes110,
	primes111,
	primes112,
	primes113,
	primes114,
	primes115,
	primes116,
	primes117,
	primes118,
	primes119,
	primes120,
	primes121,
	primes122,
	primes123,
	primes124,
	primes125,
	primes126,
	primes127,
	primes128,
	primes129,
	primes130,
	primes131,
	primes132,
	primes133,
	primes134,
	primes135,
	primes136,
	primes137,
	primes138,
	primes139,
	primes140,
	primes141,
	primes142,
	primes143,
	primes144,
	primes145,
	primes146,
	primes147,
	primes148,
	primes149,
	primes150,
	primes151,
	primes152,
	primes153,
	primes154,
	primes155,
	primes156,
	primes157,
	primes158,
	primes159,
	primes160,
	primes161,
	primes162,
	primes163,
	primes164,
	primes165,
	primes166,
	primes167,
	primes168,
	primes169,
	primes170,
	primes171,
	primes172,
	primes173,
	primes174,
	primes175,
	primes176,
	primes177,
	primes178,
	primes179,
	primes180,
	primes181,
	primes182,
	primes183,
	primes184,
	primes185,
	primes186,
	primes187,
	primes188,
	primes189,
	primes190,
	primes191,
	primes192,
	primes193,
	primes194,
	primes195,
	primes196,
	primes197,
	primes198,
	primes199,
	primes200,
	primes201,
	primes202,
	primes203,
	primes204,
	primes205,
	primes206,
	primes207,
	primes208,
	primes209,
	primes210,
	primes211,
	primes212,
	primes213,
	primes214,
	primes215,
	primes216,
	primes217,
	primes218,
	primes219,
	primes220,
	primes221,
	primes222,
	primes223,
	primes224,
	primes225,
	primes226,
	primes227,
	primes228,
	primes229,
	primes230,
	primes231,
	primes232,
	primes233,
	primes234,
	primes235,
	primes236,
	primes237,
	primes238,
	primes239,
	primes240,
	primes241,
	primes242,
	primes243,
	primes244,
	primes245,
	primes246,
	primes247,
	primes248,
	primes249,
	primes250,
	primes251,
	primes252,
	primes253,
	primes254,
	primes255,
  primes256
};

uint8_t* prime_decomposition(int n)
{
  return prime_factors[n - 1];
}

/* Entrega la cantidad de elementos que tiene la descomposicion prima de n */
uint8_t prime_decomposition_length(int n)
{
  return length[n - 1];
}
