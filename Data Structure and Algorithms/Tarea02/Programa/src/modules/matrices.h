#ifndef MATRICES
#define MATRICES

#include "vectors.h"

struct matrix3
{
  union {
    float XX;
    float M00;
  };
  union {
    float XY;
    float M01;
  };
  union {
    float XZ;
    float M02;
  };
  union {
    float YX;
    float M10;
  };
  union {
    float YY;
    float M11;
  };
  union {
    float YZ;
    float M12;
  };
  union {
    float ZX;
    float M20;
  };
  union {
    float ZY;
    float M21;
  };
  union {
    float ZZ;
    float M22;
  };
};

/** Define a una matriz en 3D */
typedef struct matrix3 Matrix;

/** Matriz identidad */
#define Matrix_Identity (Matrix){.XX = 1.f, .XY = 0.f, .XZ = 0.f, \
                                 .YX = 0.f, .YY = 1.f, .YZ = 0.f, \
                                 .ZX = 0.f, .ZY = 0.f, .ZZ = 1.f}

/** Entrega el determinante de una matriz */
float matrix_determinant(Matrix m);

/** NOT IMPLEMENTED. Retorna m! */
Matrix matrix_inverse(Matrix m);

/** Entrega la transpuesta de una matriz */
Matrix matrix_transposed(Matrix m);

/** Genera una matriz que rota vectores en los ángulos entregados
    para cada eje */
Matrix matrix_getRotation(float thetaX, float thetaY, float thetaZ);

/** Genera una matriz que escala vectores en los escalares
    entregados para cada eje */
Matrix matrix_getScaling(float scaleX, float scaleY, float scaleZ);

//Matrix matrix_multiplyMM(Matrix *m1, Matrix m2);

/** Entrega la matriz resultante de multiplicar 2 matrices */
Matrix matrix_multipliedMM(Matrix mLeft, Matrix mRight);

/** Multiplica a un vector por una matriz. El orden es m x v */
void matrix_multiplyMV(Matrix m, Vector *v);

/** Entrega el resultado de multiplicar a un vector por una matriz.
    El orden es m x v */
Vector matrix_multipliedMV(Matrix m, Vector v);

#endif /* end of include guard: MATRICES */
